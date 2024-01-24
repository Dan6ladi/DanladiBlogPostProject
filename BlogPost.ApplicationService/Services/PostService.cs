using BlogPost.ApplicationService.Interface;
using BlogPost.Domain.Entity;
using BlogPost.Repository.Interface;
using BlogPost.SharedKernel;
using BlogPost.SharedKernel.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.ApplicationService.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly EmailSettings _emailSettings;

        public PostService(IPostRepository postRepository, IOptions<EmailSettings> emailSettings)
        {
            _postRepository = postRepository;
            _emailSettings = emailSettings.Value;
        }


        public async Task<ResponseModel> CreatePost(PostRequest request, string userId,string author)
        {
            var response = new ResponseModel();
            try
            {
                Console.WriteLine($"Entered CreatePost | PAYLOAD: {JsonConvert.SerializeObject(request)} | User: {userId} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd");
                var postData = Post.CreatePost(author, request.Title, request.Content, timeStamp ,userId);
                var addPost = await _postRepository.CreatePost(postData);
                if (!addPost)
                {
                    response.Message = "Internal server error";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    return response;
                }
                response.Message = "Post created successfully";
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered CreatePost Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
            
        }

        public async Task<ResponseModel<Post>> ReadPost(string userId, int pageNumber, int MaxItem)
        {
            IPagedList<Post> paginatedResponse;
            var response = new ResponseModel<Post>();
            try
            {
                Console.WriteLine($"Entered ReadPost | PAYLOAD: {userId}| DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                var postHistory = await _postRepository.GetPost(userId);
                if(!postHistory.Any())
                {
                    Console.WriteLine($"Entered ReadPost Response | PAYLOAD: {JsonConvert.SerializeObject(postHistory)}| DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                    paginatedResponse = postHistory.ToPagedList(pageNumber, MaxItem);
                    response.Message = "Oops something went wrong";
                    response.Status = false;
                    response.Result = paginatedResponse;
                    response.Count = postHistory.Count;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Pager = PaginationResponse.GetPaginationResponse(paginatedResponse);
                    return response;
                }
                Console.WriteLine($"Entered ReadPost Response | PAYLOAD: {JsonConvert.SerializeObject(postHistory)}| DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                paginatedResponse = postHistory.ToPagedList(pageNumber, MaxItem);
                response.Message = "Data retrieved successfully";
                response.Status = true;
                response.Result = paginatedResponse;
                response.Count = postHistory.Count;
                response.StatusCode = HttpStatusCode.OK;
                response.Pager = PaginationResponse.GetPaginationResponse(paginatedResponse);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered ReadPost Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }


        public async Task<ResponseModel> UpdatePost(string postId, string content, string title)
        {
            var response = new ResponseModel();
            try
            {
                Console.WriteLine($"Entered UpdatePost | PAYLOAD: {postId},{content},{title} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                var timeStamp = DateTime.Now;
                var postData = await _postRepository.GetPostById(postId);
                if (postData == null)
                {
                    response.Message = "Error retrieving record";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                var editPost = Post.EditPost(postData, title, content, timeStamp.ToString());
                var updatePost = await _postRepository.UpdatePost(editPost);
                if (!updatePost)
                {
                    response.Message = "failed to edit post";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered UpdatePost Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }


        public async Task<ResponseModel> DeletePost(string postId)
        {
            var response = new ResponseModel();
            try
            {
                Console.WriteLine($"Entered CreatePost | PAYLOAD: {postId} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                var timeStamp = DateTime.Now;
                var postData = await _postRepository.GetPostById(postId);
                if(postData == null)
                {
                    response.Message = "Error retrieving record";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                var deletePost = await _postRepository.DeletePost(postData);
                if (!deletePost)
                {
                    response.Message = "Internal server error";
                    response.Status = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    return response;
                }
                response.Message = "post successfully deleted";
                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Entered CreatePost Exception:  {ex.Message} - {ex.InnerException} - {ex.StackTrace} | DATE: {DateTime.Now:dd MMM yyyy : HH-mm}");
                response.Message = "Internal Server Error";
                response.Status = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }

        }
    }
}
