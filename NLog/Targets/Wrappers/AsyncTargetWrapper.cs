using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000175 RID: 373
	[Target("AsyncWrapper", IsWrapper = true)]
	public class AsyncTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x000220EA File Offset: 0x000202EA
		public AsyncTargetWrapper() : this(null)
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000220F3 File Offset: 0x000202F3
		public AsyncTargetWrapper(string name, Target wrappedTarget) : this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00022103 File Offset: 0x00020303
		public AsyncTargetWrapper(Target wrappedTarget) : this(wrappedTarget, 10000, AsyncTargetWrapperOverflowAction.Discard)
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00022114 File Offset: 0x00020314
		public AsyncTargetWrapper(Target wrappedTarget, int queueLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			this.RequestQueue = new AsyncRequestQueue(10000, AsyncTargetWrapperOverflowAction.Discard);
			this.TimeToSleepBetweenBatches = 50;
			this.BatchSize = 100;
			base.WrappedTarget = wrappedTarget;
			this.QueueLimit = queueLimit;
			this.OverflowAction = overflowAction;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002217E File Offset: 0x0002037E
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00022186 File Offset: 0x00020386
		[DefaultValue(100)]
		public int BatchSize { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002218F File Offset: 0x0002038F
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00022197 File Offset: 0x00020397
		[DefaultValue(50)]
		public int TimeToSleepBetweenBatches { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000221A0 File Offset: 0x000203A0
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x000221AD File Offset: 0x000203AD
		[DefaultValue("Discard")]
		public AsyncTargetWrapperOverflowAction OverflowAction
		{
			get
			{
				return this.RequestQueue.OnOverflow;
			}
			set
			{
				this.RequestQueue.OnOverflow = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000221BB File Offset: 0x000203BB
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x000221C8 File Offset: 0x000203C8
		[DefaultValue(10000)]
		public int QueueLimit
		{
			get
			{
				return this.RequestQueue.RequestLimit;
			}
			set
			{
				this.RequestQueue.RequestLimit = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000221D6 File Offset: 0x000203D6
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x000221DE File Offset: 0x000203DE
		internal AsyncRequestQueue RequestQueue { get; private set; }

		// Token: 0x06000E14 RID: 3604 RVA: 0x000221E8 File Offset: 0x000203E8
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			lock (this.continuationQueueLock)
			{
				this.flushAllContinuations.Enqueue(asyncContinuation);
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00022230 File Offset: 0x00020430
		protected override void InitializeTarget()
		{
			if (this.TimeToSleepBetweenBatches <= 0)
			{
				throw new NLogConfigurationException("The AysncTargetWrapper's TimeToSleepBetweenBatches property must be > 0");
			}
			base.InitializeTarget();
			this.RequestQueue.Clear();
			InternalLogger.Trace("AsyncWrapper '{0}': start timer", new object[]
			{
				base.Name
			});
			this.lazyWriterTimer = new Timer(new TimerCallback(this.ProcessPendingEvents), null, -1, -1);
			this.StartLazyWriterTimer();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0002229D File Offset: 0x0002049D
		protected override void CloseTarget()
		{
			this.StopLazyWriterThread();
			if (this.RequestQueue.RequestCount > 0)
			{
				this.ProcessPendingEvents(null);
			}
			base.CloseTarget();
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000222C0 File Offset: 0x000204C0
		protected virtual void StartLazyWriterTimer()
		{
			lock (this.lockObject)
			{
				if (this.lazyWriterTimer != null)
				{
					this.lazyWriterTimer.Change(this.TimeToSleepBetweenBatches, -1);
				}
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00022318 File Offset: 0x00020518
		protected virtual void StopLazyWriterThread()
		{
			lock (this.lockObject)
			{
				if (this.lazyWriterTimer != null)
				{
					this.lazyWriterTimer.Change(-1, -1);
					this.lazyWriterTimer = null;
				}
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00022370 File Offset: 0x00020570
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.MergeEventProperties(logEvent.LogEvent);
			base.PrecalculateVolatileLayouts(logEvent.LogEvent);
			this.RequestQueue.Enqueue(logEvent);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000223B8 File Offset: 0x000205B8
		private void ProcessPendingEvents(object state)
		{
			AsyncContinuation[] array3;
			lock (this.continuationQueueLock)
			{
				AsyncContinuation[] array2;
				if (this.flushAllContinuations.Count <= 0)
				{
					AsyncContinuation[] array = new AsyncContinuation[1];
					array2 = array;
				}
				else
				{
					array2 = this.flushAllContinuations.ToArray();
				}
				array3 = array2;
				this.flushAllContinuations.Clear();
			}
			try
			{
				if (base.WrappedTarget == null)
				{
					InternalLogger.Error("AsyncWrapper '{0}': WrappedTarget is NULL", new object[]
					{
						base.Name
					});
				}
				else
				{
					AsyncContinuation[] array4 = array3;
					for (int i = 0; i < array4.Length; i++)
					{
						AsyncContinuation continuation = array4[i];
						int num = this.BatchSize;
						if (continuation != null)
						{
							num = this.RequestQueue.RequestCount;
						}
						InternalLogger.Trace("AsyncWrapper '{0}': Flushing {1} events.", new object[]
						{
							base.Name,
							num
						});
						if (this.RequestQueue.RequestCount == 0 && continuation != null)
						{
							continuation(null);
						}
						AsyncLogEventInfo[] array5 = this.RequestQueue.DequeueBatch(num);
						if (continuation != null)
						{
							base.WrappedTarget.WriteAsyncLogEvents(array5, delegate(Exception ex)
							{
								this.WrappedTarget.Flush(continuation);
							});
						}
						else
						{
							base.WrappedTarget.WriteAsyncLogEvents(array5);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2;
				InternalLogger.Error(ex2, "AsyncWrapper '{0}': Error in lazy writer timer procedure.", new object[]
				{
					base.Name
				});
				if (ex2.MustBeRethrown())
				{
					throw;
				}
			}
			finally
			{
				this.StartLazyWriterTimer();
			}
		}

		// Token: 0x040003EF RID: 1007
		private readonly object lockObject = new object();

		// Token: 0x040003F0 RID: 1008
		private Timer lazyWriterTimer;

		// Token: 0x040003F1 RID: 1009
		private readonly Queue<AsyncContinuation> flushAllContinuations = new Queue<AsyncContinuation>();

		// Token: 0x040003F2 RID: 1010
		private readonly object continuationQueueLock = new object();
	}
}
