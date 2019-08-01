using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Price_Point
{
	public class Client
	{
		private string Name { get; }
		private int Score { get; set; }

		public Client(string name) {
			Name = name;
			Score = 0;
		}

		void AddPoint() {
			Score++;
		}
	}
}