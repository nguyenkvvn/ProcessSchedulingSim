using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSchedulingSim
{
    //The processor class simulates a processor processing processes. (Wow, that's a really confusing, but succinct way of putting it.)
    //The processor will also collect metrics to produce the following: average wait time, average turnaround time, throughout per mys(?)
    //One interger value on integer time is one unit of time.
    class Processor
    {
        int timeElapsed = 0;
        //List<int> waitTimes = new List<int>();
        //List<int> turnAroundTimes = new List<int>();
        ProcessProfiles targetProfile;

        //a processor needs processes to actually process, so it will take a ProcessProfiles container as a constructor input.
        public Processor(ProcessProfiles pps)
        {
            targetProfile = pps;
        }

        //FCFS - First Come First Serve
        public void runFCFS()
        {
            //localized instance of metrics
            //waiting time is defined as time spent waiting to begin the process
            List<int> waitTimes = new List<int>();
            //turnaround time is defined as time spent waiting for the process to complete
            List<int> turnAroundTimes = new List<int>();

            //copy of the profile
            ProcessProfiles ppsCopy = targetProfile;

            //define ready queue
            List<ProcessProfile> readyQueue = new List<ProcessProfile>();
            //define completed list
            List<ProcessProfile> completedL = new List<ProcessProfile>();

            //VERBOSE
            //Console.WriteLine("\n" + "CYCLE | PROCESS | CPU//");

            //calculate runtimer
            int runtimeMax = calcRunTime();
            int runtimeCount = 0;

            //conditions to finish running: timer reaches upper maximum runtime
            //each loop cycle counts as 1 CPU burst cycle
            while (runtimeCount <= runtimeMax)
            {   
                //TIME INSERTION CHECK
                //check if processes can be slipped into the process queue
                //condition is that their time must be equal to the index time pased
                foreach (ProcessProfile p in ppsCopy.listOfProfiles)
                {
                    
                    //if the insertion time matches the insertion time of the process in check
                    if (runtimeCount == p.SubmissionTime)
                    {
                        //then add that process into the ready queue
                        readyQueue.Add(p);
                        //
                        waitTimes.Add(runtimeCount);
                    }

                }
                //PROCESS THE PROCESS PER TIME UNIT
                //process processes in the ready queue
                if (readyQueue.Count != 0)
                {
                    //process takes a step closer to completion
                    readyQueue[0].CPUBurstCount++;
                    //VERBOSE
                    //printClock(runtimeCount, readyQueue[0], runtimeMax);
                }
                //MOVE COMPLETED PROCESSES INTO COMPLETED PROCESSES QUEUE
                //foreach left for thinking reference- will error out as the collection will be modified post enumeration of the foreach loop; see alternative solution in form of reversed for loop
                /*foreach (ProcessProfile p in readyQueue)
                {
                    //checks every process inside the ready queue:
                    //if the process allotted burst time has matched the passed burst time, it is considered complete.
                    if (p.CPUBurstCount == p.CPUBurstTime)
                    {
                        //remove the process from the ready queue and move it into the completed list
                        completedL.Add(p);
                        readyQueue.Remove(p);
                    }
                }*/
                for (int i = readyQueue.Count - 1; i >= 0; i--)
                {
                    if (readyQueue[i].CPUBurstCount == readyQueue[i].CPUBurstTime)
                    {
                        //remove the process from the ready queue and move it into the completed list
                        completedL.Add(readyQueue[i]);
                        readyQueue.Remove(readyQueue[i]);
                        //add a turnaround time value for the completed process
                        turnAroundTimes.Add(runtimeCount);
                    }
                }
                //increment the runtime clock
                runtimeCount++;
            }

            Console.WriteLine("======== FCFS Metrics ========");
            Console.WriteLine("Calculated max runtime is: " + runtimeMax);
            Console.WriteLine("Average Waiting    Time: " + average(waitTimes) + " bursts.");
            Console.WriteLine("Average Turnaround Time: " + average(turnAroundTimes) + " bursts.\n");
        }

        //SJF - Shortest Job First
        public void runSJF()
        {

        }
        
        //PF - Priority First
        public void runPF()
        {

        }

        //RR - Round Robin
        public void runRR()
        {
            //localized instance of metrics
            //waiting time is defined as time spent waiting to begin the process
            List<int> waitTimes = new List<int>();
            //turnaround time is defined as time spent waiting for the process to complete
            List<int> turnAroundTimes = new List<int>();

            //copy of the profile
            ProcessProfiles ppsCopy = targetProfile;

            //define ready queue
            List<ProcessProfile> readyQueue = new List<ProcessProfile>();
            //define completed list
            List<ProcessProfile> completedL = new List<ProcessProfile>();

            //Console.WriteLine("\n" + "CYCLE | PROCESS | CPU");

            //calculate runtimer
            int runtimeMax = calcRunTime();
            int runtimeCount = 0;

            //ROUNDROBIN
            //round robin index
            int rrIndex = 0;

            //conditions to finish running: timer reaches upper maximum runtime
            //each loop cycle counts as 1 CPU burst cycle
            while (runtimeCount <= runtimeMax)
            {
                //TIME INSERTION CHECK
                //check if processes can be slipped into the process queue
                //condition is that their time must be equal to the index time pased
                foreach (ProcessProfile p in ppsCopy.listOfProfiles)
                {

                    //if the insertion time matches the insertion time of the process in check
                    if (runtimeCount == p.SubmissionTime)
                    {
                        //then add that process into the ready queue
                        readyQueue.Add(p);
                        //
                        waitTimes.Add(runtimeCount);
                    }

                }
                //PROCESS THE PROCESS PER TIME UNIT
                //process processes in the ready queue
                if (readyQueue.Count != 0)
                {
                    //process takes a step closer to completion
                    try
                    {
                        //process the upcoming redrobin process
                        readyQueue[rrIndex].CPUBurstCount++;
                        //VERBOSE
                        //Console.WriteLine("Red Robin index: " + rrIndex);
                        //printClock(runtimeCount, readyQueue[rrIndex], runtimeMax);
                    }
                    catch
                    {
                        //in case the rrindex oversteps the number of elements in the ready queue
                    }

                    //increment or reset the roundrobin counter
                    if (((rrIndex + 1) == readyQueue.Count) || (rrIndex == readyQueue.Count))
                    {
                        //if the index is at the max of the queue's contents, reset the queue.
                        rrIndex = 0;
                    }
                    else
                    {
                        //otherwise, increment it by 1
                        rrIndex++;
                    }
                }

                //MOVE COMPLETED PROCESSES INTO COMPLETED PROCESSES QUEUE
                for (int i = readyQueue.Count - 1; i >= 0; i--)
                {
                    if (readyQueue[i].CPUBurstCount == readyQueue[i].CPUBurstTime)
                    {
                        //remove the process from the ready queue and move it into the completed list
                        completedL.Add(readyQueue[i]);
                        readyQueue.Remove(readyQueue[i]);
                        //add a turnaround time value for the completed process
                        turnAroundTimes.Add(runtimeCount);
                    }
                }
                //increment the runtime clock
                runtimeCount++;
            }

            Console.WriteLine("======== RORO Metrics ========");
            Console.WriteLine("Calculated max runtime is: " + runtimeMax);
            Console.WriteLine("Average Waiting    Time: " + average(waitTimes) + " bursts.");
            Console.WriteLine("Average Turnaround Time: " + average(turnAroundTimes) + " bursts.\n");
        }

        private void printClock(int cycle, ProcessProfile p, int rmax)
        {
            //BUG - decimal places will not print
            //decimal dpercentCPU = (p.CPUBurstCount / p.CPUBurstTime)*100;
            //string percentCPU = string.Format("{0:0.##}", dpercentCPU.ToString());

            Console.WriteLine(cycle + " | P" + p.processNo + " | " + p.CPUBurstCount + "/" + p.CPUBurstTime); /*+ " | " + percentCPU + "%");*/
        }

        //calculate the maximum runtime of the processes in the process profile
        private int calcRunTime()
        {
            int sum = 0;

            foreach (ProcessProfile pp in targetProfile.listOfProfiles)
            {
                sum = sum + pp.CPUBurstTime;
            }

            //Console.WriteLine("Calculated max runtime is: " + sum);

            return sum;
        }

        //Average the integers in the list
        private double average(List<int> l)
        {
            int sum = 0;

            foreach (int value in l)
            {
                sum = sum + value;
            }

            double average = sum / l.Count;

            return average;
        }
    }
}
 