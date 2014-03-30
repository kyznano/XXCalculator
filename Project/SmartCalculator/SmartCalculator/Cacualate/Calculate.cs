using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCalculator.Cacualate
{
    public class Calculate
    {
        String formular;
        String errorString;

        //constructor
        public Calculate(String seq)
        {
            formular = seq;
        }

        public String ErrorString
        {
            get { return errorString; }
        }


        //Check and parse value to variables
        public bool RegexProcessing()
        {
            //if parsing is failed: return false and set error to errorString variable
            return true;
        }

        //You can modify all things, but you need add the result to this function
        public String CalResult(){
            return "1";
        }
    }
}
