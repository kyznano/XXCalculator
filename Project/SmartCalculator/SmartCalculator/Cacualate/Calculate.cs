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
        String pi = Math.PI.ToString();

        String CONST_EMPTY = "";
        String CONST_FALSE = "False";
        String CONST_ADD = "+";
        String CONST_SUBTRACT = "-";
        String CONST_MULTIPLY = "*";
        String CONST_DIVIDE = "/";
        String CONST_POWER = "^";
        String CONST_SQRT = "√";
        String CONST_LOG = "Log";
        String CONST_FACTORIAL = "!";
        String CONST_SIN = "Sin";
        String CONST_COS = "Cos";
        String CONST_TAN = "Tan";
        String CONST_RANDOM = "Ran";
        String CONST_PI = "π";
        String CONST_EXPONENT = "e";

        //constructor
        public Calculate(String seq)
        {
            formular = seq;
            backup_input = seq.Replace("_", "");
            answer = null;
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
            if (calculatedExp == CONST_FALSE) return CONST_FALSE;
            // replace "(expression)" by calculated expression
            while (parenthesis)
            {
                String noneParenthesis = ExtractComponents(calculatedExp);
                if (noneParenthesis == null) parenthesis = false;
                else calculatedExp = calculatedExp.Replace('(' + noneParenthesis + ')', CalComponent(noneParenthesis));
            }

            if(calculatedExp.Contains(CONST_ADD))
            {
                String[] addends = calculatedExp.Split(CONST_ADD.ToCharArray());
                double sum = 0;
                foreach(String addend in addends)
                {
                    sum += double.Parse(CalComponent(addend));
                }
                calculatedExp = sum.ToString();
            }
            else if (calculatedExp.Contains(CONST_SUBTRACT))
            {
                String[] elements = calculatedExp.Split(CONST_SUBTRACT.ToCharArray());
                double sub = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { sub = double.Parse(CalComponent(element)); firstElement = false; }
                    else sub -= double.Parse(CalComponent(element));
                }
                calculatedExp = sub.ToString();
            }
            else if (calculatedExp.Contains(CONST_MULTIPLY))
            {
                String[] elements = calculatedExp.Split(CONST_MULTIPLY.ToCharArray());
                double mul = 1;
                foreach (String element in elements)
                {
                    mul *= double.Parse(CalComponent(element));
                }
                calculatedExp = mul.ToString();
            }
            else if (calculatedExp.Contains(CONST_DIVIDE))
            {
                String[] elements = calculatedExp.Split(CONST_DIVIDE.ToCharArray());
                double div = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { div = double.Parse(CalComponent(element)); firstElement = false; }
                    else div = div / double.Parse(CalComponent(element));                    
                }
                calculatedExp = div.ToString();
            }
            else if (calculatedExp.Contains(CONST_POWER))
            {
                String[] powerComponents = calculatedExp.Split(CONST_POWER.ToCharArray());
                double powerResult = Math.Pow(double.Parse(CalComponent(powerComponents[0])), double.Parse(CalComponent(powerComponents[1])));
                calculatedExp = calculatedExp.Replace(powerComponents[0] + CONST_POWER + powerComponents[1], powerResult.ToString());
                if (powerComponents.Length > 2)
                {
                    calculatedExp = CalComponent(calculatedExp);
                }
            }
            else if (calculatedExp.Contains(CONST_FACTORIAL))   // support only one factorial symbol (!)
            {
                calculatedExp = calculatedExp.Replace(CONST_FACTORIAL, CONST_EMPTY);
                double factorialResult = Factorial(double.Parse(CalComponent(calculatedExp)));
                calculatedExp = factorialResult.ToString();
            }
            else if (calculatedExp.Contains(CONST_SIN)) // problem: Sin(π) isn't equal to 0
            {
                calculatedExp = calculatedExp.Replace(CONST_SIN, CONST_EMPTY);
                calculatedExp = Math.Sin(double.Parse(calculatedExp)).ToString();                
            }
            else if (calculatedExp.Contains(CONST_COS))
            {
                calculatedExp = calculatedExp.Replace(CONST_COS, CONST_EMPTY);
                calculatedExp = Math.Cos(double.Parse(calculatedExp)).ToString();
            }
            else if (calculatedExp.Contains(CONST_TAN))
            {
                calculatedExp = calculatedExp.Replace(CONST_TAN, CONST_EMPTY);
                calculatedExp = Math.Tan(double.Parse(calculatedExp)).ToString();
            }
            else if (calculatedExp.Contains(CONST_SQRT))
            {
                String[] elements = calculatedExp.Split(CONST_SQRT.ToCharArray());
                if (elements[0].Length > 0)
                {
                    calculatedExp = Math.Pow(double.Parse(elements[1]), 1.0 / double.Parse(elements[0])).ToString();
                }
                else
                {
                    calculatedExp = Math.Sqrt(double.Parse(elements[1])).ToString();
                }                
            }
            else if (calculatedExp.Contains(CONST_LOG))
            {
                String tempOperator = "|";
                calculatedExp = calculatedExp.Replace(CONST_LOG, tempOperator);
                String[] elements = calculatedExp.Split(tempOperator.ToCharArray());
                calculatedExp = Math.Log(double.Parse(elements[1]), double.Parse(elements[0])).ToString();
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
            if (expression.Contains(CONST_EXPONENT))
            {
                replacement = expression.Replace(CONST_EXPONENT, exponent);
            }
            else if (expression.Contains(CONST_PI))
            {
                replacement = expression.Replace(CONST_PI, pi);
            }
            /*else if (expression.Contains("Ran"))
            {
                replacement = expression.Replace("Ran", "0");
            }*/
            /*else if (expression.Contains("Ans"))
            {
                if (answer == null)
                {
                    return CONST_FALSE;
                }
                replacement = expression.Replace("Ans", answer);
            }*/
            return replacement;
        }
    }
}
