using System;
using System.Web.Mvc.Routing;

namespace System.Web.Mvc
{
	// Token: 0x0200007F RID: 127
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public sealed class RouteAttribute : Attribute, IDirectRouteFactory, IRouteInfoProvider
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000B515 File Offset: 0x00009715
		public RouteAttribute() : this(string.Empty)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B522 File Offset: 0x00009722
		public RouteAttribute(string template)
		{
			if (template == null)
			{
				throw Error.ArgumentNull("template");
			}
			this.Template = template;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000B53F File Offset: 0x0000973F
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000B547 File Offset: 0x00009747
		public string Name { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000B550 File Offset: 0x00009750
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000B558 File Offset: 0x00009758
		public int Order { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000B561 File Offset: 0x00009761
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000B569 File Offset: 0x00009769
		public string Template { get; private set; }

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B574 File Offset: 0x00009774
		RouteEntry IDirectRouteFactory.CreateRoute(DirectRouteFactoryContext context)
		{
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			return directRouteBuilder.Build();
		}
	}
}
