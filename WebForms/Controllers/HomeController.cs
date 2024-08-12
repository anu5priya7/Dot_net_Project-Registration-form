using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebForms.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDbContext _context;

        public HomeController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentDbs.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDb = await _context.StudentDbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentDb == null)
            {
                return NotFound();
            }

            return View(studentDb);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            ViewBag.Qualifications = new List<string> { "Qualification1", "Qualification2", "Qualification3" };
            ViewBag.StateList = new SelectList(new[] { "State1", "State2", "State3" });
            return View(new StudentDb { Hobbies = new List<string>() });
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FatherName,Country,State,District,Dob,Gender")] StudentDb studentDb, string[] SelectedQualifications, string[] Hobbies)
        {
            if (ModelState.IsValid)
            {
                studentDb.Qualifications = string.Join(", ", SelectedQualifications ?? new string[0]);
                studentDb.Hobbies = Hobbies?.ToList() ?? new List<string>();
                _context.Add(studentDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Qualifications = new List<string> { "Qualification1", "Qualification2", "Qualification3" };
            ViewBag.StateList = new SelectList(new[] { "State1", "State2", "State3" });
            return View(studentDb);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDb = await _context.StudentDbs.FindAsync(id);
            if (studentDb == null)
            {
                return NotFound();
            }

            ViewBag.Qualifications = new List<string> { "Qualification1", "Qualification2", "Qualification3" };
            ViewBag.StateList = new SelectList(new[] { "State1", "State2", "State3" });
            studentDb.SelectedQualifications = studentDb.Qualifications?.Split(new[] { ", " }, StringSplitOptions.None).ToList() ?? new List<string>();
            return View(studentDb);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherName,Country,State,District,Dob,Gender")] StudentDb studentDb, string[] SelectedQualifications, string[] Hobbies)
        {
            if (id != studentDb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    studentDb.Qualifications = string.Join(", ", SelectedQualifications ?? new string[0]);
                    studentDb.Hobbies = Hobbies?.ToList() ?? new List<string>();
                    _context.Update(studentDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentDbExists(studentDb.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Qualifications = new List<string> { "Qualification1", "Qualification2", "Qualification3" };
            ViewBag.StateList = new SelectList(new[] { "State1", "State2", "State3" });
            return View(studentDb);
        }

        [HttpGet]
        public JsonResult GetDistricts(string state)
        {
            var districts = new List<string>();

            switch (state)
            {
                case "State1":
                    districts = new List<string> { "District1-1", "District1-2", "District1-3" };
                    break;
                case "State2":
                    districts = new List<string> { "District2-1", "District2-2", "District2-3" };
                    break;
                case "State3":
                    districts = new List<string> { "District3-1", "District3-2", "District3-3" };
                    break;
            }

            return Json(districts);
        }

        private bool StudentDbExists(int id)
        {
            return _context.StudentDbs.Any(e => e.Id == id);
        }
    }
}
