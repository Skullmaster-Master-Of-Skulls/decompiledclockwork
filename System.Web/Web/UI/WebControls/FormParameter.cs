using System;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000580 RID: 1408
	[DefaultProperty("FormField")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormParameter : Parameter
	{
		// Token: 0x06004506 RID: 17670 RVA: 0x0011B8C3 File Offset: 0x0011A8C3
		public FormParameter()
		{
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x0011B8CB File Offset: 0x0011A8CB
		public FormParameter(string name, string formField) : base(name)
		{
			this.FormField = formField;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x0011B8DB File Offset: 0x0011A8DB
		public FormParameter(string name, DbType dbType, string formField) : base(name, dbType)
		{
			this.FormField = formField;
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x0011B8EC File Offset: 0x0011A8EC
		public FormParameter(string name, TypeCode type, string formField) : base(name, type)
		{
			this.FormField = formField;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x0011B8FD File Offset: 0x0011A8FD
		protected FormParameter(FormParameter original) : base(original)
		{
			this.FormField = original.FormField;
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x0011B914 File Offset: 0x0011A914
		// (set) Token: 0x0600450C RID: 17676 RVA: 0x0011B941 File Offset: 0x0011A941
		[DefaultValue("")]
		[WebSysDescription("FormParameter_FormField")]
		[WebCategory("Parameter")]
		public string FormField
		{
			get
			{
				object obj = base.ViewState["FormField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.FormField != value)
				{
					base.ViewState["FormField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x0011B968 File Offset: 0x0011A968
		protected override Parameter Clone()
		{
			return new FormParameter(this);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x0011B970 File Offset: 0x0011A970
		protected override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			return context.Request.Form[this.FormField];
		}
	}
}
