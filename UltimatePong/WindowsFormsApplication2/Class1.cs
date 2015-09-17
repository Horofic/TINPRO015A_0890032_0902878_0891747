using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Class1
    {
        public void change(ref int x)
        {
            plus(ref x);
          
        }

        public void plus(ref int x)
        {
            x++;
        }


    }
}
