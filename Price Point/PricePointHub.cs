using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Price_Point {
	public class PricePointHub : Hub {
		private static PricePointGame Game = new PricePointGame();

		public PricePointHub() : base() {

		}

		public void Hello() {
			Clients.All.hello();
		}

		public void Send(string name, string message) {
			Clients.All.broadcastMessage(name, message);
		}

		public void Join(string name) {
			Clients.All.prepJoin();

			Game.Players.Add(new Client(name));
			foreach (var player in Game.Players) {
				Clients.All.processJoin(player.Name);
			}
			
		}
	}
}