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
        String backup_input;
        String answer;
        List<String> components = new List<String>();

        // some constant values
        String exponent = "2.718281828";
        String pi = "3.141592654";

        //constructor
        public Calculate(String seq)
        {
            formular = seq;
            backup_input = seq.Replace("_", "");
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
            answer = CalComponent(backup_input);
            return answer;
        }

        public String CalComponent(String expression)
        {
            String calculatedExp = expression;
            bool parenthesis = true;
            calculatedExp = ReplaceConstants(expression);
            // replace "(expression)" by calculated expression
            while (parenthesis)
            {
                String noneParenthesis = ExtractComponents(calculatedExp);
                if (noneParenthesis == null) parenthesis = false;
                else calculatedExp = calculatedExp.Replace('(' + noneParenthesis + ')', CalComponent(noneParenthesis));
            }
            String operationsString = "+-*/";
            char[] operations = operationsString.ToCharArray();
            if(calculatedExp.Contains('+'))
            {
                String[] addends = calculatedExp.Split('+');
                double sum = 0;
                foreach(String addend in addends)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    sum += double.Parse(CalComponent(addend));
                }
                calculatedExp = sum.ToString();
            }
            else if (calculatedExp.Contains('-'))
            {
                String[] elements = calculatedExp.Split('-');
                double sub = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { sub = double.Parse(CalComponent(element)); firstElement = false; }
                    else sub -= double.Parse(CalComponent(element));
                }
                calculatedExp = sub.ToString();
            }
            else if (calculatedExp.Contains('*'))
            {
                String[] elements = calculatedExp.Split('*');
                double mul = 1;
                foreach (String element in elements)
                {
                    mul *= double.Parse(CalComponent(element));
                }
                calculatedExp = mul.ToString();
            }
            else if (calculatedExp.Contains('/'))
            {
                String[] elements = calculatedExp.Split('/');
                double div = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { div = double.Parse(CalComponent(element)); firstElement = false; }
                    else div = div / double.Parse(CalComponent(element));                    
                }
                calculatedExp = div.ToString();
            }
            else if (calculatedExp.Contains('^'))
            {
                String[] powerComponents = calculatedExp.Split('^');
                double powerResult = Math.Pow(double.Parse(CalComponent(powerComponents[0])), double.Parse(CalComponent(powerComponents[1])));
                calculatedExp = calculatedExp.Replace(powerComponents[0] + "^" + powerComponents[1], powerResult.ToString());
                if (powerComponents.Length > 2)
                {
                    calculatedExp = CalComponent(calculatedExp);
                }
            }
            else if (calculatedExp.Contains('!'))   // support one factorial symbol (!) only
            {
                calculatedExp = calculatedExp.Replace("!", "");
                double factorialResult = Factorial(double.Parse(CalComponent(calculatedExp)));
                calculatedExp = factorialResult.ToString();
            }
            return calculatedExp;
        }

        public String ExtractComponents(String expression)
        {
            if (expression.Contains(')'))    // extract character "(" and ")"
            {
                    String tail = expression.Split(')')[0];
                    String[] tail_split = tail.Split('(');
                    return tail_split[tail_split.Length-1];
            }
            return null;
        }

/*
        public float Power(float x, float p)
        {
            if (p < 1) return 1;
            float result = 1;
            for (int i = 0; i < p; i++)
            {
                result *= x;
            }
            return result;
        }
*/

        public double Factorial(double number)
        {
            double result = 1;
            int startValue = 0;
            while (startValue < number)
            {
                result *= ++startValue;
            }
            return result;
        }

        public String ReplaceConstants(String expression)
        {
            String replacement = expression;
            if (expression.Contains("e"))
            {
                replacement = expression.Replace("e", exponent);
            }
            else if (expression.Contains("π"))
            {
                replacement = expression.Replace("π", pi);
            }
            /*else if (expression.Contains("Ran"))
            {
                replacement = expression.Replace("Ran", "0");
            }*/
            return replacement;
        }
    }
}
