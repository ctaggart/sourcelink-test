using System;

namespace ClassLibrary1
{
    public static class Class1
    {
        public static int TestXy(int value)
        {
            if (value == 0)
            {
                throw new Exception("Not valid");
            }
            return value;
        }
    }
}
