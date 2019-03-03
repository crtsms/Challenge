using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.DAL;
using WebApplication.Model;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class SkillController : Controller
    {
        //Database context
        private HRContext db = new HRContext();

        // GET: Skill
        public async Task<ActionResult> Index()
        {
            //Retrieve the Skill information and Map 
            List<SkillVM> skillVM = new List<SkillVM>();
            foreach (Skill skill in await db.Skills.ToListAsync()) skillVM.Add(skill);

            return View(skillVM);
        }

        // GET: Skill/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Skill information and Map to ModeView
            SkillVM skillVM = await db.Skills.FindAsync(id);
            if (skillVM == null)
                return HttpNotFound();

            return View(skillVM);
        }

        // GET: Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] SkillVM skillVM)
        {
            if (ModelState.IsValid)
            {
                //Map to Model
                Skill skill = skillVM;
                db.Skills.Add(skill);

                //Call the EF to save chages
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(skillVM);
        }

        // GET: Skill/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Skill information and Map to ModeView
            SkillVM skillVM = await db.Skills.FindAsync(id);
            if (skillVM == null)
                return HttpNotFound();

            return View(skillVM);
        }

        // POST: Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] SkillVM skillVM)
        {
            if (ModelState.IsValid)
            {
                //Map the modelView to Model 
                Skill skill = skillVM;
                db.Entry(skill).State = EntityState.Modified;

                //Call the EF to save chages
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(skillVM);
        }

        // GET: Skill/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve Skill information for user comfirmation before delete
            SkillVM skillVM = await db.Skills.FindAsync(id);
            if (skillVM == null)
                return HttpNotFound();

            return View(skillVM);
        }

        // POST: Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);

            //Check if skill are assigned to employees
            if (db.EmployeeSkills.Where(x => x.SkillId == id).Count() > 0)
            {
                ModelState.AddModelError(string.Empty, "This skill are already associated with employees!");                
                SkillVM skillVM = skill;

                return View(skillVM);
            }

            //Delete the employee
            db.Skills.Remove(skill);

            //Call the EF to save chages
            await db.SaveChangesAsync();
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
