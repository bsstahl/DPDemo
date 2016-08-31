using System;
using System.Windows.Input;

namespace ShortestPath.Utilities
{
  public class Command : ICommand
  {
    public Func<dynamic, bool> _canExecuteDelegate = null;
    public Action<dynamic> _executeDelegate = null;
    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public Command(Action<dynamic> execute)
    {
      _executeDelegate = execute;
      _canExecuteDelegate = (e) => true;
    }

    public Command(Action<dynamic> execute, Func<dynamic, bool> canExecute)
    {
      _executeDelegate = execute;
      _canExecuteDelegate = canExecute;
    }

    public bool CanExecute(dynamic args)
    {
      return _canExecuteDelegate(args);
    }

    public void Execute(dynamic args)
    {
      _executeDelegate(args);
    }
  }
}
