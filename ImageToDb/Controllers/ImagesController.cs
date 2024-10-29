using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageToDb.Data;
using ImageToDb.Models;
namespace ImageToDb.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> resim, string uploadedBy, string imageDesc)
        {
            foreach (var file in resim)
            {
                Image image = new Image();
                if (resim != null)

                {
                    if (file.Length > 0)

                    //Convert Image to byte and save to database

                    {

                        byte[] myByteArray = null;
                        using (var myFileStream = file.OpenReadStream())
                        using (var myMemoryStream = new MemoryStream())
                        {
                            myFileStream.CopyTo(myMemoryStream);
                            myByteArray = myMemoryStream.ToArray();
                        }
                        image.Img = myByteArray;
                        image.CreatedDate = DateTime.Now;
                        image.UploadedBy = uploadedBy;
                        image.ImageDesc = imageDesc;

                    }
                }
                _context.Images.Add(image);
                await _context.SaveChangesAsync();


            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile resim, string uploadedBy, string imageDesc)
        {
            Image image = new Image();
            image.ImageId = id;
            if (resim != null)

            {
                if (resim.Length > 0)

                //Convert Image to byte and save to database

                {

                    byte[] myByteArray = null;
                    using (var myFileStream = resim.OpenReadStream())
                    using (var myMemoryStream = new MemoryStream())
                    {
                        myFileStream.CopyTo(myMemoryStream);
                        myByteArray = myMemoryStream.ToArray();
                    }
                    image.Img = myByteArray;
                    image.CreatedDate = DateTime.Now;
                    image.UploadedBy = uploadedBy;
                    image.ImageDesc = imageDesc;
                }
            }
            _context.Update(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.SingleOrDefaultAsync(m => m.ImageId == id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
