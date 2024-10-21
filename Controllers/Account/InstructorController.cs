﻿using BrainStormEra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BrainStormEra.Controllers.Account
{
    public class InstructorController : Controller
    {
        private readonly SwpDb7Context _context;

        public InstructorController(SwpDb7Context context)
        {
            _context = context;
        }

        public IActionResult ViewListOfInstructorCourse()
        {
            var userId = HttpContext.Session.GetString("user_id");
            if (userId.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            var courses = _context.Courses
                          .Where(c => _context.Enrollments
                              .Any(e => e.UserId == userId && e.CourseId == c.CourseId))
                          .ToList();

            return View(courses);
        }

    }
}