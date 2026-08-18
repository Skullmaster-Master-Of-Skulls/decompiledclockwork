using System;
using System.ComponentModel;
using System.Data;
using System.Web.Routing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004BE RID: 1214
	[DefaultProperty("RouteKey")]
	public class RouteParameter : Parameter
	{
		// Token: 0x06003C8F RID: 15503 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public RouteParameter()
		{
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000C455D File Offset: 0x000C275D
		public RouteParameter(string name, string routeKey) : base(name)
		{
			this.RouteKey = routeKey;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000C456D File Offset: 0x000C276D
		public RouteParameter(string name, DbType dbType, string routeKey) : base(name, dbType)
		{
			this.RouteKey = routeKey;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000C457E File Offset: 0x000C277E
		public RouteParameter(string name, TypeCode type, string routeKey) : base(name, type)
		{
			this.RouteKey = routeKey;
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000C458F File Offset: 0x000C278F
		protected RouteParameter(RouteParameter original) : base(original)
		{
			this.RouteKey = original.RouteKey;
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x000C45A4 File Offset: 0x000C27A4
		// (set) Token: 0x06003C95 RID: 15509 RVA: 0x000C45D1 File Offset: 0x000C27D1
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("RouteParameter_RouteKey")]
		public string RouteKey
		{
			get
			{
				object obj = base.ViewState["RouteKey"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.RouteKey != value)
				{
					base.ViewState["RouteKey"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000C45F8 File Offset: 0x000C27F8
		protected override Parameter Clone()
		{
			return new RouteParameter(this);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000C4600 File Offset: 0x000C2800
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null || control == null)
			{
				return null;
			}
			RouteData routeData = control.Page.RouteData;
			if (routeData == null)
			{
				return null;
			}
			return routeData.Values[this.RouteKey];
		}
	}
}
