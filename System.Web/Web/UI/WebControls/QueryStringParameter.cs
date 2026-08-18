using System;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000620 RID: 1568
	[DefaultProperty("QueryStringField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class QueryStringParameter : Parameter
	{
		// Token: 0x06004DCA RID: 19914 RVA: 0x0013B924 File Offset: 0x0013A924
		public QueryStringParameter()
		{
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0013B92C File Offset: 0x0013A92C
		public QueryStringParameter(string name, string queryStringField) : base(name)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0013B93C File Offset: 0x0013A93C
		public QueryStringParameter(string name, DbType dbType, string queryStringField) : base(name, dbType)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0013B94D File Offset: 0x0013A94D
		public QueryStringParameter(string name, TypeCode type, string queryStringField) : base(name, type)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0013B95E File Offset: 0x0013A95E
		protected QueryStringParameter(QueryStringParameter original) : base(original)
		{
			this.QueryStringField = original.QueryStringField;
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x0013B974 File Offset: 0x0013A974
		// (set) Token: 0x06004DD0 RID: 19920 RVA: 0x0013B9A1 File Offset: 0x0013A9A1
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("QueryStringParameter_QueryStringField")]
		public string QueryStringField
		{
			get
			{
				object obj = base.ViewState["QueryStringField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.QueryStringField != value)
				{
					base.ViewState["QueryStringField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0013B9C8 File Offset: 0x0013A9C8
		protected override Parameter Clone()
		{
			return new QueryStringParameter(this);
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0013B9D0 File Offset: 0x0013A9D0
		protected override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			return context.Request.QueryString[this.QueryStringField];
		}
	}
}
