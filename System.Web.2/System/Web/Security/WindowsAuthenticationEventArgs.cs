using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005FA RID: 1530
	public sealed class WindowsAuthenticationEventArgs : EventArgs
	{
		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x0010CF5B File Offset: 0x0010B15B
		// (set) Token: 0x06004D62 RID: 19810 RVA: 0x0010CF63 File Offset: 0x0010B163
		public IPrincipal User
		{
			get
			{
				return this._User;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this._User = value;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0010CF6C File Offset: 0x0010B16C
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x0010CF74 File Offset: 0x0010B174
		public WindowsIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0010CF7C File Offset: 0x0010B17C
		public WindowsAuthenticationEventArgs(WindowsIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04002950 RID: 10576
		private IPrincipal _User;

		// Token: 0x04002951 RID: 10577
		private HttpContext _Context;

		// Token: 0x04002952 RID: 10578
		private WindowsIdentity _Identity;
	}
}
