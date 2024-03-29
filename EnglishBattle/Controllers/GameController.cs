﻿using EnglishBattle.Models;
using EnglishBattleModel.Data;
using EnglishBattleModel.Data.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace EnglishBattle.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            GameViewModel model = new GameViewModel();

            Joueur joueur = (Joueur)Session["Joueur"];
            Partie partie = (Partie)Session["partie"];
            model.joueur = joueur;
            Question question;            

            if ((int)Session["numQuestion"] >= 5)
            {
                Session["resultat"] = "Félicitations !";
                return RedirectToAction("FinalPage", "Game");
            }

            //Création de la question
            question = model.CreerQuestion(partie);
            Session["numQuestion"] = (int)Session["numQuestion"]+1;
            model.numQuestion = (int)Session["numQuestion"];

            Session["question"] = question;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(GameViewModel model)
        {
            Partie partie = (Partie)Session["partie"];
            Question question= (Question)Session["question"];

            question.dateReponse = DateTime.Now;
            QuestionService questionService = new QuestionService(new EnglishBattleEntities());
            questionService.Update(question);

            TimeSpan timeSpan = (TimeSpan)(question.dateReponse - question.dateEnvoie);
            if (timeSpan.TotalSeconds < 60)
            {
                //Dans les temps
                if((question.reponseParticipePasse == model.participe.ToLower()) && (question.reponsePreterit == model.preterit.ToLower()))
                {
                    //On incrémente le score du joueur
                    partie.score++;
                    PartieService partieService = new PartieService(new EnglishBattleEntities());
                    partieService.Update(partie);
                    Session["partie"] = partie;
                    return RedirectToAction("Index", "Game");
                }
            }
            Session["partie"] = partie;
            Session["question"] = question;
            Session["resultat"] = "Perdu...";
            return RedirectToAction("FinalPage", "Game");
        }

        public ActionResult FinalPage()
        {
            FinalViewModel finalViewModel = new FinalViewModel();
            Partie partie = (Partie)Session["partie"];
            Question question = (Question)Session["question"];
            Joueur joueur = (Joueur)Session["Joueur"];

            finalViewModel.partie = partie;
            finalViewModel.question = question;
            finalViewModel.joueur = joueur;
            finalViewModel.message = (string)Session["resultat"];

            //Calcul du HallOfFame
            PartieService partieService = new PartieService(new EnglishBattleEntities());
            List<Partie> partiesTrie;
            JoueurService joueurService;

            List<Partie> parties = partieService.GetList();
            partiesTrie = parties.OrderByDescending(p => p.score).Take(5).ToList();

            List<HallOfFame> hallOfFames = new List<HallOfFame>();
            foreach (Partie partie1 in partiesTrie)
            {
                joueurService = new JoueurService(new EnglishBattleEntities());
                Joueur joueur1 = joueurService.GetItem(partie1.idJoueur);
                hallOfFames.Add(new HallOfFame(joueur1.nom, joueur1.prenom, partie1.score));
            }
            finalViewModel.hallOfFames = hallOfFames;
            Session["HallOfFame"] = hallOfFames;

            return View(finalViewModel);
        }

    }
}