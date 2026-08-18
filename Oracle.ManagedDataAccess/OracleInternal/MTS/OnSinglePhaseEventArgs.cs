using System;
using System.Transactions;

namespace OracleInternal.MTS
{
	// Token: 0x02000137 RID: 311
	internal class OnSinglePhaseEventArgs : EventArgs
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0008BE84 File Offset: 0x0008A084
		internal OnSinglePhaseEventArgs(SinglePhaseEnlistment enlistment)
		{
			this.m_singlePhaseEnlistment = enlistment;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0008BE94 File Offset: 0x0008A094
		internal SinglePhaseEnlistment Enlistment
		{
			get
			{
				return this.m_singlePhaseEnlistment;
			}
		}

		// Token: 0x04000DA2 RID: 3490
		private SinglePhaseEnlistment m_singlePhaseEnlistment;
	}
}
