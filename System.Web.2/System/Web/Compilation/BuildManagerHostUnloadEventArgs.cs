using System;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x0200082B RID: 2091
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class BuildManagerHostUnloadEventArgs : EventArgs
	{
		// Token: 0x060063E9 RID: 25577 RVA: 0x0015E0FD File Offset: 0x0015C2FD
		public BuildManagerHostUnloadEventArgs(ApplicationShutdownReason reason)
		{
			this._reason = reason;
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x0015E10C File Offset: 0x0015C30C
		public ApplicationShutdownReason Reason
		{
			get
			{
				return this._reason;
			}
		}

		// Token: 0x040033BC RID: 13244
		private ApplicationShutdownReason _reason;
	}
}
