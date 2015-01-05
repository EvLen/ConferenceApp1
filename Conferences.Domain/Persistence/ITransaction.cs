using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain.Persistence
{
    public interface ITransaction : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
