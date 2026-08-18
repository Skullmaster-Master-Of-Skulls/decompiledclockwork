using System;
using System.Transactions;

namespace OracleInternal.MTS
{
	// Token: 0x02000135 RID: 309
	internal class OnCommitEventArgs : EventArgs
	{
		// Token: 0x06000C89 RID: 3209 RVA: 0x0008BE60 File Offset: 0x0008A060
		internal OnCommitEventArgs(Enlistment enlistment)
		{
			this.m_enlistment = enlistment;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0008BE70 File Offset: 0x0008A070
		internal Enlistment Enlistment
		{
			get
			{
				return this.m_enlistment;
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private Enlistment m_enlistment;
	}
}
