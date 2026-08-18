using System;
using System.Collections.Generic;
using System.Runtime;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F3 RID: 2291
	internal class MsmqReceiveContextLockManager : IDisposable
	{
		// Token: 0x06005763 RID: 22371 RVA: 0x0014091C File Offset: 0x0013EB1C
		public MsmqReceiveContextLockManager(MsmqReceiveContextSettings receiveContextSettings, MsmqQueue queue)
		{
			this.disposed = false;
			this.queue = queue;
			this.receiveContextSettings = receiveContextSettings;
			this.messageExpiryMap = new Dictionary<long, MsmqReceiveContext>();
			this.transMessages = new Dictionary<Guid, List<MsmqReceiveContext>>();
			this.transactionCompletedHandler = new TransactionCompletedEventHandler(this.OnTransactionCompleted);
			this.messageExpiryTimer = new IOThreadTimer(new Action<object>(this.CleanupExpiredLocks), null, false);
			this.messageExpiryTimer.Set(this.messageTimeoutInterval);
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06005764 RID: 22372 RVA: 0x001409B5 File Offset: 0x0013EBB5
		public MsmqQueue Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x001409C0 File Offset: 0x0013EBC0
		public MsmqReceiveContext CreateMsmqReceiveContext(long lookupId)
		{
			DateTime expiryTime = TimeoutHelper.Add(DateTime.UtcNow, this.receiveContextSettings.ValidityDuration);
			MsmqReceiveContext msmqReceiveContext = new MsmqReceiveContext(lookupId, expiryTime, this);
			msmqReceiveContext.Faulted += this.OnReceiveContextFaulted;
			object obj = this.internalStateLock;
			lock (obj)
			{
				this.messageExpiryMap.Add(lookupId, msmqReceiveContext);
			}
			return msmqReceiveContext;
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x00140A3C File Offset: 0x0013EC3C
		public void DeleteMessage(MsmqReceiveContext receiveContext, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			long lookupId = receiveContext.LookupId;
			object obj = this.internalStateLock;
			lock (obj)
			{
				if (!this.messageExpiryMap.ContainsKey(lookupId))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MessageValidityExpired", new object[]
					{
						lookupId
					})));
				}
				MsmqReceiveContext msmqReceiveContext = this.messageExpiryMap[lookupId];
				if (DateTime.UtcNow > msmqReceiveContext.ExpiryTime)
				{
					msmqReceiveContext.MarkContextExpired();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MessageValidityExpired", new object[]
					{
						lookupId
					})));
				}
				((ILockingQueue)this.queue).DeleteMessage(lookupId, timeoutHelper.RemainingTime());
				if (Transaction.Current != null)
				{
					List<MsmqReceiveContext> list;
					if (!this.transMessages.TryGetValue(Transaction.Current.TransactionInformation.DistributedIdentifier, out list))
					{
						list = new List<MsmqReceiveContext>();
						this.transMessages.Add(Transaction.Current.TransactionInformation.DistributedIdentifier, list);
						Transaction.Current.TransactionCompleted += this.transactionCompletedHandler;
					}
					list.Add(msmqReceiveContext);
				}
				else
				{
					this.messageExpiryMap.Remove(lookupId);
				}
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x00140BAC File Offset: 0x0013EDAC
		public void UnlockMessage(MsmqReceiveContext receiveContext, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			long lookupId = receiveContext.LookupId;
			object obj = this.internalStateLock;
			lock (obj)
			{
				if (this.ReceiveContextExists(receiveContext))
				{
					((ILockingQueue)this.queue).UnlockMessage(lookupId, timeoutHelper.RemainingTime());
					this.messageExpiryMap.Remove(lookupId);
				}
			}
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00140C24 File Offset: 0x0013EE24
		private bool ReceiveContextExists(MsmqReceiveContext receiveContext)
		{
			MsmqReceiveContext msmqReceiveContext = null;
			return this.messageExpiryMap.TryGetValue(receiveContext.LookupId, out msmqReceiveContext) && receiveContext == msmqReceiveContext;
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x00140C50 File Offset: 0x0013EE50
		private void OnTransactionCompleted(object sender, TransactionEventArgs e)
		{
			e.Transaction.TransactionCompleted -= this.transactionCompletedHandler;
			object obj = this.internalStateLock;
			lock (obj)
			{
				List<MsmqReceiveContext> list;
				if (e.Transaction.TransactionInformation.Status == TransactionStatus.Committed && this.transMessages.TryGetValue(e.Transaction.TransactionInformation.DistributedIdentifier, out list))
				{
					foreach (MsmqReceiveContext msmqReceiveContext in list)
					{
						this.messageExpiryMap.Remove(msmqReceiveContext.LookupId);
					}
				}
				this.transMessages.Remove(e.Transaction.TransactionInformation.DistributedIdentifier);
			}
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x00140D34 File Offset: 0x0013EF34
		private void CleanupExpiredLocks(object state)
		{
			object obj = this.internalStateLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					if (this.messageExpiryMap.Count < 1)
					{
						this.messageExpiryTimer.Set(this.messageTimeoutInterval);
					}
					else
					{
						List<MsmqReceiveContext> list = new List<MsmqReceiveContext>();
						try
						{
							foreach (KeyValuePair<long, MsmqReceiveContext> keyValuePair in this.messageExpiryMap)
							{
								if (DateTime.UtcNow > keyValuePair.Value.ExpiryTime)
								{
									list.Add(keyValuePair.Value);
								}
							}
							try
							{
								foreach (MsmqReceiveContext msmqReceiveContext in list)
								{
									msmqReceiveContext.MarkContextExpired();
								}
							}
							catch (MsmqException ex)
							{
								MsmqDiagnostics.ExpectedException(ex);
							}
						}
						finally
						{
							this.messageExpiryTimer.Set(this.messageTimeoutInterval);
						}
					}
				}
			}
		}

		// Token: 0x0600576B RID: 22379 RVA: 0x00140E7C File Offset: 0x0013F07C
		private void OnReceiveContextFaulted(object sender, EventArgs e)
		{
			try
			{
				MsmqReceiveContext receiveContext = (MsmqReceiveContext)sender;
				this.UnlockMessage(receiveContext, TimeSpan.Zero);
			}
			catch (MsmqException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x00140EB8 File Offset: 0x0013F0B8
		public void Dispose()
		{
			object obj = this.internalStateLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.messageExpiryTimer.Cancel();
					this.messageExpiryTimer = null;
				}
			}
		}

		// Token: 0x040035BC RID: 13756
		private MsmqReceiveContextSettings receiveContextSettings;

		// Token: 0x040035BD RID: 13757
		private IOThreadTimer messageExpiryTimer;

		// Token: 0x040035BE RID: 13758
		private TimeSpan messageTimeoutInterval = TimeSpan.FromSeconds(60.0);

		// Token: 0x040035BF RID: 13759
		private Dictionary<long, MsmqReceiveContext> messageExpiryMap;

		// Token: 0x040035C0 RID: 13760
		private Dictionary<Guid, List<MsmqReceiveContext>> transMessages;

		// Token: 0x040035C1 RID: 13761
		private MsmqQueue queue;

		// Token: 0x040035C2 RID: 13762
		private TransactionCompletedEventHandler transactionCompletedHandler;

		// Token: 0x040035C3 RID: 13763
		private bool disposed;

		// Token: 0x040035C4 RID: 13764
		private object internalStateLock = new object();
	}
}
