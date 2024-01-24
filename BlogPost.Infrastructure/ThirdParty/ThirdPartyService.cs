using BlogPost.Infrastructure.Model;
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
using static BlogPost.SharedKernel.Utilities;

namespace BlogPost.Infrastructure.ThirdParty
{
    public class ThirdPartyService : IThirdPartyService
    {
        private readonly AppSettings _appSettings;
        public ThirdPartyService(IOptions<AppSettings> appSettings) 
        {
            _appSettings = appSettings.Value;
        }

        public async Task<ResponseModel<ThirdPartyResponse>> FaceBook(FaceBookRequest request)
        {
            var response = new ResponseModel<ThirdPartyResponse>();
            string serializedRequest = JsonConvert.SerializeObject(request);
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var passwordHash = PasswordHasher.HashPassword(request.PassWord, request.UserName);
            string url = $"{_appSettings.FaceBookUrl}userName = {request.UserName}&password ={passwordHash}";   
            Console.WriteLine($"LoginToFaceBook | PAYLOAD: {serializedRequest} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm} | URL: {url}");

            var data = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var httpResponse = await client.PostAsync(url, data);
            string result = httpResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"LoginToFaceBook response: {httpResponse.Content} | StatusCode: {httpResponse.StatusCode} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
            httpResponse.StatusCode = HttpStatusCode.OK;
            var thirdPartyResponse = new ThirdPartyResponse { isSuccessful = true }; 
            if (httpResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                response.StatusCode = httpResponse.StatusCode;
                response.Message = "Retrieved successfully";
                response.Status = true;
                response.Data = thirdPartyResponse;
                return response;
            }
            else
            {
                response.Message = "Oops! something went wrong.";
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Status = false;
                return response;
            }
        }
    }
}
