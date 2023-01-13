using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Application.Services
{
   public interface IPasswordHasherService
    {
        string Hash(string password);

        bool VerifyPassword(string passwordHash, string password);
    }
}
