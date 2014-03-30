using System.ComponentModel;

namespace SmartCalculator.Model
{
    public class CalObjectModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //The formular for equation, system quation => it will be true; otherwise, false
        private bool isSpecial;

        public bool IsSpecial
        {
            get { return isSpecial; }
            set { isSpecial = value; }
        }

        //Contain all character in formular textblock
        private string formular;

        public string Formular
        {
            get { return formular; }
            set {
                if (value != formular)
                {
                    formular = value;
                    NotifyPropertyChanged("Formular");
                }
            }
        }

        //Save the result of calculator
        private string result;

        public string Result
        {
            get { return result; }
            set {
                if (value != result)
                {
                    result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }

        //constructor
        public CalObjectModel(string a, string b)
        {
            Formular = a;
            Result = b;
            IsSpecial = false;
        }

        //Handle when Object changed
        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            {
                handle(this, new PropertyChangedEventArgs(name));
            }

        }

    }
}
