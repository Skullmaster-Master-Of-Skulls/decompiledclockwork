using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A7 RID: 935
	[DefaultProperty("CookieName")]
	public class CookieParameter : Parameter
	{
		// Token: 0x06002C7F RID: 11391 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public CookieParameter()
		{
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x00091002 File Offset: 0x0008F202
		public CookieParameter(string name, string cookieName) : base(name)
		{
			this.CookieName = cookieName;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00091012 File Offset: 0x0008F212
		public CookieParameter(string name, DbType dbType, string cookieName) : base(name, dbType)
		{
			this.CookieName = cookieName;
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x00091023 File Offset: 0x0008F223
		public CookieParameter(string name, TypeCode type, string cookieName) : base(name, type)
		{
			this.CookieName = cookieName;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x00091034 File Offset: 0x0008F234
		protected CookieParameter(CookieParameter original) : base(original)
		{
			this.CookieName = original.CookieName;
			this.ValidateInput = original.ValidateInput;
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x00091058 File Offset: 0x0008F258
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x00091085 File Offset: 0x0008F285
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("CookieParameter_CookieName")]
		public string CookieName
		{
			get
			{
				object obj = base.ViewState["CookieName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.CookieName != value)
				{
					base.ViewState["CookieName"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000910AC File Offset: 0x0008F2AC
		protected override Parameter Clone()
		{
			return new CookieParameter(this);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000910B4 File Offset: 0x0008F2B4
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			HttpCookieCollection httpCookieCollection = this.ValidateInput ? context.Request.Cookies : context.Request.Unvalidated.Cookies;
			HttpCookie httpCookie = httpCookieCollection[this.CookieName];
			if (httpCookie == null)
			{
				return null;
			}
			return httpCookie.Value;
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x0009110C File Offset: 0x0008F30C
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x00091135 File Offset: 0x0008F335
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
