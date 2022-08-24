using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMessengerCoreAPI.Services
{
    public interface IRGDialogsService
    {
        public Guid FindDialog(List<Guid> li);
    }
}
