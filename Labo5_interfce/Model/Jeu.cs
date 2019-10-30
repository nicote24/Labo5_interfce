using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo5_interfce.Model
{
    class Jeu : INotifyPropertyChanged
    {
        string nom;
        string chemin;
        private int id;

        public Jeu()
        {

        }
        public string Nom { get => nom; set { nom = value; OnPropertyChanged("Nom"); } }
        public string Chemin { get => chemin; set { chemin = value; OnPropertyChanged("Chemin"); } }

        public int Id{ get => id; set { id = value; OnPropertyChanged("id"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
