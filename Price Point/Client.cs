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

		public Client(string name) {
			Name = name;
			Score = 0;
		}

		void AddPoint() {
			Score++;
		}
	}
}