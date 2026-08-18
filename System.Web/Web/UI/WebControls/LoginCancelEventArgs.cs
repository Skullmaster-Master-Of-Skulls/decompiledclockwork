using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005D4 RID: 1492
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginCancelEventArgs : EventArgs
	{
		// Token: 0x0600492A RID: 18730 RVA: 0x0012A73B File Offset: 0x0012973B
		public LoginCancelEventArgs() : this(false)
		{
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x0012A744 File Offset: 0x00129744
		public LoginCancelEventArgs(bool cancel)
		{
			this._cancel = cancel;
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x0600492C RID: 18732 RVA: 0x0012A753 File Offset: 0x00129753
		// (set) Token: 0x0600492D RID: 18733 RVA: 0x0012A75B File Offset: 0x0012975B
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x04002B19 RID: 11033
		private bool _cancel;
	}
}
