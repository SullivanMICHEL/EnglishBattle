using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattleModel.Data.Services
{
    public class QuestionService
    {
        private EnglishBattleEntities context;
        public QuestionService(EnglishBattleEntities context)
        {
            this.context = context;
        }

        public Question GetItem(int id)
        {
            using (context)
            {
                return context.Question.Find(id);
            }
        }

        public Question GetItem(string reponseParticipe, string reponsePeterit)
        {
            using (context)
            {
                IQueryable<Question> quizzs = from question in context.Question
                                             where question.reponseParticipePasse == reponseParticipe && question.reponsePreterit == reponsePeterit
                                             select question;
                return quizzs.FirstOrDefault();
            }
        }

        public List<Question> GetList()
        {
            using (context)
            {
                return context.Question.ToList();
            }
        }

        public void Insert(Question question)
        {
            using (context)
            {
                context.Question.Add(question);
                context.SaveChanges();
            }
        }

        public void Update(Question question)
        {
            using (context)
            {
                context.Entry(question).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Question question)
        {
            using (context)
            {
                context.Entry(question).State =
                System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
