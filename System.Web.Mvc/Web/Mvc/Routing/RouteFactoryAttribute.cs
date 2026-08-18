using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public abstract class RouteFactoryAttribute : Attribute, IDirectRouteFactory
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002F62 File Offset: 0x00001162
		protected RouteFactoryAttribute(string template)
		{
			this._template = template;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F71 File Offset: 0x00001171
		public string Template
		{
			get
			{
				return this._template;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F79 File Offset: 0x00001179
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002F81 File Offset: 0x00001181
		public string Name { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F8A File Offset: 0x0000118A
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002F92 File Offset: 0x00001192
		public int Order { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002F9B File Offset: 0x0000119B
		public virtual RouteValueDictionary Defaults
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F9E File Offset: 0x0000119E
		public virtual RouteValueDictionary Constraints
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002FA1 File Offset: 0x000011A1
		public virtual RouteValueDictionary DataTokens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FA4 File Offset: 0x000011A4
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			RouteValueDictionary defaults = directRouteBuilder.Defaults;
			if (defaults == null)
			{
				directRouteBuilder.Defaults = this.Defaults;
			}
			else
			{
				RouteValueDictionary defaults2 = this.Defaults;
				if (defaults2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in defaults2)
					{
						defaults[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			RouteValueDictionary constraints = directRouteBuilder.Constraints;
			if (constraints == null)
			{
				directRouteBuilder.Constraints = this.Constraints;
			}
			else
			{
				RouteValueDictionary constraints2 = this.Constraints;
				if (constraints2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair2 in constraints2)
					{
						constraints[keyValuePair2.Key] = keyValuePair2.Value;
					}
				}
			}
			RouteValueDictionary dataTokens = directRouteBuilder.DataTokens;
			if (dataTokens == null)
			{
				directRouteBuilder.DataTokens = this.DataTokens;
			}
			else
			{
				RouteValueDictionary dataTokens2 = this.DataTokens;
				if (dataTokens2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair3 in dataTokens2)
					{
						dataTokens[keyValuePair3.Key] = keyValuePair3.Value;
					}
				}
			}
			return directRouteBuilder.Build();
		}

		// Token: 0x04000019 RID: 25
		private readonly string _template;
	}
}
