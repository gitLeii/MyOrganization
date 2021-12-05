using MyOrganization.DAL;
using MyOrganization.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyOrganization.Controllers
{
    public class EmployeeController : Controller
    {
        private OrgContext db = new OrgContext();
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Organization);
            return View(db.Employees.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Email, Type, OrganizationID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var checkProfile = from p in db.Employees
                                   where p.OrganizationID == employee.OrganizationID
                                   && p.Name == employee.Name
                                   select p;
                if (checkProfile.FirstOrDefault() != null)
                {
                    ModelState.AddModelError("name", "Employee Already Exists.");
                    return View(employee);
                }
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("../Organization/Details", new { id = employee.OrganizationID });
            }
            return View(employee);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", employee.OrganizationID);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Name,Email,Type, OrganizationID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var checkProfile = from p in db.Employees
                                   where p.OrganizationID == employee.OrganizationID
                                   && p.Name == employee.Name
                                   select p;
                if (checkProfile.FirstOrDefault() != null)
                {
                    ModelState.AddModelError("name", "Employee Already Exists.");
                    return View(employee);
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Organization/Details", new { id = employee.OrganizationID });
            }
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", employee.OrganizationID);
            return View(employee);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return View(employee);
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("../Organization/Details", new { id = employee.OrganizationID });

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