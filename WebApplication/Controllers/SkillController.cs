using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.DAL;
using WebApplication.Model;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class SkillController : Controller
    {
        private TreamsContext db = new TreamsContext();

        // GET: Skill
        public ActionResult Index()
        {
            List<SkillVM> viewModel = new List<SkillVM>();

            foreach (var item in db.Skills.ToList())
            {
                var skill = new SkillVM();
                skill.Id = item.Id;
                skill.Name = item.Name;

                if (item.Description != null)
                    skill.Description = item.Description.Length < 100 ? item.Description : item.Description.Substring(0, 100) + "...";

                viewModel.Add(skill);
            }

            return View(viewModel);

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
