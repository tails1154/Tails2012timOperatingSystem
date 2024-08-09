using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guiOSbutnotmadebyai
{
    internal class tailsmath
    {
        public int add(int number1, int number2)
        {
            return number1 + number2;
        }
        public int subtract(int number1, int number2)
        {
            return (number1 - number2);
        }
        public int multiply(int number1, int number2)
        {
            var answer = 0;
            for (int i = 0; i < number2; i++)
            {
                answer = number1 + number2;
            }
            return answer;
        }
        public int divide(int number1, int number2)
        {
            return number1 / number2;
        }
    }
}
