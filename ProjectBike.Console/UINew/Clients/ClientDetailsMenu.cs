using ProjectBike.Console.UINew.Core;
using ProjectBike.ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBike.Console.UINew.Clients;

public class ClientDetailMenu : MenuBase
{
    private int _clientId;
    private readonly IClientService _clientSvc;

    public ClientDetailMenu(int clientId, IClientService clientSvc)
    {
        clientId = _clientId;
        clientSvc = _clientSvc;
    }
}