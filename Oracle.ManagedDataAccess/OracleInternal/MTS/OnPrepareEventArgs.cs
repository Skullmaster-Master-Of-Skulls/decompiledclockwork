using System;
using System.Transactions;

namespace OracleInternal.MTS
{
	// Token: 0x02000134 RID: 308
	internal class OnPrepareEventArgs : EventArgs
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x0008BE48 File Offset: 0x0008A048
		internal OnPrepareEventArgs(PreparingEnlistment enlistment)
		{
			this.m_preparingEnlistment = enlistment;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0008BE58 File Offset: 0x0008A058
		internal PreparingEnlistment Enlistment
		{
			get
			{
				return this.m_preparingEnlistment;
			}
		}

		// Token: 0x04000DA0 RID: 3488
		private PreparingEnlistment m_preparingEnlistment;
	}
}
