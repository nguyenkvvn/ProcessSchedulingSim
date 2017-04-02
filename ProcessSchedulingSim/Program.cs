using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSchedulingSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Process Scheduling Simulator");
            Console.WriteLine("Kevin Vinh Nguyen");
            Console.WriteLine("CSC 4320 - Operating Systems");
            Console.WriteLine("Chad Frederick");
            Console.WriteLine("--------------------");

            //take in input CSV file
            //format: priority, submission time, cpu burst time, IO burst time, CPU, IO?
            //number of rows is number of processes (profiles)
            Console.WriteLine("\nPlease enter path for CSV CPU profile sample:");
            string path = Console.ReadLine();

            ProcessProfiles pp = new ProcessProfiles(path);

            //simulate running the following scheduling algorithms
            //FCFS
            //SJF
            //Priority First
            //Round Robin

            Console.ReadKey();
        }
    }
}
