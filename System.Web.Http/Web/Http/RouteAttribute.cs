using System;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class RouteAttribute : Attribute, IDirectRouteFactory, IHttpRouteInfoProvider
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000A0AC File Offset: 0x000082AC
		public RouteAttribute()
		{
			this.Template = string.Empty;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A0BF File Offset: 0x000082BF
		public RouteAttribute(string template)
		{
			if (template == null)
			{
				throw Error.ArgumentNull("template");
			}
			this.Template = template;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A0DC File Offset: 0x000082DC
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public string Name { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000A0ED File Offset: 0x000082ED
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000A0F5 File Offset: 0x000082F5
		public int Order { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A0FE File Offset: 0x000082FE
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000A106 File Offset: 0x00008306
		public string Template { get; private set; }

		// Token: 0x06000333 RID: 819 RVA: 0x0000A110 File Offset: 0x00008310
		RouteEntry IDirectRouteFactory.CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			return directRouteBuilder.Build();
		}
	}
}
