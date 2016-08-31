using ShortestPath.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ShortestPath.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

        const int _gridSize = 6;

        #region DisplayGrid
        private Optimization.Entities.Grid _displayGrid = null;
        public Optimization.Entities.Grid DisplayGrid
        {
            get { return _displayGrid; }
            set
            {
                _displayGrid = value;
                this.NotifyPropertyChanged("DisplayGrid");
            }
        }
        #endregion DisplayGrid

        public MainViewModel()
        {
            this.ExecuteSearchCommand = new Command(this.ExecuteSearch);
            this.CloseCommand = new Command(this.ApplicationClose);

            this.LoadBoard();
        }

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
                {
                    _engine = new ShortestPath.Optimization.Naive.Engine();
                    _engine.PropertyChanged += Engine_PropertyChanged;
                }
                return _engine;
            }
            set
            {
                if (_engine != null)
                    _engine.PropertyChanged -= Engine_PropertyChanged;
                _engine = value;
                _engine.PropertyChanged += Engine_PropertyChanged;

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
                this.DisplayGrid = this.ConstructGrid();
                this.SearchResults = new List<string>();

                // Construct the appropriate path engine
                if (value == 0)
                {
                    this.Engine = new ShortestPath.Optimization.Naive.Engine();
                }
                else
                {
                    this.Engine = new ShortestPath.Optimization.DP.Engine();
                }
                this.NotifyPropertyChanged("SearchOptionIndex");
            }
        }

        void Engine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Path")
            {
                this.PathLength = this.Engine.Path.Length;
                this.SearchResults = this.Engine.Path.ToList();
            }
        }
        #endregion SearchOptionIndex

        #region ExecuteSearch
        void ExecuteSearch(object arg)
        {
            this.DisplayGrid = this.ConstructGrid();

            this.Engine.FindPath(DisplayGrid);
        }
        #endregion ExecuteSearch
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

        private void LoadBoard()
        {
            DisplayGrid = this.ConstructGrid();
        }

        private Optimization.Entities.Grid ConstructGrid()
        {
            var startPoint = new Optimization.Entities.GridLocation(0, 2);
            var endPoint = new Optimization.Entities.GridLocation(3, 1);
            startPoint.IsStartPoint = true;
            endPoint.IsEndPoint = true;

            var roadblocks = new List<Optimization.Entities.GridLocation>();
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 1, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 2, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 3, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 0, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 1, true));

            return new Optimization.Entities.Grid(_gridSize, _gridSize, roadblocks, startPoint, endPoint);
        }

        #region ApplicationClose
        void ApplicationClose(object arg)
        {
            Application.Current.MainWindow.Close();
        }
        #endregion ApplicationClose
    }
}
