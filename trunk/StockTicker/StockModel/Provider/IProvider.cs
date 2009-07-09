using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace StockTicker.Provider
{
    public interface IProvider
    {
        object Service { get; }

        void Login(string user, string password);
        void Logout();

        ObservableCollection<object> GetPortfolios();
        // TODO: we could also pass the object type and parse the name from there
        // to delete, create, or rename.
        void CreatePortfolio(string name);
        void RenamePortfolio(string name, string newName);
        void DeletePortfolio(string name);

        ObservableCollection<object> GetPositions(string portfolio);
        ObservableCollection<object> GetPosition(string security);


    }
}
