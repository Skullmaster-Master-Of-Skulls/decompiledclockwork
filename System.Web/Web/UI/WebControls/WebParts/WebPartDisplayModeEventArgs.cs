using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000723 RID: 1827
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebPartDisplayModeEventArgs : EventArgs
	{
		// Token: 0x06005895 RID: 22677 RVA: 0x00163F29 File Offset: 0x00162F29
		public WebPartDisplayModeEventArgs(WebPartDisplayMode oldDisplayMode)
		{
			this._oldDisplayMode = oldDisplayMode;
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x00163F38 File Offset: 0x00162F38
		// (set) Token: 0x06005897 RID: 22679 RVA: 0x00163F40 File Offset: 0x00162F40
		public WebPartDisplayMode OldDisplayMode
		{
			get
			{
				return this._oldDisplayMode;
			}
			set
			{
				this._oldDisplayMode = value;
			}
		}

		// Token: 0x04002FEB RID: 12267
		private WebPartDisplayMode _oldDisplayMode;
	}
}
