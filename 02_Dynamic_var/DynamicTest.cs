using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Dynamic_var
{
    public class DynamicTest
    {
        public int MyProperty { get; set; }
        public dynamic Test { get; set; }

        public void SetValue(dynamic value)
        {
            int i = -1000;

            if (value is int)
                Test = i;
            else
                Test = value;
        }

        public override string ToString()
        {
            return $"{MyProperty} ===> Test {Test} {Test.GetType().Name}";
        }
    }
}
