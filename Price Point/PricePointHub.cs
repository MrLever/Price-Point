using System;
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
	}
}