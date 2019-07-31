﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Price_Point
{
	public class PricePointHub : Hub
	{
		public void Hello() {
			Clients.All.hello();
		}

		public void Send(string name, string message) {
			Clients.All.broadcastMessage(name, message);
		}

		public void Join(string name) {
			Clients.All.processJoin(name);
		}
	}
}