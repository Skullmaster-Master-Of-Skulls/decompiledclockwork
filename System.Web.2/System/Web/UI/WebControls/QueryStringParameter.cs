using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A7 RID: 1191
	[DefaultProperty("QueryStringField")]
	public class QueryStringParameter : Parameter
	{
		// Token: 0x06003B99 RID: 15257 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public QueryStringParameter()
		{
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000C1B02 File Offset: 0x000BFD02
		public QueryStringParameter(string name, string queryStringField) : base(name)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000C1B12 File Offset: 0x000BFD12
		public QueryStringParameter(string name, DbType dbType, string queryStringField) : base(name, dbType)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000C1B23 File Offset: 0x000BFD23
		public QueryStringParameter(string name, TypeCode type, string queryStringField) : base(name, type)
		{
			this.QueryStringField = queryStringField;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000C1B34 File Offset: 0x000BFD34
		protected QueryStringParameter(QueryStringParameter original) : base(original)
		{
			this.QueryStringField = original.QueryStringField;
			this.ValidateInput = original.ValidateInput;
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000C1B58 File Offset: 0x000BFD58
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000C1B85 File Offset: 0x000BFD85
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

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000C1BAC File Offset: 0x000BFDAC
		protected override Parameter Clone()
		{
			return new QueryStringParameter(this);
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000C1BB4 File Offset: 0x000BFDB4
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			NameValueCollection nameValueCollection = this.ValidateInput ? context.Request.QueryString : context.Request.Unvalidated.QueryString;
			return nameValueCollection[this.QueryStringField];
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000C1C00 File Offset: 0x000BFE00
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000C1C29 File Offset: 0x000BFE29
		[WebCategory("Behavior")]
		[WebSysDescription("Parameter_ValidateInput")]
		[DefaultValue(true)]
		public bool ValidateInput
		{
			get
			{
				object obj = base.ViewState["ValidateInput"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (this.ValidateInput != value)
				{
					base.ViewState["ValidateInput"] = value;
					base.OnParameterChanged();
				}
			}
		}
	}
}
