using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.DAL;
using WebApplication.Model;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class EmployeeSkillController : Controller
    {
        //Database context
        private TreamsContext db = new TreamsContext();

        // GET: EmployeeSkill/Create
        public ActionResult Create(int EmployeeId)
        {
            //Retrieve the Employee And Skills information and Map to ModelView
            var viewModel = new EmployeeSkillVM();
            viewModel.Employee = db.Employees.Find(EmployeeId);
            viewModel.Skills = db.Skills.ToList();
            return View(viewModel);
        }

        // POST: EmployeeSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {

            //Getting the object associated with its id
            Int32 employee = Convert.ToInt32(collection["Employee.Id"]);
            Int32 skill = Convert.ToInt32(collection["SkillId"]);
            Int32 years = Convert.ToInt32(collection["YearsExperience"]);

            //Check if skill already registered for the employee
            if (db.Employees.FirstOrDefault(x => x.Id == employee).EmployeeSkills.FirstOrDefault(x => x.SkillId == skill) != null)
            {
                ModelState.AddModelError("SkillId", "This skill already registered for the Employee");
                var viewModel = new EmployeeSkillVM();
                viewModel.Employee = db.Employees.FirstOrDefault(x => x.Id == employee);
                viewModel.Skills = db.Skills.ToList();
                return View(viewModel);
            }
            else
            {
                //Create the Model in order to save on database 
                EmployeeSkill employeeSkill = new EmployeeSkill
                {
                    Employee = db.Employees.Find(employee),
                    Skill = db.Skills.Find(skill),
                    YearsExperience = years
                };

                db.EmployeeSkills.Add(employeeSkill);

                //Call the EF to save chages
                db.SaveChanges();

                return RedirectToAction("Edit", new RouteValueDictionary(
                    new { controller = "Employee", action = "Edit", Id = employee }));
            }
         
        }

        // GET: EmployeeSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Retrieve the Employee And Skills information and Map to ModelView
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EmployeeSkillVM();
            viewModel.Employee = db.EmployeeSkills.Find(id).Employee;
            viewModel.Skills = db.Skills.ToList();
            viewModel.YearsExperience = employeeSkill.YearsExperience;

            ViewBag.SkillId = employeeSkill.SkillId;

            return View(viewModel);

        }

        // POST: EmployeeSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //Prepare user data to Update
            var employeeSkills = db.EmployeeSkills.Find(id);

            //Getting the object associated with its id
            Int32 employee = Convert.ToInt32(collection["Employee.Id"]);
            Int32 skill = Convert.ToInt32(collection["SkillId"]);
            Int32 years = Convert.ToInt32(collection["YearsExperience"]);


            //Map the modelView to Model 
            employeeSkills.Employee = db.Employees.Find(employee);
            employeeSkills.Skill = db.Skills.Find(skill);
            employeeSkills.YearsExperience = years;

            db.Entry(employeeSkills).State = EntityState.Modified;

            //Call the EF to save chages
            db.SaveChanges();

            return RedirectToAction("Edit", new RouteValueDictionary(
                new { controller = "Employee", action = "Edit", Id = employee }));
        }

        // GET: EmployeeSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Retrieve the Employee And Skills information and Map to ModelView
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EmployeeSkillVM();
            viewModel.Employee = db.EmployeeSkills.Find(id).Employee;
            viewModel.Skills = db.Skills.ToList();
            viewModel.YearsExperience = employeeSkill.YearsExperience;

            ViewBag.SkillId = employeeSkill.SkillId;

            return View(viewModel);
        }

        // POST: EmployeeSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete the Skill
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            db.EmployeeSkills.Remove(employeeSkill);
            db.SaveChanges();

            return RedirectToAction("Edit", new RouteValueDictionary(
                new { controller = "Employee", action = "Edit", Id = employeeSkill.EmployeeId }));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
