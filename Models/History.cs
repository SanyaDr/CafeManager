using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManager3.Models
{
    public class History
    {
        public int Id { get; set; }
        public int AccountId { set; get; }
        public string Date { set; get; } = null!;
        public int Amount { set; get; }
    }
}
