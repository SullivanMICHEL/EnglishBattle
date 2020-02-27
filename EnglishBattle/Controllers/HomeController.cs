﻿using EnglishBattle.Models;
using EnglishBattleModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishBattle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            Joueur joueur = new Joueur();

            if (ModelState.IsValid)
            {
                JoueurService joueurService = new JoueurService(new EnglishBattleEntities());

                joueur = joueurService.GetItem(model.Email, model.MotDePasse);
                if ((model.Email == joueur.email) && (model.MotDePasse == joueur.motDePasse))
                {
                    //Création de la partie
                    Session["Joueur"] = joueur;
                    return RedirectToAction("CreerPartie", "Home");
                }
                else
                {
                    Response.Write("Email et/ou mot de passe incorrect");
                }
            }
            return View(model);
        }

        public ActionResult CreerPartie()
        {
            Partie partie;
            GameViewModel modelGame = new GameViewModel();
            Joueur joueur = (Joueur) Session["Joueur"];
            //Création de la partie
            partie = modelGame.LancerPartie(joueur);
            Session["numQuestion"] = 0;
            Session["partie"] = partie;
            return RedirectToAction("Index", "Game");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}