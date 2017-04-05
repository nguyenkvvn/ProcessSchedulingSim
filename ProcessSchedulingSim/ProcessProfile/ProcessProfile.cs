using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSchedulingSim
{
    //This class represents one row of a Process Profile in the Processes Profile csv input.
    class ProcessProfile
    {
        public int Priority;
        public int SubmissionTime;
        public int CPUBurstTime;

        public int CPUBurstCount;
        Boolean done;

        public int processNo;
        public int clockStart;
        public int clockEnd;

        public ProcessProfile(int p, int st, int cbt)
        {
            Priority = p;
            SubmissionTime = st;
            CPUBurstTime = cbt;
            done = false;
            CPUBurstCount = 0;
        }
    }
}
