using Blog_F1.Data;
using Blog_F1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_F1.Repositories
{
    public class BlogPostRepository: IBlogPostRepository
    {
        private readonly F1DbContext f1DbContext;

        public BlogPostRepository(F1DbContext f1DbContext)
        {
            this.f1DbContext = f1DbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await f1DbContext.AddAsync(blogPost);
            await f1DbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog=await f1DbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                f1DbContext.BlogPosts.Remove(existingBlog);
                await f1DbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await f1DbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await f1DbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await f1DbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog=await f1DbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id=blogPost.Id;
                existingBlog.Naglowek = blogPost.Naglowek;
                existingBlog.StronaTytul = blogPost.StronaTytul;
                existingBlog.Zawartosc = blogPost.Zawartosc;
                existingBlog.KrotkiOpis = blogPost.KrotkiOpis;
                existingBlog.Autor = blogPost.Autor;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Widocznosc = blogPost.Widocznosc;
                existingBlog.DataPublikacji = blogPost.DataPublikacji;
                existingBlog.Tags=blogPost.Tags;

                await f1DbContext.SaveChangesAsync();
                return existingBlog;

            }

            return null;
        }
    }
}
