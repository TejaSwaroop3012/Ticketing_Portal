using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLibrary.Models
{
    public class TicketException:Exception
    {
        public TicketException(string strMsg):base(strMsg)
        {
            
        }
    }
}
