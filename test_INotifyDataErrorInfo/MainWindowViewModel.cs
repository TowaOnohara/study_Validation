using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_INotifyDataErrorInfo
{
    public class MainWindowViewModel : BaseViewModel
    {
        private List<Person> _People;
        public List<Person> People
        {
            get { return _People; }
            set
            {
                if (_People == value)
                    return;
                _People = value;
                RaisePropertyChanged(nameof(People));

            }
        }

        public MainWindowViewModel()
        {
            People = new List<Person>
            {
                new Person(){ Name = "Taro", Age = 10 },
                new Person(){ Name = "Jiro", Age =  9 },
            };
        }
    }

    public class Person : BaseViewModel
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private int _Age;
        public int Age
        {
            get { return _Age; }
            set
            {
                if (_Age == value)
                    return;
                _Age = value;
                RaisePropertyChanged(nameof(Age));
            }
        }
    }
}
