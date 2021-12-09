using MyOrganization.DAL;
using MyOrganization.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyOrganization.Controllers
{
    public class OrganizationController : Controller
    {
        private OrgContext db = new OrgContext();
        // GET: Organization
        public ActionResult Index(int? id)
        {
            Organization organization = db.Organizations.Find(id);

            var userValidation = (int)Session["ID"];
            if (userValidation != organization.ID)
            {
                Session["ID"] = null;
                return RedirectToAction("Login", "Account");
            }

            return View(organization);
        }

        // GET: Organization/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            var userValidation = (int)Session["ID"];
            if (userValidation != organization.ID)
            {
                Session["ID"] = null;
                return RedirectToAction("Login", "Account");
            }

            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Organization/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Name,Address")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                var userValidation = (int)Session["ID"];
                if (userValidation != organization.ID)
                {
                    Session["ID"] = null;
                    return RedirectToAction("Login", "Account");
                }
                var checkProfile = from p in db.Organizations
                                   where p.Name == organization.Name
                                   select p;

                if (checkProfile.FirstOrDefault() != null)
                {
                    ModelState.AddModelError("name", "Organization Already Exists.");
                    return View(organization);
                }
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = organization.ID });
            }
            return View(organization);
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organization/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Name,Address")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                var userValidation = (int)Session["ID"];
                if (userValidation != organization.ID)
                {
                    Session["ID"] = null;
                    return RedirectToAction("Login", "Account");
                }
                var checkProfile = from p in db.Organizations
                                   where p.Name == organization.Name
                                   && p.ID != organization.ID
                                   select p;

                if (checkProfile.FirstOrDefault() != null)
                {
                    ModelState.AddModelError("name", "Organization Already Exists.");
                    return View(organization);
                }
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = organization.ID });
            }
            return View(organization);
        }

        // GET: Organization/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            var userValidation = (int)Session["ID"];
            if (userValidation != organization.ID)
            {
                Session["ID"] = null;
                return RedirectToAction("Login", "Account");
            }
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
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
