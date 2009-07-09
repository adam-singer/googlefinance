using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace StockTicker.Commands
{
    public class ActionCommands
    {
        public static RoutedUICommand LoginCommand { private set; get; }
        public static RoutedUICommand LogoutCommand { private set; get; }
        public static RoutedUICommand ExitCommand { private set; get; }

        public static RoutedUICommand CreatePortfolio { private set; get; }
        public static RoutedUICommand RenamePortfolio { private set; get; }
        public static RoutedUICommand DeletePortfolio { private set; get; }

        /// <summary>
        /// Static constructor.
        /// Creates several Routed UI commands with and without shortcut keys.
        /// </summary>
        static ActionCommands()
        {
            LoginCommand = MakeRoutedUICommand("Login", Key.I, "Ctrl+I");
            LogoutCommand = MakeRoutedUICommand("Logout", Key.O, "Crtl+O");
            ExitCommand = MakeRoutedUICommand("Exit");

            CreatePortfolio = MakeRoutedUICommand("CreatePortfolio", Key.N, "Crtl+N");
            RenamePortfolio = MakeRoutedUICommand("RenamePortfolio", Key.R, "Crtl+R");
            DeletePortfolio = MakeRoutedUICommand("DeletePortfolio", Key.D, "Crtl+D");

        }

        /// <summary>
        /// Creates a routed command instance without shortcut key.
        /// </summary>
        /// <param name="name">Given name.</param>
        /// <returns>The routed UI command.</returns>
        private static RoutedUICommand MakeRoutedUICommand(string name)
        {
            return new RoutedUICommand(name, name, typeof(ActionCommands));
        }

        /// <summary>
        /// Creates a routed command instance with a shortcut key.
        /// </summary>
        /// <param name="name">Given name.</param>
        /// <param name="key">Shortcut key.</param>
        /// <param name="displayString">The Routed UI command.</param>
        /// <returns></returns>
        private static RoutedUICommand MakeRoutedUICommand(string name, Key key, string displayString)
        {
            InputGestureCollection gestures = new InputGestureCollection();
            gestures.Add(new KeyGesture(key, ModifierKeys.Control, displayString));

            return new RoutedUICommand(name, name, typeof(ActionCommands), gestures);
        }
    }
}
