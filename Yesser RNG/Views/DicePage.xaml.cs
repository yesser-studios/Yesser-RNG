using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yesser_RNG.Views
{
    public sealed partial class DicePage : Page, INotifyPropertyChanged
    {
        Random rand = new Random();

        public DicePage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void RollB_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<int> rolled = new List<int>();

            for (int i = 0; i < NumberDiceBox.Value; i++)
            {
                rolled.Add(rand.Next((int)NumberSidesBox.Value + 1));
            }

            string text = "";
            bool didDoFirst = false;
            foreach (int i in rolled)
            {
                if (!didDoFirst && rolled.IndexOf(i) == 0)
                {
                    didDoFirst = true;
                    text += $"{i}";
                }
                else
                    text += $"; {i}";
            }
            RolledBox.Text = text;
            MaxBox.Text = rolled.Max().ToString();
            AverageBox.Text = rolled.Average().ToString();
            MinBox.Text = rolled.Min().ToString();

            AfterRollGrid.Visibility = Visibility.Visible;
        }

        private void OutBox_Clicked(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box == null) return;
            box.SelectAll();
        }
    }
}
