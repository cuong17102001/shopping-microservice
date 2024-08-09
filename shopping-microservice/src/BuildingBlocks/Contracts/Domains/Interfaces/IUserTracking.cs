using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains.Interfaces
{
    public interface IUserTracking
    {
        string CreatedDate { get; set; }
        string LastModifiedDate { get; set; }
    }
}
