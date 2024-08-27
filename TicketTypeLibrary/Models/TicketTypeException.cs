using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTypeLibrary.Models
{
    public class TicketTypeException : Exception
    {
        public TicketTypeException(string msg) : base(msg) { }
    }
}
