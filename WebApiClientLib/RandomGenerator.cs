using System;
namespace WebApiClientLib
{
    public class RandomGenerator : IIntegerGenerator
    {
        public int GenerateInt()
        {
            Random r = new Random(DateTime.Now.Second);
            return r.Next();
        }
    }
}
