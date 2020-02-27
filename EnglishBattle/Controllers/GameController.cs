using EnglishBattle.Models;
using EnglishBattleModel.Data;
using EnglishBattleModel.Data.Services;
using System;
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

            Session["partie"] = partie;
            Session["question"] = question;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(GameViewModel model)
        {
            Partie partie = (Partie)Session["partie"];
            Question question= (Question)Session["question"];
            QuestionService questionService;
            PartieService partieService;
            TimeSpan timeSpan;

            question.dateReponse = DateTime.Now;
            questionService = new QuestionService(new EnglishBattleEntities());
            questionService.Update(question);

            timeSpan = (TimeSpan)(question.dateReponse - question.dateEnvoie);
            if (timeSpan.TotalSeconds < 60)
            {
                //Dans les temps
                if((question.reponseParticipePasse == model.participe.ToLower()) && (question.reponsePreterit == model.preterit.ToLower()))
                {
                    //On incrémente le score du joueur
                    partie.score++;
                    partieService = new PartieService(new EnglishBattleEntities());
                    partieService.Update(partie);
                    
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
            return View(finalViewModel);
        }

    }
}