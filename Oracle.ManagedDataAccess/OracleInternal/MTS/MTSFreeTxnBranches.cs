using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;

namespace OracleInternal.MTS
{
	// Token: 0x0200012D RID: 301
	internal class MTSFreeTxnBranches
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x0008B8E4 File Offset: 0x00089AE4
		internal void ClearBranches()
		{
			List<string> keys = this.m_freeBranchesByUserAuth.GetKeys();
			for (int i = 0; i < keys.Count; i++)
			{
				TxnBranchesByDBInst txnBranchesByDBInst = this.m_freeBranchesByUserAuth[keys[i]];
				if (txnBranchesByDBInst != null)
				{
					txnBranchesByDBInst.ClearBranches();
				}
			}
			this.m_freeBranchesByUserAuth.Clear();
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0008B938 File Offset: 0x00089B38
		internal int Count
		{
			get
			{
				int num = 0;
				List<string> keys = this.m_freeBranchesByUserAuth.GetKeys();
				for (int i = 0; i < keys.Count; i++)
				{
					TxnBranchesByDBInst txnBranchesByDBInst = this.m_freeBranchesByUserAuth[keys[i]];
					if (txnBranchesByDBInst != null)
					{
						num += txnBranchesByDBInst.BranchCount;
					}
				}
				return num;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0008B988 File Offset: 0x00089B88
		internal void EnqueueBranch(MTSTxnBranch txnBranch)
		{
			ConnectionString connCreds = txnBranch.m_connCreds;
			string dbInstance = txnBranch.m_dbInstance;
			TxnBranchesByDBInst txnBranchesByDBInst = null;
			if ((txnBranchesByDBInst = this.m_freeBranchesByUserAuth[connCreds.UserAuthenticationString]) == null)
			{
				lock (this.m_lock)
				{
					if ((txnBranchesByDBInst = this.m_freeBranchesByUserAuth[connCreds.UserAuthenticationString]) == null)
					{
						txnBranchesByDBInst = new TxnBranchesByDBInst();
						this.m_freeBranchesByUserAuth[connCreds.UserAuthenticationString] = txnBranchesByDBInst;
					}
				}
			}
			txnBranchesByDBInst.EnqueueBranch(dbInstance, txnBranch);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0008BA20 File Offset: 0x00089C20
		internal bool DequeueBranch(ConnectionString cs, string dbInstance, out MTSTxnBranch txnBranch)
		{
			txnBranch = null;
			TxnBranchesByDBInst txnBranchesByDBInst;
			return (txnBranchesByDBInst = this.m_freeBranchesByUserAuth[cs.UserAuthenticationString]) != null && txnBranchesByDBInst.DequeueBranch(dbInstance, out txnBranch);
		}

		// Token: 0x04000D97 RID: 3479
		private object m_lock = new object();

		// Token: 0x04000D98 RID: 3480
		internal SyncDictionary<string, TxnBranchesByDBInst> m_freeBranchesByUserAuth = new SyncDictionary<string, TxnBranchesByDBInst>();
	}
}
