using Blog_F1.Data;
using Blog_F1.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_F1.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly F1DbContext f1DbContext;

        public TagRepository(F1DbContext f1DbContext)
        {
            this.f1DbContext = f1DbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await f1DbContext.Tags.AddAsync(tag);
            await f1DbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag =await f1DbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                f1DbContext.Tags.Remove(existingTag);
                await f1DbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await f1DbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return f1DbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag= await f1DbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await f1DbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
