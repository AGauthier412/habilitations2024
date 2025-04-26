using habilitations2024.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.dal
{
    public class ProfilAccess
    {
        // Instance unique de l'accès aux données
        private readonly Access access = null;

        // Constructeur pour créer l'accès aux données
        public ProfilAccess()
        {
            access = Access.getInstance();
        }

        // Récupère et retourne les profils
        public List<Profil> GetLesProfils()
        {
            List<Profil> lesProfils = new List<Profil>();
            if (access.Manager != null)
            {
                string req = "select * from profil order by nom;";
                try
                {
                    List<Object[]> records = access.Manager.ReqSelect(req);
                    if (records != null)
                    {
                        foreach (Object[] record in records)
                        {
                            Profil profil = new Profil((int)record[0], (string)record[1]);
                            lesProfils.Add(profil);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
            }

            return lesProfils;
        }
    }
}
