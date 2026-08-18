using System;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200050C RID: 1292
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CreateUserErrorEventArgs : EventArgs
	{
		// Token: 0x06003F05 RID: 16133 RVA: 0x00105F04 File Offset: 0x00104F04
		public CreateUserErrorEventArgs(MembershipCreateStatus s)
		{
			this._error = s;
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x00105F13 File Offset: 0x00104F13
		// (set) Token: 0x06003F07 RID: 16135 RVA: 0x00105F1B File Offset: 0x00104F1B
		public MembershipCreateStatus CreateUserError
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}

		// Token: 0x040027AC RID: 10156
		private MembershipCreateStatus _error;
	}
}
