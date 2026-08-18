using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200065E RID: 1630
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableSectionStyle : Style
	{
		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x0013FEC0 File Offset: 0x0013EEC0
		// (set) Token: 0x06004FB2 RID: 20402 RVA: 0x0013FEE9 File Offset: 0x0013EEE9
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("TableSectionStyle_Visible")]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}
	}
}
