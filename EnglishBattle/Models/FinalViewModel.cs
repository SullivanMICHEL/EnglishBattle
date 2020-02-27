using EnglishBattleModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishBattle.Models
{
    public class FinalViewModel
    {
        public string message { get; set; }
        public Joueur joueur{ get; set; }
        public Question question { get; set; }
        public Partie partie { get; set; }
        public List<HallOfFame> hallOfFames { get; set; }
    }
    public class HallOfFame
    {
        public string Nom;
        public string Prenom;
        public int Score;
    }
}