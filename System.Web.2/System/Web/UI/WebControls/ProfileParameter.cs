using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A4 RID: 1188
	[DefaultProperty("PropertyName")]
	public class ProfileParameter : Parameter
	{
		// Token: 0x06003B8C RID: 15244 RVA: 0x00090DC4 File Offset: 0x0008EFC4
		public ProfileParameter()
		{
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000C1880 File Offset: 0x000BFA80
		public ProfileParameter(string name, string propertyName) : base(name)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000C1890 File Offset: 0x000BFA90
		public ProfileParameter(string name, TypeCode type, string propertyName) : base(name, type)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000C18A1 File Offset: 0x000BFAA1
		public ProfileParameter(string name, DbType dbType, string propertyName) : base(name, dbType)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000C18B2 File Offset: 0x000BFAB2
		protected ProfileParameter(ProfileParameter original) : base(original)
		{
			this.PropertyName = original.PropertyName;
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000C18C8 File Offset: 0x000BFAC8
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000C18F5 File Offset: 0x000BFAF5
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("ProfileParameter_PropertyName")]
		public string PropertyName
		{
			get
			{
				object obj = base.ViewState["PropertyName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.PropertyName != value)
				{
					base.ViewState["PropertyName"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000C191C File Offset: 0x000BFB1C
		protected override Parameter Clone()
		{
			return new ProfileParameter(this);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000C1924 File Offset: 0x000BFB24
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Profile == null)
			{
				return null;
			}
			return DataBinder.Eval(context.Profile, this.PropertyName);
		}
	}
}
