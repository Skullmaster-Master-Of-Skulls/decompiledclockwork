using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Runtime;
using System.Security;
using System.Security.Permissions;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E0 RID: 2272
	internal class MsmqDefaultLockingQueue : MsmqQueue, ILockingQueue
	{
		// Token: 0x0600566F RID: 22127 RVA: 0x0013CF80 File Offset: 0x0013B180
		public MsmqDefaultLockingQueue(string formatName, int accessMode) : base(formatName, accessMode)
		{
			this.lockMap = new Dictionary<long, MsmqDefaultLockingQueue.TransactionLookupEntry>();
			this.dtcTransMap = new Dictionary<Guid, List<long>>();
			this.internalStateLock = new object();
			this.transactionCompletedHandler = new TransactionCompletedEventHandler(this.Current_TransactionCompleted);
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x0013CFD4 File Offset: 0x0013B1D4
		public override MsmqQueue.ReceiveResult TryReceive(NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			MsmqQueueHandle handle = base.GetHandle();
			int num;
			for (;;)
			{
				num = this.PeekLockCore(handle, (MsmqInputMessage)message, timeoutHelper.RemainingTime());
				if (num == 0)
				{
					break;
				}
				if (!MsmqQueue.IsReceiveErrorDueToInsufficientBuffer(num))
				{
					goto IL_39;
				}
				message.GrowBuffers();
			}
			return MsmqQueue.ReceiveResult.MessageReceived;
			IL_39:
			if (num == -1072824293)
			{
				return MsmqQueue.ReceiveResult.Timeout;
			}
			if (num == -1072824312)
			{
				return MsmqQueue.ReceiveResult.OperationCancelled;
			}
			if (num == -1072824313)
			{
				return MsmqQueue.ReceiveResult.OperationCancelled;
			}
			if (MsmqQueue.IsErrorDueToStaleHandle(num))
			{
				base.HandleIsStale(handle);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveError", new object[]
			{
				MsmqError.GetErrorString(num)
			}), num));
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x0013D070 File Offset: 0x0013B270
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int PeekLockCore(MsmqQueueHandle handle, MsmqInputMessage message, TimeSpan timeout)
		{
			int num = 0;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IntPtr properties = message.Pin();
			try
			{
				bool flag = false;
				while (!flag)
				{
					ITransaction transaction;
					num = UnsafeNativeMethods.MQBeginTransaction(out transaction);
					if (num != 0)
					{
						return num;
					}
					int num2 = TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime());
					int timeout2 = (num2 == 0) ? 0 : 100;
					for (;;)
					{
						object obj = this.receiveLock;
						lock (obj)
						{
							num = UnsafeNativeMethods.MQReceiveMessage(handle.DangerousGetHandle(), timeout2, 0, properties, null, IntPtr.Zero, IntPtr.Zero, transaction);
							if (num == -1072824293)
							{
								if (TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime()) == 0)
								{
									return num;
								}
								continue;
							}
							else if (num != 0)
							{
								BOID boid = default(BOID);
								transaction.Abort(ref boid, 0, 0);
								return num;
							}
						}
						break;
					}
					object obj2 = this.internalStateLock;
					lock (obj2)
					{
						MsmqDefaultLockingQueue.TransactionLookupEntry transactionLookupEntry;
						if (!this.lockMap.TryGetValue(message.LookupId.Value, out transactionLookupEntry))
						{
							this.lockMap.Add(message.LookupId.Value, new MsmqDefaultLockingQueue.TransactionLookupEntry(message.LookupId.Value, transaction));
							flag = true;
						}
						else
						{
							transactionLookupEntry.MsmqInternalTransaction = transaction;
						}
					}
				}
			}
			finally
			{
				message.Unpin();
			}
			return num;
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x0013D214 File Offset: 0x0013B414
		public void DeleteMessage(long lookupId, TimeSpan timeout)
		{
			if (Transaction.Current != null && Transaction.Current.TransactionInformation.Status != System.Transactions.TransactionStatus.Active)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqAmbientTransactionInactive")));
			}
			object obj = this.internalStateLock;
			MsmqDefaultLockingQueue.TransactionLookupEntry transactionLookupEntry;
			lock (obj)
			{
				if (!this.lockMap.TryGetValue(lookupId, out transactionLookupEntry))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MessageNotInLockedState", new object[]
					{
						lookupId
					})));
				}
				if (transactionLookupEntry.MsmqInternalTransaction == null)
				{
					this.lockMap.Remove(transactionLookupEntry.LookupId);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MessageNotInLockedState", new object[]
					{
						lookupId
					})));
				}
			}
			if (Transaction.Current == null)
			{
				transactionLookupEntry.MsmqInternalTransaction.Commit(0, 0, 0);
				object obj2 = this.internalStateLock;
				lock (obj2)
				{
					this.lockMap.Remove(lookupId);
					return;
				}
			}
			object obj3 = this.receiveLock;
			lock (obj3)
			{
				MsmqQueueHandle handle = base.GetHandle();
				BOID boid = default(BOID);
				transactionLookupEntry.MsmqInternalTransaction.Abort(ref boid, 0, 0);
				transactionLookupEntry.MsmqInternalTransaction = null;
				using (MsmqEmptyMessage msmqEmptyMessage = new MsmqEmptyMessage())
				{
					int num = 0;
					try
					{
						num = base.ReceiveByLookupIdCoreDtcTransacted(handle, lookupId, msmqEmptyMessage, MsmqTransactionMode.CurrentOrThrow, 1073741856);
					}
					catch (ObjectDisposedException ex)
					{
						MsmqDiagnostics.ExpectedException(ex);
					}
					if (num != 0)
					{
						if (MsmqQueue.IsErrorDueToStaleHandle(num))
						{
							base.HandleIsStale(handle);
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqCannotReacquireLock"), num));
					}
				}
			}
			object obj4 = this.internalStateLock;
			lock (obj4)
			{
				List<long> list;
				if (!this.dtcTransMap.TryGetValue(Transaction.Current.TransactionInformation.DistributedIdentifier, out list))
				{
					list = new List<long>();
					this.dtcTransMap.Add(Transaction.Current.TransactionInformation.DistributedIdentifier, list);
					Transaction.Current.TransactionCompleted += this.transactionCompletedHandler;
				}
				list.Add(lookupId);
			}
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0013D4B4 File Offset: 0x0013B6B4
		public void UnlockMessage(long lookupId, TimeSpan timeout)
		{
			object obj = this.internalStateLock;
			lock (obj)
			{
				MsmqDefaultLockingQueue.TransactionLookupEntry transactionLookupEntry;
				if (this.lockMap.TryGetValue(lookupId, out transactionLookupEntry))
				{
					if (transactionLookupEntry.MsmqInternalTransaction != null)
					{
						BOID boid = default(BOID);
						transactionLookupEntry.MsmqInternalTransaction.Abort(ref boid, 0, 0);
					}
					this.lockMap.Remove(lookupId);
				}
			}
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x0013D52C File Offset: 0x0013B72C
		private void Current_TransactionCompleted(object sender, TransactionEventArgs e)
		{
			e.Transaction.TransactionCompleted -= this.transactionCompletedHandler;
			if (e.Transaction.TransactionInformation.Status == System.Transactions.TransactionStatus.Aborted)
			{
				List<long> list = null;
				object obj = this.internalStateLock;
				lock (obj)
				{
					if (this.dtcTransMap.TryGetValue(e.Transaction.TransactionInformation.DistributedIdentifier, out list))
					{
						this.dtcTransMap.Remove(e.Transaction.TransactionInformation.DistributedIdentifier);
					}
				}
				if (list == null)
				{
					return;
				}
				using (List<long>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						long lookupId = enumerator.Current;
						this.TryRelockMessage(lookupId);
					}
					return;
				}
			}
			if (e.Transaction.TransactionInformation.Status == System.Transactions.TransactionStatus.Committed)
			{
				List<long> list2 = null;
				object obj2 = this.internalStateLock;
				lock (obj2)
				{
					if (this.dtcTransMap.TryGetValue(e.Transaction.TransactionInformation.DistributedIdentifier, out list2))
					{
						this.dtcTransMap.Remove(e.Transaction.TransactionInformation.DistributedIdentifier);
					}
					if (list2 != null)
					{
						foreach (long key in list2)
						{
							this.lockMap.Remove(key);
						}
					}
				}
			}
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x0013D6E0 File Offset: 0x0013B8E0
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int TryRelockMessage(long lookupId)
		{
			int num = 0;
			using (MsmqEmptyMessage msmqEmptyMessage = new MsmqEmptyMessage())
			{
				IntPtr properties = msmqEmptyMessage.Pin();
				try
				{
					object obj = this.receiveLock;
					lock (obj)
					{
						MsmqQueueHandle handle = base.GetHandle();
						object obj2 = this.internalStateLock;
						lock (obj2)
						{
							MsmqDefaultLockingQueue.TransactionLookupEntry transactionLookupEntry;
							if (!this.lockMap.TryGetValue(lookupId, out transactionLookupEntry))
							{
								return num;
							}
							if (transactionLookupEntry.MsmqInternalTransaction == null)
							{
								ITransaction transaction;
								num = UnsafeNativeMethods.MQBeginTransaction(out transaction);
								if (num != 0)
								{
									return num;
								}
								num = UnsafeNativeMethods.MQReceiveMessageByLookupId(handle, lookupId, 1073741856, properties, null, IntPtr.Zero, transaction);
								if (num != 0)
								{
									BOID boid = default(BOID);
									transaction.Abort(ref boid, 0, 0);
									return num;
								}
								transactionLookupEntry.MsmqInternalTransaction = transaction;
							}
						}
					}
				}
				finally
				{
					msmqEmptyMessage.Unpin();
				}
			}
			return num;
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x0013D7F8 File Offset: 0x0013B9F8
		public override void CloseQueue()
		{
			object obj = this.internalStateLock;
			long[] array;
			lock (obj)
			{
				array = new long[this.lockMap.Keys.Count];
				this.lockMap.Keys.CopyTo(array, 0);
			}
			foreach (long lookupId in array)
			{
				this.UnlockMessage(lookupId, TimeSpan.Zero);
			}
			base.CloseQueue();
		}

		// Token: 0x0400356C RID: 13676
		private Dictionary<long, MsmqDefaultLockingQueue.TransactionLookupEntry> lockMap;

		// Token: 0x0400356D RID: 13677
		private Dictionary<Guid, List<long>> dtcTransMap;

		// Token: 0x0400356E RID: 13678
		private object internalStateLock;

		// Token: 0x0400356F RID: 13679
		private TransactionCompletedEventHandler transactionCompletedHandler;

		// Token: 0x04003570 RID: 13680
		private object receiveLock = new object();

		// Token: 0x02000D8B RID: 3467
		private class TransactionLookupEntry
		{
			// Token: 0x06007E93 RID: 32403 RVA: 0x001D7BE5 File Offset: 0x001D5DE5
			public TransactionLookupEntry(long lookupId, ITransaction transaction)
			{
				this.LookupId = lookupId;
				this.MsmqInternalTransaction = transaction;
			}

			// Token: 0x040048A1 RID: 18593
			public long LookupId;

			// Token: 0x040048A2 RID: 18594
			public ITransaction MsmqInternalTransaction;
		}
	}
}
