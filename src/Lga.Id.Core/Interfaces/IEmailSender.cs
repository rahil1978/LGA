using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lga.Id.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
