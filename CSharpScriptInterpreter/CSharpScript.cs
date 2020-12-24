using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CSharpScript
{
    public void Main()
    {
        for (int i = 0; i < 10; i++)
        {
            Say(i);
        }
        throw new Exception("0");
    }

    private void Say(int i)
    {
        Console.WriteLine(i);
    }

    private int Add(int a, int b)
    {
        return a + b;
    }

}

