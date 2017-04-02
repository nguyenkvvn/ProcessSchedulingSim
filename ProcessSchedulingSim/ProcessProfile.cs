using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSchedulingSim
{
    class ProcessProfile
    {
        int Priority;
        int SubmissionTime;
        int CPUBurstTime;

        public ProcessProfile(int p, int st, int cbt)
        {
            Priority = p;
            SubmissionTime = st;
            CPUBurstTime = cbt;
        }
    }
}
