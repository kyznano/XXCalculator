using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SmartCalculator.Model;
using SmartCalculator.Cacualate;
using SmartCalculator.CommandIF;
using System.Windows.Input;
using System.Windows.Controls;
using SmartCalculator.View;
namespace SmartCalculator.ViewModel
{
    public class CalObjectViewModel : INotifyPropertyChanged
    {
        #region   Variables define
        //List of calculator line
        public ObservableCollection<CalObjectModel> listObject { get; set; }
        //The pointer to the line of calculator
        public int CurrentPos = 0;
        //State of screem: if it's start screen, isStart = true, else it's mode screen and others, it will be false
        //We use it to identify when we need add character to formular
        private bool isStart = true;
        //Variable point to the Grid ScreenUI to handle something
        private Grid des;


        #region Declare Command CMD correspond to the function it will handle
        private Command addStringCMD;

        public Command AddStringCMD
        {
            get
            {
                return addStringCMD;
            }
            set
            {
                addStringCMD = value;
            }
        }

        private Command delCharCMD;

        public Command DelCharCMD
        {
            get
            {
                return delCharCMD;
            }
            set { delCharCMD = value; }
        }
        private Command acCMD;

        public Command AcCMD
        {
            get { return acCMD; }
            set { acCMD = value; }
        }
        private Command moveLeftCMD;

        public Command MoveLeftCMD
        {
            get { return moveLeftCMD; }
            set { moveLeftCMD = value; }
        }
        private Command moveRightCMD;

        public Command MoveRightCMD
        {
            get { return moveRightCMD; }
            set { moveRightCMD = value; }
        }
        private Command calCMD;

        public Command CalCMD
        {
            get { return calCMD; }
            set { calCMD = value; }
        }
        private Command switchScreenCMD;

        public Command SwitchScreenCMD
        {
            get { return switchScreenCMD; }
            set { switchScreenCMD = value; }
        }
        private Command quadCalCMD;

        public Command QuadCalCMD
        {
            get { return quadCalCMD; }
            set { quadCalCMD = value; }
        }
        private Command cubiCalCMD;

        public Command CubiCalCMD
        {
            get { return cubiCalCMD; }
            set { cubiCalCMD = value; }
        }
        private Command system2VariablesCMD;

        public Command System2VariablesCMD
        {
            get { return system2VariablesCMD; }
            set { system2VariablesCMD = value; }
        }
        private Command system3VariablesCMD;

        public Command System3VariablesCMD
        {
            get { return system3VariablesCMD; }
            set { system3VariablesCMD = value; }
        }
        #endregion
        #endregion

        #region Declare handle function
        //add new line of calculator
        private void AddObject(CalObjectModel o)
        {
            listObject.Add(o);
        }
        //Move pointer to left
        private void MoveLeft(object para)
        {
            int i = listObject[CurrentPos].Formular.IndexOf("_");
            if (i > 0 && isStart)
            {
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Remove(i, 1);
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Insert(i - 1, "_");
            }
        }
        //Move pointer to right
        private void MoveRight(object para)
        {
            int i = listObject[CurrentPos].Formular.IndexOf("_");
            if (i != listObject[CurrentPos].Formular.Length - 1 && isStart)
            {
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Remove(i, 1);
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Insert(i + 1, "_");
            }
        }
        //Add string para to the position of current pointer
        private void AddCharcter(object para)
        {
            string val = para.ToString();
            int i = listObject[CurrentPos].Formular.IndexOf("_");
            if (val == "A")
            {
                if (listObject[CurrentPos].Formular.Length == 1)
                {
                    val += "=";
                }
            }
            
            if (isStart && listObject[CurrentPos].IsSpecial == false)
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Insert(i, val);
            else
                if (isStart && listObject[CurrentPos].IsSpecial && Editable(i))
                {
                    listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Insert(i, val);
                }
        }
        //Delete character on the left of current pointer
        private void DelCharacter(object para)
        {
            int i = listObject[CurrentPos].Formular.IndexOf("_");
            if (listObject[CurrentPos].Formular.Length > 1 && isStart && listObject[CurrentPos].IsSpecial == false)
            {
                listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Remove(i-1, 1);
            }
            else
                if (listObject[CurrentPos].Formular.Length > 1 && isStart && listObject[CurrentPos].IsSpecial&& Editable(i))
                {
                    if (listObject[CurrentPos].Formular[i - 1] != ']' && listObject[CurrentPos].Formular[i - 1] != '[')
                    listObject[CurrentPos].Formular = listObject[CurrentPos].Formular.Remove(i - 1, 1);
                }

        }
        //Clear all things in screen
        private void ResetScreen(object para)
        {
            if (isStart)
            {
                listObject.Clear();
                Init();
                CurrentPos = 0;
            }
        }
        //Calculate Formular and put result to Result field
        private void Calculate(object para)
        {
            if (isStart)
            {
                Calculate cal = new Calculate(listObject[CurrentPos].Formular);
                if (cal.RegexProcessing())
                {
                    listObject[CurrentPos].Result = "= " + cal.CalResult();
                    if (listObject.Count - 1 == CurrentPos)
                    {
                        listObject.Add(new CalObjectModel("_", "0"));
                    }
                    CurrentPos++;
                }
                else
                {
                    ErrorHandle(cal.ErrorString);
                }
            }
        }

        //Init some data for first screen
        private void Init()
        {
            listObject.Add(new CalObjectModel("_", "0"));
        }

        //Show error
        private void ErrorHandle(string para)
        {
            MessageBox.Show("Error:\n" + para);
        }

        //Switch screen from ScreenView to ScreenMode or otherwise.
        private void SwitchScreen(object para)
        {
            if (para != null)
            {
                des = para as Grid;
                ScreenView t = new ScreenView();
                if (true == isStart)
                {
                    des.Children.Add(new ScreenMode());
                    isStart = false;
                }
                else
                {
                    des.Children.Add(new ScreenView());
                    isStart = true;
                }
            }
            else
            {
                des.Children.Add(new ScreenView());
                isStart = true;
            }
            des.Children.RemoveAt(0);
        }

        //Add string formular for quadratic equation
        private void QuadCal(object para)
        {
            listObject[CurrentPos].Formular = "[_] x^2 + [] x + [] = 0";
            listObject[CurrentPos].IsSpecial = true;
            SwitchScreen(null);
        }

        //Add string formular for cubic equation
        private void CubiCal(object para)
        {
            listObject[CurrentPos].Formular = "[_] x^3 + [] x^2 + [] x + [] = 0";
            listObject[CurrentPos].IsSpecial = true;
            SwitchScreen(null);
        }

        //Add string formular for systems of linear equations with 2 variables
        private void System2Variable(object para)
        {
            listObject[CurrentPos].Formular = "[_] x + [] y = []\n[] x + [] y = []";
            listObject[CurrentPos].IsSpecial = true;
            SwitchScreen(null);
        }
        //Add string formular for systems of linear equations with 3 variables
        private void System3Variable(object para)
        {
            listObject[CurrentPos].Formular = "[_] x + [] y + []z = []\n[] x + [] y + []z = []\n[] x + [] y + []z = []\n";
            listObject[CurrentPos].IsSpecial = true;
            SwitchScreen(null);
        }


        //When it's special formular, we need to check if that position can be edited
        private bool Editable(int pos)
        {
            bool left = false;
            for (int i = pos - 1; i >= 0; i--)
            {
                if (listObject[CurrentPos].Formular[i] == '[')
                {
                    left = true;
                    break;
                }
                if (listObject[CurrentPos].Formular[i] == ']')
                {
                    break;
                }
            }
            if (left)
            {
                for (int i = pos + 1; i < listObject[CurrentPos].Formular.Length; i++)
                {
                    if (listObject[CurrentPos].Formular[i] == ']')
                    {
                        return true;
                    }
                    if (listObject[CurrentPos].Formular[i] == '[')
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Event changed handle
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (null != handle)
            {
                handle(this, new PropertyChangedEventArgs(name));
            }

        }
        #endregion
        //Constructor
        public CalObjectViewModel()
        {
            listObject = new ObservableCollection<CalObjectModel>();
            Init();
            //Register all Command here
            addStringCMD = new Command(AddCharcter);
            delCharCMD = new Command(DelCharacter);
            acCMD = new Command(ResetScreen);
            moveLeftCMD = new Command(MoveLeft);
            moveRightCMD = new Command(MoveRight);
            calCMD = new Command(Calculate);
            switchScreenCMD = new Command(SwitchScreen);
            quadCalCMD = new Command(QuadCal);
            cubiCalCMD = new Command(CubiCal);
            system2VariablesCMD = new Command(System2Variable);
            system3VariablesCMD = new Command(System3Variable);
        }
    }
}
