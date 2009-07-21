using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace StockViewModel
{
    /// <summary>
    /// Abstract class that encapsulates a routed ui command.
    /// </summary>
    public abstract class CommandModel 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandModel()
        {
            Command = new RoutedUICommand();
        }

        /// <summary>
        /// Gets the routed command.
        /// </summary>
        public RoutedUICommand Command { private set; get; }

        /// <summary>
        /// Abstract method to execute the command. Needs implementation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void OnExecute(object sender, ExecutedRoutedEventArgs e);

        /// <summary>
        /// Determines if a command is enabled. Override to provide custom behavior.
        /// Do not call the base version when overriding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
