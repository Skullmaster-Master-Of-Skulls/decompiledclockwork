using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003D RID: 61
	internal abstract class IteratorAsyncResult<TIteratorState> : AsyncResult
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00008877 File Offset: 0x00006A77
		protected IteratorAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
			this.onStepCompletedCallback = Fx.ThunkCallback(new AsyncCallback(this.OnStepCompleted));
			this.thisLock = new object();
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000088A3 File Offset: 0x00006AA3
		protected TimeSpan OriginalTimeout
		{
			get
			{
				return this.timeoutHelper.OriginalTimeout;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000088B0 File Offset: 0x00006AB0
		public static IteratorAsyncResult<TIteratorState>.AsyncStep CallAsync(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end)
		{
			return new IteratorAsyncResult<TIteratorState>.AsyncStep(begin, end, false);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000088BA File Offset: 0x00006ABA
		public static IteratorAsyncResult<TIteratorState>.AsyncStep CallAsync(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end, IteratorAsyncResult<TIteratorState>.IAsyncCatch[] catches)
		{
			return new IteratorAsyncResult<TIteratorState>.AsyncStep(begin, end, false, catches);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000088C5 File Offset: 0x00006AC5
		public static IteratorAsyncResult<TIteratorState>.AsyncStep CallParallel(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end)
		{
			return new IteratorAsyncResult<TIteratorState>.AsyncStep(begin, end, true);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000088CF File Offset: 0x00006ACF
		public static IteratorAsyncResult<TIteratorState>.AsyncStep CallParallel(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end, IteratorAsyncResult<TIteratorState>.IAsyncCatch[] catches)
		{
			return new IteratorAsyncResult<TIteratorState>.AsyncStep(begin, end, true, catches);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000088DA File Offset: 0x00006ADA
		protected void Start(TIteratorState iterState, TimeSpan timeout)
		{
			this.iterState = iterState;
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.completedSynchronously = true;
			this.steps = this.GetAsyncSteps();
			this.ExecuteSteps();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008908 File Offset: 0x00006B08
		protected TimeSpan RemainingTime()
		{
			return this.timeoutHelper.RemainingTime();
		}

		// Token: 0x06000302 RID: 770
		protected abstract IEnumerator<IteratorAsyncResult<TIteratorState>.AsyncStep> GetAsyncSteps();

		// Token: 0x06000303 RID: 771 RVA: 0x00008915 File Offset: 0x00006B15
		protected void CompleteOnce()
		{
			this.CompleteOnce(null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000891E File Offset: 0x00006B1E
		protected void CompleteOnce(Exception error)
		{
			if (Interlocked.CompareExchange(ref this.completedCalled, 1, 0) == 0)
			{
				base.Complete(this.completedSynchronously, error);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000893C File Offset: 0x00006B3C
		private void ExecuteSteps()
		{
			while (!base.IsCompleted)
			{
				if (!this.steps.MoveNext())
				{
					this.CompleteIfNoPendingSteps();
					return;
				}
				IteratorAsyncResult<TIteratorState>.AsyncStep asyncStep = this.steps.Current;
				IAsyncResult asyncResult = this.StartStep(asyncStep);
				if (asyncResult != null)
				{
					if (asyncResult.CompletedSynchronously)
					{
						this.FinishStep(asyncStep, asyncResult);
					}
					else if (!asyncStep.IsParallel)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008998 File Offset: 0x00006B98
		private IAsyncResult StartStep(IteratorAsyncResult<TIteratorState>.AsyncStep step)
		{
			IAsyncResult result = null;
			Exception ex = null;
			try
			{
				this.OnStepStart();
				result = step.Begin(this.iterState, this.timeoutHelper.RemainingTime(), this.onStepCompletedCallback, step);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				this.HandleException(ex, step);
			}
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008A00 File Offset: 0x00006C00
		private void OnStepStart()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.numPendingSteps++;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008A48 File Offset: 0x00006C48
		private void OnStepCompletion()
		{
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				this.numPendingSteps--;
				if (this.numPendingSteps == 0 && this.shouldComplete)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.CompleteOnce();
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008AB0 File Offset: 0x00006CB0
		private void CompleteIfNoPendingSteps()
		{
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.numPendingSteps == 0)
				{
					flag = true;
				}
				else
				{
					this.shouldComplete = true;
				}
			}
			if (flag)
			{
				this.CompleteOnce();
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008B08 File Offset: 0x00006D08
		private void OnStepCompleted(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			this.completedSynchronously = false;
			IteratorAsyncResult<TIteratorState>.AsyncStep asyncStep = (IteratorAsyncResult<TIteratorState>.AsyncStep)result.AsyncState;
			this.FinishStep(asyncStep, result);
			if (!asyncStep.IsParallel)
			{
				this.ExecuteSteps();
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008B48 File Offset: 0x00006D48
		private void FinishStep(IteratorAsyncResult<TIteratorState>.AsyncStep step, IAsyncResult result)
		{
			Exception ex = null;
			try
			{
				step.End(this.iterState, result);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				this.HandleException(ex, step);
			}
			this.OnStepCompletion();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008B9C File Offset: 0x00006D9C
		private void HandleException(Exception e, IteratorAsyncResult<TIteratorState>.AsyncStep step)
		{
			if (step.Catches != null)
			{
				for (int i = 0; i < step.Catches.Length; i++)
				{
					Exception ex;
					if (step.Catches[i].HandleException(this.iterState, e, out ex))
					{
						if (ex == null)
						{
							return;
						}
						this.CompleteOnce(ex);
					}
				}
			}
			this.CompleteOnce(e);
		}

		// Token: 0x040000A6 RID: 166
		private TIteratorState iterState;

		// Token: 0x040000A7 RID: 167
		private TimeoutHelper timeoutHelper;

		// Token: 0x040000A8 RID: 168
		private IEnumerator<IteratorAsyncResult<TIteratorState>.AsyncStep> steps;

		// Token: 0x040000A9 RID: 169
		private bool completedSynchronously;

		// Token: 0x040000AA RID: 170
		private int completedCalled;

		// Token: 0x040000AB RID: 171
		private int numPendingSteps;

		// Token: 0x040000AC RID: 172
		private bool shouldComplete;

		// Token: 0x040000AD RID: 173
		private object thisLock;

		// Token: 0x040000AE RID: 174
		private AsyncCallback onStepCompletedCallback;

		// Token: 0x020000D3 RID: 211
		// (Invoke) Token: 0x060007FA RID: 2042
		public delegate IAsyncResult BeginCall(TIteratorState iterState, TimeSpan timeout, AsyncCallback asyncCallback, object state);

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x060007FE RID: 2046
		public delegate void EndCall(TIteratorState iterState, IAsyncResult result);

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x06000802 RID: 2050
		public delegate Exception ExceptionHandler<TException>(TIteratorState iterState, TException exception) where TException : Exception;

		// Token: 0x020000D6 RID: 214
		public class AsyncStep
		{
			// Token: 0x06000805 RID: 2053 RVA: 0x00014F3A File Offset: 0x0001313A
			public AsyncStep(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end, bool isParallel)
			{
				this.Begin = begin;
				this.End = end;
				this.IsParallel = isParallel;
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x00014F57 File Offset: 0x00013157
			public AsyncStep(IteratorAsyncResult<TIteratorState>.BeginCall begin, IteratorAsyncResult<TIteratorState>.EndCall end, bool isParallel, IteratorAsyncResult<TIteratorState>.IAsyncCatch[] catches) : this(begin, end, isParallel)
			{
				this.Catches = catches;
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000807 RID: 2055 RVA: 0x00014F6A File Offset: 0x0001316A
			// (set) Token: 0x06000808 RID: 2056 RVA: 0x00014F72 File Offset: 0x00013172
			public IteratorAsyncResult<TIteratorState>.IAsyncCatch[] Catches { get; private set; }

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000809 RID: 2057 RVA: 0x00014F7B File Offset: 0x0001317B
			// (set) Token: 0x0600080A RID: 2058 RVA: 0x00014F83 File Offset: 0x00013183
			public IteratorAsyncResult<TIteratorState>.BeginCall Begin { get; private set; }

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x0600080B RID: 2059 RVA: 0x00014F8C File Offset: 0x0001318C
			// (set) Token: 0x0600080C RID: 2060 RVA: 0x00014F94 File Offset: 0x00013194
			public IteratorAsyncResult<TIteratorState>.EndCall End { get; private set; }

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600080D RID: 2061 RVA: 0x00014F9D File Offset: 0x0001319D
			// (set) Token: 0x0600080E RID: 2062 RVA: 0x00014FA5 File Offset: 0x000131A5
			public bool IsParallel { get; private set; }
		}

		// Token: 0x020000D7 RID: 215
		public interface IAsyncCatch
		{
			// Token: 0x0600080F RID: 2063
			bool HandleException(TIteratorState iterState, Exception ex, out Exception outEx);
		}

		// Token: 0x020000D8 RID: 216
		public class AsyncCatch<TException> : IteratorAsyncResult<TIteratorState>.IAsyncCatch where TException : Exception
		{
			// Token: 0x06000810 RID: 2064 RVA: 0x00014FAE File Offset: 0x000131AE
			public AsyncCatch(IteratorAsyncResult<TIteratorState>.ExceptionHandler<TException> handler)
			{
				this.handler = handler;
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x00014FC0 File Offset: 0x000131C0
			public bool HandleException(TIteratorState state, Exception ex, out Exception outEx)
			{
				outEx = null;
				TException ex2 = ex as TException;
				if (ex2 != null)
				{
					outEx = this.handler(state, ex2);
					return true;
				}
				return false;
			}

			// Token: 0x04000212 RID: 530
			private readonly IteratorAsyncResult<TIteratorState>.ExceptionHandler<TException> handler;
		}
	}
}
