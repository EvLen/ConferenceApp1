using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Tag : Entity
    {
        public virtual string Name { get; set; }
    }
}
