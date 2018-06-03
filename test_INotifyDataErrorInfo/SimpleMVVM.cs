using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace test_INotifyDataErrorInfo
{
    /// <summary>
    /// ViewModel基本クラス
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get { return _errors.Values.Any(x => x != null); }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void ValidateProperty(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            // check parameter whether collect or not
            if (string.IsNullOrWhiteSpace(propertyName)) return null;
            if (!_errors.ContainsKey(propertyName)) return null;

            // get value
            this._errors.TryGetValue(propertyName, out List<string> errorInfo);
            return errorInfo;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion    
    }

    /// <summary>
    /// コマンド処理定義
    /// </summary>
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
