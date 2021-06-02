using System;

namespace Lga.Id.Core.Entities
{
    public class State : BaseEntity
    {   
        public string StateName { get; set; }
        public Decimal Median { get; set; }
    }
}