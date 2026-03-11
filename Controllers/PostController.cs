using Microsoft.AspNetCore.Mvc;

namespace Mypost.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly PostService _postService;

    public PostsController(PostService postService)
    {
        _postService = postService;
    }

    // GET /posts
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    // GET /posts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(string id)
    {
        var post = await _postService.GetPostByIdAsync(id);

        if (post == null)
            return NotFound();

        return Ok(post);
    }

    // POST /posts
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        var createdPost = await _postService.CreatePostAsync(request.Content, request.Images);

        return CreatedAtAction(
            nameof(GetPost),
            new { id = createdPost.Id },
            createdPost
        );
    }

    // PUT /posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(string id, [FromBody] Post post)
    {
        var success = await _postService.UpdatePostAsync(id, post.Content, post.Images);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE /posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var success = await _postService.DeletePostAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}