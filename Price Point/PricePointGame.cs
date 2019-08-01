using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Price_Point {
	public class PricePointGame {
		public List<Client> Players { get; set; }
		public int CurrentPriceFixer { get; set; }
		public bool GameStarted { get; set; }

		public PricePointGame() {
			Players = new List<Client>();
			CurrentPriceFixer = 0;
			GameStarted = false;
		}

		public void NextTurn() {
			CurrentPriceFixer = (CurrentPriceFixer + 1) % Players.Count();
		}
	}
}