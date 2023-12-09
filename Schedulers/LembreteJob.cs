using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using projetoCurricular_v002.Models;

namespace projetoCurricular_v002.Schedulers
{
    [DisallowConcurrentExecution]
    public class LembreteJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            /*---------- Tarefa do Lembrete de aniversario ----------*/
            LembreteHelper lembreteHelper = new LembreteHelper(Program._ligacao);
            lembreteHelper.CriarLembreteAniversario();
            return Task.CompletedTask;
        }
        
            


    }
}
