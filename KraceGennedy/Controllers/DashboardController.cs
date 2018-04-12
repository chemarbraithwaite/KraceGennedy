using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using KraceGennedy.Models;

namespace KraceGennedy.Controllers
{
    public class DashboardController : Controller
    {
        private DatabaseCon db = new DatabaseCon();

        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            Notify notify = new Notify();
            return View(notify);
        }

        [HttpPost]
        public ActionResult Email(Notify notify)
        {

            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            if (notify == null)
                    return RedirectToAction("Index", "Dashboard");
                
            if (notify.Tomorrow)
            {
                if (notify.RainKngTomorrow)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Tomorrow";
                    mailMessage.Body = "You will be working only 4 hours tomorrow owing to the rain";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Kingston"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);
                }
                else
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Tomorrow";
                    mailMessage.Body = "You will be working the full 8 hours tomorrow";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Kingston"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);


                }

                if (notify.RainMobdayTomorrow)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Tomorrow";
                    mailMessage.Body = "You will be working only 4 hours tomorrow owing to the rain";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Montego Bay"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);

                }
                else
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Tomorrow";
                    mailMessage.Body = "You will be working the full 8 hours tomorrow";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Montego Bay"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);

                }
            }
            else if (notify.Today)
            {
                if (notify.RainKngToday)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Today";
                    mailMessage.Body = "You will be working only 4 hours today owing to the rain";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Kingston"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                        
                    sendEmail(mailMessage);
                }
                else
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Today";
                    mailMessage.Body = "You will be working the full 8 hours today";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Kingston"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);


                }

                if (notify.RainMobdayToday)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Today";
                    mailMessage.Body = "You will be working only 4 hours today owing to the rain";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Montego Bay"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);

                }
                else
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("kracegennedy@gmail.com");
                    mailMessage.Subject = "Schedule for Today";
                    mailMessage.Body = "You will be working the full 8 hours today";
                    var kngEmps = db.Employees.Where(e => e.city.Equals("Montego Bay"));
                    foreach (var item in kngEmps)
                    {
                        mailMessage.To.Add(item.email);
                    }
                    sendEmail(mailMessage);

                }

            }





            Notify note = new Notify();
            note.Sent = true;
            return View("Index", note);


        }

        public void sendEmail(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);

        }

        // GET: Dashboard/Details/5
        public ActionResult Employees()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");

            var employees = db.Employees.Include(e => e.Role1);
            return View(employees.ToList());
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            ViewBag.role = new SelectList(db.Roles, "ID", "roleDes");
            return View();
        }

        // POST: Dashboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,telephoneNo,role,email,address1,city,country,lat,lon")] Employee employee)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Employees");
            }

            ViewBag.role = new SelectList(db.Roles, "ID", "roleDes", employee.role);
            return View(employee);
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.role = new SelectList(db.Roles, "ID", "roleDes", employee.role);
            return View(employee);
        }

        // POST: Dashboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,telephoneNo,role,email,address1,city,country,lat,lon")] Employee employee)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Employees");
            }
            ViewBag.role = new SelectList(db.Roles, "ID", "roleDes", employee.role);
            return View(employee);
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
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

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Employees");
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
