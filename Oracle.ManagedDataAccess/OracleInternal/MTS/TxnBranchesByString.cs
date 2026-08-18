using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x0200011F RID: 287
	internal class TxnBranchesByString : SyncDictionary<string, MTSTxnBranches>
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x0008A778 File Offset: 0x00088978
		internal void ClearBranches()
		{
			List<string> keys = base.GetKeys();
			for (int i = 0; i < keys.Count; i++)
			{
				MTSTxnBranches mtstxnBranches = base[keys[i]];
				if (mtstxnBranches != null)
				{
					mtstxnBranches.ClearBranches();
				}
			}
			base.Clear();
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0008A7BC File Offset: 0x000889BC
		internal int BranchCount
		{
			get
			{
				int num = 0;
				List<string> keys = base.GetKeys();
				for (int i = 0; i < keys.Count; i++)
				{
					MTSTxnBranches mtstxnBranches = base[keys[i]];
					if (mtstxnBranches != null)
					{
						num += mtstxnBranches.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0008A800 File Offset: 0x00088A00
		internal void EnqueueBranch(string dbInstance, MTSTxnBranch txnBranch)
		{
			MTSTxnBranches mtstxnBranches = null;
			if ((mtstxnBranches = base[dbInstance]) == null)
			{
				lock (this.m_lock)
				{
					if ((mtstxnBranches = base[dbInstance]) == null)
					{
						mtstxnBranches = new MTSTxnBranches();
						base[dbInstance] = mtstxnBranches;
					}
				}
			}
			mtstxnBranches.AddIfNotExist(txnBranch);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0008A868 File Offset: 0x00088A68
		internal bool DequeueBranch(string dbInstance, out MTSTxnBranch txnBranch)
		{
			txnBranch = null;
			MTSTxnBranches mtstxnBranches;
			return (mtstxnBranches = base[dbInstance]) != null && mtstxnBranches.Dequeue(out txnBranch);
		}

		// Token: 0x04000D5A RID: 3418
		private object m_lock = new object();
	}
}
