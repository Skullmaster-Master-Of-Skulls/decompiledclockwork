using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000090 RID: 144
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06000390 RID: 912 RVA: 0x00008EE3 File Offset: 0x000070E3
		private void InitializeThreadCounts()
		{
			this._upgradeLockOwnerId = -1;
			this._writeLockOwnerId = -1;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008EF3 File Offset: 0x000070F3
		[__DynamicallyInvokable]
		public ReaderWriterLockSlim() : this(LockRecursionPolicy.NoRecursion)
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008EFC File Offset: 0x000070FC
		[__DynamicallyInvokable]
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == LockRecursionPolicy.SupportsRecursion)
			{
				this._fIsReentrant = true;
			}
			this.InitializeThreadCounts();
			this._waiterStates = ReaderWriterLockSlim.WaiterStates.NoWaiters;
			this._lockID = Interlocked.Increment(ref ReaderWriterLockSlim.s_nextLockID);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008F2C File Offset: 0x0000712C
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00008F39 File Offset: 0x00007139
		private bool HasNoWaiters
		{
			get
			{
				return (this._waiterStates & ReaderWriterLockSlim.WaiterStates.NoWaiters) > ReaderWriterLockSlim.WaiterStates.None;
			}
			set
			{
				if (value)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.NoWaiters;
					return;
				}
				this._waiterStates &= ~ReaderWriterLockSlim.WaiterStates.NoWaiters;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00008F5F File Offset: 0x0000715F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
		{
			return rwc.lockID == 0L || (rwc.readercount == 0 && rwc.writercount == 0 && rwc.upgradecount == 0);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00008F86 File Offset: 0x00007186
		private bool IsRwHashEntryChanged(ReaderWriterCount lrwc)
		{
			return lrwc.lockID != this._lockID;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00008F9C File Offset: 0x0000719C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReaderWriterCount GetThreadRWCount(bool dontAllocate)
		{
			ReaderWriterCount next = ReaderWriterLockSlim.t_rwc;
			ReaderWriterCount readerWriterCount = null;
			while (next != null)
			{
				if (next.lockID == this._lockID)
				{
					return next;
				}
				if (!dontAllocate && readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(next))
				{
					readerWriterCount = next;
				}
				next = next.next;
			}
			if (dontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				readerWriterCount = new ReaderWriterCount();
				readerWriterCount.next = ReaderWriterLockSlim.t_rwc;
				ReaderWriterLockSlim.t_rwc = readerWriterCount;
			}
			readerWriterCount.lockID = this._lockID;
			return readerWriterCount;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00009009 File Offset: 0x00007209
		[__DynamicallyInvokable]
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009013 File Offset: 0x00007213
		[__DynamicallyInvokable]
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00009021 File Offset: 0x00007221
		[__DynamicallyInvokable]
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009030 File Offset: 0x00007230
		private bool TryEnterReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			Thread.BeginCriticalRegion();
			bool flag = false;
			try
			{
				flag = this.TryEnterReadLockCore(timeout);
			}
			finally
			{
				if (!flag)
				{
					Thread.EndCriticalRegion();
				}
			}
			return flag;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00009068 File Offset: 0x00007268
		private bool TryEnterReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("LockRecursionException_ReadAfterWriteNotAllowed"));
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.GetString("LockRecursionException_RecursiveReadNotAllowed"));
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					return true;
				}
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					threadRWCount.readercount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					this._fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this._owners >= 268435454U)
			{
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyRead())
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else if (this._readEvent == null)
				{
					this.LazyCreateEvent(ref this._readEvent, ReaderWriterLockSlim.EnterLockType.Read);
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this._readEvent, ref this._numReadWaiters, timeout, ReaderWriterLockSlim.EnterLockType.Read);
					if (!flag)
					{
						return false;
					}
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
			}
			this._owners += 1U;
			threadRWCount.readercount++;
			this._spinLock.Exit();
			return flag;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000092B9 File Offset: 0x000074B9
		[__DynamicallyInvokable]
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000092C3 File Offset: 0x000074C3
		[__DynamicallyInvokable]
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000092D1 File Offset: 0x000074D1
		[__DynamicallyInvokable]
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000092E0 File Offset: 0x000074E0
		private bool TryEnterWriteLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			Thread.BeginCriticalRegion();
			bool flag = false;
			try
			{
				flag = this.TryEnterWriteLockCore(timeout);
			}
			finally
			{
				if (!flag)
				{
					Thread.EndCriticalRegion();
				}
			}
			return flag;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009318 File Offset: 0x00007518
		private bool TryEnterWriteLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			bool flag = false;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("LockRecursionException_RecursiveWriteNotAllowed"));
				}
				ReaderWriterLockSlim.EnterSpinLockReason reason;
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					flag = true;
					reason = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				}
				else
				{
					reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				}
				this._spinLock.Enter(reason);
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.GetString("LockRecursionException_WriteAfterReadNotAllowed"));
				}
			}
			else
			{
				ReaderWriterLockSlim.EnterSpinLockReason reason2;
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.EnterRecursiveWrite;
				}
				else if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				}
				else
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				}
				this._spinLock.Enter(reason2);
				threadRWCount = this.GetThreadRWCount(false);
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					threadRWCount.writercount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.GetString("LockRecursionException_WriteAfterReadNotAllowed"));
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || threadRWCount == null)
						{
							goto IL_176;
						}
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						if (threadRWCount.readercount <= 0)
						{
							goto IL_176;
						}
						this.SetWriterAcquired();
					}
					IL_23B:
					if (this._fIsReentrant)
					{
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						threadRWCount.writercount++;
					}
					this._spinLock.Exit();
					this._writeLockOwnerId = currentManagedThreadId;
					return true;
				}
				IL_176:
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyWrite(flag))
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(flag ? ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite : ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite);
				}
				else if (flag)
				{
					if (this._waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this._waitUpgradeEvent, ReaderWriterLockSlim.EnterLockType.UpgradeToWrite);
					}
					else if (!this.WaitOnEvent(this._waitUpgradeEvent, ref this._numWriteUpgradeWaiters, timeout, ReaderWriterLockSlim.EnterLockType.UpgradeToWrite))
					{
						return false;
					}
				}
				else if (this._writeEvent == null)
				{
					this.LazyCreateEvent(ref this._writeEvent, ReaderWriterLockSlim.EnterLockType.Write);
				}
				else if (!this.WaitOnEvent(this._writeEvent, ref this._numWriteWaiters, timeout, ReaderWriterLockSlim.EnterLockType.Write))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_23B;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000959A File Offset: 0x0000779A
		[__DynamicallyInvokable]
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000095A4 File Offset: 0x000077A4
		[__DynamicallyInvokable]
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000095B2 File Offset: 0x000077B2
		[__DynamicallyInvokable]
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000095C0 File Offset: 0x000077C0
		private bool TryEnterUpgradeableReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			Thread.BeginCriticalRegion();
			bool flag = false;
			try
			{
				flag = this.TryEnterUpgradeableReadLockCore(timeout);
			}
			finally
			{
				if (!flag)
				{
					Thread.EndCriticalRegion();
				}
			}
			return flag;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000095F8 File Offset: 0x000077F8
		private bool TryEnterUpgradeableReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("LockRecursionException_RecursiveUpgradeNotAllowed"));
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("LockRecursionException_UpgradeAfterWriteNotAllowed"));
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.GetString("LockRecursionException_UpgradeAfterReadNotAllowed"));
				}
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.upgradecount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					this._owners += 1U;
					this._upgradeLockOwnerId = currentManagedThreadId;
					threadRWCount.upgradecount++;
					if (threadRWCount.readercount > 0)
					{
						this._fUpgradeThreadHoldingRead = true;
					}
					this._spinLock.Exit();
					return true;
				}
				if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.GetString("LockRecursionException_UpgradeAfterReadNotAllowed"));
				}
			}
			int num = 0;
			while (this._upgradeLockOwnerId != -1 || this._owners >= 268435454U)
			{
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyRead())
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				}
				else if (this._upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this._upgradeEvent, ReaderWriterLockSlim.EnterLockType.UpgradeableRead);
				}
				else if (!this.WaitOnEvent(this._upgradeEvent, ref this._numUpgradeWaiters, timeout, ReaderWriterLockSlim.EnterLockType.UpgradeableRead))
				{
					return false;
				}
			}
			this._owners += 1U;
			this._upgradeLockOwnerId = currentManagedThreadId;
			if (this._fIsReentrant)
			{
				if (this.IsRwHashEntryChanged(threadRWCount))
				{
					threadRWCount = this.GetThreadRWCount(false);
				}
				threadRWCount.upgradecount++;
			}
			this._spinLock.Exit();
			return true;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000981C File Offset: 0x00007A1C
		[__DynamicallyInvokable]
		public void ExitReadLock()
		{
			this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
			ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
			if (threadRWCount == null || threadRWCount.readercount < 1)
			{
				this._spinLock.Exit();
				throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedRead"));
			}
			if (this._fIsReentrant)
			{
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this._spinLock.Exit();
					Thread.EndCriticalRegion();
					return;
				}
				if (Environment.CurrentManagedThreadId == this._upgradeLockOwnerId)
				{
					this._fUpgradeThreadHoldingRead = false;
				}
			}
			this._owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
			Thread.EndCriticalRegion();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000098D4 File Offset: 0x00007AD4
		[__DynamicallyInvokable]
		public void ExitWriteLock()
		{
			if (!this._fIsReentrant)
			{
				if (Environment.CurrentManagedThreadId != this._writeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedWrite"));
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyWrite);
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyWrite);
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount == null)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedWrite"));
				}
				if (threadRWCount.writercount < 1)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedWrite"));
				}
				threadRWCount.writercount--;
				if (threadRWCount.writercount > 0)
				{
					this._spinLock.Exit();
					Thread.EndCriticalRegion();
					return;
				}
			}
			this.ClearWriterAcquired();
			this._writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
			Thread.EndCriticalRegion();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000099AC File Offset: 0x00007BAC
		[__DynamicallyInvokable]
		public void ExitUpgradeableReadLock()
		{
			if (!this._fIsReentrant)
			{
				if (Environment.CurrentManagedThreadId != this._upgradeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedUpgrade"));
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount == null)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedUpgrade"));
				}
				if (threadRWCount.upgradecount < 1)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_MisMatchedUpgrade"));
				}
				threadRWCount.upgradecount--;
				if (threadRWCount.upgradecount > 0)
				{
					this._spinLock.Exit();
					Thread.EndCriticalRegion();
					return;
				}
				this._fUpgradeThreadHoldingRead = false;
			}
			this._owners -= 1U;
			this._upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
			Thread.EndCriticalRegion();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009A94 File Offset: 0x00007C94
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, ReaderWriterLockSlim.EnterLockType enterLockType)
		{
			this._spinLock.Exit();
			EventWaitHandle eventWaitHandle = new EventWaitHandle(false, (enterLockType == ReaderWriterLockSlim.EnterLockType.Read) ? EventResetMode.ManualReset : EventResetMode.AutoReset);
			ReaderWriterLockSlim.EnterSpinLockReason reason;
			if (enterLockType > ReaderWriterLockSlim.EnterLockType.UpgradeableRead)
			{
				if (enterLockType != ReaderWriterLockSlim.EnterLockType.Write)
				{
					reason = (ReaderWriterLockSlim.EnterSpinLockReason)11;
				}
				else
				{
					reason = (ReaderWriterLockSlim.EnterSpinLockReason)10;
				}
			}
			else
			{
				reason = ReaderWriterLockSlim.EnterSpinLockReason.Wait;
			}
			this._spinLock.Enter(reason);
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Dispose();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00009AEC File Offset: 0x00007CEC
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, ReaderWriterLockSlim.TimeoutTracker timeout, ReaderWriterLockSlim.EnterLockType enterLockType)
		{
			ReaderWriterLockSlim.WaiterStates waiterStates = ReaderWriterLockSlim.WaiterStates.None;
			ReaderWriterLockSlim.EnterSpinLockReason reason;
			switch (enterLockType)
			{
			case ReaderWriterLockSlim.EnterLockType.Read:
				break;
			case ReaderWriterLockSlim.EnterLockType.UpgradeableRead:
				waiterStates = ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled;
				break;
			case ReaderWriterLockSlim.EnterLockType.Write:
				waiterStates = ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				goto IL_25;
			default:
				reason = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				goto IL_25;
			}
			reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead;
			IL_25:
			if (waiterStates != ReaderWriterLockSlim.WaiterStates.None && (this._waiterStates & waiterStates) != ReaderWriterLockSlim.WaiterStates.None)
			{
				this._waiterStates &= ~waiterStates;
			}
			waitEvent.Reset();
			numWaiters += 1U;
			this.HasNoWaiters = false;
			if (this._numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this._numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this._spinLock.Exit();
			try
			{
				flag = waitEvent.WaitOne(timeout.RemainingMilliseconds);
			}
			finally
			{
				this._spinLock.Enter(reason);
				numWaiters -= 1U;
				if (flag && waiterStates != ReaderWriterLockSlim.WaiterStates.None && (this._waiterStates & waiterStates) != ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates &= ~waiterStates;
				}
				if (this._numWriteWaiters == 0U && this._numWriteUpgradeWaiters == 0U && this._numUpgradeWaiters == 0U && this._numReadWaiters == 0U)
				{
					this.HasNoWaiters = true;
				}
				if (this._numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this._numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					if (enterLockType >= ReaderWriterLockSlim.EnterLockType.Write)
					{
						this.ExitAndWakeUpAppropriateReadWaiters();
					}
					else
					{
						this._spinLock.Exit();
					}
				}
			}
			return flag;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009C30 File Offset: 0x00007E30
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.HasNoWaiters)
			{
				this._spinLock.Exit();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009C4C File Offset: 0x00007E4C
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			uint numReaders = this.GetNumReaders();
			if (this._fIsReentrant && this._numWriteUpgradeWaiters > 0U && this._fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this._spinLock.Exit();
				this._waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this._numWriteUpgradeWaiters > 0U)
			{
				this._spinLock.Exit();
				this._waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this._numWriteWaiters > 0U)
			{
				ReaderWriterLockSlim.WaiterStates waiterStates = this._waiterStates & ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				if (waiterStates == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				}
				this._spinLock.Exit();
				if (waiterStates == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._writeEvent.Set();
					return;
				}
			}
			else
			{
				this.ExitAndWakeUpAppropriateReadWaiters();
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009D04 File Offset: 0x00007F04
		private void ExitAndWakeUpAppropriateReadWaiters()
		{
			if (this._numWriteWaiters != 0U || this._numWriteUpgradeWaiters != 0U || this.HasNoWaiters)
			{
				this._spinLock.Exit();
				return;
			}
			bool flag = this._numReadWaiters > 0U;
			bool flag2 = this._numUpgradeWaiters != 0U && this._upgradeLockOwnerId == -1;
			if (flag2)
			{
				if ((this._waiterStates & ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled) == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled;
				}
				else
				{
					flag2 = false;
				}
			}
			this._spinLock.Exit();
			if (flag)
			{
				this._readEvent.Set();
			}
			if (flag2)
			{
				this._upgradeEvent.Set();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009D9C File Offset: 0x00007F9C
		private bool IsWriterAcquired()
		{
			return (this._owners & 3221225471U) == 0U;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009DAD File Offset: 0x00007FAD
		private void SetWriterAcquired()
		{
			this._owners |= 2147483648U;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00009DC1 File Offset: 0x00007FC1
		private void ClearWriterAcquired()
		{
			this._owners &= 2147483647U;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009DD5 File Offset: 0x00007FD5
		private void SetWritersWaiting()
		{
			this._owners |= 1073741824U;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00009DE9 File Offset: 0x00007FE9
		private void ClearWritersWaiting()
		{
			this._owners &= 3221225471U;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009DFD File Offset: 0x00007FFD
		private void SetUpgraderWaiting()
		{
			this._owners |= 536870912U;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009E11 File Offset: 0x00008011
		private void ClearUpgraderWaiting()
		{
			this._owners &= 3758096383U;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00009E25 File Offset: 0x00008025
		private uint GetNumReaders()
		{
			return this._owners & 268435455U;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009E33 File Offset: 0x00008033
		private bool ShouldSpinForEnterAnyRead()
		{
			return this.HasNoWaiters || (this._numWriteWaiters == 0U && this._numWriteUpgradeWaiters == 0U);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009E52 File Offset: 0x00008052
		private bool ShouldSpinForEnterAnyWrite(bool isUpgradeToWrite)
		{
			return isUpgradeToWrite || this._numWriteUpgradeWaiters == 0U;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00009E62 File Offset: 0x00008062
		private static void SpinWait(int spinCount)
		{
			if (spinCount < 5 && ReaderWriterLockSlim.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * spinCount);
				return;
			}
			Thread.Sleep(0);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009E80 File Offset: 0x00008080
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00009E8C File Offset: 0x0000808C
		private void Dispose(bool disposing)
		{
			if (disposing && !this._fDisposed)
			{
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_IncorrectDispose"));
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new SynchronizationLockException(SR.GetString("SynchronizationLockException_IncorrectDispose"));
				}
				if (this._writeEvent != null)
				{
					this._writeEvent.Dispose();
					this._writeEvent = null;
				}
				if (this._readEvent != null)
				{
					this._readEvent.Dispose();
					this._readEvent = null;
				}
				if (this._upgradeEvent != null)
				{
					this._upgradeEvent.Dispose();
					this._upgradeEvent = null;
				}
				if (this._waitUpgradeEvent != null)
				{
					this._waitUpgradeEvent.Dispose();
					this._waitUpgradeEvent = null;
				}
				this._fDisposed = true;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00009F6C File Offset: 0x0000816C
		[__DynamicallyInvokable]
		public bool IsReadLockHeld
		{
			[__DynamicallyInvokable]
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00009F7A File Offset: 0x0000817A
		[__DynamicallyInvokable]
		public bool IsUpgradeableReadLockHeld
		{
			[__DynamicallyInvokable]
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00009F88 File Offset: 0x00008188
		[__DynamicallyInvokable]
		public bool IsWriteLockHeld
		{
			[__DynamicallyInvokable]
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00009F96 File Offset: 0x00008196
		[__DynamicallyInvokable]
		public LockRecursionPolicy RecursionPolicy
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._fIsReentrant)
				{
					return LockRecursionPolicy.SupportsRecursion;
				}
				return LockRecursionPolicy.NoRecursion;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00009FA4 File Offset: 0x000081A4
		[__DynamicallyInvokable]
		public int CurrentReadCount
		{
			[__DynamicallyInvokable]
			get
			{
				int numReaders = (int)this.GetNumReaders();
				if (this._upgradeLockOwnerId != -1)
				{
					return numReaders - 1;
				}
				return numReaders;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00009FC8 File Offset: 0x000081C8
		[__DynamicallyInvokable]
		public int RecursiveReadCount
		{
			[__DynamicallyInvokable]
			get
			{
				int result = 0;
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null)
				{
					result = threadRWCount.readercount;
				}
				return result;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00009FEC File Offset: 0x000081EC
		[__DynamicallyInvokable]
		public int RecursiveUpgradeCount
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.upgradecount;
					}
					return result;
				}
				if (Environment.CurrentManagedThreadId == this._upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000A028 File Offset: 0x00008228
		[__DynamicallyInvokable]
		public int RecursiveWriteCount
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.writercount;
					}
					return result;
				}
				if (Environment.CurrentManagedThreadId == this._writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000A063 File Offset: 0x00008263
		[__DynamicallyInvokable]
		public int WaitingReadCount
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)this._numReadWaiters;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000A06B File Offset: 0x0000826B
		[__DynamicallyInvokable]
		public int WaitingUpgradeCount
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)this._numUpgradeWaiters;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000A073 File Offset: 0x00008273
		[__DynamicallyInvokable]
		public int WaitingWriteCount
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)this._numWriteWaiters;
			}
		}

		// Token: 0x040004AA RID: 1194
		private static readonly int ProcessorCount = Environment.ProcessorCount;

		// Token: 0x040004AB RID: 1195
		private readonly bool _fIsReentrant;

		// Token: 0x040004AC RID: 1196
		private ReaderWriterLockSlim.SpinLock _spinLock;

		// Token: 0x040004AD RID: 1197
		private uint _numWriteWaiters;

		// Token: 0x040004AE RID: 1198
		private uint _numReadWaiters;

		// Token: 0x040004AF RID: 1199
		private uint _numWriteUpgradeWaiters;

		// Token: 0x040004B0 RID: 1200
		private uint _numUpgradeWaiters;

		// Token: 0x040004B1 RID: 1201
		private ReaderWriterLockSlim.WaiterStates _waiterStates;

		// Token: 0x040004B2 RID: 1202
		private int _upgradeLockOwnerId;

		// Token: 0x040004B3 RID: 1203
		private int _writeLockOwnerId;

		// Token: 0x040004B4 RID: 1204
		private EventWaitHandle _writeEvent;

		// Token: 0x040004B5 RID: 1205
		private EventWaitHandle _readEvent;

		// Token: 0x040004B6 RID: 1206
		private EventWaitHandle _upgradeEvent;

		// Token: 0x040004B7 RID: 1207
		private EventWaitHandle _waitUpgradeEvent;

		// Token: 0x040004B8 RID: 1208
		private static long s_nextLockID;

		// Token: 0x040004B9 RID: 1209
		private long _lockID;

		// Token: 0x040004BA RID: 1210
		[ThreadStatic]
		private static ReaderWriterCount t_rwc;

		// Token: 0x040004BB RID: 1211
		private bool _fUpgradeThreadHoldingRead;

		// Token: 0x040004BC RID: 1212
		private const int MaxSpinCount = 20;

		// Token: 0x040004BD RID: 1213
		private uint _owners;

		// Token: 0x040004BE RID: 1214
		private const uint WRITER_HELD = 2147483648U;

		// Token: 0x040004BF RID: 1215
		private const uint WAITING_WRITERS = 1073741824U;

		// Token: 0x040004C0 RID: 1216
		private const uint WAITING_UPGRADER = 536870912U;

		// Token: 0x040004C1 RID: 1217
		private const uint MAX_READER = 268435454U;

		// Token: 0x040004C2 RID: 1218
		private const uint READER_MASK = 268435455U;

		// Token: 0x040004C3 RID: 1219
		private bool _fDisposed;

		// Token: 0x02000301 RID: 769
		private struct TimeoutTracker
		{
			// Token: 0x06001A66 RID: 6758 RVA: 0x00060C98 File Offset: 0x0005EE98
			public TimeoutTracker(TimeSpan timeout)
			{
				long num = (long)timeout.TotalMilliseconds;
				if (num < -1L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("timeout");
				}
				this._total = (int)num;
				if (this._total != -1 && this._total != 0)
				{
					this._start = Environment.TickCount;
					return;
				}
				this._start = 0;
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x00060CF3 File Offset: 0x0005EEF3
			public TimeoutTracker(int millisecondsTimeout)
			{
				if (millisecondsTimeout < -1)
				{
					throw new ArgumentOutOfRangeException("millisecondsTimeout");
				}
				this._total = millisecondsTimeout;
				if (this._total != -1 && this._total != 0)
				{
					this._start = Environment.TickCount;
					return;
				}
				this._start = 0;
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00060D30 File Offset: 0x0005EF30
			public int RemainingMilliseconds
			{
				get
				{
					if (this._total == -1 || this._total == 0)
					{
						return this._total;
					}
					int num = Environment.TickCount - this._start;
					if (num < 0 || num >= this._total)
					{
						return 0;
					}
					return this._total - num;
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00060D79 File Offset: 0x0005EF79
			public bool IsExpired
			{
				get
				{
					return this.RemainingMilliseconds == 0;
				}
			}

			// Token: 0x04000E00 RID: 3584
			private int _total;

			// Token: 0x04000E01 RID: 3585
			private int _start;
		}

		// Token: 0x02000302 RID: 770
		private struct SpinLock
		{
			// Token: 0x06001A6A RID: 6762 RVA: 0x00060D84 File Offset: 0x0005EF84
			private static int GetEnterDeprioritizationStateChange(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				switch (reason & ReaderWriterLockSlim.EnterSpinLockReason.OperationMask)
				{
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead:
					return 0;
				case ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead:
					return 1;
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite:
					return 65536;
				default:
					return 65537;
				}
			}

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00060DB8 File Offset: 0x0005EFB8
			private ushort EnterForEnterAnyReadDeprioritizedCount
			{
				get
				{
					return (ushort)((uint)this._enterDeprioritizationState >> 16);
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00060DC4 File Offset: 0x0005EFC4
			private ushort EnterForEnterAnyWriteDeprioritizedCount
			{
				get
				{
					return (ushort)this._enterDeprioritizationState;
				}
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x00060DCD File Offset: 0x0005EFCD
			private bool IsEnterDeprioritized(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				switch (reason)
				{
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead:
					return this.EnterForEnterAnyReadDeprioritizedCount > 0;
				default:
					return false;
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite:
					return this.EnterForEnterAnyWriteDeprioritizedCount > 0;
				case ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite:
					return this.EnterForEnterAnyWriteDeprioritizedCount > 1;
				}
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x00060E04 File Offset: 0x0005F004
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private bool TryEnter()
			{
				return Interlocked.CompareExchange(ref this._isLocked, 1, 0) == 0;
			}

			// Token: 0x06001A6F RID: 6767 RVA: 0x00060E16 File Offset: 0x0005F016
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Enter(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				if (!this.TryEnter())
				{
					this.EnterSpin(reason);
				}
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x00060E28 File Offset: 0x0005F028
			private void EnterSpin(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				int enterDeprioritizationStateChange = ReaderWriterLockSlim.SpinLock.GetEnterDeprioritizationStateChange(reason);
				if (enterDeprioritizationStateChange != 0)
				{
					Interlocked.Add(ref this._enterDeprioritizationState, enterDeprioritizationStateChange);
				}
				int processorCount = ReaderWriterLockSlim.ProcessorCount;
				int num = 0;
				for (;;)
				{
					if (num < 10 && processorCount > 1)
					{
						Thread.SpinWait(20 * (num + 1));
					}
					else if (num < 15)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Sleep(1);
					}
					if (!this.IsEnterDeprioritized(reason))
					{
						if (this._isLocked == 0 && this.TryEnter())
						{
							break;
						}
					}
					else if (num >= 20)
					{
						reason |= ReaderWriterLockSlim.EnterSpinLockReason.Wait;
						num = -1;
					}
					num++;
				}
				if (enterDeprioritizationStateChange != 0)
				{
					Interlocked.Add(ref this._enterDeprioritizationState, -enterDeprioritizationStateChange);
				}
			}

			// Token: 0x06001A71 RID: 6769 RVA: 0x00060EB9 File Offset: 0x0005F0B9
			public void Exit()
			{
				Volatile.Write(ref this._isLocked, 0);
			}

			// Token: 0x04000E02 RID: 3586
			private int _isLocked;

			// Token: 0x04000E03 RID: 3587
			private int _enterDeprioritizationState;

			// Token: 0x04000E04 RID: 3588
			private const int DeprioritizeEnterAnyReadIncrement = 65536;

			// Token: 0x04000E05 RID: 3589
			private const int DeprioritizeEnterAnyWriteIncrement = 1;

			// Token: 0x04000E06 RID: 3590
			private const int LockSpinCycles = 20;

			// Token: 0x04000E07 RID: 3591
			private const int LockSpinCount = 10;

			// Token: 0x04000E08 RID: 3592
			private const int LockSleep0Count = 5;

			// Token: 0x04000E09 RID: 3593
			private const int DeprioritizedLockSleep1Count = 5;
		}

		// Token: 0x02000303 RID: 771
		[Flags]
		private enum WaiterStates : byte
		{
			// Token: 0x04000E0B RID: 3595
			None = 0,
			// Token: 0x04000E0C RID: 3596
			NoWaiters = 1,
			// Token: 0x04000E0D RID: 3597
			WriteWaiterSignaled = 2,
			// Token: 0x04000E0E RID: 3598
			UpgradeableReadWaiterSignaled = 4
		}

		// Token: 0x02000304 RID: 772
		private enum EnterSpinLockReason
		{
			// Token: 0x04000E10 RID: 3600
			EnterAnyRead,
			// Token: 0x04000E11 RID: 3601
			ExitAnyRead,
			// Token: 0x04000E12 RID: 3602
			EnterWrite,
			// Token: 0x04000E13 RID: 3603
			UpgradeToWrite,
			// Token: 0x04000E14 RID: 3604
			EnterRecursiveWrite,
			// Token: 0x04000E15 RID: 3605
			ExitAnyWrite,
			// Token: 0x04000E16 RID: 3606
			OperationMask = 7,
			// Token: 0x04000E17 RID: 3607
			Wait
		}

		// Token: 0x02000305 RID: 773
		private enum EnterLockType
		{
			// Token: 0x04000E19 RID: 3609
			Read,
			// Token: 0x04000E1A RID: 3610
			UpgradeableRead,
			// Token: 0x04000E1B RID: 3611
			Write,
			// Token: 0x04000E1C RID: 3612
			UpgradeToWrite
		}
	}
}
