using ShortestPath.Optimization.Entities;
using ShortestPath.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ShortestPath.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        const int _gridSize = 6;

        public MainViewModel()
        {
            this.ExecuteSearchCommand = new Command(this.ExecuteSearch);
            this.CloseCommand = new Command(this.ApplicationClose);
        }

        #region DisplayGrid

        private DisplayGrid _displayGrid = null;
        public DisplayGrid DisplayGrid
        {
            get { return _displayGrid; }
            set
            {
                _displayGrid = value;
                this.NotifyPropertyChanged("DisplayGrid");
            }
        }

        #endregion DisplayGrid

        #region CloseCommand
        private ICommand _closeCommand = null;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; }
        }
        #endregion CloseCommand

        #region ExecuteSearchCommand

        private ICommand _executeSearchCommand = null;
        public ICommand ExecuteSearchCommand
        {
            get { return _executeSearchCommand; }
            set { _executeSearchCommand = value; }
        }

        #endregion ExecuteCommand

        #region Engine

        private Optimization.Interfaces.IPathProvider _engine = null;
        public Optimization.Interfaces.IPathProvider Engine
        {
            get
            {
                if (_engine == null)
                    _engine = new ShortestPath.Optimization.Naive.Engine();
                return _engine;
            }
            set
            {
                _engine = value;
                this.NotifyPropertyChanged("Engine");
            }
        }

        #endregion Engine

        #region SearchOptionIndex

        private int _searchOptionIndex = 0;
        public int SearchOptionIndex
        {
            get { return _searchOptionIndex; }
            set
            {
                _searchOptionIndex = value;
                this.NotifyPropertyChanged("SearchOptionIndex");
            }
        }

        #endregion SearchOptionIndex

        #region PathLength

        private int _pathLength = 0;
        public int PathLength
        {
            get { return _pathLength; }
            set
            {
                _pathLength = value;
                this.NotifyPropertyChanged("PathLength");
            }
        }

        #endregion PathLength

        #region SearchResult

        private List<string> _searchResults = new List<string>();
        public List<string> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                this.NotifyPropertyChanged("SearchResults");
            }
        }

        #endregion SearchResult

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Implementation

        private Grid ConstructGrid()
        {
            var roadblocks = new List<GridLocation>();
            roadblocks.Add(new GridLocation(1, 1, true));
            roadblocks.Add(new GridLocation(1, 2, true));
            roadblocks.Add(new GridLocation(1, 3, true));
            roadblocks.Add(new GridLocation(2, 0, true));
            roadblocks.Add(new GridLocation(2, 1, true));

            return new Grid(_gridSize, _gridSize, roadblocks);
        }

        #region ExecuteSearch

        void ExecuteSearch(object arg)
        {
            var startPoint = new GridLocation(0, 2);
            var endPoint = new GridLocation(3, 1);

            // Construct the appropriate path engine
            if (this.SearchOptionIndex == 0)
                this.Engine = new ShortestPath.Optimization.Naive.Engine();
            else
                this.Engine = new ShortestPath.Optimization.DP.Engine();

            var grid = this.ConstructGrid();
            var path = this.Engine.FindPath(grid, startPoint, endPoint);

            this.DisplayGrid = new DisplayGrid(grid, startPoint, endPoint);
            this.PathLength = path.Length;
            this.SearchResults = path.ToList();
        }

        #endregion ExecuteSearch

        #region ApplicationClose

        void ApplicationClose(object arg)
        {
            Application.Current.MainWindow.Close();
        }

        #endregion ApplicationClose
    }
}
