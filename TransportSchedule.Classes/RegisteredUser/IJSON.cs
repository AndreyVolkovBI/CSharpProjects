using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.RegisteredUser
{
    public interface IJSON
    {
        void WriteToFile(User user, int id);

        User ReadFile(int id);

        List<User> DownloadUsers();

        int CountUsers();

        List<T> Read<T>(List<T> fileName, string line);
    }
}
