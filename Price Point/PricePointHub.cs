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
			Game.ResetBidStates();
			var priceFixer = Game.Players[Game.CurrentPriceFixer].Name;
			Clients.All.selectFixer(priceFixer);
		}

		public void PostItemForSale(string url, string itemName) {
			Clients.All.receiveItemForSale(url, itemName);
		}

		public void StartBidding(string itemPrice) {
			var priceFixer = Game.Players[Game.CurrentPriceFixer].Name;
			Game.CurrentItemPrice = Convert.ToDecimal(itemPrice);
			Clients.All.startBidding(priceFixer);
		}

		public void PostBid(string name, string bid) {
			//Record bid
			foreach (var player in Game.Players) {
				if (player.Name == name) {
					player.PlaceBid(Convert.ToDecimal(bid));
					break;
				}
			}

			//Check for end of bidding
			if (Game.BiddingOver()) {
				EndBidding();
			}
		}

		public void EndBidding() {
			string winner = Game.DecideWinner();
			Clients.All.endBidding();
			Clients.All.declareWinner(winner);

			Game.NextTurn();
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