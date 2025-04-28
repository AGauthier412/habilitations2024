using habilitations2024.dal;
using habilitations2024.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.controller
{
    public class FrmAuthentificationController
    {
        private readonly DeveloppeurAccess developpeurAccess;

        public FrmAuthentificationController()
        {
            developpeurAccess = new DeveloppeurAccess();
        }

        public bool ControleAuthentification(Admin admin)
        {
            return developpeurAccess.ControleAuthentification(admin);
        }
    }
}
