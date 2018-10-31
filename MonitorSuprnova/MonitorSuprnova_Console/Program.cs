using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorSuprnova_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess.WorkerModel.OnStart();
            Task.Run(async() =>
            {
                while (true)
                {
                    await DataAccess.WorkerModel.ProcessData();

                    await Task.Delay(10000);
                }
            });
            Console.Read();
        }
    }
}
