﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace habilitations2024.model
{
    public class Profil
    {
        public int Idprofil { get; set; }
        public string Nom { get; set; }

        public Profil(int idprofil, string nom)
        {
            this.Idprofil = idprofil;
            this.Nom = nom;
        }

        public override string ToString()
        {
            return this.Nom;
        }
    }
}
