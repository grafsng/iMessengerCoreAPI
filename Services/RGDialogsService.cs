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
        public Guid FindDialog(List<Guid> li)
        {
            RGDialogsClients dialogClients = new RGDialogsClients();

            var RGDialogsClientsList = dialogClients.Init();
            var possibleDialogsList = RGDialogsClientsList.Select(x=>x.IDRGDialog).Distinct().ToList();
            var foundEntities = RGDialogsClientsList.Where(x => li.Contains(x.IDClient)).ToList();

            var result = Guid.Empty;
            var results = new List<Guid>(){};

            foreach (var pos in possibleDialogsList) 
            {
                var foundDialogs = foundEntities.Where(x => x.IDRGDialog == pos).ToList();
                bool t = false;

                if (foundDialogs.Any())
                {
                    t = foundDialogs.Select(x => x.IDClient).ToList().SequenceEqual(li);
                }
                if (t)
                {
                    results.Add(pos);
                }
            }

            if (results.Count == 1)
            {
                result = results[0];
            }

            return result;
        }
    }
}
