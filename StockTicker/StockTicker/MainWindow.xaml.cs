using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockTicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Menu Command Hanlders

        private void LoginCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
           
        }

        private void LoginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void LogoutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void LogoutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

       
        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreatePortfolio_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void CreatePortfolio_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }


        private void RenamePortfolio_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void RenamePortfolio_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }


        private void DeletePortfolio_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void DeletePortfolio_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }




        #endregion 
    }
}
