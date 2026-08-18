using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public abstract class RouteFactoryAttribute : Attribute, IDirectRouteFactory
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000035F9 File Offset: 0x000017F9
		protected RouteFactoryAttribute(string template)
		{
			this._template = template;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003608 File Offset: 0x00001808
		public string Template
		{
			get
			{
				return this._template;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003610 File Offset: 0x00001810
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003618 File Offset: 0x00001818
		public string Name { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003621 File Offset: 0x00001821
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003629 File Offset: 0x00001829
		public int Order { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003632 File Offset: 0x00001832
		public virtual IDictionary<string, object> Defaults
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003635 File Offset: 0x00001835
		public virtual IDictionary<string, object> Constraints
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003638 File Offset: 0x00001838
		public virtual IDictionary<string, object> DataTokens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000363C File Offset: 0x0000183C
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			IDictionary<string, object> defaults = directRouteBuilder.Defaults;
			if (defaults == null)
			{
				directRouteBuilder.Defaults = this.Defaults;
			}
			else
			{
				IDictionary<string, object> defaults2 = this.Defaults;
				if (defaults2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in defaults2)
					{
						defaults[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			IDictionary<string, object> constraints = directRouteBuilder.Constraints;
			if (constraints == null)
			{
				directRouteBuilder.Constraints = this.Constraints;
			}
			else
			{
				IDictionary<string, object> constraints2 = this.Constraints;
				if (constraints2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair2 in constraints2)
					{
						constraints[keyValuePair2.Key] = keyValuePair2.Value;
					}
				}
			}
			IDictionary<string, object> dataTokens = directRouteBuilder.DataTokens;
			if (dataTokens == null)
			{
				directRouteBuilder.DataTokens = this.DataTokens;
			}
			else
			{
				IDictionary<string, object> dataTokens2 = this.DataTokens;
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

		// Token: 0x04000020 RID: 32
		private readonly string _template;
	}
}
