using System;
using System.Collections;
using System.Transactions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200000D RID: 13
	internal class OracleResourcePool
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000253E File Offset: 0x0000153E
		internal OracleResourcePool(OracleResourcePool.TransactionEndDelegate deleg)
		{
			this.m_resHolders = Hashtable.Synchronized(new Hashtable());
			this.m_transactionEndDelegate = deleg;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002560 File Offset: 0x00001560
		internal object GetResource(string txnLocalId)
		{
			object result = null;
			try
			{
				OracleResourceHolder oracleResourceHolder = this.m_resHolders[txnLocalId] as OracleResourceHolder;
				if (oracleResourceHolder == null)
				{
					return null;
				}
				if (!oracleResourceHolder.m_disposed)
				{
					lock (oracleResourceHolder)
					{
						if (!oracleResourceHolder.m_disposed && oracleResourceHolder.m_stack.Count > 0)
						{
							result = oracleResourceHolder.m_stack.Pop();
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						" (ERROR) OracleResourcePool::GetResource(), Exception = {0}\n",
						ex.Message
					});
				}
				return null;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002620 File Offset: 0x00001620
		public bool PutResource(Transaction txn, object resource)
		{
			OracleResourceHolder oracleResourceHolder = null;
			bool flag = false;
			try
			{
				this.GetResourceHolder(txn, ref oracleResourceHolder, ref flag);
				lock (oracleResourceHolder)
				{
					if (oracleResourceHolder.m_disposed)
					{
						this.m_transactionEndDelegate(resource);
						return false;
					}
					oracleResourceHolder.m_stack.Push(resource);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						" (ERROR) OracleResourcePool::PutResource(), Exception = {0}\n",
						ex.Message
					});
				}
				if (oracleResourceHolder != null && flag)
				{
					oracleResourceHolder.Dispose();
				}
				else
				{
					this.m_transactionEndDelegate(resource);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026EC File Offset: 0x000016EC
		public void CacheResourceWithLocalTxn(Transaction txn, object resource)
		{
			OracleResourceHolder oracleResourceHolder = null;
			bool flag = false;
			try
			{
				this.GetResourceHolder(txn, ref oracleResourceHolder, ref flag);
				if (oracleResourceHolder != null)
				{
					lock (oracleResourceHolder)
					{
						if (!oracleResourceHolder.m_disposed)
						{
							oracleResourceHolder.m_resourceWithLocalTxn = resource;
						}
						else
						{
							this.m_transactionEndDelegate(resource);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						" (ERROR) OracleResourcePool::CacheResourceWithLocalTxn(), Exception = {0}\n",
						ex.Message
					});
				}
				if (oracleResourceHolder != null && flag)
				{
					oracleResourceHolder.Dispose();
				}
				else
				{
					this.m_transactionEndDelegate(resource);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027A8 File Offset: 0x000017A8
		private void GetResourceHolder(Transaction txn, ref OracleResourceHolder orh, ref bool orhWasCreatedHere)
		{
			string localIdentifier = txn.TransactionInformation.LocalIdentifier;
			orh = (this.m_resHolders[localIdentifier] as OracleResourceHolder);
			if (orh == null)
			{
				lock (OracleResourcePool.m_orhLock)
				{
					orh = (this.m_resHolders[localIdentifier] as OracleResourceHolder);
					if (orh == null)
					{
						orh = new OracleResourceHolder(localIdentifier, this);
						orhWasCreatedHere = true;
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(2U, new string[]
							{
								" (POOL) OracleResourcePool::PutResource(), Registering TransactionCompleted for LID = {0}\n",
								localIdentifier
							});
						}
						txn.TransactionCompleted += orh.TransactionCompleted;
						this.m_resHolders[localIdentifier] = orh;
					}
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000286C File Offset: 0x0000186C
		internal void RemoveResourceHolder(OracleResourceHolder resHolder)
		{
			try
			{
				lock (resHolder)
				{
					if (resHolder.m_resourceWithLocalTxn != null)
					{
						this.m_transactionEndDelegate(resHolder.m_resourceWithLocalTxn);
					}
					int num = resHolder.m_stack.Count;
					while (num != 0)
					{
						num--;
						this.m_transactionEndDelegate(resHolder.m_stack.Pop());
					}
				}
				this.m_resHolders.Remove(resHolder.m_txnLocalId);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						" (ERROR) OracleResourcePool::RemoveResourceHolder(), Exception = {0}\n",
						ex.Message
					});
				}
			}
		}

		// Token: 0x0400002E RID: 46
		private Hashtable m_resHolders;

		// Token: 0x0400002F RID: 47
		internal OracleResourcePool.TransactionEndDelegate m_transactionEndDelegate;

		// Token: 0x04000030 RID: 48
		internal static object m_orhLock = new object();

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x06000031 RID: 49
		internal delegate void TransactionEndDelegate(object resource);
	}
}
