using System;
using System.Collections.Generic;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000044 RID: 68
	internal abstract class RandomDelayQueuedSendsAsyncResult<TItem> : IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>> where TItem : class
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000961C File Offset: 0x0000781C
		public RandomDelayQueuedSendsAsyncResult(TimeSpan maxRandomDelay, InputQueue<TItem> itemQueue, AsyncCallback callback, object state) : base(callback, state)
		{
			this.itemQueue = itemQueue;
			this.doDelay = (maxRandomDelay > TimeSpan.Zero);
			if (this.doDelay)
			{
				this.random = new Random();
				this.maxRandomDelayInMillis = maxRandomDelay.TotalMilliseconds;
				if (this.itemQueue.PendingCount > 0)
				{
					this.preCalculatedDelays = new int[this.itemQueue.PendingCount];
					this.PreCalculateSendDelays();
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009694 File Offset: 0x00007894
		public IAsyncResult BeginDelay(AsyncCallback callback, object state)
		{
			return new RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult(this, callback, state);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000969E File Offset: 0x0000789E
		public void EndDelay(IAsyncResult result)
		{
			RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult.End(result);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000096A6 File Offset: 0x000078A6
		protected override IEnumerator<IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep> GetAsyncSteps()
		{
			for (;;)
			{
				yield return RandomDelayQueuedSendsAsyncResult<TItem>.GetDequeueStep();
				if (this.currentItem == null)
				{
					break;
				}
				if (this.doDelay)
				{
					yield return RandomDelayQueuedSendsAsyncResult<TItem>.GetDelayStep();
				}
				yield return RandomDelayQueuedSendsAsyncResult<TItem>.GetSendItemStep();
			}
			yield break;
			yield break;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000096B5 File Offset: 0x000078B5
		protected void Start(TimeSpan timeout)
		{
			base.Start(this, timeout);
		}

		// Token: 0x06000353 RID: 851
		protected abstract IAsyncResult OnBeginSendItem(TItem item, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06000354 RID: 852
		protected abstract void OnEndSendItem(IAsyncResult result);

		// Token: 0x06000355 RID: 853 RVA: 0x000096C0 File Offset: 0x000078C0
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep GetDequeueStep()
		{
			if (RandomDelayQueuedSendsAsyncResult<TItem>.dequeueStep == null)
			{
				RandomDelayQueuedSendsAsyncResult<TItem>.dequeueStep = IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.CallAsync((RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, TimeSpan t, AsyncCallback c, object s) => thisPtr.itemQueue.BeginDequeue(TimeSpan.MaxValue, c, s), delegate(RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, IAsyncResult r)
				{
					thisPtr.currentItem = thisPtr.itemQueue.EndDequeue(r);
				});
			}
			return RandomDelayQueuedSendsAsyncResult<TItem>.dequeueStep;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009724 File Offset: 0x00007924
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep GetDelayStep()
		{
			if (RandomDelayQueuedSendsAsyncResult<TItem>.delayStep == null)
			{
				RandomDelayQueuedSendsAsyncResult<TItem>.delayStep = IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.CallAsync((RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, TimeSpan t, AsyncCallback c, object s) => thisPtr.BeginDelay(c, s), delegate(RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, IAsyncResult r)
				{
					thisPtr.EndDelay(r);
				});
			}
			return RandomDelayQueuedSendsAsyncResult<TItem>.delayStep;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009788 File Offset: 0x00007988
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep GetSendItemStep()
		{
			if (RandomDelayQueuedSendsAsyncResult<TItem>.sendItemStep == null)
			{
				RandomDelayQueuedSendsAsyncResult<TItem>.sendItemStep = IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.CallParallel((RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, TimeSpan t, AsyncCallback c, object s) => thisPtr.OnBeginSendItem(thisPtr.currentItem, t, c, s), delegate(RandomDelayQueuedSendsAsyncResult<TItem> thisPtr, IAsyncResult r)
				{
					thisPtr.OnEndSendItem(r);
				});
			}
			return RandomDelayQueuedSendsAsyncResult<TItem>.sendItemStep;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000097EC File Offset: 0x000079EC
		private void PreCalculateSendDelays()
		{
			this.currentDelayIndex = 0;
			for (int i = 0; i < this.preCalculatedDelays.Length; i++)
			{
				this.preCalculatedDelays[i] = (int)(this.random.NextDouble() * this.maxRandomDelayInMillis);
			}
			Array.Sort<int>(this.preCalculatedDelays);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000983C File Offset: 0x00007A3C
		private int GetNextDelay()
		{
			int result;
			if (this.preCalculatedDelays == null || this.preCalculatedDelays.Length == 0)
			{
				result = (int)(this.maxRandomDelayInMillis * this.random.NextDouble());
			}
			else if (this.preCalculatedDelays.Length == 1 || this.currentDelayIndex == 0)
			{
				result = this.preCalculatedDelays[0];
			}
			else
			{
				this.currentDelayIndex++;
				if (this.currentDelayIndex == this.preCalculatedDelays.Length)
				{
					this.currentDelayIndex = 1;
				}
				result = this.preCalculatedDelays[this.currentDelayIndex] - this.preCalculatedDelays[this.currentDelayIndex - 1];
			}
			return result;
		}

		// Token: 0x040000D0 RID: 208
		private readonly InputQueue<TItem> itemQueue;

		// Token: 0x040000D1 RID: 209
		private readonly Random random;

		// Token: 0x040000D2 RID: 210
		private readonly double maxRandomDelayInMillis;

		// Token: 0x040000D3 RID: 211
		private readonly int[] preCalculatedDelays;

		// Token: 0x040000D4 RID: 212
		private readonly bool doDelay;

		// Token: 0x040000D5 RID: 213
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep dequeueStep;

		// Token: 0x040000D6 RID: 214
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep delayStep;

		// Token: 0x040000D7 RID: 215
		private static IteratorAsyncResult<RandomDelayQueuedSendsAsyncResult<TItem>>.AsyncStep sendItemStep;

		// Token: 0x040000D8 RID: 216
		private TItem currentItem;

		// Token: 0x040000D9 RID: 217
		private int currentDelayIndex;

		// Token: 0x020000E6 RID: 230
		private class DelayAsyncResult : AsyncResult
		{
			// Token: 0x0600082F RID: 2095 RVA: 0x00015314 File Offset: 0x00013514
			public DelayAsyncResult(RandomDelayQueuedSendsAsyncResult<TItem> parent, AsyncCallback callback, object state) : base(callback, state)
			{
				int nextDelay = parent.GetNextDelay();
				if (nextDelay != 0)
				{
					this.delayTimer = new IOThreadTimer(RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult.onDelayCompletedCallback, this, true);
					this.delayTimer.Set(nextDelay);
					return;
				}
				base.Complete(true);
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x00015359 File Offset: 0x00013559
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult>(result);
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x00015364 File Offset: 0x00013564
			private static void OnDelayCompleted(object state)
			{
				RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult delayAsyncResult = (RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult)state;
				delayAsyncResult.delayTimer.Cancel();
				delayAsyncResult.Complete(false);
			}

			// Token: 0x0400027A RID: 634
			private readonly IOThreadTimer delayTimer;

			// Token: 0x0400027B RID: 635
			private static Action<object> onDelayCompletedCallback = new Action<object>(RandomDelayQueuedSendsAsyncResult<TItem>.DelayAsyncResult.OnDelayCompleted);
		}
	}
}
