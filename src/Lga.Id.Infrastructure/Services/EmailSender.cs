using Lga.Id.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lga.Id.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
          //Raheel: Just to demonstrate how we will add external services
            return Task.CompletedTask;
        }
    }
}
