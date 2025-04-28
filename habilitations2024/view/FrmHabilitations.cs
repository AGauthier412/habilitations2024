using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using habilitations2024.controller;
using habilitations2024.model;
using MySql.Data.MySqlClient;

namespace habilitations2024.view
{
    public partial class FrmHabilitations : Form
    {
        // Booléen pour savoir si une modification est demandée
        private Boolean enCoursDeModifDeveloppeur = false;
        
        // Objet pour gérer la liste des développeurs
        private BindingSource bdgDeveloppeurs = new BindingSource();
        
        // Objet pour gérer la liste des profils
        private BindingSource bdgProfils = new BindingSource();
        
        // Controleur de la fenêtre
        private FrmHabilitationsController controller;
        
        public FrmHabilitations()
        {
            InitializeComponent();
            Init();
        }


        // Création du controleur et remplissage des listes
        private void Init()
        {
            controller = new FrmHabilitationsController();
            RemplirListeDeveloppeurs();
            RemplirListeProfils();
            EnCourseModifDeveloppeur(false);
            EnCoursModifPwd(false);
        }

        // Affiche les développeurs
        private void RemplirListeDeveloppeurs()
        {
            List<Developpeur> lesDeveloppeurs = controller.GetLesDeveloppeurs();
            bdgDeveloppeurs.DataSource = lesDeveloppeurs;
            dgvDeveloppeurs.DataSource = bdgDeveloppeurs;
            dgvDeveloppeurs.Columns["iddeveloppeur"].Visible = false;
            dgvDeveloppeurs.Columns["pwd"].Visible = false;
            dgvDeveloppeurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // Affiche les profils
        private void RemplirListeProfils()
        {
            List<Profil> lesProfils = controller.GetLesProfils();
            bdgProfils.DataSource = lesProfils;
            cboProfil.DataSource = bdgProfils;
        }

        // Demande de modification d'un développeur
        private void btnDemandeModifDev_Click(object sender, EventArgs e)
        {
            if (dgvDeveloppeurs.SelectedRows.Count > 0)
            {
                EnCourseModifDeveloppeur(true);
                Developpeur developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                txtNom.Text = developpeur.Nom;
                txtPrenom.Text = developpeur.Prenom;
                txtTel.Text = developpeur.Tel;
                txtMail.Text = developpeur.Mail;
                cboProfil.SelectedIndex = cboProfil.FindStringExact(developpeur.Profil.Nom);
            }
            else
            {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }

        // Demande de suppression d'un développeur
        private void btnDemandeSupprDev_Click(object sender, EventArgs e)
        {
            if (dgvDeveloppeurs.SelectedRows.Count > 0)
            {
                Developpeur developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                if (MessageBox.Show("Voulez-vous vraiment supprimer " + developpeur.Nom + " " + developpeur.Prenom + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    controller.DelDeveloppeur(developpeur);
                    RemplirListeDeveloppeurs();
                }
            }
            else
            {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }

        // Demande de changement du pwd
        private void btnDemandeChangePwd_Click(object sender, EventArgs e)
        {
            if (dgvDeveloppeurs.SelectedRows.Count > 0)
            {
                EnCoursModifPwd(true);
            }
            else
            {
                MessageBox.Show("Une ligne doit être sélectionnée.", "Information");
            }
        }

        // Demande d'enregistrement de l'ajout ou de la modification d'un développeur
        private void btnEnregDev_Click(object sender, EventArgs e)
        {
            if (!txtNom.Text.Equals("") && !txtPrenom.Text.Equals("") && !txtTel.Text.Equals("") && !txtMail.Text.Equals("") && cboProfil.SelectedIndex != -1)
            {
                Profil profil = (Profil)bdgProfils.List[bdgProfils.Position];
                if (enCoursDeModifDeveloppeur)
                {
                    Developpeur developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                    developpeur.Nom = txtNom.Text;
                    developpeur.Prenom = txtPrenom.Text;
                    developpeur.Tel = txtTel.Text;
                    developpeur.Mail = txtMail.Text;
                    developpeur.Profil = profil;
                    controller.UpdateDeveloppeur(developpeur);
                }
                else
                {
                    Developpeur developpeur = new Developpeur(0, txtNom.Text, txtPrenom.Text, txtTel.Text, txtMail.Text, profil);
                    controller.AddDeveloppeur(developpeur);
                }
                RemplirListeDeveloppeurs();
                EnCourseModifDeveloppeur(false);
            }
            else
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Information");
            }
        }

        // Annule la demande d'ajout ou de modification d'un développeur
        // Vide les zones de saisie du développeur
        private void btnAnnulDev_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCourseModifDeveloppeur(false);
            }
        }

        // Demande d'enregistrement du nouveau pwd
        private void btnEnregPwd_Click(object sender, EventArgs e)
        {
            if (!txtPwd1.Text.Equals("") && !txtPwd2.Text.Equals("") && txtPwd1.Text.Equals(txtPwd2.Text))
            {
                Developpeur developpeur = (Developpeur)bdgDeveloppeurs.List[bdgDeveloppeurs.Position];
                developpeur.Pwd = txtPwd1.Text;
                controller.UpdatePwd(developpeur);
                EnCoursModifPwd(false);
            }
            else
            {
                MessageBox.Show("Les 2 zones doivent être remplies et de contenu identique", "Information");
            }
        }

        // Annulation de demande d'enregistrement d'un nouveau pwd
        private void btnAnnulPwd_Click(object sender, EventArgs e)
        {
            EnCoursModifPwd(false);
        }

        // Modification d'affichage suivant si on est en cours de modif ou d'ajout d'un developpeur
        private void EnCourseModifDeveloppeur(Boolean modif)
        {
            enCoursDeModifDeveloppeur = modif;
            grbLesDeveloppeurs.Enabled = !modif;
            if (modif)
            {
                grbDeveloppeur.Text = "modifier un développeur";
            }
            else
            {
                grbDeveloppeur.Text = "ajouter un développeur";
                txtNom.Text = "";
                txtPrenom.Text = "";
                txtTel.Text = "";
                txtMail.Text = "";
            }
        }

        // Modification d'affichage suivant si on est ou non en cours de modif du pwd
        private void EnCoursModifPwd(Boolean modif)
        {
            grbPwd.Enabled = modif;
            grbLesDeveloppeurs.Enabled = !modif;
            grbDeveloppeur.Enabled = !modif;
            txtPwd1.Text = "";
            txtPwd2.Text = "";
        }
    }
}
