using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingWebApplication.Messages
{
    public class SomethingHappened : Contracts.SomethingHappened
    {
        public string What { get; set; }

        public DateTime When { get; set; }
    }
}
