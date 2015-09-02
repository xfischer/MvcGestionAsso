﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.Controllers
{
    public class AbonnementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Abonnements
        public async Task<ActionResult> Index()
        {
            var abonnements = db.Abonnements.Include(a => a.Adherent).Include(a => a.Formule);
            return View(await abonnements.ToListAsync());
        }

        // GET: Abonnements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = await db.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            return View(abonnement);
        }

        // GET: Abonnements/Create
        public ActionResult Create()
        {
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "AdherentNom");
            ViewBag.FormuleId = new SelectList(db.Formules, "FormuleId", "FormuleNom");
            return View();
        }

        // POST: Abonnements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AbonnementId,AdherentId,FormuleId,TypeReglement,DateDebut,DateFin")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                db.Abonnements.Add(abonnement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
            ViewBag.FormuleId = new SelectList(db.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
            return View(abonnement);
        }

        // GET: Abonnements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = await db.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
            ViewBag.FormuleId = new SelectList(db.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
            return View(abonnement);
        }

        // POST: Abonnements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AbonnementId,AdherentId,FormuleId,TypeReglement,DateDebut,DateFin")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonnement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdherentId = new SelectList(db.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
            ViewBag.FormuleId = new SelectList(db.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
            return View(abonnement);
        }

        // GET: Abonnements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = await db.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            return View(abonnement);
        }

        // POST: Abonnements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Abonnement abonnement = await db.Abonnements.FindAsync(id);
            db.Abonnements.Remove(abonnement);
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