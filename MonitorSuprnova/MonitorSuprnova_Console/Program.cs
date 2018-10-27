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
            Console.Read();
            while (true)
            {
                Console.WriteLine(DataAccess.DA.GetData());

                Console.Read();
            }
            
        }
    }
}
