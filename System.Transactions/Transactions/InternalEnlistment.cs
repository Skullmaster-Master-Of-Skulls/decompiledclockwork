using System;
using System.Globalization;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x0200003A RID: 58
	internal class InternalEnlistment : ISinglePhaseNotificationInternal, IEnlistmentNotificationInternal
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x0002FE04 File Offset: 0x0002F204
		protected InternalEnlistment(Enlistment enlistment, IEnlistmentNotification twoPhaseNotifications)
		{
			this.enlistment = enlistment;
			this.twoPhaseNotifications = twoPhaseNotifications;
			this.enlistmentId = 1;
			this.traceIdentifier = EnlistmentTraceIdentifier.Empty;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0002FE44 File Offset: 0x0002F244
		protected InternalEnlistment(Enlistment enlistment, InternalTransaction transaction, Transaction atomicTransaction)
		{
			this.enlistment = enlistment;
			this.transaction = transaction;
			this.atomicTransaction = atomicTransaction;
			this.enlistmentId = transaction.enlistmentCount++;
			this.traceIdentifier = EnlistmentTraceIdentifier.Empty;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0002FE94 File Offset: 0x0002F294
		internal InternalEnlistment(Enlistment enlistment, InternalTransaction transaction, IEnlistmentNotification twoPhaseNotifications, ISinglePhaseNotification singlePhaseNotifications, Transaction atomicTransaction)
		{
			this.enlistment = enlistment;
			this.transaction = transaction;
			this.twoPhaseNotifications = twoPhaseNotifications;
			this.singlePhaseNotifications = singlePhaseNotifications;
			this.atomicTransaction = atomicTransaction;
			this.enlistmentId = transaction.enlistmentCount++;
			this.traceIdentifier = EnlistmentTraceIdentifier.Empty;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0002FEF4 File Offset: 0x0002F2F4
		internal InternalEnlistment(Enlistment enlistment, IEnlistmentNotification twoPhaseNotifications, InternalTransaction transaction, Transaction atomicTransaction)
		{
			this.enlistment = enlistment;
			this.twoPhaseNotifications = twoPhaseNotifications;
			this.transaction = transaction;
			this.atomicTransaction = atomicTransaction;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0002FF24 File Offset: 0x0002F324
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0002FF44 File Offset: 0x0002F344
		internal EnlistmentState State
		{
			get
			{
				return this.twoPhaseState;
			}
			set
			{
				this.twoPhaseState = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0002FF64 File Offset: 0x0002F364
		internal Enlistment Enlistment
		{
			get
			{
				return this.enlistment;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0002FF84 File Offset: 0x0002F384
		internal PreparingEnlistment PreparingEnlistment
		{
			get
			{
				if (this.preparingEnlistment == null)
				{
					this.preparingEnlistment = new PreparingEnlistment(this);
				}
				return this.preparingEnlistment;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0002FFB4 File Offset: 0x0002F3B4
		internal SinglePhaseEnlistment SinglePhaseEnlistment
		{
			get
			{
				if (this.singlePhaseEnlistment == null)
				{
					this.singlePhaseEnlistment = new SinglePhaseEnlistment(this);
				}
				return this.singlePhaseEnlistment;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0002FFE4 File Offset: 0x0002F3E4
		internal InternalTransaction Transaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00030004 File Offset: 0x0002F404
		internal virtual object SyncRoot
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00030024 File Offset: 0x0002F424
		internal IEnlistmentNotification EnlistmentNotification
		{
			get
			{
				return this.twoPhaseNotifications;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00030044 File Offset: 0x0002F444
		internal ISinglePhaseNotification SinglePhaseNotification
		{
			get
			{
				return this.singlePhaseNotifications;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00030064 File Offset: 0x0002F464
		internal virtual IPromotableSinglePhaseNotification PromotableSinglePhaseNotification
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00030084 File Offset: 0x0002F484
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000300A4 File Offset: 0x0002F4A4
		internal IPromotedEnlistment PromotedEnlistment
		{
			get
			{
				return this.promotedEnlistment;
			}
			set
			{
				this.promotedEnlistment = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000300C4 File Offset: 0x0002F4C4
		internal EnlistmentTraceIdentifier EnlistmentTraceId
		{
			get
			{
				if (this.traceIdentifier == EnlistmentTraceIdentifier.Empty)
				{
					lock (this.SyncRoot)
					{
						if (this.traceIdentifier == EnlistmentTraceIdentifier.Empty)
						{
							EnlistmentTraceIdentifier enlistmentTraceIdentifier;
							if (null != this.atomicTransaction)
							{
								enlistmentTraceIdentifier = new EnlistmentTraceIdentifier(Guid.Empty, this.atomicTransaction.TransactionTraceId, this.enlistmentId);
							}
							else
							{
								enlistmentTraceIdentifier = new EnlistmentTraceIdentifier(Guid.Empty, new TransactionTraceIdentifier(InternalTransaction.InstanceIdentifier + Convert.ToString(Interlocked.Increment(ref InternalTransaction.nextHash), CultureInfo.InvariantCulture), 0), this.enlistmentId);
							}
							Thread.MemoryBarrier();
							this.traceIdentifier = enlistmentTraceIdentifier;
						}
					}
				}
				return this.traceIdentifier;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000301A4 File Offset: 0x0002F5A4
		internal virtual void FinishEnlistment()
		{
			InternalTransaction internalTransaction = this.Transaction;
			internalTransaction.phase0Volatiles.preparedVolatileEnlistments = internalTransaction.phase0Volatiles.preparedVolatileEnlistments + 1;
			this.CheckComplete();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000301D4 File Offset: 0x0002F5D4
		internal virtual void CheckComplete()
		{
			if (this.Transaction.phase0Volatiles.preparedVolatileEnlistments == this.Transaction.phase0VolatileWaveCount + this.Transaction.phase0Volatiles.dependentClones)
			{
				this.Transaction.State.Phase0VolatilePrepareDone(this.Transaction);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00030234 File Offset: 0x0002F634
		internal virtual Guid ResourceManagerIdentifier
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00030254 File Offset: 0x0002F654
		void ISinglePhaseNotificationInternal.SinglePhaseCommit(IPromotedEnlistment singlePhaseEnlistment)
		{
			bool flag = false;
			this.promotedEnlistment = singlePhaseEnlistment;
			try
			{
				this.singlePhaseNotifications.SinglePhaseCommit(this.SinglePhaseEnlistment);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.SinglePhaseEnlistment.InDoubt();
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000302B4 File Offset: 0x0002F6B4
		void IEnlistmentNotificationInternal.Prepare(IPromotedEnlistment preparingEnlistment)
		{
			this.promotedEnlistment = preparingEnlistment;
			this.twoPhaseNotifications.Prepare(this.PreparingEnlistment);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000302E4 File Offset: 0x0002F6E4
		void IEnlistmentNotificationInternal.Commit(IPromotedEnlistment enlistment)
		{
			this.promotedEnlistment = enlistment;
			this.twoPhaseNotifications.Commit(this.Enlistment);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00030314 File Offset: 0x0002F714
		void IEnlistmentNotificationInternal.Rollback(IPromotedEnlistment enlistment)
		{
			this.promotedEnlistment = enlistment;
			this.twoPhaseNotifications.Rollback(this.Enlistment);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00030344 File Offset: 0x0002F744
		void IEnlistmentNotificationInternal.InDoubt(IPromotedEnlistment enlistment)
		{
			this.promotedEnlistment = enlistment;
			this.twoPhaseNotifications.InDoubt(this.Enlistment);
		}

		// Token: 0x040000E3 RID: 227
		internal EnlistmentState twoPhaseState;

		// Token: 0x040000E4 RID: 228
		protected IEnlistmentNotification twoPhaseNotifications;

		// Token: 0x040000E5 RID: 229
		protected ISinglePhaseNotification singlePhaseNotifications;

		// Token: 0x040000E6 RID: 230
		protected InternalTransaction transaction;

		// Token: 0x040000E7 RID: 231
		private Transaction atomicTransaction;

		// Token: 0x040000E8 RID: 232
		private EnlistmentTraceIdentifier traceIdentifier;

		// Token: 0x040000E9 RID: 233
		private int enlistmentId;

		// Token: 0x040000EA RID: 234
		private Enlistment enlistment;

		// Token: 0x040000EB RID: 235
		private PreparingEnlistment preparingEnlistment;

		// Token: 0x040000EC RID: 236
		private SinglePhaseEnlistment singlePhaseEnlistment;

		// Token: 0x040000ED RID: 237
		private IPromotedEnlistment promotedEnlistment;
	}
}
