using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatBackend
{
    [ServiceContract]
    public interface IChatBackend
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessage(CompositeType composite);

        void SendMessage(string text);
    }
}
