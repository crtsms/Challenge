using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.DAL;
using WebApplication.ViewModels;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    public class EmployeeSkillController : Controller
    {
        //Database context
        private HRContext db = new HRContext();

        // GET: EmployeeSkill
        public async Task<ActionResult> Index(int id)
        {
            //Retrieve the Employee information and Map 
            List<EmployeeSkillVM> employeeSkillVM = new List<EmployeeSkillVM>();
            foreach (EmployeeSkill employeeSkill in await db.EmployeeSkills.Where(x => x.EmployeeId == id).ToListAsync())
                employeeSkillVM.Add(employeeSkill);

            //return ID in view bag, in case of employee don't have any skill yet
            //this ID is used in the link to add new skill
            ViewBag.EmployeeId = id;

            return View(employeeSkillVM);
        }

        // GET: EmployeeSkill/Create
        public async Task<ActionResult> Create(int id)
        {
            if(await db.Employees.FindAsync(id) == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Employee information and Map to ModeView
            EmployeeSkillVM employeeSkillVM = new EmployeeSkillVM();
            employeeSkillVM.EmployeeId = id;

            //Retrieve all skill on database not assigned to Employee
            var skills = (from s in db.Skills
                          .Except(from es in db.EmployeeSkills where es.EmployeeId == id select es.Skill)
                          select new SelectListItem { Value = s.Id.ToString(), Text = s.Name });

            //Attach to modelView
            employeeSkillVM.Skills = skills;

            return View(employeeSkillVM);
        }

        // POST: EmployeeSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeId,SkillId,YearsExperience")] EmployeeSkillVM employeeSkillVM)
        {
            if (ModelState.IsValid)
            {
                //Map to Model
                EmployeeSkill employeeSkill = employeeSkillVM;
                employeeSkill.Skill = await db.Skills.FindAsync(employeeSkillVM.SkillId);
                employeeSkill.Employee = await db.Employees.FindAsync(employeeSkillVM.EmployeeId);

                //Call the EF to save chages
                db.EmployeeSkills.Add(employeeSkill);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Employee", new { Id = employeeSkill.EmployeeId });
            }

            return View(employeeSkillVM);
        }

        // GET: EmployeeSkill/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Employee information and Map to ModeView
            EmployeeSkillVM employeeSkillVM = await db.EmployeeSkills.FindAsync(id);

            //Retrieve all skill on database not assigned to Employee
            var skills = (from s in db.Skills
                          .Except(from es in db.EmployeeSkills
                                  where es.EmployeeId == employeeSkillVM.EmployeeId && es.Id != id
                                  select es.Skill)
                          select new SelectListItem{ Value = s.Id.ToString(), Text = s.Name });

            //Attach to modelView
            employeeSkillVM.Skills = skills;

            return View(employeeSkillVM);
        }

        // POST: EmployeeSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,Skill,SkillId,YearsExperience")] EmployeeSkillVM employeeSkillVM)
        {
            if (ModelState.IsValid)
            {
                //Map the modelView to Model 
                EmployeeSkill employeeSkill = employeeSkillVM;
                db.Entry(employeeSkill).State = EntityState.Modified;

                //Call the EF to save chages
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Employee", new { Id = employeeSkill.EmployeeId });
            }
            return View(employeeSkillVM);
        }

        // GET: EmployeeSkill/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);            

            //Retrieve Employee information for user comfirmation before delete
            EmployeeSkillVM employeeSkillVM = await db.EmployeeSkills.FindAsync(id);
            if (employeeSkillVM == null)
                return HttpNotFound();

            return View(employeeSkillVM);
        }

        // POST: EmployeeSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Delete the Skill
            EmployeeSkill employeeSkill = await db.EmployeeSkills.FindAsync(id);
            db.EmployeeSkills.Remove(employeeSkill);

            //Call the EF to save chages
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Employee", new { Id = employeeSkill.EmployeeId });
        }

        public ActionResult SkillAlreadyAssigned(int? Id, int SkillId, int EmployeeId)
        {
            int skills = 0;

            //Validation for new objects            
            if (Id == null){
                skills = (from es in db.EmployeeSkills
                          where es.EmployeeId == EmployeeId
                          && es.SkillId == SkillId
                          select es.Id).Count();
            }else{
                skills = (from es in db.EmployeeSkills
                          where es.EmployeeId == EmployeeId
                          && es.SkillId == SkillId
                          && es.Id != Id
                          select es.Id).Count();
            }

            //If Skill already assigned to employee, return false to not allow the
            //same skill assigned to the same employee again
            if (skills > 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
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
