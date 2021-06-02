using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{
    public class ErrorResourceModel
    {
        public bool Success => false;
        public List<string> Messages { get; private set; }
        public ErrorResourceModel(List<string> messages)
        {
            this.Messages = messages ?? new List<string>();
        }
        public ErrorResourceModel(string message)
        {
            this.Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }
    }
}
