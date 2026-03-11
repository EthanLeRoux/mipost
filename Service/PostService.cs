public class PostService
{
    private readonly PostRepository _postRepository;

    public PostService(PostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAllAsync();
    }

    public async Task<Post?> GetPostByIdAsync(string id)
    {
        return await _postRepository.GetByIdAsync(id);
    }

    public async Task<Post> CreatePostAsync(string content, List<string>? images = null)
    {
        var post = new Post
        {
            Content = content,
            Images = images ?? new List<string>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _postRepository.CreateAsync(post);
        return post;
    }

    public async Task<bool> UpdatePostAsync(string id, string content, List<string>? images = null)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);

        if (existingPost == null)
            return false;

        existingPost.Content = content;
        existingPost.Images = images ?? new List<string>();
        existingPost.UpdatedAt = DateTime.UtcNow;

        await _postRepository.UpdateAsync(id, existingPost);
        return true;
    }

    public async Task<bool> DeletePostAsync(string id)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);

        if (existingPost == null)
            return false;

        await _postRepository.DeleteAsync(id);
        return true;
    }
}