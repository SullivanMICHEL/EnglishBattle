﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnglishBattleModel.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EnglishBattleEntities : DbContext
    {
        public EnglishBattleEntities()
            : base("name=EnglishBattleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Joueur> Joueur { get; set; }
        public virtual DbSet<Partie> Partie { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Verbe> Verbe { get; set; }
        public virtual DbSet<Ville> Ville { get; set; }
    }
}
