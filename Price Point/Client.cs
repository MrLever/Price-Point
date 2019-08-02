using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Price_Point
{
	public class Client
	{
		public string Name { get; }
		public int Score { get; set; }
		public bool BidPlaced { get; set; }
		public decimal Bid { get; set; }
		public Client(string name) {
			Name = name;
			Score = 0;
			BidPlaced = false;
		}

		public void PlaceBid(decimal bid) {
			BidPlaced = true;
			Bid = bid;
		}

		public void AddPoint() {
			Score++;
		}
	}
}