using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes
{
    public class Factory
    {
        static Factory _factory;

        public static Factory Default
        {
            get
            {
                if (_factory == null)
                    _factory = new Factory();
                return _factory;
            }
        }

        IRepository _fileRepository = new FileRepository();
        IRepository _dbRepository = new DatabaseRepository();

        public IRepository GetRepository<T>()
        {
            if (typeof(T) == typeof(FileRepository))
                return _fileRepository;
            if (typeof(T) == typeof(DatabaseRepository))
                return _dbRepository;
            return null;
        }
    }
}
