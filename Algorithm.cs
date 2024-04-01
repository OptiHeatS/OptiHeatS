//This is a test for an Algorithm in: Optimization of Electricity Consumption in a Building
//Work to be done. Can be used as a base
//Not added to Boiler usage and main part of code.
//Not fully integratable and usable but a start
using System;

class Program
{
    static void Main(string[] args)
    {
        int i, b, n, a, f, p, pt, m, c, o, x;
        int[] temp = new int[25];
        int[] price = new int[25];
        int pcum;

        Console.WriteLine("\n\n\n ******* The cumulative cost for one day**************\n");
        Console.WriteLine(" the step is the fall of the temperature in degree per hour : \n");
        Console.WriteLine(" - 1 Degree for well insulated building \n");
        Console.WriteLine(" - 2 Degree for badly insulated building \n");

        while (n > 0)
        {
            Console.Write(" do you want to seize you? If yes write 1 else 0 : ");
            n = int.Parse(Console.ReadLine());
            if (n == 0) return;
        }

        Console.Write("\n enter the step [1,2] : ");
        b = int.Parse(Console.ReadLine());

        Console.Write("\n the cost of off temperature: ");
        c = int.Parse(Console.ReadLine());

        Console.Write("\n The cost of full temperature: ");
        p = int.Parse(Console.ReadLine());

        Console.Write("\n the cost of the peak temperature: ");
        pt = int.Parse(Console.ReadLine());

        Console.WriteLine("\n enter the temperature ");

        for (i = 1; i < 9; i++)
            price[i] = c;
        for (i = 9; i < 17; i++)
            price[i] = p;
        for (i = 17; i < 22; i++)
            price[i] = pt;
        for (i = 22; i < 25; i++)
            price[i] = p;

        Console.Write("\n For a classic control type without erasing enter 3 \n");
        Console.Write("\n For a regulation type with erase enter 4 \n");
        Console.Write("\n give the type : ");
        f = int.Parse(Console.ReadLine());

        if (f == 3)
            Console.Write(" Gives the constant temperature in the building ");
        Console.Write(" give temperature minimum : ");
        a = int.Parse(Console.ReadLine());
        Console.Write(" give temperature max : ");
        m = int.Parse(Console.ReadLine());

        for (i = 1; i < 25; i++)
            if (price[i] - price[i + 1] >= pt - p)
            {
                o = i + 1;
                Console.Write("\n Hour is : " + o + "h\n");
                Console.Write(" the temperature a " + o + "h est :");
                Console.Write(a);
                for (i = 0; i < o; i++)
                    Console.Write("\n enter the temperature " + (o - i) + "h :");
                temp[o - i] = int.Parse(Console.ReadLine());
                Console.Write("\n");
                for (i = 1; i < 25 - o; i++)
                    Console.Write("\n enter temperature " + (25 - i) + "h : ");
                temp[25 - i] = int.Parse(Console.ReadLine());
                for (i = 1; i < 25; i++)
                    Console.Write("\n the temperature " + i + "h : " + temp[i]);
                temp[25] = temp[1];
                pcum = 0;
                for (i = 1; i < 25; i++)
                    pcum = pcum + (temp[i + 1] - temp[i] + b) * price[i];
                Console.Write("\n");
                Console.Write("\n The cumulative cost for One day is : " + pcum + "Dollars");
                Console.Write("\n The control U is ");
                Console.Write("\n");
                for (i = 1; i < 25; i++)
                    Console.Write("\n - U(" + i + ") = " + (temp[i + 1] - temp[i]));
            }
        Console.ReadLine();
    }
}

//https://hal.science/hal-03033996/document