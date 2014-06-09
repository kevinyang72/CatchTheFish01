/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/     

*/
using System.Windows;
using Jarloo.CardStock.ViewModels;

namespace Jarloo.CardStock.Views
{
    public partial class CardDeckView : Window
    {
        public CardDeckView()
        {
            InitializeComponent();

            DataContext = new CardDeckViewModel();
        }
    }
}