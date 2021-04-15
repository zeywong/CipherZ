using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CipherZ_DPHelpers
{
    public class RelayCommand : ICommand
    {
        Action _ExecuteMethod;
        Func<bool> _CanExecuteMethod;

        public event EventHandler CanExecuteChanged = delegate { };

        public RelayCommand(Action executeMethod)
        {
            _ExecuteMethod = executeMethod;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
        {
            if (_CanExecuteMethod != null) { return _CanExecuteMethod(); }
            return _ExecuteMethod != null ? true : false;
        }

        public void Execute(object parameter)
        {
            _ExecuteMethod?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        Action<T> _ExecuteMethod;
        Func<T, bool> _CanExecuteMethod;

        public event EventHandler CanExecuteChanged = delegate { };

        public RelayCommand(Action<T> executeMethod)
        {
            _ExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _ExecuteMethod = executeMethod;
            _CanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
        {
            if (_CanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _CanExecuteMethod(tparm);
            }

            if (_ExecuteMethod != null) { return true; }

            return false;
        }

        public void Execute(object parameter) => _ExecuteMethod?.Invoke((T)parameter);
    }
}
