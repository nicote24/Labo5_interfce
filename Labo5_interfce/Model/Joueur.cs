using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo5_interfce.Model
{
    class Joueur : INotifyPropertyChanged
    {
        private int id;
        private string nom, prenom, courriel, password;
        private DateTime date;

        public Joueur()
        {
            id = 0;
            nom = "";
            prenom = "";
            courriel = "";
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; OnpropertyChanged("Nom"); }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; OnpropertyChanged("Prenom"); }
        }

        public string Courriel
        {
            get { return courriel; }
            set { courriel = value; OnpropertyChanged("Courriel"); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnpropertyChanged("Password"); }
        }

        public DateTime Date
        {
            get { return date.Date; }
            set { date = value; OnpropertyChanged("Date"); }
        }

        public int Id
        {
            get { return id; }
            set { id = value; OnpropertyChanged("Id"); }
        }


        #region 
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnpropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
