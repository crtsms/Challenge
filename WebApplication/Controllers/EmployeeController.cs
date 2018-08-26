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
    public class EmployeeController : Controller
    {
        //Database context
        private TreamsContext db = new TreamsContext();

        // GET: Employee
        public ActionResult Index()
        {
            //Retrieve the Employee information
            var viewModel = new EmployeeVM();

            //Map to ModeView
            viewModel.Employees = db.Employees.ToList();
            viewModel.Skills = db.Skills.ToList();

            return View(viewModel);

        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee collection)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(collection);
                db.SaveChanges();

                return RedirectToAction("Edit", new RouteValueDictionary(
                    new { controller = "Employee", action = "Edit", Id = collection.Id }));
            }

            return View(collection);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Retrieve the Employee information
            var viewModel = new EmployeeVM();
            var result = db.Employees.Find(id);

            //Map to ModeView
            viewModel.Employees = new List<Employee> { result };
            viewModel.Skills = db.Skills.ToList();
            viewModel.EmployeeSkills = db.EmployeeSkills.Where(x => x.EmployeeId == result.Id).ToList();

            if (viewModel.Employees == null)
            {
                viewModel.Skills = db.Skills.ToList();
                return HttpNotFound();
            }

            return View(viewModel);

        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //Prepare user data to Update
            var employee = db.Employees.Find(id);

            if (employee == null)
                return HttpNotFound();

            //Map the modelView to Model 
            employee.FirstName = collection["FirstName"];
            employee.LastName = collection["LastName"];
            employee.PhoneNumber = collection["PhoneNumber"];

            db.Entry(employee).State = EntityState.Modified;

            //Call the EF to save chages
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            //Retrieve Employee information for user comfirmation before delete
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Employee employee = db.Employees.Find(id);
            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Delete all the Employ Skills
            foreach(var item in db.Employees.Find(id).EmployeeSkills.ToList())
            {
                EmployeeSkill employeeSkill = db.EmployeeSkills.Find(item.Id);
                db.EmployeeSkills.Remove(employeeSkill);
            }

            //Delete the employee
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);

            //Call the EF to save chages
            db.SaveChanges();

            return RedirectToAction("Index");
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
