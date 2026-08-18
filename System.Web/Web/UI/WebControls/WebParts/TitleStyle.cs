using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006FD RID: 1789
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TitleStyle : TableItemStyle
	{
		// Token: 0x06005762 RID: 22370 RVA: 0x00160D1A File Offset: 0x0015FD1A
		public TitleStyle()
		{
			this.Wrap = false;
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06005763 RID: 22371 RVA: 0x00160D29 File Offset: 0x0015FD29
		// (set) Token: 0x06005764 RID: 22372 RVA: 0x00160D31 File Offset: 0x0015FD31
		[DefaultValue(false)]
		public override bool Wrap
		{
			get
			{
				return base.Wrap;
			}
			set
			{
				base.Wrap = value;
			}
		}
	}
}
