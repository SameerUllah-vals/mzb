using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class CodeGenerator
    {
        Random rand = new Random();
        public String generateCode(string string1, string string2)
        {
            char[] Name = string1.ToCharArray();
            string FirstLetter = Convert.ToString(Name[0]);
            char[] Name2 = string2.ToCharArray();
            string LastLetter = Convert.ToString(Name2[0]);
            int num = rand.Next(999);
            string code = FirstLetter + LastLetter + num.ToString();

            return code;
        }

        public String generateCode(string stringName)
        {
            char[] Name = stringName.ToCharArray();
            string FirstLetter = Convert.ToString(Name[0]);
            string LastLetter = Convert.ToString(Name.Last());
            int num = rand.Next(999);
            string code = FirstLetter + LastLetter + num.ToString();
            return code;
        }
    }
}
