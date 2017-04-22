using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//referring to StackOverflow tutorial
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace ProcessSchedulingSim
{
    //This class is a wrapper for the input CSV, and will process the CSV into a list of values based on index. This is to simplify having redundant code read in and out from a CSV.
    class ProcessProfiles
    {
        List<int> LPriority;
        List<int> LSubmissionTime;
        List<int> LCPUBurstTime;
        public List<ProcessProfile> listOfProfiles;

        public ProcessProfiles(string pathToInput)
        {
            LPriority = new List<int>();
            LSubmissionTime = new List<int>();
            LCPUBurstTime = new List<int>();

            listOfProfiles = new List<ProcessProfile>();
            interpret(pathToInput);
        }

        //interpret will read into the CSV
        private Boolean interpret(string path)
        {
            //referring to StackOverflow tutorial on reading CSV
            using (TextFieldParser parser = new TextFieldParser(@path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //for each column
                    int csvCounter = 0;
                    //Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        switch (csvCounter)
                        {
                            //Priority Column
                            case 0:
                                csvCounter++;
                                LPriority.Add(System.Convert.ToInt32(field));
                                //VERBOSE: Console.Write("Row Parsed: " + field);
                                break;
                            //Submission Time Column
                            case 1:
                                csvCounter++;
                                LSubmissionTime.Add(System.Convert.ToInt32(field));
                                //VERBOSE: Console.Write("," + field);
                                break;
                            //CPU Burst Time Column
                            case 2:
                                csvCounter = 0;
                                LCPUBurstTime.Add(System.Convert.ToInt32(field));
                                //VERBOSE: Console.WriteLine("," + field);
                                break;
                        }
                    }
                }
            }

            //process the seperated values into one ProcessProfile object
            for (int i = 0; i < LPriority.Count; i++)
            {
                ProcessProfile pp = new ProcessProfile(LPriority[i], LSubmissionTime[i], LCPUBurstTime[i]);

                pp.processNo = i;

                //drop it into the list of profiles
                listOfProfiles.Add(pp);
            }

            return true;
        }

        public override string ToString()
        {
            string printme = "ProcessNO\tPriority\tSubmitTime\tCPUBurstTime\tCompleteOrder\n";

            foreach (ProcessProfile p in listOfProfiles)
            {
                printme = printme + p.processNo + "\t" + p.Priority + "\t" + p.SubmissionTime + "\t" + p.CPUBurstTime + "\t" + p.completeOrder + "\n";
            }

            return printme;
        }
    }

    class ProcessProfile
    {
        public int Priority;
        public int SubmissionTime;
        public int CPUBurstTime;

        public int CPUBurstCount;
        Boolean done;
        public int completeOrder;

        public int processNo;
        public int clockStart;
        public int clockEnd;

        public ProcessProfile(int p, int st, int cbt)
        {
            Priority = p;
            SubmissionTime = st;
            CPUBurstTime = cbt;
            done = false;
            completeOrder = 0;
            CPUBurstCount = 0;
        }
    }
}
