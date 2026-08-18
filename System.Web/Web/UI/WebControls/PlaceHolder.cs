using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200061A RID: 1562
	[ControlBuilder(typeof(PlaceHolderControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PlaceHolder : Control
	{
		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x0013AD18 File Offset: 0x00139D18
		// (set) Token: 0x06004D96 RID: 19862 RVA: 0x0013AD20 File Offset: 0x00139D20
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}
	}
}
