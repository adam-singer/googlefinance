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
    public abstract class BaseModel : INotifyPropertyChanged
    {
        // Dispatcher associates with model;
        protected Dispatcher _dispatcher;
    
    
        
    
    }
}
