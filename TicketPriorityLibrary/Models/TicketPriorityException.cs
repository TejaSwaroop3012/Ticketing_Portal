using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPriorityLibrary.Models
{
    public class TicketPriorityException : Exception
    {
        public TicketPriorityException(string errMsg) : base(errMsg) { }
    }
}
