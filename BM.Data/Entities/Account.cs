using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    public class Account : BaseEntity
    {
        public int AccountNo { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public string Note { get; set; }
    }
}
