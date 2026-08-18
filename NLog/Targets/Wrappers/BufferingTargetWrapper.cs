using System;
using System.ComponentModel;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000178 RID: 376
	[Target("BufferingWrapper", IsWrapper = true)]
	public class BufferingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x0002260A File Offset: 0x0002080A
		public BufferingTargetWrapper() : this(null)
		{
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00022613 File Offset: 0x00020813
		public BufferingTargetWrapper(string name, Target wrappedTarget) : this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00022623 File Offset: 0x00020823
		public BufferingTargetWrapper(Target wrappedTarget) : this(wrappedTarget, 100)
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002262E File Offset: 0x0002082E
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize) : this(wrappedTarget, bufferSize, -1)
		{
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00022639 File Offset: 0x00020839
		public BufferingTargetWrapper(Target wrappedTarget, int bufferSize, int flushTimeout)
		{
			base.WrappedTarget = wrappedTarget;
			this.BufferSize = bufferSize;
			this.FlushTimeout = flushTimeout;
			this.SlidingTimeout = true;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0002265D File Offset: 0x0002085D
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00022665 File Offset: 0x00020865
		[DefaultValue(100)]
		public int BufferSize { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0002266E File Offset: 0x0002086E
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00022676 File Offset: 0x00020876
		[DefaultValue(-1)]
		public int FlushTimeout { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002267F File Offset: 0x0002087F
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00022687 File Offset: 0x00020887
		[DefaultValue(true)]
		public bool SlidingTimeout { get; set; }

		// Token: 0x06000E2A RID: 3626 RVA: 0x000226B0 File Offset: 0x000208B0
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
			if (eventsAndClear.Length == 0)
			{
				base.WrappedTarget.Flush(asyncContinuation);
				return;
			}
			InternalLogger.Trace("BufferingWrapper '{0}': Flush {1} events async", new object[]
			{
				base.Name,
				eventsAndClear.Length
			});
			base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear, delegate(Exception ex)
			{
				this.WrappedTarget.Flush(asyncContinuation);
			});
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002273C File Offset: 0x0002093C
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this.buffer = new LogEventInfoBuffer(this.BufferSize, false, 0);
			InternalLogger.Trace("BufferingWrapper '{0}': start timer", new object[]
			{
				base.Name
			});
			this.flushTimer = new Timer(new TimerCallback(this.FlushCallback), null, -1, -1);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00022797 File Offset: 0x00020997
		protected override void CloseTarget()
		{
			base.CloseTarget();
			if (this.flushTimer != null)
			{
				this.flushTimer.Dispose();
				this.flushTimer = null;
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000227BC File Offset: 0x000209BC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.WrappedTarget.PrecalculateVolatileLayouts(logEvent.LogEvent);
			int num = this.buffer.Append(logEvent);
			if (num >= this.BufferSize)
			{
				InternalLogger.Trace("BufferingWrapper '{0}': writing {1} events because of exceeding buffersize ({0}).", new object[]
				{
					base.Name,
					num
				});
				AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
				base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear);
				return;
			}
			if (this.FlushTimeout > 0 && (this.SlidingTimeout || num == 1))
			{
				this.flushTimer.Change(this.FlushTimeout, -1);
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00022858 File Offset: 0x00020A58
		private void FlushCallback(object state)
		{
			lock (base.SyncRoot)
			{
				if (base.IsInitialized)
				{
					AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
					if (eventsAndClear.Length > 0)
					{
						base.WrappedTarget.WriteAsyncLogEvents(eventsAndClear);
					}
				}
			}
		}

		// Token: 0x040003FA RID: 1018
		private LogEventInfoBuffer buffer;

		// Token: 0x040003FB RID: 1019
		private Timer flushTimer;
	}
}
