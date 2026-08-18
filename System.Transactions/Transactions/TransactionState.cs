using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000015 RID: 21
	internal abstract class TransactionState
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0002B5E4 File Offset: 0x0002A9E4
		internal static TransactionStateActive _TransactionStateActive
		{
			get
			{
				if (TransactionState._transactionStateActive == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateActive == null)
						{
							TransactionStateActive transactionStateActive = new TransactionStateActive();
							Thread.MemoryBarrier();
							TransactionState._transactionStateActive = transactionStateActive;
						}
					}
				}
				return TransactionState._transactionStateActive;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0002B654 File Offset: 0x0002AA54
		internal static TransactionStateSubordinateActive _TransactionStateSubordinateActive
		{
			get
			{
				if (TransactionState._transactionStateSubordinateActive == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateSubordinateActive == null)
						{
							TransactionStateSubordinateActive transactionStateSubordinateActive = new TransactionStateSubordinateActive();
							Thread.MemoryBarrier();
							TransactionState._transactionStateSubordinateActive = transactionStateSubordinateActive;
						}
					}
				}
				return TransactionState._transactionStateSubordinateActive;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0002B6C4 File Offset: 0x0002AAC4
		internal static TransactionStatePSPEOperation _TransactionStatePSPEOperation
		{
			get
			{
				if (TransactionState._transactionStatePSPEOperation == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePSPEOperation == null)
						{
							TransactionStatePSPEOperation transactionStatePSPEOperation = new TransactionStatePSPEOperation();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePSPEOperation = transactionStatePSPEOperation;
						}
					}
				}
				return TransactionState._transactionStatePSPEOperation;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0002B734 File Offset: 0x0002AB34
		protected static TransactionStatePhase0 _TransactionStatePhase0
		{
			get
			{
				if (TransactionState._transactionStatePhase0 == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePhase0 == null)
						{
							TransactionStatePhase0 transactionStatePhase = new TransactionStatePhase0();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePhase0 = transactionStatePhase;
						}
					}
				}
				return TransactionState._transactionStatePhase0;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0002B7A4 File Offset: 0x0002ABA4
		protected static TransactionStateVolatilePhase1 _TransactionStateVolatilePhase1
		{
			get
			{
				if (TransactionState._transactionStateVolatilePhase1 == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateVolatilePhase1 == null)
						{
							TransactionStateVolatilePhase1 transactionStateVolatilePhase = new TransactionStateVolatilePhase1();
							Thread.MemoryBarrier();
							TransactionState._transactionStateVolatilePhase1 = transactionStateVolatilePhase;
						}
					}
				}
				return TransactionState._transactionStateVolatilePhase1;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0002B814 File Offset: 0x0002AC14
		protected static TransactionStateVolatileSPC _TransactionStateVolatileSPC
		{
			get
			{
				if (TransactionState._transactionStateVolatileSPC == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateVolatileSPC == null)
						{
							TransactionStateVolatileSPC transactionStateVolatileSPC = new TransactionStateVolatileSPC();
							Thread.MemoryBarrier();
							TransactionState._transactionStateVolatileSPC = transactionStateVolatileSPC;
						}
					}
				}
				return TransactionState._transactionStateVolatileSPC;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0002B884 File Offset: 0x0002AC84
		protected static TransactionStateSPC _TransactionStateSPC
		{
			get
			{
				if (TransactionState._transactionStateSPC == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateSPC == null)
						{
							TransactionStateSPC transactionStateSPC = new TransactionStateSPC();
							Thread.MemoryBarrier();
							TransactionState._transactionStateSPC = transactionStateSPC;
						}
					}
				}
				return TransactionState._transactionStateSPC;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0002B8F4 File Offset: 0x0002ACF4
		protected static TransactionStateAborted _TransactionStateAborted
		{
			get
			{
				if (TransactionState._transactionStateAborted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateAborted == null)
						{
							TransactionStateAborted transactionStateAborted = new TransactionStateAborted();
							Thread.MemoryBarrier();
							TransactionState._transactionStateAborted = transactionStateAborted;
						}
					}
				}
				return TransactionState._transactionStateAborted;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0002B964 File Offset: 0x0002AD64
		protected static TransactionStateCommitted _TransactionStateCommitted
		{
			get
			{
				if (TransactionState._transactionStateCommitted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateCommitted == null)
						{
							TransactionStateCommitted transactionStateCommitted = new TransactionStateCommitted();
							Thread.MemoryBarrier();
							TransactionState._transactionStateCommitted = transactionStateCommitted;
						}
					}
				}
				return TransactionState._transactionStateCommitted;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0002B9D4 File Offset: 0x0002ADD4
		protected static TransactionStateInDoubt _TransactionStateInDoubt
		{
			get
			{
				if (TransactionState._transactionStateInDoubt == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateInDoubt == null)
						{
							TransactionStateInDoubt transactionStateInDoubt = new TransactionStateInDoubt();
							Thread.MemoryBarrier();
							TransactionState._transactionStateInDoubt = transactionStateInDoubt;
						}
					}
				}
				return TransactionState._transactionStateInDoubt;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0002BA44 File Offset: 0x0002AE44
		internal static TransactionStatePromoted _TransactionStatePromoted
		{
			get
			{
				if (TransactionState._transactionStatePromoted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromoted == null)
						{
							TransactionStatePromoted transactionStatePromoted = new TransactionStatePromoted();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromoted = transactionStatePromoted;
						}
					}
				}
				return TransactionState._transactionStatePromoted;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0002BAB4 File Offset: 0x0002AEB4
		internal static TransactionStateNonCommittablePromoted _TransactionStateNonCommittablePromoted
		{
			get
			{
				if (TransactionState._transactionStateNonCommittablePromoted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateNonCommittablePromoted == null)
						{
							TransactionStateNonCommittablePromoted transactionStateNonCommittablePromoted = new TransactionStateNonCommittablePromoted();
							Thread.MemoryBarrier();
							TransactionState._transactionStateNonCommittablePromoted = transactionStateNonCommittablePromoted;
						}
					}
				}
				return TransactionState._transactionStateNonCommittablePromoted;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0002BB24 File Offset: 0x0002AF24
		protected static TransactionStatePromotedP0Wave _TransactionStatePromotedP0Wave
		{
			get
			{
				if (TransactionState._transactionStatePromotedP0Wave == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedP0Wave == null)
						{
							TransactionStatePromotedP0Wave transactionStatePromotedP0Wave = new TransactionStatePromotedP0Wave();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedP0Wave = transactionStatePromotedP0Wave;
						}
					}
				}
				return TransactionState._transactionStatePromotedP0Wave;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0002BB94 File Offset: 0x0002AF94
		protected static TransactionStatePromotedCommitting _TransactionStatePromotedCommitting
		{
			get
			{
				if (TransactionState._transactionStatePromotedCommitting == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedCommitting == null)
						{
							TransactionStatePromotedCommitting transactionStatePromotedCommitting = new TransactionStatePromotedCommitting();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedCommitting = transactionStatePromotedCommitting;
						}
					}
				}
				return TransactionState._transactionStatePromotedCommitting;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0002BC04 File Offset: 0x0002B004
		protected static TransactionStatePromotedPhase0 _TransactionStatePromotedPhase0
		{
			get
			{
				if (TransactionState._transactionStatePromotedPhase0 == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedPhase0 == null)
						{
							TransactionStatePromotedPhase0 transactionStatePromotedPhase = new TransactionStatePromotedPhase0();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedPhase0 = transactionStatePromotedPhase;
						}
					}
				}
				return TransactionState._transactionStatePromotedPhase0;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0002BC74 File Offset: 0x0002B074
		protected static TransactionStatePromotedPhase1 _TransactionStatePromotedPhase1
		{
			get
			{
				if (TransactionState._transactionStatePromotedPhase1 == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedPhase1 == null)
						{
							TransactionStatePromotedPhase1 transactionStatePromotedPhase = new TransactionStatePromotedPhase1();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedPhase1 = transactionStatePromotedPhase;
						}
					}
				}
				return TransactionState._transactionStatePromotedPhase1;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0002BCE4 File Offset: 0x0002B0E4
		protected static TransactionStatePromotedP0Aborting _TransactionStatePromotedP0Aborting
		{
			get
			{
				if (TransactionState._transactionStatePromotedP0Aborting == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedP0Aborting == null)
						{
							TransactionStatePromotedP0Aborting transactionStatePromotedP0Aborting = new TransactionStatePromotedP0Aborting();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedP0Aborting = transactionStatePromotedP0Aborting;
						}
					}
				}
				return TransactionState._transactionStatePromotedP0Aborting;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0002BD54 File Offset: 0x0002B154
		protected static TransactionStatePromotedP1Aborting _TransactionStatePromotedP1Aborting
		{
			get
			{
				if (TransactionState._transactionStatePromotedP1Aborting == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedP1Aborting == null)
						{
							TransactionStatePromotedP1Aborting transactionStatePromotedP1Aborting = new TransactionStatePromotedP1Aborting();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedP1Aborting = transactionStatePromotedP1Aborting;
						}
					}
				}
				return TransactionState._transactionStatePromotedP1Aborting;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0002BDC4 File Offset: 0x0002B1C4
		protected static TransactionStatePromotedAborted _TransactionStatePromotedAborted
		{
			get
			{
				if (TransactionState._transactionStatePromotedAborted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedAborted == null)
						{
							TransactionStatePromotedAborted transactionStatePromotedAborted = new TransactionStatePromotedAborted();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedAborted = transactionStatePromotedAborted;
						}
					}
				}
				return TransactionState._transactionStatePromotedAborted;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0002BE34 File Offset: 0x0002B234
		protected static TransactionStatePromotedCommitted _TransactionStatePromotedCommitted
		{
			get
			{
				if (TransactionState._transactionStatePromotedCommitted == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedCommitted == null)
						{
							TransactionStatePromotedCommitted transactionStatePromotedCommitted = new TransactionStatePromotedCommitted();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedCommitted = transactionStatePromotedCommitted;
						}
					}
				}
				return TransactionState._transactionStatePromotedCommitted;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0002BEA4 File Offset: 0x0002B2A4
		protected static TransactionStatePromotedIndoubt _TransactionStatePromotedIndoubt
		{
			get
			{
				if (TransactionState._transactionStatePromotedIndoubt == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStatePromotedIndoubt == null)
						{
							TransactionStatePromotedIndoubt transactionStatePromotedIndoubt = new TransactionStatePromotedIndoubt();
							Thread.MemoryBarrier();
							TransactionState._transactionStatePromotedIndoubt = transactionStatePromotedIndoubt;
						}
					}
				}
				return TransactionState._transactionStatePromotedIndoubt;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0002BF14 File Offset: 0x0002B314
		protected static TransactionStateDelegated _TransactionStateDelegated
		{
			get
			{
				if (TransactionState._transactionStateDelegated == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateDelegated == null)
						{
							TransactionStateDelegated transactionStateDelegated = new TransactionStateDelegated();
							Thread.MemoryBarrier();
							TransactionState._transactionStateDelegated = transactionStateDelegated;
						}
					}
				}
				return TransactionState._transactionStateDelegated;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0002BF84 File Offset: 0x0002B384
		internal static TransactionStateDelegatedSubordinate _TransactionStateDelegatedSubordinate
		{
			get
			{
				if (TransactionState._transactionStateDelegatedSubordinate == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateDelegatedSubordinate == null)
						{
							TransactionStateDelegatedSubordinate transactionStateDelegatedSubordinate = new TransactionStateDelegatedSubordinate();
							Thread.MemoryBarrier();
							TransactionState._transactionStateDelegatedSubordinate = transactionStateDelegatedSubordinate;
						}
					}
				}
				return TransactionState._transactionStateDelegatedSubordinate;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0002BFF4 File Offset: 0x0002B3F4
		protected static TransactionStateDelegatedP0Wave _TransactionStateDelegatedP0Wave
		{
			get
			{
				if (TransactionState._transactionStateDelegatedP0Wave == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateDelegatedP0Wave == null)
						{
							TransactionStateDelegatedP0Wave transactionStateDelegatedP0Wave = new TransactionStateDelegatedP0Wave();
							Thread.MemoryBarrier();
							TransactionState._transactionStateDelegatedP0Wave = transactionStateDelegatedP0Wave;
						}
					}
				}
				return TransactionState._transactionStateDelegatedP0Wave;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0002C064 File Offset: 0x0002B464
		protected static TransactionStateDelegatedCommitting _TransactionStateDelegatedCommitting
		{
			get
			{
				if (TransactionState._transactionStateDelegatedCommitting == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateDelegatedCommitting == null)
						{
							TransactionStateDelegatedCommitting transactionStateDelegatedCommitting = new TransactionStateDelegatedCommitting();
							Thread.MemoryBarrier();
							TransactionState._transactionStateDelegatedCommitting = transactionStateDelegatedCommitting;
						}
					}
				}
				return TransactionState._transactionStateDelegatedCommitting;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0002C0D4 File Offset: 0x0002B4D4
		protected static TransactionStateDelegatedAborting _TransactionStateDelegatedAborting
		{
			get
			{
				if (TransactionState._transactionStateDelegatedAborting == null)
				{
					lock (TransactionState.ClassSyncObject)
					{
						if (TransactionState._transactionStateDelegatedAborting == null)
						{
							TransactionStateDelegatedAborting transactionStateDelegatedAborting = new TransactionStateDelegatedAborting();
							Thread.MemoryBarrier();
							TransactionState._transactionStateDelegatedAborting = transactionStateDelegatedAborting;
						}
					}
				}
				return TransactionState._transactionStateDelegatedAborting;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0002C144 File Offset: 0x0002B544
		internal static object ClassSyncObject
		{
			get
			{
				if (TransactionState.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref TransactionState.classSyncObject, value, null);
				}
				return TransactionState.classSyncObject;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0002C174 File Offset: 0x0002B574
		internal void CommonEnterState(InternalTransaction tx)
		{
			tx.State = this;
		}

		// Token: 0x06000083 RID: 131
		internal abstract void EnterState(InternalTransaction tx);

		// Token: 0x06000084 RID: 132 RVA: 0x0002C194 File Offset: 0x0002B594
		internal virtual void BeginCommit(InternalTransaction tx, bool asyncCommit, AsyncCallback asyncCallback, object asyncState)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0002C1C4 File Offset: 0x0002B5C4
		internal virtual void EndCommit(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0002C1F4 File Offset: 0x0002B5F4
		internal virtual void Rollback(InternalTransaction tx, Exception e)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0002C224 File Offset: 0x0002B624
		internal virtual Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0002C254 File Offset: 0x0002B654
		internal virtual Enlistment EnlistDurable(InternalTransaction tx, Guid resourceManagerIdentifier, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0002C284 File Offset: 0x0002B684
		internal virtual Enlistment EnlistVolatile(InternalTransaction tx, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0002C2B4 File Offset: 0x0002B6B4
		internal virtual Enlistment EnlistVolatile(InternalTransaction tx, ISinglePhaseNotification enlistmentNotification, EnlistmentOptions enlistmentOptions, Transaction atomicTransaction)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0002C2E4 File Offset: 0x0002B6E4
		internal virtual void CheckForFinishedTransaction(InternalTransaction tx)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0002C2F4 File Offset: 0x0002B6F4
		internal virtual Guid get_Identifier(InternalTransaction tx)
		{
			return Guid.Empty;
		}

		// Token: 0x0600008D RID: 141
		internal abstract TransactionStatus get_Status(InternalTransaction tx);

		// Token: 0x0600008E RID: 142 RVA: 0x0002C314 File Offset: 0x0002B714
		internal virtual void AddOutcomeRegistrant(InternalTransaction tx, TransactionCompletedEventHandler transactionCompletedDelegate)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0002C344 File Offset: 0x0002B744
		internal virtual void GetObjectData(InternalTransaction tx, SerializationInfo serializationInfo, StreamingContext context)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0002C374 File Offset: 0x0002B774
		internal virtual bool EnlistPromotableSinglePhase(InternalTransaction tx, IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Transaction atomicTransaction)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0002C3A4 File Offset: 0x0002B7A4
		internal virtual void CompleteBlockingClone(InternalTransaction tx)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0002C3B4 File Offset: 0x0002B7B4
		internal virtual void CompleteAbortingClone(InternalTransaction tx)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0002C3C4 File Offset: 0x0002B7C4
		internal virtual void CreateBlockingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0002C3F4 File Offset: 0x0002B7F4
		internal virtual void CreateAbortingClone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0002C424 File Offset: 0x0002B824
		internal virtual void ChangeStateTransactionAborted(InternalTransaction tx, Exception e)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0002C454 File Offset: 0x0002B854
		internal virtual void ChangeStateTransactionCommitted(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0002C484 File Offset: 0x0002B884
		internal virtual void InDoubtFromEnlistment(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0002C4B4 File Offset: 0x0002B8B4
		internal virtual void ChangeStatePromotedAborted(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0002C4E4 File Offset: 0x0002B8E4
		internal virtual void ChangeStatePromotedCommitted(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0002C514 File Offset: 0x0002B914
		internal virtual void InDoubtFromDtc(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0002C544 File Offset: 0x0002B944
		internal virtual void ChangeStatePromotedPhase0(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0002C574 File Offset: 0x0002B974
		internal virtual void ChangeStatePromotedPhase1(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0002C5A4 File Offset: 0x0002B9A4
		internal virtual void ChangeStateAbortedDuringPromotion(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0002C5D4 File Offset: 0x0002B9D4
		internal virtual void Timeout(InternalTransaction tx)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0002C5E4 File Offset: 0x0002B9E4
		internal virtual void Phase0VolatilePrepareDone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0002C614 File Offset: 0x0002BA14
		internal virtual void Phase1VolatilePrepareDone(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0002C644 File Offset: 0x0002BA44
		internal virtual void RestartCommitIfNeeded(InternalTransaction tx)
		{
			if (DiagnosticTrace.Error)
			{
				InvalidOperationExceptionTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0002C674 File Offset: 0x0002BA74
		internal virtual bool ContinuePhase0Prepares()
		{
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0002C684 File Offset: 0x0002BA84
		internal virtual bool ContinuePhase1Prepares()
		{
			return false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0002C694 File Offset: 0x0002BA94
		internal virtual void Promote(InternalTransaction tx)
		{
			throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceLtm"), tx.innerException);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0002C6C4 File Offset: 0x0002BAC4
		internal virtual void DisposeRoot(InternalTransaction tx)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0002C6D4 File Offset: 0x0002BAD4
		internal virtual bool IsCompleted(InternalTransaction tx)
		{
			tx.needPulse = true;
			return false;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0002C6F4 File Offset: 0x0002BAF4
		protected void AddVolatileEnlistment(ref VolatileEnlistmentSet enlistments, Enlistment enlistment)
		{
			if (enlistments.volatileEnlistmentCount == enlistments.volatileEnlistmentSize)
			{
				InternalEnlistment[] array = new InternalEnlistment[enlistments.volatileEnlistmentSize + 8];
				if (enlistments.volatileEnlistmentSize > 0)
				{
					Array.Copy(enlistments.volatileEnlistments, array, enlistments.volatileEnlistmentSize);
				}
				enlistments.volatileEnlistmentSize += 8;
				enlistments.volatileEnlistments = array;
			}
			enlistments.volatileEnlistments[enlistments.volatileEnlistmentCount] = enlistment.InternalEnlistment;
			enlistments.volatileEnlistmentCount++;
			VolatileEnlistmentState._VolatileEnlistmentActive.EnterState(enlistments.volatileEnlistments[enlistments.volatileEnlistmentCount - 1]);
		}

		// Token: 0x040000C7 RID: 199
		private static TransactionStateActive _transactionStateActive;

		// Token: 0x040000C8 RID: 200
		private static TransactionStateSubordinateActive _transactionStateSubordinateActive;

		// Token: 0x040000C9 RID: 201
		private static TransactionStatePhase0 _transactionStatePhase0;

		// Token: 0x040000CA RID: 202
		private static TransactionStateVolatilePhase1 _transactionStateVolatilePhase1;

		// Token: 0x040000CB RID: 203
		private static TransactionStateVolatileSPC _transactionStateVolatileSPC;

		// Token: 0x040000CC RID: 204
		private static TransactionStateSPC _transactionStateSPC;

		// Token: 0x040000CD RID: 205
		private static TransactionStateAborted _transactionStateAborted;

		// Token: 0x040000CE RID: 206
		private static TransactionStateCommitted _transactionStateCommitted;

		// Token: 0x040000CF RID: 207
		private static TransactionStateInDoubt _transactionStateInDoubt;

		// Token: 0x040000D0 RID: 208
		private static TransactionStatePromoted _transactionStatePromoted;

		// Token: 0x040000D1 RID: 209
		private static TransactionStateNonCommittablePromoted _transactionStateNonCommittablePromoted;

		// Token: 0x040000D2 RID: 210
		private static TransactionStatePromotedP0Wave _transactionStatePromotedP0Wave;

		// Token: 0x040000D3 RID: 211
		private static TransactionStatePromotedCommitting _transactionStatePromotedCommitting;

		// Token: 0x040000D4 RID: 212
		private static TransactionStatePromotedPhase0 _transactionStatePromotedPhase0;

		// Token: 0x040000D5 RID: 213
		private static TransactionStatePromotedPhase1 _transactionStatePromotedPhase1;

		// Token: 0x040000D6 RID: 214
		private static TransactionStatePromotedP0Aborting _transactionStatePromotedP0Aborting;

		// Token: 0x040000D7 RID: 215
		private static TransactionStatePromotedP1Aborting _transactionStatePromotedP1Aborting;

		// Token: 0x040000D8 RID: 216
		private static TransactionStatePromotedAborted _transactionStatePromotedAborted;

		// Token: 0x040000D9 RID: 217
		private static TransactionStatePromotedCommitted _transactionStatePromotedCommitted;

		// Token: 0x040000DA RID: 218
		private static TransactionStatePromotedIndoubt _transactionStatePromotedIndoubt;

		// Token: 0x040000DB RID: 219
		private static TransactionStateDelegated _transactionStateDelegated;

		// Token: 0x040000DC RID: 220
		private static TransactionStateDelegatedSubordinate _transactionStateDelegatedSubordinate;

		// Token: 0x040000DD RID: 221
		private static TransactionStateDelegatedP0Wave _transactionStateDelegatedP0Wave;

		// Token: 0x040000DE RID: 222
		private static TransactionStateDelegatedCommitting _transactionStateDelegatedCommitting;

		// Token: 0x040000DF RID: 223
		private static TransactionStateDelegatedAborting _transactionStateDelegatedAborting;

		// Token: 0x040000E0 RID: 224
		private static TransactionStatePSPEOperation _transactionStatePSPEOperation;

		// Token: 0x040000E1 RID: 225
		private static object classSyncObject;
	}
}
