using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.RegisteredUser
{
    public interface IUserLogic
    {
        int IsUserExit(string email, string password);

        User IsUserExist(string email);

        string GetHash(string password);

        User CreateNewUser(string fullName, string email, string password);
    }
}
