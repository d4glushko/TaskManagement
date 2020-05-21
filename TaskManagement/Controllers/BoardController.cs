using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Board
        public ActionResult Index()
        {
            return View(db.ColumnModels.Include(x => x.Tasks).ToList());
        }

        // GET: Board/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnModel columnModel = db.ColumnModels.Find(id);
            if (columnModel == null)
            {
                return HttpNotFound();
            }
            return View(columnModel);
        }

        // GET: Board/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Position")] ColumnModel columnModel)
        {
            if (ModelState.IsValid)
            {
                columnModel.ID = Guid.NewGuid();
                db.ColumnModels.Add(columnModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(columnModel);
        }

        // GET: Board/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnModel columnModel = db.ColumnModels.Find(id);
            if (columnModel == null)
            {
                return HttpNotFound();
            }
            return View(columnModel);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Position")] ColumnModel columnModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(columnModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(columnModel);
        }

        // GET: Board/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnModel columnModel = db.ColumnModels.Find(id);
            if (columnModel == null)
            {
                return HttpNotFound();
            }
            return View(columnModel);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ColumnModel columnModel = db.ColumnModels.Find(id);
            var tasks = columnModel.Tasks.ToList();
            foreach (var task in tasks)
            {
                db.TaskModels.Remove(task);
            }
            db.ColumnModels.Remove(columnModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TaskModels/CreateTask
        public ActionResult CreateTask(Guid? columnId)
        {
            var userId = User.Identity.GetUserId();
            if (columnId == null || string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColumnModel columnModel = db.ColumnModels.Find(columnId);
            Users user = db.Users.Find(userId);
            if (columnModel == null || user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColumnID = new SelectList(db.ColumnModels.OrderBy(x => x.Position), "ID", "Name", columnModel.ID);
            //ViewBag.ColumnID = new SelectList(db.ColumnModels, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users.OrderBy(x => x.Email), "Id", "Email", user.Id);
            return View();
        }

        // POST: TaskModels/CreateTask
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask([Bind(Include = "ID,ColumnID,UserID,Name,Description,Position")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.ID = Guid.NewGuid();
                db.TaskModels.Add(taskModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColumnID = new SelectList(db.ColumnModels, "ID", "Name", taskModel.ColumnID);
            ViewBag.UserID = new SelectList(db.Users.OrderBy(x => x.Email), "Id", "Email", taskModel.UserID);
            return View(taskModel);
        }

        // GET: TaskModels/EditTask/5
        public ActionResult EditTask(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.TaskModels.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColumnID = new SelectList(db.ColumnModels.OrderBy(x => x.Position), "ID", "Name", taskModel.ColumnID);
            ViewBag.UserID = new SelectList(db.Users.OrderBy(x => x.Email), "Id", "Email", taskModel.UserID);
            return View(taskModel);
        }

        // POST: TaskModels/EditTask/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask([Bind(Include = "ID,ColumnID,UserID,Name,Description,Position")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColumnID = new SelectList(db.ColumnModels.OrderBy(x => x.Position), "ID", "Name", taskModel.ColumnID);
            ViewBag.UserID = new SelectList(db.Users.OrderBy(x => x.Email), "Id", "Email", taskModel.UserID);
            return View(taskModel);
        }

        // GET: TaskModels/DeleteTask/5
        public ActionResult DeleteTask(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = db.TaskModels.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // POST: TaskModels/DeleteTask/5
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTaskConfirmed(Guid id)
        {
            TaskModel taskModel = db.TaskModels.Find(id);
            db.TaskModels.Remove(taskModel);
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
