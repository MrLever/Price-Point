using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Price_Point.Startup))]

namespace Price_Point
{
	public class Startup
	{
		public void Configuration(IAppBuilder app) {
			app.MapSignalR();
		}
	}
}
