using System;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200061F RID: 1567
	[DefaultProperty("PropertyName")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ProfileParameter : Parameter
	{
		// Token: 0x06004DC1 RID: 19905 RVA: 0x0013B857 File Offset: 0x0013A857
		public ProfileParameter()
		{
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0013B85F File Offset: 0x0013A85F
		public ProfileParameter(string name, string propertyName) : base(name)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0013B86F File Offset: 0x0013A86F
		public ProfileParameter(string name, TypeCode type, string propertyName) : base(name, type)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0013B880 File Offset: 0x0013A880
		public ProfileParameter(string name, DbType dbType, string propertyName) : base(name, dbType)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0013B891 File Offset: 0x0013A891
		protected ProfileParameter(ProfileParameter original) : base(original)
		{
			this.PropertyName = original.PropertyName;
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x0013B8A8 File Offset: 0x0013A8A8
		// (set) Token: 0x06004DC7 RID: 19911 RVA: 0x0013B8D5 File Offset: 0x0013A8D5
		[DefaultValue("")]
		[WebSysDescription("ProfileParameter_PropertyName")]
		[WebCategory("Parameter")]
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

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0013B8FC File Offset: 0x0013A8FC
		protected override Parameter Clone()
		{
			return new ProfileParameter(this);
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x0013B904 File Offset: 0x0013A904
		protected override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Profile == null)
			{
				return null;
			}
			return DataBinder.Eval(context.Profile, this.PropertyName);
		}
	}
}
