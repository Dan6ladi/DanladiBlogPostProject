using BlogPost.ApplicationService.Interface;
using BlogPost.SharedKernel.Models;
using BlogPost.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using BlogPost.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using BlogPost.Domain.Entity;

namespace BlogPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostManagementController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserRepository _userRepository;
        public BlogPostManagementController(IPostService postService, IUserRepository userRepository) 
        {
            _postService = postService;
            _userRepository = userRepository;
        }


        [Route("CreatePost")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(ResponseModel<string>))]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest request)
        {
            if (request is null) { return BadRequest("Invalid Request"); }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId.Value))
            {
                return Unauthorized("Unauthorized request, kindly log in");
            }
            var user = await _userRepository.GetUserById(userId.Value);
            var postResponse = await _postService.CreatePost(request,userId.Value,user.UserName);
            if (postResponse.StatusCode == HttpStatusCode.BadRequest || postResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(postResponse);
            }
            return Ok(postResponse);

        }


        [Route("ReadPost")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(ResponseModel<Post>))]
        public async Task<IActionResult> ReadPost(int pageNumber, int MaxItem)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId.Value))
            {
                return Unauthorized("Unauthorized request, kindly log in");
            }
            var readResponse = await _postService.ReadPost(userId.Value, pageNumber,MaxItem);
            if (readResponse.StatusCode == HttpStatusCode.BadRequest || readResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(readResponse);
            }
            return Ok(readResponse);

        }


        [Route("UpdatePost")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(ResponseModel<string>))]
        public async Task<IActionResult> UpdatePost(string content, string title, string postId)
        {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(title))
            {
                return BadRequest(" content and title cannot be null or empty");
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId.Value))
            {
                return Unauthorized("Unauthorized request, kindly log in");
            }
            var updateResponse = await _postService.UpdatePost(postId, content, title);
            if (updateResponse.StatusCode == HttpStatusCode.BadRequest || updateResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(updateResponse);
            }
            return Ok(updateResponse);

        }


        [Route("DeletePost")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(ResponseModel<string>))]
        public async Task<IActionResult> DeletePost(string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return BadRequest(" postId cannot be null or empty");
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId.Value))
            {
                return Unauthorized("Unauthorized request, kindly log in");
            }
            var deleteResponse = await _postService.DeletePost(postId);
            if (deleteResponse.StatusCode == HttpStatusCode.BadRequest || deleteResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(deleteResponse);
            }
            return Ok(deleteResponse);

        }

    }
}
