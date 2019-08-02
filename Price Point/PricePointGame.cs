using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Price_Point {
	public class PricePointGame {
		public List<Client> Players { get; set; }
		public int CurrentPriceFixer { get; set; }
		public decimal CurrentItemPrice { get; set; }
		public bool GameStarted { get; set; }

		public PricePointGame() {
			Players = new List<Client>();
			CurrentPriceFixer = 0;
			GameStarted = false;
		}

		public void NextTurn() {
			CurrentPriceFixer = (CurrentPriceFixer + 1) % Players.Count();
		}

		public void ResetBidStates() {
			foreach (var player in Players) {
				player.BidPlaced = false;
				player.Bid = decimal.MaxValue;
			}
		}

		public bool BiddingOver() {
			foreach (var player in Players) {
				if (player.BidPlaced == false && player.Name != Players[CurrentPriceFixer].Name) {
					return false;
				}
			}

			return true;
		}

		public string DecideWinner() {
			string winner = "No one! Everyone was over :(";
			decimal bestDiff = decimal.MaxValue;

			foreach(var player in Players) {
				decimal diff = CurrentItemPrice - player.Bid;
				if (diff < 0) continue;

				if (diff < bestDiff) {
					winner = player.Name;
					bestDiff = diff;
					continue;
				}

				if (diff == bestDiff) {
					winner += " and " + player.Name;
				}
			}

			return winner;
		}
	}
}