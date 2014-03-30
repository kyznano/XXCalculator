using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartCalculator.CommandIF
{
    public class Command : ICommand
    {
        //Check can execute
        Func<object, bool> canExecute;
        //function to execute
        Action<object> executeAction;
 // 2 constructor
    public Command(Action<object> executeAction)
        : this(executeAction, null)
    {
    }
 
    public Command(Action<object> executeAction, Func<object, bool> canExecute)
    {
        if (executeAction == null)
        {
            throw new ArgumentNullException("executeAction");
        }
        this.executeAction = executeAction;
        this.canExecute = canExecute;
    }

    //Defines the method that determines whether the command can execute in its current state
    public bool CanExecute(object parameter)
    {
        bool result = true;
        Func<object, bool> canExecuteHandler = this.canExecute;
        if (canExecuteHandler != null)
        {
            result = canExecuteHandler(parameter);
        }
 
        return result;
    }
 
    public event EventHandler CanExecuteChanged;
 
    //Occurs when changes occur that affect whether the command should execute
    public void RaiseCanExecuteChanged()
    {
        EventHandler handler = this.CanExecuteChanged;
        if (handler != null)
        {
            handler(this, new EventArgs());
        }
    }
   //Defines the method to be called when the command is invoked
    public void Execute(object parameter)
    {
        this.executeAction(parameter);
    }
}
}
