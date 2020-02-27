using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattleModel.Data.Services
{
    public class VerbeService
    {
        private EnglishBattleEntities context;
        public VerbeService(EnglishBattleEntities context)
        {
            this.context = context;
        }

        public Verbe GetItem(int id)
        {
            using (context)
            {
                return context.Verbe.Find(id);
            }
        }

        public Verbe GetItem(string baseVerbale, string participePasse)
        {
            using (context)
            {
                IQueryable<Verbe> verbes = from verbe in context.Verbe
                                             where verbe.baseVerbale == baseVerbale && verbe.participePasse == participePasse
                                             select verbe;
                return verbes.FirstOrDefault();
            }
        }

        public List<Verbe> GetList()
        {
            using (context)
            {
                return context.Verbe.ToList();
            }
        }

        public void Insert(Verbe verbe)
        {
            using (context)
            {
                context.Verbe.Add(verbe);
                context.SaveChanges();
            }
        }

        public void Update(Verbe verbe)
        {
            using (context)
            {
                context.Entry(verbe).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Verbe verbe)
        {
            using (context)
            {
                context.Verbe.Attach(verbe);

                List<Question> quizzs = verbe.Question.ToList();
                foreach (Question quizz in quizzs)
                {
                    context.Entry(quizz).State =
                    System.Data.Entity.EntityState.Deleted;
                }

                context.Entry(verbe).State =
                System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

    }
}
