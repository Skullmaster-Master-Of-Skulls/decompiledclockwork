using System;
using System.Collections;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000094 RID: 148
	internal class OletxInternalResourceManager
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0003C6C4 File Offset: 0x0003BAC4
		internal OletxInternalResourceManager(OletxTransactionManager oletxTm)
		{
			this.oletxTm = oletxTm;
			this.myGuid = Guid.NewGuid();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0003C6F4 File Offset: 0x0003BAF4
		public void TMDown()
		{
			this.resourceManagerShim = null;
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxInternalResourceManager.TMDown");
			}
			Hashtable hashtable = null;
			lock (TransactionManager.PromotedTransactionTable.SyncRoot)
			{
				hashtable = (Hashtable)TransactionManager.PromotedTransactionTable.Clone();
			}
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				WeakReference weakReference = (WeakReference)enumerator.Value;
				if (weakReference != null)
				{
					Transaction transaction = (Transaction)weakReference.Target;
					if (null != transaction)
					{
						RealOletxTransaction realOletxTransaction = transaction.internalTransaction.PromotedTransaction.realOletxTransaction;
						if (realOletxTransaction.OletxTransactionManagerInstance == this.oletxTm)
						{
							realOletxTransaction.TMDown();
						}
					}
				}
			}
			Hashtable hashtable2 = null;
			if (OletxTransactionManager.resourceManagerHashTable != null)
			{
				OletxTransactionManager.resourceManagerHashTableLock.AcquireReaderLock(-1);
				try
				{
					hashtable2 = (Hashtable)OletxTransactionManager.resourceManagerHashTable.Clone();
				}
				finally
				{
					OletxTransactionManager.resourceManagerHashTableLock.ReleaseReaderLock();
				}
			}
			if (hashtable2 != null)
			{
				enumerator = hashtable2.GetEnumerator();
				while (enumerator.MoveNext())
				{
					OletxResourceManager oletxResourceManager = (OletxResourceManager)enumerator.Value;
					if (oletxResourceManager != null)
					{
						oletxResourceManager.TMDownFromInternalRM(this.oletxTm);
					}
				}
			}
			this.oletxTm.dtcTransactionManagerLock.AcquireWriterLock(-1);
			try
			{
				this.oletxTm.ReinitializeProxy();
			}
			finally
			{
				this.oletxTm.dtcTransactionManagerLock.ReleaseWriterLock();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxInternalResourceManager.TMDown");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0003C8B4 File Offset: 0x0003BCB4
		internal Guid Identifier
		{
			get
			{
				return this.myGuid;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0003C8D4 File Offset: 0x0003BCD4
		internal void CallReenlistComplete()
		{
			this.resourceManagerShim.ReenlistComplete();
		}

		// Token: 0x04000228 RID: 552
		private OletxTransactionManager oletxTm;

		// Token: 0x04000229 RID: 553
		private Guid myGuid;

		// Token: 0x0400022A RID: 554
		internal IResourceManagerShim resourceManagerShim;
	}
}
