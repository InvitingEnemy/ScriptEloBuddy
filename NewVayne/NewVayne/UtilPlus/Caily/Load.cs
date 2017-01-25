using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVayne.UtilPlus.Caily
{
    class Load
    {
        public static void Execute()
        {
            //AEnemy 
            AEnemy.Execute();
            //MenuManager
            MenuManager.Load();
            //EventManager
            EventManager.Load();
        }
    }
}
   