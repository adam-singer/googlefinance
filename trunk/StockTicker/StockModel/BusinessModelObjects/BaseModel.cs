using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;


namespace StockModel.BusinessModelObjects
{
    /// <summary>
    /// Abstract base class for business object models.
    /// </summary>
    /// <remarks>
    /// Methods ensure that they are called on the UI thread only.
    /// </remarks>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        // Dispatcher associates with model;
        protected Dispatcher _dispatcher;
        private PropertyChangedEventHandler _propertyChangedEvent;

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseModel()
        {
            // Save off dispatcher
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #region INotifyPropertyChanged Members
        /// <summary>
        /// PropertyChanged event for INotifyPropertyChanged implementation. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ConfirmOnUIThread();
                _propertyChangedEvent += value;
            }
            remove
            {
                ConfirmOnUIThread();
                _propertyChangedEvent -= value;
            }
        }
        #endregion

        /// <summary>
        /// Utility function for use by subclasses to notify that a property value has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void Notify(string propertyName)
        {
            ConfirmOnUIThread();
            ConfirmPropertyName(propertyName);

            if (_propertyChangedEvent != null)
            {
                _propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        /// <summary>
        /// Debugging facility that ensures methods are called on the UI thread.
        /// </summary>
        [Conditional("Debug")]
        protected void ConfirmOnUIThread()
        {
            Debug.Assert(Dispatcher.CurrentDispatcher == _dispatcher, "Call must be made on UI thread.");
        }

        /// <summary>
        /// Debugging facility that ensures the property does exist on the class.
        /// </summary>
        /// <param name="propertyName"></param>
        [Conditional("Debug")]
        private void ConfirmPropertyName(string propertyName)
        {
            Debug.Assert(GetType().GetProperty(propertyName) != null, "Property " + propertyName + " is not a value name.");
        }
    }
}
