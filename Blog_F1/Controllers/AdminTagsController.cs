using Blog_F1.Data;
using Blog_F1.Models.Domain;
using Blog_F1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog_F1.Controllers
{
    public class AdminTagsController : Controller
    {
        private F1DbContext f1DbContext;
        public AdminTagsController(F1DbContext f1DbContext)
        {
            this.f1DbContext = f1DbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapowanie AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };


            f1DbContext.Tags.Add(tag);
            f1DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {

            //uzywamy dbContext aby odczytac tagi
            var tags=f1DbContext.Tags.ToList();

            return View(tags);
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var tag=f1DbContext.Tags.FirstOrDefault(x => x.Id == id);

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag=f1DbContext.Tags.Find(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //zapisanie zmian
                f1DbContext.SaveChanges();

                //Powiadomienie o sukcesie
                return RedirectToAction("Edit", new {id=editTagRequest.Id});
            }

            //Powiadomienie o błedzie
            return RedirectToAction("Edit", new {id=editTagRequest.Id});
        }

        public IActionResult Delete(EditTagRequest editTagRequest) 
        {
           var tag= f1DbContext.Tags.Find(editTagRequest.Id);

        if(tag != null)
            {
                f1DbContext.Tags.Remove(tag);
                f1DbContext.SaveChanges();

                //powiadomienie o sukcesie
                return RedirectToAction("List");
            }

        //powiadomienie o błędzieS
        return RedirectToAction("Edit", new {id=editTagRequest.Id});
        }
    }
}
