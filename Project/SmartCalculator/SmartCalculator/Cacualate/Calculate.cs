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
        List<String> components = new List<String>();

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
            return CalComponent(backup_input);
        }

        public String CalComponent(String expression)
        {
            String calculatedExp = expression;
            bool parenthesis = true;
            // replace "(expression)" by calculated expression
            while (parenthesis)
            {
                String noneParenthesis = ExtractComponents(expression);
                if (noneParenthesis == null) parenthesis = false;
                else calculatedExp = expression.Replace('(' + noneParenthesis + ')', CalComponent(noneParenthesis));
            }
            String operationsString = "+-*/";
            char[] operations = operationsString.ToCharArray();
            if(calculatedExp.Contains('+'))
            {
                String[] addends = calculatedExp.Split('+');
                float sum = 0;
                foreach(String addend in addends)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    sum += float.Parse(CalComponent(addend));
                }
                calculatedExp = sum.ToString();
            }
            else if (calculatedExp.Contains('-'))
            {
                String[] elements = calculatedExp.Split('-');
                float sub = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { sub = float.Parse(CalComponent(element)); firstElement = false; }
                    else sub -= float.Parse(CalComponent(element));
                }
                calculatedExp = sub.ToString();
            }
            else if (calculatedExp.Contains('*'))
            {
                String[] elements = calculatedExp.Split('*');
                float mul = 1;
                foreach (String element in elements)
                {
                    mul *= float.Parse(CalComponent(element));
                }
                calculatedExp = mul.ToString();
            }
            else if (calculatedExp.Contains('/'))
            {
                String[] elements = calculatedExp.Split('/');
                float div = 0;
                bool firstElement = true;
                foreach (String element in elements)
                {
                    if (firstElement) { div = float.Parse(CalComponent(element)); firstElement = false; }
                    else div = div / float.Parse(CalComponent(element));                    
                }
                calculatedExp = div.ToString();
            }
            else if (calculatedExp.Contains('.'))
            {
                
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
    }
}
