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

        public String CalComponent(String i)
        {
            String s = i;
            bool o = true;
            while (o)
            {
                String res = ExtractComponents(s);
                if (res == null) o = false;
                else
                {
                    String new_res = CalComponent(res);
                    s = s.Replace('(' + res + ')', new_res);
                }
            }
            String de = "+-*/";
            char[] delimiter = de.ToCharArray();
            if(s.Contains('+'))
            {
                String[] spl = s.Split('+');
                float res = 0;
                foreach(String addend in spl)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    res += float.Parse(CalComponent(addend));
                }
                s = res.ToString();
                //return "0";
            } else if(s.Contains('-'))
            {
                String[] spl = s.Split('-');
                float res = 0;
                bool first = true;
                foreach(String addend in spl)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    if (first) { res = float.Parse(CalComponent(addend)); first = false; }
                    else res -= float.Parse(CalComponent(addend));
                }
                s = res.ToString();
            }
            else if (s.Contains('*'))
            {
                String[] spl = s.Split('*');
                float res = 1;
                foreach (String addend in spl)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    res = res*float.Parse(CalComponent(addend));
                }
                s = res.ToString();
            }
            else if (s.Contains('/'))
            {
                String[] spl = s.Split('/');
                float res = 0;
                bool first = true;
                foreach (String addend in spl)
                {
                    //s = s.Replace(left + '+' + right, float(CalComponent(left)) + float(CalComponent(right)));
                    if (first) { res = float.Parse(CalComponent(addend)); first = false; }
                    else res = res / float.Parse(CalComponent(addend));                    
                }
                s = res.ToString();
            }
            return s;
        }

        public String ExtractComponents(String i)
        {
            if (i.Contains(')'))
            {
                    String tail = i.Split(')')[0];
                    String[] tail_split = tail.Split('(');
                    return tail_split[tail_split.Length-1];
            }
            return null;
        }
    }
}
