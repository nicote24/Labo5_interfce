using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DemoInterface3.Command;
using DemoInterface3.ViewModel;
using Labo5_interfce.Model;
using Labo5_interfce.View;

namespace Labo5_interfce.ViewModel
{
    class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private Joueur joueur;
        private Jeu jeu;
        private Window fenetre;
        private string exception, exception2;
        private Style style;
       private string chemin1;
        private ObservableCollection<Button> jeux;

        public MainViewModel() : base()
        {
            jeux = new ObservableCollection<Button>();
            jeu = new Jeu();
            joueur = new Joueur();
            style = new Style(typeof(Button));
            style.Setters.Add(new Setter(Control.FontSizeProperty, 20.0));
            ConnexionCommand = new BaseCommand(Connexion, obj => true);
            InscriptionCommand = new BaseCommand(Inscription, obj => true);
            AjouterCommand = new BaseCommand(Ajouter, obj => true);
            OpenGameCommand = new BaseCommand(OpenGame, obj => true);
            exception = "";
            exception2 = "";
        }
        public string Exception { get => exception; set { exception = value; } }

        public string Exception2 { get => exception2; set { exception2 = value; } }
        public ICommand ConnexionCommand { get; set; }
        public ICommand InscriptionCommand { get; set; }
        public ICommand AjouterCommand { get; set; }

        public ICommand OpenGameCommand { get; set; }

        public ObservableCollection<Button> Jeux { get=>jeux; set{ jeux = value; } }

        public ICommand OpenCommands { get; set; }
        public Joueur Joueur { get => joueur; set { joueur = value; OnPropertyChanged("Joueur"); } }

        public Jeu Jeu { get => jeu; set { jeu = value; OnPropertyChanged("Jeu"); } }

        public bool CanUpdate
        {
            get
            {
                if (joueur==null)
                    return false;
                else return true;
            }
        }
        public ICommand OpenCommand
        {
            get;
            private set;
        }

        public void OpenWindow()
        {
            fenetre = new Page2();
            Shearch();
            fenetre.Show();
            fenetre.DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Connexion(object obj)
        {
            string testPassword="";
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT courriel, password,id FROM tblJoueur WHERE courriel=@param1", con);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = Joueur.Courriel;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblJoueur");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    testPassword = row[1].ToString();
                    Joueur.Courriel = row[0].ToString();
                    Joueur.Id = Convert.ToInt32(row[2]);
                }

                if (testPassword==Joueur.Password)
                {
                    OpenWindow();
                    Exception = "";
                }
                else
                {
                    Exception = "mot de passe invalide";
                }
            }
            catch (Exception e)
            {
                Exception = e.Message;
            }

        }

        private void Inscription(object obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tblJoueur (nom,prenom,courriel,password,dataNaissance) VALUES (@nom,@prenom,@courriel,@password,@dateNaissance)", con);
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 30).Value = Joueur.Nom;
                cmd.Parameters.Add("@prenom", SqlDbType.VarChar, 30).Value = Joueur.Prenom;
                cmd.Parameters.Add("@courriel", SqlDbType.VarChar, 30).Value = Joueur.Courriel;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 30).Value = Joueur.Password;
                cmd.Parameters.Add("@dateNaissance", SqlDbType.Date).Value = Joueur.Date.ToShortDateString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                OpenWindow();
            }
            catch (Exception e)
            {
                Exception = e.Message;
            }

        }

        private void Ajouter(object obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tblJeu (nom,chemin,id) VALUES (@nom,@chemin,@id)", con);
                cmd.Parameters.Add("@nom", SqlDbType.VarChar, 30).Value = Jeu.Nom;
                cmd.Parameters.Add("@chemin", SqlDbType.VarChar, 30).Value = Jeu.Chemin;
                cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = Joueur.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                exception2 = "ajout a la base de donnée réussie";
            }
            catch (Exception e)
            {
                exception2 = "ajout a la base donné échoué";
            }

        }

        private void Shearch()
        {
            Jeux.Clear();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblJeu WHERE id=@Joueur", con);
                cmd.Parameters.Add("@Joueur", SqlDbType.SmallInt).Value = Joueur.Id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "tblJoueur");

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    chemin1 = row[1].ToString();
                    Jeux.Add(new Button(){ Content= row[0].ToString(),Style=style, Command=OpenGameCommand });
                }
            }
            catch(Exception e)
            {
                exception2 = e.Message;
            }
        }

        private void OpenGame(object obj)
        {
            Process processus = Process.Start(chemin1);
        }
    }
}
