using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020003D6 RID: 982
	[ComVisible(false)]
	[DebuggerDisplay("Participant Count={ParticipantCount},Participants Remaining={ParticipantsRemaining}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Barrier : IDisposable
	{
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000AF944 File Offset: 0x000ADB44
		[__DynamicallyInvokable]
		public int ParticipantsRemaining
		{
			[__DynamicallyInvokable]
			get
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num = currentTotalCount & 32767;
				int num2 = (currentTotalCount & 2147418112) >> 16;
				return num - num2;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000AF970 File Offset: 0x000ADB70
		[__DynamicallyInvokable]
		public int ParticipantCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_currentTotalCount & 32767;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000AF980 File Offset: 0x000ADB80
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x000AF98D File Offset: 0x000ADB8D
		[__DynamicallyInvokable]
		public long CurrentPhaseNumber
		{
			[__DynamicallyInvokable]
			get
			{
				return Volatile.Read(ref this.m_currentPhase);
			}
			internal set
			{
				Volatile.Write(ref this.m_currentPhase, value);
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000AF99B File Offset: 0x000ADB9B
		[__DynamicallyInvokable]
		public Barrier(int participantCount) : this(participantCount, null)
		{
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000AF9A8 File Offset: 0x000ADBA8
		[__DynamicallyInvokable]
		public Barrier(int participantCount, Action<Barrier> postPhaseAction)
		{
			if (participantCount < 0 || participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_ctor_ArgumentOutOfRange"));
			}
			this.m_currentTotalCount = participantCount;
			this.m_postPhaseAction = postPhaseAction;
			this.m_oddEvent = new ManualResetEventSlim(true);
			this.m_evenEvent = new ManualResetEventSlim(false);
			if (postPhaseAction != null && !ExecutionContext.IsFlowSuppressed())
			{
				this.m_ownerThreadContext = ExecutionContext.Capture();
			}
			this.m_actionCallerID = 0;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000AFA26 File Offset: 0x000ADC26
		private void GetCurrentTotal(int currentTotal, out int current, out int total, out bool sense)
		{
			total = (currentTotal & 32767);
			current = (currentTotal & 2147418112) >> 16;
			sense = ((currentTotal & int.MinValue) == 0);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000AFA50 File Offset: 0x000ADC50
		private bool SetCurrentTotal(int currentTotal, int current, int total, bool sense)
		{
			int num = current << 16 | total;
			if (!sense)
			{
				num |= int.MinValue;
			}
			return Interlocked.CompareExchange(ref this.m_currentTotalCount, num, currentTotal) == currentTotal;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000AFA80 File Offset: 0x000ADC80
		[__DynamicallyInvokable]
		public long AddParticipant()
		{
			long result;
			try
			{
				result = this.AddParticipants(1);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			}
			return result;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000AFABC File Offset: 0x000ADCBC
		[__DynamicallyInvokable]
		public long AddParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_AddParticipants_NonPositive_ArgumentOutOfRange"));
			}
			if (participantCount > 32767)
			{
				throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int current;
				int num;
				this.GetCurrentTotal(currentTotalCount, out current, out num, out flag);
				if (participantCount + num > 32767)
				{
					break;
				}
				if (this.SetCurrentTotal(currentTotalCount, current, num + participantCount, flag))
				{
					goto Block_6;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_AddParticipants_Overflow_ArgumentOutOfRange"));
			Block_6:
			long currentPhaseNumber = this.CurrentPhaseNumber;
			long num2 = (flag != (currentPhaseNumber % 2L == 0L)) ? (currentPhaseNumber + 1L) : currentPhaseNumber;
			if (num2 != currentPhaseNumber)
			{
				if (flag)
				{
					this.m_oddEvent.Wait();
				}
				else
				{
					this.m_evenEvent.Wait();
				}
			}
			else if (flag && this.m_evenEvent.IsSet)
			{
				this.m_evenEvent.Reset();
			}
			else if (!flag && this.m_oddEvent.IsSet)
			{
				this.m_oddEvent.Reset();
			}
			return num2;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000AFC10 File Offset: 0x000ADE10
		[__DynamicallyInvokable]
		public void RemoveParticipant()
		{
			this.RemoveParticipants(1);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000AFC1C File Offset: 0x000ADE1C
		[__DynamicallyInvokable]
		public void RemoveParticipants(int participantCount)
		{
			this.ThrowIfDisposed();
			if (participantCount < 1)
			{
				throw new ArgumentOutOfRangeException("participantCount", participantCount, SR.GetString("Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				if (num2 < participantCount)
				{
					break;
				}
				if (num2 - participantCount < num)
				{
					goto Block_5;
				}
				int num3 = num2 - participantCount;
				if (num3 > 0 && num == num3)
				{
					if (this.SetCurrentTotal(currentTotalCount, 0, num2 - participantCount, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(currentTotalCount, num, num2 - participantCount, flag))
				{
					return;
				}
				spinWait.SpinOnce();
			}
			throw new ArgumentOutOfRangeException("participantCount", SR.GetString("Barrier_RemoveParticipants_ArgumentOutOfRange"));
			Block_5:
			throw new InvalidOperationException(SR.GetString("Barrier_RemoveParticipants_InvalidOperation"));
			Block_8:
			this.FinishPhase(flag);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000AFD0C File Offset: 0x000ADF0C
		[__DynamicallyInvokable]
		public void SignalAndWait()
		{
			this.SignalAndWait(default(CancellationToken));
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000AFD28 File Offset: 0x000ADF28
		[__DynamicallyInvokable]
		public void SignalAndWait(CancellationToken cancellationToken)
		{
			this.SignalAndWait(-1, cancellationToken);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000AFD34 File Offset: 0x000ADF34
		[__DynamicallyInvokable]
		public bool SignalAndWait(TimeSpan timeout)
		{
			return this.SignalAndWait(timeout, default(CancellationToken));
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000AFD54 File Offset: 0x000ADF54
		[__DynamicallyInvokable]
		public bool SignalAndWait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("Barrier_SignalAndWait_ArgumentOutOfRange"));
			}
			return this.SignalAndWait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000AFDA4 File Offset: 0x000ADFA4
		[__DynamicallyInvokable]
		public bool SignalAndWait(int millisecondsTimeout)
		{
			return this.SignalAndWait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000AFDC4 File Offset: 0x000ADFC4
		[__DynamicallyInvokable]
		public bool SignalAndWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.GetString("Barrier_SignalAndWait_ArgumentOutOfRange"));
			}
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			SpinWait spinWait = default(SpinWait);
			bool flag;
			long currentPhaseNumber;
			for (;;)
			{
				int currentTotalCount = this.m_currentTotalCount;
				int num;
				int num2;
				this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag);
				currentPhaseNumber = this.CurrentPhaseNumber;
				if (num2 == 0)
				{
					break;
				}
				if (num == 0 && flag != (this.CurrentPhaseNumber % 2L == 0L))
				{
					goto Block_6;
				}
				if (num + 1 == num2)
				{
					if (this.SetCurrentTotal(currentTotalCount, 0, num2, !flag))
					{
						goto Block_8;
					}
				}
				else if (this.SetCurrentTotal(currentTotalCount, num + 1, num2, flag))
				{
					goto IL_107;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(SR.GetString("Barrier_SignalAndWait_InvalidOperation_ZeroTotal"));
			Block_6:
			throw new InvalidOperationException(SR.GetString("Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded"));
			Block_8:
			if (CdsSyncEtwBCLProvider.Log.IsEnabled())
			{
				CdsSyncEtwBCLProvider.Log.Barrier_PhaseFinished(flag, this.CurrentPhaseNumber);
			}
			this.FinishPhase(flag);
			return true;
			IL_107:
			ManualResetEventSlim currentPhaseEvent = flag ? this.m_evenEvent : this.m_oddEvent;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				flag3 = this.DiscontinuousWait(currentPhaseEvent, millisecondsTimeout, cancellationToken, currentPhaseNumber);
			}
			catch (OperationCanceledException)
			{
				flag2 = true;
			}
			catch (ObjectDisposedException)
			{
				if (currentPhaseNumber >= this.CurrentPhaseNumber)
				{
					throw;
				}
				flag3 = true;
			}
			if (!flag3)
			{
				spinWait.Reset();
				for (;;)
				{
					int currentTotalCount = this.m_currentTotalCount;
					int num;
					int num2;
					bool flag4;
					this.GetCurrentTotal(currentTotalCount, out num, out num2, out flag4);
					if (currentPhaseNumber < this.CurrentPhaseNumber || flag != flag4)
					{
						break;
					}
					if (this.SetCurrentTotal(currentTotalCount, num - 1, num2, flag))
					{
						goto Block_14;
					}
					spinWait.SpinOnce();
				}
				this.WaitCurrentPhase(currentPhaseEvent, currentPhaseNumber);
				goto IL_1B4;
				Block_14:
				if (flag2)
				{
					throw new OperationCanceledException(SR.GetString("Common_OperationCanceled"), cancellationToken);
				}
				return false;
			}
			IL_1B4:
			if (this.m_exception != null)
			{
				throw new BarrierPostPhaseException(this.m_exception);
			}
			return true;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000AFFB8 File Offset: 0x000AE1B8
		[SecuritySafeCritical]
		private void FinishPhase(bool observedSense)
		{
			if (this.m_postPhaseAction != null)
			{
				try
				{
					this.m_actionCallerID = Thread.CurrentThread.ManagedThreadId;
					if (this.m_ownerThreadContext != null)
					{
						ExecutionContext ownerThreadContext = this.m_ownerThreadContext;
						this.m_ownerThreadContext = this.m_ownerThreadContext.CreateCopy();
						ContextCallback contextCallback = Barrier.s_invokePostPhaseAction;
						if (contextCallback == null)
						{
							contextCallback = (Barrier.s_invokePostPhaseAction = new ContextCallback(Barrier.InvokePostPhaseAction));
						}
						ExecutionContext.Run(ownerThreadContext, contextCallback, this);
						ownerThreadContext.Dispose();
					}
					else
					{
						this.m_postPhaseAction(this);
					}
					this.m_exception = null;
					return;
				}
				catch (Exception exception)
				{
					this.m_exception = exception;
					return;
				}
				finally
				{
					this.m_actionCallerID = 0;
					this.SetResetEvents(observedSense);
					if (this.m_exception != null)
					{
						throw new BarrierPostPhaseException(this.m_exception);
					}
				}
			}
			this.SetResetEvents(observedSense);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000B0094 File Offset: 0x000AE294
		[SecurityCritical]
		private static void InvokePostPhaseAction(object obj)
		{
			Barrier barrier = (Barrier)obj;
			barrier.m_postPhaseAction(barrier);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000B00B4 File Offset: 0x000AE2B4
		private void SetResetEvents(bool observedSense)
		{
			this.CurrentPhaseNumber += 1L;
			if (observedSense)
			{
				this.m_oddEvent.Reset();
				this.m_evenEvent.Set();
				return;
			}
			this.m_evenEvent.Reset();
			this.m_oddEvent.Set();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000B0100 File Offset: 0x000AE300
		private void WaitCurrentPhase(ManualResetEventSlim currentPhaseEvent, long observedPhase)
		{
			SpinWait spinWait = default(SpinWait);
			while (!currentPhaseEvent.IsSet && this.CurrentPhaseNumber - observedPhase <= 1L)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000B0134 File Offset: 0x000AE334
		private bool DiscontinuousWait(ManualResetEventSlim currentPhaseEvent, int totalTimeout, CancellationToken token, long observedPhase)
		{
			int num = 100;
			int num2 = 10000;
			while (observedPhase == this.CurrentPhaseNumber)
			{
				int num3 = (totalTimeout == -1) ? num : Math.Min(num, totalTimeout);
				if (currentPhaseEvent.Wait(num3, token))
				{
					return true;
				}
				if (totalTimeout != -1)
				{
					totalTimeout -= num3;
					if (totalTimeout <= 0)
					{
						return false;
					}
				}
				num = ((num >= num2) ? num2 : Math.Min(num << 1, num2));
			}
			this.WaitCurrentPhase(currentPhaseEvent, observedPhase);
			return true;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000B019B File Offset: 0x000AE39B
		[__DynamicallyInvokable]
		public void Dispose()
		{
			if (this.m_actionCallerID != 0 && Thread.CurrentThread.ManagedThreadId == this.m_actionCallerID)
			{
				throw new InvalidOperationException(SR.GetString("Barrier_InvalidOperation_CalledFromPHA"));
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000B01D4 File Offset: 0x000AE3D4
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_oddEvent.Dispose();
					this.m_evenEvent.Dispose();
					if (this.m_ownerThreadContext != null)
					{
						this.m_ownerThreadContext.Dispose();
						this.m_ownerThreadContext = null;
					}
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000B0223 File Offset: 0x000AE423
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Barrier", SR.GetString("Barrier_Dispose"));
			}
		}

		// Token: 0x04002067 RID: 8295
		private volatile int m_currentTotalCount;

		// Token: 0x04002068 RID: 8296
		private const int CURRENT_MASK = 2147418112;

		// Token: 0x04002069 RID: 8297
		private const int TOTAL_MASK = 32767;

		// Token: 0x0400206A RID: 8298
		private const int SENSE_MASK = -2147483648;

		// Token: 0x0400206B RID: 8299
		private const int MAX_PARTICIPANTS = 32767;

		// Token: 0x0400206C RID: 8300
		private long m_currentPhase;

		// Token: 0x0400206D RID: 8301
		private bool m_disposed;

		// Token: 0x0400206E RID: 8302
		private ManualResetEventSlim m_oddEvent;

		// Token: 0x0400206F RID: 8303
		private ManualResetEventSlim m_evenEvent;

		// Token: 0x04002070 RID: 8304
		private ExecutionContext m_ownerThreadContext;

		// Token: 0x04002071 RID: 8305
		[SecurityCritical]
		private static ContextCallback s_invokePostPhaseAction;

		// Token: 0x04002072 RID: 8306
		private Action<Barrier> m_postPhaseAction;

		// Token: 0x04002073 RID: 8307
		private Exception m_exception;

		// Token: 0x04002074 RID: 8308
		private int m_actionCallerID;
	}
}
