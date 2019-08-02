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

		public void StartGame() {
			Clients.All.startGame();

			StartRound();
		}

		public void StartRound() {
			var priceFixer = Game.Players[Game.CurrentPriceFixer].Name;
			Clients.All.selectFixer(priceFixer);
		}

		public void StartBidding() {
			var priceFixer = Game.Players[Game.CurrentPriceFixer].Name;
			Clients.All.startBidding(priceFixer);
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