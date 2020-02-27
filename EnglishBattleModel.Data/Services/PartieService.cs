using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattleModel.Data.Services
{
    public class PartieService
    {
        private EnglishBattleEntities context;
        public PartieService(EnglishBattleEntities context)
        {
            this.context = context;
        }

        public Partie GetItem(int id)
        {
            using (context)
            {
                return context.Partie.Find(id);
            }
        }

        public Partie GetItem(int score, int idJoueur)
        {
            using (context)
            {
                IQueryable<Partie> parties = from partie in context.Partie
                                             where partie.score == score && partie.idJoueur == idJoueur
                                             select partie;
                return parties.FirstOrDefault();
            }
        }

        public List<Partie> GetList()
        {
            using (context)
            {
                return context.Partie.ToList();
            }
        }

        public void Insert(Partie partie)
        {
            using (context)
            {
                context.Partie.Add(partie);
                context.SaveChanges();
            }
        }

        public void Update(Partie partie)
        {
            using (context)
            {
                context.Entry(partie).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Partie partie)
        {
            using (context)
            {
                context.Partie.Attach(partie);
                List<Question> quizzs = partie.Question.ToList();
                foreach (Question quizz in quizzs)
                {
                    context.Entry(quizz).State =
                    System.Data.Entity.EntityState.Deleted;
                }
                context.Entry(partie).State =
                System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
