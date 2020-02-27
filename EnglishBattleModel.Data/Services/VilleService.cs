using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattleModel.Data.Services
{
    public class VilleService
    {
        private EnglishBattleEntities context;
        public VilleService(EnglishBattleEntities context)
        {
            this.context = context;
        }

        public Ville GetItem(int id)
        {
            using (context)
            {
                return context.Ville.Find(id);
            }
        }

        public Ville GetItem(string nom, string codePostal)
        {
            using (context)
            {
                IQueryable<Ville> villes = from ville in context.Ville
                                             where ville.nom == nom && ville.codePostal == codePostal
                                             select ville;
                return villes.FirstOrDefault();
            }
        }

        public List<Ville> GetList()
        {
            using (context)
            {
                return context.Ville.ToList();
            }
        }

        public void Insert(Ville ville)
        {
            using (context)
            {
                context.Ville.Add(ville);
                context.SaveChanges();
            }
        }

        public void Update(Ville ville)
        {
            using (context)
            {
                context.Entry(ville).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Ville ville)
        {
            using (context)
            {
                context.Ville.Attach(ville);
                List<Joueur> joueurs = ville.Joueur.ToList();
                foreach (Joueur joueur in joueurs)
                {
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
                }
                context.Entry(ville).State =
                System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}
