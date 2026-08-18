using System;
using System.Transactions;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x0200012F RID: 303
	internal class MTSProxy
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x0008BAC4 File Offset: 0x00089CC4
		internal MTSProxy(string easyConnectName)
		{
			this.m_dbEasyConnectName = easyConnectName;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0008BB00 File Offset: 0x00089D00
		internal MTSTxnRM GetRM(bool bIsCCP, string serviceName, string pdbName, Transaction txn)
		{
			MTSTxnRM mtstxnRM = null;
			string localIdentifier = txn.TransactionInformation.LocalIdentifier;
			MTSProxy.MTSTxnRMPool ccprms = this.m_CCPRMs;
			if ((mtstxnRM = ccprms[localIdentifier]) == null)
			{
				lock (this.m_lock)
				{
					if ((mtstxnRM = ccprms[localIdentifier]) == null)
					{
						mtstxnRM = (ccprms[localIdentifier] = MTSTxnRMCache.GetRM(bIsCCP));
						mtstxnRM.Initialize(this.m_dbEasyConnectName, serviceName, pdbName, txn);
					}
				}
			}
			return mtstxnRM;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0008BB90 File Offset: 0x00089D90
		internal void RemoveRM(bool bIsCCP, string txnLocalID)
		{
			MTSProxy.MTSTxnRMPool ccprms = this.m_CCPRMs;
			MTSTxnRM mtstxnRM;
			if ((mtstxnRM = ccprms[txnLocalID]) != null)
			{
				ccprms.Remove(txnLocalID);
				mtstxnRM.Reset();
				MTSTxnRMCache.PutRM(mtstxnRM);
			}
		}

		// Token: 0x04000D9A RID: 3482
		private MTSProxy.MTSTxnRMPool m_CCPRMs = new MTSProxy.MTSTxnRMPool();

		// Token: 0x04000D9B RID: 3483
		private MTSProxy.MTSTxnRMPool m_OCPRMs = new MTSProxy.MTSTxnRMPool();

		// Token: 0x04000D9C RID: 3484
		private string m_dbEasyConnectName = string.Empty;

		// Token: 0x04000D9D RID: 3485
		private object m_lock = new object();

		// Token: 0x02000130 RID: 304
		private class MTSTxnRMPool : SyncDictionary<string, MTSTxnRM>
		{
		}
	}
}
