using iMessengerCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMessengerCoreAPI.Services
{
    public class RGDialogsService : IRGDialogsService
    {
        public List<Guid> FindDialog(List<Guid> li)
        {
            RGDialogsClients dialogClients = new RGDialogsClients();

            var RGDialogsClientsList = dialogClients.Init();
            var possibleDialogsList = RGDialogsClientsList.Select(x=>x.IDRGDialog).Distinct().ToList();
            var foundEntities = RGDialogsClientsList.Where(x => li.Contains(x.IDClient)).ToList();

            var result = new List<Guid>() { Guid.Empty };

            foreach (var pos in possibleDialogsList) 
            {
                var p = foundEntities.Where(x => x.IDRGDialog == pos).ToList();
                bool t = false;

                if (p.Any())
                {
                    t = p.Select(x => x.IDClient).ToList().SequenceEqual(li);
                }
                if (t)
                {
                    result.Add(pos);
                }
            }
            if (result.Count>1)
            {
                result.Remove(Guid.Empty);
            }

            return result;
        }
    }
}
