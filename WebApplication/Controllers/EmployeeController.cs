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
    public class EmployeeController : Controller
    {
        //Database context
        private HRContext db = new HRContext();

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            //Retrieve the Employee information and Map 
            List<EmployeeVM> employeeVM = new List<EmployeeVM>();
            foreach (Employee employee in await db.Employees.ToListAsync()) employeeVM.Add(employee);

            return View(employeeVM);
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Employee information and Map to ModeView
            EmployeeVM employeeVM = await db.Employees.FindAsync(id);
            if (employeeVM == null)
                return HttpNotFound();

            return View(employeeVM);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,PhoneNumber")] EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                //Map to Model
                Employee employee = employeeVM;
                db.Employees.Add(employee);

                //Call the EF to save chages
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { employee.Id });
            }

            return View(employeeVM);
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve the Employee information and Map to ModeView
            EmployeeVM employeeVM = await db.Employees.FindAsync(id);
            if (employeeVM == null)
                return HttpNotFound();

            return View(employeeVM);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumber")] EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                //Map the modelView to Model 
                Employee employee = employeeVM;
                db.Entry(employee).State = EntityState.Modified;

                //Call the EF to save chages
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Retrieve Employee information for user comfirmation before delete
            EmployeeVM employeeVM = await db.Employees.FindAsync(id);
            if (employeeVM == null)
                return HttpNotFound();

            return View(employeeVM);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //Delete all the Employ Skills
            foreach (EmployeeSkill employeeSkill in await db.EmployeeSkills.Where(x => x.EmployeeId == id).ToListAsync())
                db.EmployeeSkills.Remove(employeeSkill);

            //Delete the employee
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);

            //Call the EF to save chages
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
