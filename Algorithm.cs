//This is a test for an Algorithm in: Optimization of Electricity Consumption in a Building
//Work to be done. Can be used as a base
//Not added to Boiler usage and main part of code.
//Not fully integratable and usable but a start
using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        int i, b, n, a, f, p, pt, m, c, o, x; //Define all needed variables 
        int[] temp = new int[24]; 
        int[] price = new int[24]; //price and temp for 24 hours
        int pcum;

        /*Console.WriteLine(" ******* The cumulative cost for one day**************");
        Console.WriteLine(" The step is the fall of the temperature in degree per hour : ");
        Console.WriteLine(" - 1 Degree for well insulated building ");
        Console.WriteLine(" - 2 Degree for badly insulated building ");*/

        /*Console.WriteLine("Do you want to start?");
        Console.WriteLine("Type 1 for yes, type 0 for no");

        n = int.Parse(Console.ReadLine());
        while(n > 0){
            Console.WriteLine("Start");
            continue;
            if(n == 0){
                break;
            }
        }*/

        /*while (n > 0)
        {
            Console.Write(" do you want to seize you? If yes write 1 else 0 : ");
            n = int.Parse(Console.ReadLine());
            if (n == 0) return;
        }*/
        /*
        if(n == 0){
            Console.WriteLine("Goodbye.");
            return;
        }
        if(n == 1){
            Console.WriteLine("Thank you for choosing our Algorithm: Going on");
        }*/

        Console.WriteLine("Type 1 for start, 0 for end"); //start menu (choices to start or not start this algorithm) - only for testing 
        n = int.Parse(Console.ReadLine());
        switch(n){
            case 1:
            Console.WriteLine("Thank you for choosing our Algorithm: Going on");
            break;
            case 0:
            Console.WriteLine("Goodbye");
            return;
            default:
            Console.WriteLine("Not an available option");
            goto case 0;
        }
        Console.WriteLine("The step is the fall of the temperature in degree per hour:"); //well or badly insulated building
        Console.WriteLine("1 Degree for well insulated building");
        Console.WriteLine("2 Degree for badly insulated building");

        Console.Write("Enter the step [1,2]:");
        b = int.Parse(Console.ReadLine());

        Console.Write("The cost of off temperature (from 01 - 08h):");
        c = int.Parse(Console.ReadLine());

        Console.Write("The cost of full temperature(from 09 - 16h and 22 - 00h):");
        p = int.Parse(Console.ReadLine());

        Console.Write("The cost of the peak temperature(from 17 - 21h):");
        pt = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the time:"); //used for calculations later - can be reworked with excel tables

        for (i = 1; i < 9; i++)
            price[i] = c;
        for (i = 9; i < 17; i++)
            price[i] = p;
        for (i = 17; i < 22; i++)
            price[i] = pt;
        for (i = 22; i < 24; i++)
            price[i] = p;

        Console.WriteLine("For a classic control type without erasing enter 3"); //used for calculations later - can be reworked with excel tables
        Console.WriteLine("For a regulation type with erase enter 4");
        Console.WriteLine("Give the type:");
        f = int.Parse(Console.ReadLine());

        if (f == 3)
        Console.WriteLine("Gives the constant temperature in the building"); //needed for calculations - again, can be reworked with excel tables 
        Console.WriteLine("Give temperature minimum:");
        a = int.Parse(Console.ReadLine());
        Console.Write("Give temperature max:");
        m = int.Parse(Console.ReadLine());

        for (i = 1; i < 25; i++)                            ///CALCULATIONS - NOT DONE (BUGS AND ERRORS ARE EXPECTED A LOT)
            if (price[i] - price[i + 1] >= pt - p)
            {
                o = i + 1;
                Console.WriteLine("Hour is:" + o + "h");
                Console.WriteLine("The temperature a " + o + "h est :");
                Console.WriteLine(a);
                for (i = 0; i < o; i++)
                    Console.WriteLine("enter the temperature " + (o - i) + "h :");
                temp[o - i] = int.Parse(Console.ReadLine());
                Console.WriteLine("");
                for (i = 1; i < 25 - o; i++)
                    Console.WriteLine(" enter temperature " + (25 - i) + "h : ");
                temp[25 - i] = int.Parse(Console.ReadLine());
                for (i = 1; i < 25; i++)
                    Console.WriteLine(" the temperature " + i + "h : " + temp[i]);
                temp[25] = temp[1];
                pcum = 0;
                for (i = 1; i < 25; i++)
                    pcum = pcum + (temp[i + 1] - temp[i] + b) * price[i];
                Console.WriteLine("");
                Console.WriteLine(" The cumulative cost for One day is : " + pcum + "Dollars");
                Console.WriteLine(" The control U is ");
                Console.WriteLine("");
                for (i = 1; i < 25; i++)
                    Console.WriteLine(" - U(" + i + ") = " + (temp[i + 1] - temp[i]));
            }
        Console.ReadLine();
    }
}

//https://hal.science/hal-03033996/document