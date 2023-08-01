using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.Data;
using Practise.Models;

namespace Practise.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _context.Employee.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult AddEmployees()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> AddEmployees(Employees employees)
        //{
        //    var employee = await _context.Employee.AddAsync(employees);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> AddEmployees(IFormFile imageFile, Employees employee)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique filename for the uploaded image
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                // Define the target file path (e.g., in wwwroot/images)
                string targetPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // Create the "images" folder if it doesn't exist
                string imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                // Save the uploaded file to the target path
                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Set the "Image" property of the employee to the filename
                employee.Image = fileName;
            }

            if (ModelState.IsValid)
            {
                // Save the employee details to the database
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to the home page after successful upload.
            }

            return View(employee);
        }
        [HttpGet]
        public IActionResult EditEmployees(Guid Id)
        {
           if (Id == null || _context.Employee == null)
            {
                return NotFound();
            }
           var employees = _context.Employee.Find(Id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }
        [HttpPost]
        public IActionResult EditEmployees(Employees employees)
        {
            _context.Employee.Update(employees);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            if(Id==null || _context.Employee == null)
            {
                return NotFound();
            }
            var employees = _context.Employee.Find(Id);
            if (employees == null)
            {
                return NotFound(Id);
            }
            return View(employees);
            //Employees employees = _context.Employee.Find(id);

            //var dialog = new Dialog("Are you Sure you want to delete this employee?");
            //dialog.AddButton("Yes", "/Employees/DeleteConfirmed/" + id);
            //dialog.AddButton("No", "/Employees");

            //// Display the confirmation dialog.
            //return DialogResult(dialog);
        }
        [HttpPost]
        public IActionResult Delete(Employees employees)
        {
            _context.Employee.Remove(employees);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
