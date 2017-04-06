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

            Processor CPU;

            Console.WriteLine("\n");
            //simulate running the following scheduling algorithms
            //FCFS
            CPU = new Processor(pp);
            CPU.runFCFS();

            //SJF
            CPU = new Processor(pp);
            CPU.runSJF();

            //Priority First
            CPU = new Processor(pp);
            CPU.runPF();

            //Round Robin
            CPU = new Processor(pp);
            CPU.runRR();

            Console.WriteLine("\nSimulation complete. Press any key to exit...");
            //prevent the console from closing before output can be read
            Console.ReadKey();
        }
    }
}
