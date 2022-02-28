using MyOrganization.DAL;
using MyOrganization.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MyOrganization.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private OrgContext db = new OrgContext();

        // GET: Asset
        public ActionResult Index()
        {
            var assets = db.Assets.Include(a => a.Organization);
            return View(assets.ToList());
        }

        // GET: Asset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (Session["ID"] != null)
            {
                var userValidation = (int)Session["ID"];
                if (userValidation != asset.OrganizationID)
                {
                    Session["ID"] = null;
                    return RedirectToAction("Login", "Account");
                }
                return View(asset);
            }
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Asset/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Asset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssetID,Name,Amount,OrganizationID")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                var checkProfile = from p in db.Assets
                                   where p.OrganizationID == asset.OrganizationID
                                   && p.Name == asset.Name
                                   select p;
                if (checkProfile.FirstOrDefault() != null)
                {
                    asset.Amount = asset.Amount + checkProfile.First().Amount;
                    return RedirectToAction("../Organization/Details", new { id = asset.OrganizationID });
                }
                ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", asset.OrganizationID);
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("../Organization/Details", new { id = asset.OrganizationID });
            }
            ViewBag.OrganizationID = new SelectList(db.Organizations, "ID", "Name", asset.OrganizationID);
            return View(asset);
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (Session["ID"] != null)
            {
                var userValidation = (int)Session["ID"];
                if (userValidation != asset.OrganizationID)
                {
                    Session["ID"] = null;
                    return RedirectToAction("Login", "Account");
                }
                return View(asset);
            }
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssetID,Name,Amount,OrganizationID")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Organization/Details", new { id = asset.OrganizationID });
            }
            return RedirectToAction("../Organization/Details", new { id = asset.OrganizationID });
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
            db.SaveChanges();
            return RedirectToAction("../Organization/Details", new { id = asset.OrganizationID });
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
