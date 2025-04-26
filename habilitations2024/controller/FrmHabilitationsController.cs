using habilitations2024.dal;
using habilitations2024.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.controller
{
    public class FrmHabilitationsController
    {
        // objet d'accès aux opérations possibles sur Developpeur
        private readonly DeveloppeurAccess developpeurAccess;

        // objet d'accès aux opérations possible sur Profil
        private readonly ProfilAccess profilAccess;

        // Récupère les acces aux données
        public FrmHabilitationsController()
        {
            developpeurAccess = new DeveloppeurAccess();
            profilAccess = new ProfilAccess();
        }

        // Récupère et retourne la liste des développeurs
        public List<Developpeur> GetLesDeveloppeurs()
        {
            return developpeurAccess.GetLesDeveloppeurs();
        }

        // Récupère et retourne la liste des profils
        public List<Profil> GetLesProfils()
        {
            return profilAccess.GetLesProfils();
        }

        // Demande de suppression d'un développeur
        public void DelDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.DelDeveloppeur(developpeur);
        }

        // Demande d'ajout d'un développeur
        public void AddDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.AddDeveloppeur(developpeur);
        }

        // Demande de modification d'un développeur
        public void UpdateDeveloppeur(Developpeur developpeur)
        {
            developpeurAccess.UpdateDeveloppeur(developpeur);
        }

        // Demande de changement de pwd
        public void UpdatePwd(Developpeur developpeur)
        {
            developpeurAccess.UpdatePwd(developpeur);
        }
    }
}
