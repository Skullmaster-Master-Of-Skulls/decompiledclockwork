using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x02000043 RID: 67
	internal abstract class EnlistmentState
	{
		// Token: 0x060001F7 RID: 503
		internal abstract void EnterState(InternalEnlistment enlistment);

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00030F54 File Offset: 0x00030354
		private static object ClassSyncObject
		{
			get
			{
				if (EnlistmentState.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref EnlistmentState.classSyncObject, value, null);
				}
				return EnlistmentState.classSyncObject;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00030F84 File Offset: 0x00030384
		internal static EnlistmentStatePromoted _EnlistmentStatePromoted
		{
			get
			{
				if (EnlistmentState._enlistmentStatePromoted == null)
				{
					lock (EnlistmentState.ClassSyncObject)
					{
						if (EnlistmentState._enlistmentStatePromoted == null)
						{
							EnlistmentStatePromoted enlistmentStatePromoted = new EnlistmentStatePromoted();
							Thread.MemoryBarrier();
							EnlistmentState._enlistmentStatePromoted = enlistmentStatePromoted;
						}
					}
				}
				return EnlistmentState._enlistmentStatePromoted;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00030FF4 File Offset: 0x000303F4
		internal virtual void EnlistmentDone(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00031014 File Offset: 0x00030414
		internal virtual void Prepared(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00031034 File Offset: 0x00030434
		internal virtual void ForceRollback(InternalEnlistment enlistment, Exception e)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00031054 File Offset: 0x00030454
		internal virtual void Committed(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00031074 File Offset: 0x00030474
		internal virtual void Aborted(InternalEnlistment enlistment, Exception e)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00031094 File Offset: 0x00030494
		internal virtual void InDoubt(InternalEnlistment enlistment, Exception e)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000310B4 File Offset: 0x000304B4
		internal virtual byte[] RecoveryInformation(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000310D4 File Offset: 0x000304D4
		internal virtual void InternalAborted(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000310F4 File Offset: 0x000304F4
		internal virtual void InternalCommitted(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00031114 File Offset: 0x00030514
		internal virtual void InternalIndoubt(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00031134 File Offset: 0x00030534
		internal virtual void ChangeStateCommitting(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00031154 File Offset: 0x00030554
		internal virtual void ChangeStatePromoted(InternalEnlistment enlistment, IPromotedEnlistment promotedEnlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00031174 File Offset: 0x00030574
		internal virtual void ChangeStateDelegated(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00031194 File Offset: 0x00030594
		internal virtual void ChangeStatePreparing(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000311B4 File Offset: 0x000305B4
		internal virtual void ChangeStateSinglePhaseCommit(InternalEnlistment enlistment)
		{
			throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceLtm"), null);
		}

		// Token: 0x040000F6 RID: 246
		internal static EnlistmentStatePromoted _enlistmentStatePromoted;

		// Token: 0x040000F7 RID: 247
		private static object classSyncObject;
	}
}
