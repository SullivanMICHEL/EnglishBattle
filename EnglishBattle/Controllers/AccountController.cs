using EnglishBattle.Models;
using EnglishBattleModel.Data;
using EnglishBattleModel.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishBattle.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            List<SelectListItem> niveaux = new List<SelectListItem>();
            niveaux.Add(new SelectListItem { Text = "A1", Value = "1" });
            niveaux.Add(new SelectListItem { Text = "A2", Value = "2" });
            niveaux.Add(new SelectListItem { Text = "B1", Value = "3" });
            niveaux.Add(new SelectListItem { Text = "B2", Value = "4" });
            niveaux.Add(new SelectListItem { Text = "C1", Value = "5" });
            
            
            VilleService villeService = new VilleService(new EnglishBattleEntities());

            List<Ville> villes = villeService.GetList();

            List<SelectListItem> listeVille = new List<SelectListItem>();
            for (int i = 0; i < villes.Count; i++)
            {
                listeVille.Add(new SelectListItem { Text = villes[i].nom, Value = villes[i].id.ToString() });
            }

            ViewBag.Niveau = niveaux;
            ViewBag.Ville = listeVille;
            return View();
        }
        [HttpPost]
        public ActionResult Register(InscriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Joueur joueur = new Joueur();
                joueur.nom = model.Nom;
                joueur.prenom = model.Prenom;
                joueur.email = model.Email;
                joueur.motDePasse = model.MotDePasse;
                joueur.niveau = model.Niveau;
                joueur.idVille = model.Ville;

                //Session["Joueur"] = joueur;
                JoueurService joueurService = new JoueurService(new EnglishBattleEntities());
                joueurService.Insert(joueur);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}