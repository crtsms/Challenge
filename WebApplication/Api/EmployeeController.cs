using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.DAL;
using WebApplication.ViewModels;

namespace WebApplication.Api
{
    public class EmployeeController : ApiController
    {
        private HRContext db = new HRContext();

        // GET: api/Employee/5
        [ResponseType(typeof(EmployeeVM))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {

            //Retrieve the Employee information and Map to ModeView
            EmployeeVM employeeVM = await db.Employees.FindAsync(id).Include(employee => employee.EmployeeSkills);
            if (employeeVM == null)
                return NotFound();

            return Ok(employeeVM);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployeeVM(int id, EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeVM.Id)
            {
                return BadRequest();
            }

            db.Entry(employeeVM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeVMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employee
        [ResponseType(typeof(EmployeeVM))]
        public async Task<IHttpActionResult> PostEmployeeVM(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employeeVM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employeeVM.Id }, employeeVM);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(EmployeeVM))]
        public async Task<IHttpActionResult> DeleteEmployeeVM(int id)
        {
            EmployeeVM employeeVM = await db.Employees.FindAsync(id);
            if (employeeVM == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employeeVM);
            await db.SaveChangesAsync();

            return Ok(employeeVM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeVMExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}