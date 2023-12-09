using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Schedulers
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }
        public Type JobType { get; }
        public string CronExpression { get; }
    }
}
