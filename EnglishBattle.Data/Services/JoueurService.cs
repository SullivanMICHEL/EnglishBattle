using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishBattle.Data
{
    public class JoueurService
    {
        private EnglishBattleEntities context;
        public JoueurService(EnglishBattleEntities context)
        {
            this.context = context;
        }

        public Joueur GetItem(int id)
        {
            using (context)
            {
                return context.Joueur.Find(id);
            }
        }

        public Joueur GetItem(string email, string motDePasse)
        {
            using (context)
            {
                IQueryable<Joueur> joueurs = from joueur in context.Joueur
                                             where joueur.email == email && joueur.motDePasse == motDePasse
                                             select joueur;
                return joueurs.FirstOrDefault();
            }
        }

        public List<Joueur> GetList()
        {
            using (context)
            {
                return context.Joueur.ToList();
            }
        }

        public void Insert(Joueur joueur)
        {
            using (context)
            {
                context.Joueur.Add(joueur);
                context.SaveChanges();
            }
        }

        public void Update(Joueur joueur)
        {
            using (context)
            {
                context.Entry(joueur).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Joueur joueur)
        {
            using (context)
            {
                context.Joueur.Attach(joueur);
                List<Partie> parties = joueur.Partie.ToList();
                foreach (Partie partie in parties)
                {
                    List<Question> quizzs = partie.Question.ToList();
                    foreach (Question quizz in quizzs)
                    {
                        context.Entry(quizz).State =
                        System.Data.Entity.EntityState.Deleted;
                    }
                    context.Entry(partie).State =
                    System.Data.Entity.EntityState.Deleted;
                }
                context.Entry(joueur).State =
                System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}
