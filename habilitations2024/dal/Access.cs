using habilitations2024.bddmanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.dal
{
    public class Access
    {
        // chaine de connexion à la bdd
        private static readonly string connectionString = "Server=localhost; Port=3308; User Id=habilitations; Password=motdepasseuser; Database=habilitations;";

        // instance unique de la classe
        private static Access instance = null;

        // objet d'accès aux données
        public BddManager Manager { get; }

        private Access()
        {
            try
            {
                Manager = BddManager.GetInstance(connectionString);
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }

        public static Access getInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }

            return instance;
        }
    }
}
