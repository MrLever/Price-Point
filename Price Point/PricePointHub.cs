using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Price_Point
{
	public class PricePointHub : Hub
	{
		private static List<Client> Players = new List<Client>(); 
		public PricePointHub() : base() {

		}

		public void Hello() {
			Clients.All.hello();
		}

		public void Send(string name, string message) {
			Clients.All.broadcastMessage(name, message);
		}

		public void Join(string name) {
			Players.Add(new Client(name));
			Clients.All.processJoin();
		}
	}
}