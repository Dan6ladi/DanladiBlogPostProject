using BlogPost.ApplicationService.Authentication;
using BlogPost.ApplicationService.Interface;
using BlogPost.Domain.Entity;
using BlogPost.Infrastructure.Email;
using BlogPost.Infrastructure.Model;
using BlogPost.Infrastructure.ThirdParty;
using BlogPost.Repository.Interface;
using BlogPost.SharedKernel;
using BlogPost.SharedKernel.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BlogPost.SharedKernel.Enumerations;
using static BlogPost.SharedKernel.Utilities;

namespace BlogPost.ApplicationService.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailService _emailService;
        private readonly IThirdPartyService _thirdPartyService;
        public AuthenticationService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IEmailService emailService, IThirdPartyService thirdPartyService)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
            _thirdPartyService = thirdPartyService;
        }

        public async Task<ResponseModel> RegisterUser(SignUpRequest request)
        {
            var response = new ResponseModel();
            try
            {
                Console.WriteLine($"Entered RegisterUser | DATE: {DateTime.Now:dd MMM yyyy : HH-mm} | PAYLOAD: {JsonConvert.SerializeObject(request)}");
                //check if user signed up using email, social media account or opted to create account with bio data
                if (request.SignUpType == SignUpType.Email || request.SignUpType == SignUpType.SocialMediaAccount)
                {
                    var addUser = User.CreateUser(request.EmailAddress, request.UserName, request.SignUpType);
                    var user = await _userRepository.CreateUser(addUser);
                    if (!user)
                    {
                        response.Message = "Internal server error";
                        response.Status = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        return response;
                    }
                }
                else if(request.SignUpType == SignUpType.CreateAccount)
                {
                    var passwordSalt = PasswordHasher.GeneratePasswordSalt(request.FirstName, request.LastName);
                    var passwordHash = PasswordHasher.HashPassword(request.Password, passwordSalt);
                    var addUser = User.CreateUser(request.FirstName, request.LastName, request.EmailAddress, passwordHash, passwordSalt, request.UserName, request.SignUpType);
                    var user = await _userRepository.CreateUser(addUser);
                    if (!user)
                    {
                        response.Message = "Internal server error";
                        response.Status = false;
                        response.StatusCode = HttpStatusCode.InternalServerError;
                        return response;
                    }
                }

                var status = _emailService.SendWelcomeMessage(request.EmailAddress, request.FirstName);
                if (!status)
                {
                    response.Message = "failed to send email";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    return response;
                }

                response.Message = "Registration Successful, an email has been sent to your mail for more information";
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered RegisterUser Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
            
        }

        public async Task<ResponseModel<LoginResponse>> Login(LoginRequest request)
        {
            var response = new ResponseModel<LoginResponse>();
            try
            {
                var maskedObject = Utilities.MaskedProperty(request);
                Console.WriteLine($"Entered Login | DATE: {DateTime.Now:dd MMM yyyy : HH-mm} | PAYLOAD: {maskedObject}");
                var user = await _userRepository.GetUser(request.EmailAddress);
                if(user == null)
                {
                    response.Message = "Password or Username incorrect";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var passwordHash = PasswordHasher.HashPassword(request.Password, user.PasswordSalt);
                if(passwordHash != user.Password)
                {
                    response.Message = "Password or Username incorrect";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var accessToken = _tokenGenerator.GenerateAccessToken(user);
                var loginResponse = new LoginResponse()
                {
                    Email = user.EmailAddress,
                    Id = user.Id,
                    AccessToken = accessToken,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                };
                response.Message = "Login successful";
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = loginResponse;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered Login Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }


        public async Task<ResponseModel<ThirdPartyResponse>> ThirdPartyRegister(ThirdPartyRequest request)
        {
            var response = new ResponseModel<ThirdPartyResponse>();
            try
            {
                Console.WriteLine($"Entered ThirdPartyRegister | DATE: {DateTime.Now:dd MMM yyyy : HH-mm} | PAYLOAD: {JsonConvert.SerializeObject(request)}");

                switch (request.ThirdPartyType)
                {
                    case ThirdPartyType.Email:
                        // Send verification mail with verification link
                        break;

                    case ThirdPartyType.FaceBook:
                        var facebook = new FaceBookRequest
                        {
                            UserName = request.UserName,
                            PassWord = request.Password
                        };
                        var faceBookResponse = await _thirdPartyService.FaceBook(facebook);

                        if (!faceBookResponse.Status)
                        {
                            response.Message = faceBookResponse.Message;
                            response.Status = faceBookResponse.Status;
                            response.StatusCode = faceBookResponse.StatusCode;
                            return response;
                        }
                        break;

                    case ThirdPartyType.Twitter:
                        // Handle Twitter case
                        break;

                    default:
                        break;
                }

                response.Message = "Successful";
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = new ThirdPartyResponse { isSuccessful= true };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered ThirdPartyRegister Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }
    }
}
