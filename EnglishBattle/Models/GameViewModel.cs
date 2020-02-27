using EnglishBattleModel.Data;
using EnglishBattleModel.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishBattle.Models
{
    public class GameViewModel
    {
        public Partie newPartie = new Partie();
        public Question question = new Question();
        public Verbe verbe = new Verbe();
        PartieService partieService;
        QuestionService questionService;
        VerbeService verbeService;

        [Display(Name = "Prétérit :")]
        [Required]
        public string preterit { get; set; }
        [Display(Name = "Participe passé :")]
        [Required]
        public string participe { get; set; }
        public Joueur joueur { get; set; }
        public int numQuestion { get; set; }

        public Partie LancerPartie(Joueur joueur)
        {
            //On regarde si le joueur a déja fait une partie
            //Si oui on récupère la partie concerné et on lance le jeu 
            partieService = new PartieService(new EnglishBattleEntities());
            foreach (Partie partie in partieService.GetList())
            {
                if (partie.idJoueur == joueur.id)
                {
                    //le joueur a déjà joué
                    partieService = new PartieService(new EnglishBattleEntities());
                    newPartie = partieService.GetItem(partie.id);
                    if (newPartie != null)
                    {
                        return newPartie;
                    }
                }
            }
            //Création de la partie
            newPartie.Joueur = joueur;

            partieService = new PartieService(new EnglishBattleEntities());
            partieService.Insert(newPartie);
            return newPartie;
        }

        public Question CreerQuestion(Partie partie)
        {
            var rand = new Random();
            verbeService = new VerbeService(new EnglishBattleEntities());
            verbe = verbeService.GetItem((int)rand.Next(1, 161));

            question.idPartie = partie.id;
            question.idVerbe = verbe.id;
            question.Verbe = verbe;
            question.reponseParticipePasse = verbe.participePasse;
            question.reponsePreterit = verbe.preterit;
            question.dateEnvoie = DateTime.Now;

            questionService = new QuestionService(new EnglishBattleEntities());
            questionService.Insert(question);
            return question;
        }
    }
}