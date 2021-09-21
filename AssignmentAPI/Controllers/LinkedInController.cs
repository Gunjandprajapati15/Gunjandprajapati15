using AngularApplicationTest.Models;
using AngularApplicationTest.Repositories;
using AngularApplicationTest.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularApplicationTest.Controllers
{
    [Route("api/[controller]")]
    public class LinkedInController : Controller
    {
        ILinkedInService _linkedInService;

        public LinkedInController(ILinkedInService linkedInService)
        {
            _linkedInService = linkedInService;
        }

        /// <summary>
        /// List of linkedin post
        /// </summary>
        /// <returns>post data</returns>
        [Route("posts")]
        [HttpGet]
        public async Task<List<PostView>> ListPost()
        {
            return await _linkedInService.ListPost();
        }

        /// <summary>
        /// get post data by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>post data</returns>
        [Route("posts/{postId}")]
        [HttpGet]
        public async Task<Post> GetPostById(int postId)
        {
            return await _linkedInService.GetPostById(postId);
        }

        /// <summary>
        /// add post using post data
        /// </summary>
        /// <param name="file">post image</param>
        /// <param name="postJson">post json data</param>
        /// <returns></returns>
        [Route("posts")]
        [HttpPost]
        public async Task<int> AddPost([FromForm] IFormFile file, [FromForm] string postJson)
        {
            var post = JsonConvert.DeserializeObject<Post>(postJson);
            return await _linkedInService.AddPost(post, file);
        }

        /// <summary>
        /// update post data
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="file">post image</param>
        /// <param name="postJson">post data</param>
        /// <returns></returns>
        [Route("posts/{postId}")]
        [HttpPut]
        public async Task UpdatePost([FromRoute] int postId, [FromForm] IFormFile file, [FromForm] string postJson)
        {
            var post = JsonConvert.DeserializeObject<Post>(postJson);
            await _linkedInService.UpdatePost(postId, post, file);
        }

        /// <summary>
        /// delete post with data
        /// </summary>
        /// <param name="postId">post id</param>
        /// <returns></returns>
        [Route("posts/{postId}")]
        [HttpDelete]
        public async Task DeletePost([FromRoute] int postId)
        {
            await _linkedInService.DeletePost(postId);
        }

        /// <summary>
        /// Like individual post
        /// </summary>
        /// <param name="postId">post id</param>
        /// <returns></returns>
        [Route("posts/{postId}/like")]
        [HttpPost]
        public async Task LikePost([FromRoute] int postId)
        {
            await _linkedInService.LikePost(postId);
        }

        /// <summary>
        /// list of comments
        /// </summary>
        /// <param name="postId">post id</param>
        /// <returns>comment data</returns>
        [Route("posts/{postId}/comments")]
        [HttpGet]
        public async Task<List<Comment>> ListComment([FromRoute] int postId)
        {
            return await _linkedInService.ListComment(postId);
        }

        /// <summary>
        /// get comment data by comment id for particular post
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="commentId">comment id</param>
        /// <returns>comment data</returns>
        [Route("posts/{postId}/comments/{commentId}")]
        [HttpGet]
        public async Task<Comment> GetCommentId([FromRoute] int postId, [FromRoute] int commentId)
        {
            return await _linkedInService.GetCommentById(postId, commentId);
        }

        /// <summary>
        /// Add comment against post
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="comment">comment data</param>
        /// <returns></returns>
        [Route("posts/{postId}/comments")]
        [HttpPost]
        public async Task<int> AddComent([FromRoute] int postId, [FromBody] Comment comment)
        {
            return await _linkedInService.AddComent(postId, comment);
        }

        /// <summary>
        /// update comment
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="commentId">comment id</param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Route("posts/{postId}/comments/{commentId}")]
        [HttpPut]
        public async Task UpdateComment([FromRoute] int postId, [FromRoute] int commentId, [FromBody] Comment comment)
        {
            await _linkedInService.UpdateComment(postId, commentId, comment);
        }

        /// <summary>
        /// delete comment
        /// </summary>
        /// <param name="postId">post id</param>
        /// <param name="commentId">comment id</param>
        /// <returns></returns>
        [Route("posts/{postId}/comments/{commentId}")]
        [HttpDelete]
        public async Task DeleteComment([FromRoute] int postId, [FromRoute] int commentId)
        {
            await _linkedInService.DeleteComment(postId, commentId);
        }

    }
}
