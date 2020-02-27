using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishBattleModel.Data.Services
{
    public class HighScoresService
    {
        private EnglishBattleEntities context;
        public HighScoresService(EnglishBattleEntities context)
        {
            this.context = context;
        }


    }
}
