using Blog_F1.Models.Domain;
using Blog_F1.Models.ViewModels;
using Blog_F1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_F1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]

        public async Task<IActionResult> Add()
        {
            var tags=await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Naglowek = addBlogPostRequest.Naglowek,
                StronaTytul = addBlogPostRequest.StronaTytul,
                Zawartosc = addBlogPostRequest.Zawartosc,
                KrotkiOpis = addBlogPostRequest.KrotkiOpis,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                DataPublikacji = addBlogPostRequest.DataPublikacji,
                Autor = addBlogPostRequest.Autor,
                Widocznosc = addBlogPostRequest.Widocznosc,
            };

            var selectedTags = new List<Tag>();

            foreach(var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid=Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPost=await blogPostRepository.GetAllAsync();

            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tagsDomainModel=await tagRepository.GetAllAsync();
            var blogPost = await blogPostRepository.GetAsync(id);

            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Naglowek = blogPost.Naglowek,
                    StronaTytul = blogPost.StronaTytul,
                    Zawartosc = blogPost.Zawartosc,
                    Autor = blogPost.Autor,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    KrotkiOpis = blogPost.KrotkiOpis,
                    DataPublikacji = blogPost.DataPublikacji,
                    Widocznosc = blogPost.Widocznosc,
                    Tags = tagsDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

            return View(model);
            }

                       

            return View(null);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Naglowek = editBlogPostRequest.Naglowek,
                StronaTytul = editBlogPostRequest.StronaTytul,
                Zawartosc = editBlogPostRequest.Zawartosc,
                Autor = editBlogPostRequest.Autor,
                KrotkiOpis = editBlogPostRequest.KrotkiOpis,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                DataPublikacji = editBlogPostRequest.DataPublikacji,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Widocznosc = editBlogPostRequest.Widocznosc
            };

            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag=await tagRepository.GetAsync(tag);

                    if(foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;

            var updatedBlog=await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(updatedBlog != null)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deletedBlogPost=await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id=editBlogPostRequest.Id});

        }

    }
}
