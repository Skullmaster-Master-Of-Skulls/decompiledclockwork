using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D6 RID: 1494
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		// Token: 0x0600379D RID: 14237 RVA: 0x000F0ABB File Offset: 0x000EECBB
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit) : this(commit, true)
		{
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000F0AC5 File Offset: 0x000EECC5
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.commit = commit;
			this.lastTransaction = lastTransaction;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000F0ADB File Offset: 0x000EECDB
		public bool TransactionCommitted
		{
			get
			{
				return this.commit;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000F0AE3 File Offset: 0x000EECE3
		public bool LastTransaction
		{
			get
			{
				return this.lastTransaction;
			}
		}

		// Token: 0x04002B01 RID: 11009
		private bool commit;

		// Token: 0x04002B02 RID: 11010
		private bool lastTransaction;
	}
}
