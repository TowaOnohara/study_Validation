using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace test_INotifyDataErrorInfo
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion    
    }

    public class DelegeteCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        #region ICommand

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._execute?.Invoke(parameter);
        }
        #endregion    


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public DelegeteCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentException("execute");
            this._canExecute = canExecute ?? throw new ArgumentException("canExecute");
        }
        public DelegeteCommand(Action<object> execute)
        {
            this._execute = execute ?? throw new ArgumentException("execute");
        }
    }
}
