using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Management
{
	// Token: 0x02000183 RID: 387
	internal sealed class WebEventBuffer
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x00040018 File Offset: 0x0003E218
		internal WebEventBuffer(BufferedWebEventProvider provider, string bufferMode, WebEventBufferFlushCallback callback)
		{
			this._provider = provider;
			HealthMonitoringSection healthMonitoring = RuntimeConfig.GetAppLKGConfig().HealthMonitoring;
			BufferModesCollection bufferModes = healthMonitoring.BufferModes;
			BufferModeSettings bufferModeSettings = bufferModes[bufferMode];
			if (bufferModeSettings == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Health_mon_buffer_mode_not_found", new object[]
				{
					bufferMode
				}));
			}
			if (bufferModeSettings.RegularFlushInterval == TimeSpan.MaxValue)
			{
				this._regularFlushIntervalMs = WebEventBuffer.Infinite;
			}
			else
			{
				try
				{
					this._regularFlushIntervalMs = (long)bufferModeSettings.RegularFlushInterval.TotalMilliseconds;
				}
				catch (OverflowException)
				{
					this._regularFlushIntervalMs = WebEventBuffer.Infinite;
				}
			}
			if (bufferModeSettings.UrgentFlushInterval == TimeSpan.MaxValue)
			{
				this._urgentFlushIntervalMs = WebEventBuffer.Infinite;
			}
			else
			{
				try
				{
					this._urgentFlushIntervalMs = (long)bufferModeSettings.UrgentFlushInterval.TotalMilliseconds;
				}
				catch (OverflowException)
				{
					this._urgentFlushIntervalMs = WebEventBuffer.Infinite;
				}
			}
			this._urgentFlushThreshold = bufferModeSettings.UrgentFlushThreshold;
			this._maxBufferSize = bufferModeSettings.MaxBufferSize;
			this._maxFlushSize = bufferModeSettings.MaxFlushSize;
			this._maxBufferThreads = bufferModeSettings.MaxBufferThreads;
			this._burstWaitTimeMs = Math.Min(this._burstWaitTimeMs, this._urgentFlushIntervalMs);
			this._flushCallback = callback;
			this._buffer = new Queue();
			if (this._regularFlushIntervalMs != WebEventBuffer.Infinite)
			{
				this._startTime = DateTime.UtcNow;
				this._regularTimeoutUsed = true;
				this._urgentFlushScheduled = false;
				this.SetTimer(this.GetNextRegularFlushDueTimeInMs());
			}
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000401D4 File Offset: 0x0003E3D4
		private void FlushTimerCallback(object state)
		{
			this.Flush(this._maxFlushSize, FlushCallReason.Timer);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000401E4 File Offset: 0x0003E3E4
		private bool AnticipateBurst(DateTime now)
		{
			return this._urgentFlushThreshold == 1 && this._buffer.Count == 1 && (now - this._lastAdd).TotalMilliseconds >= (double)this._urgentFlushIntervalMs;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004022C File Offset: 0x0003E42C
		private long GetNextRegularFlushDueTimeInMs()
		{
			long regularFlushIntervalMs = this._regularFlushIntervalMs;
			if (this._regularFlushIntervalMs == WebEventBuffer.Infinite)
			{
				return WebEventBuffer.Infinite;
			}
			DateTime utcNow = DateTime.UtcNow;
			long num = (long)(utcNow - this._startTime).TotalMilliseconds;
			long num2 = (num + regularFlushIntervalMs + 499L) / regularFlushIntervalMs * regularFlushIntervalMs;
			return num2 - num;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00040282 File Offset: 0x0003E482
		private void SetTimer(long waitTimeMs)
		{
			if (this._timer == null)
			{
				this._timer = new Timer(new TimerCallback(this.FlushTimerCallback), null, waitTimeMs, -1L);
				return;
			}
			this._timer.Change(waitTimeMs, -1L);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000402B8 File Offset: 0x0003E4B8
		internal void Flush(int max, FlushCallReason reason)
		{
			WebBaseEvent[] array = null;
			DateTime utcNow = DateTime.UtcNow;
			long num = 0L;
			DateTime lastNotification = DateTime.MaxValue;
			int eventsDiscardedSinceLastNotification = -1;
			int num2 = -1;
			int num3 = 0;
			EventNotificationType notificationType = EventNotificationType.Regular;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			Queue buffer = this._buffer;
			lock (buffer)
			{
				if (this._buffer.Count == 0)
				{
					flag = false;
				}
				switch (reason)
				{
				case FlushCallReason.UrgentFlushThresholdExceeded:
				{
					if (this._urgentFlushScheduled)
					{
						return;
					}
					flag = false;
					flag2 = true;
					flag3 = true;
					if (this.AnticipateBurst(utcNow))
					{
						num = this._burstWaitTimeMs;
					}
					else
					{
						num = 0L;
					}
					long num4 = (long)(utcNow - this._lastScheduledFlushTime).TotalMilliseconds;
					if (num4 + num < this._urgentFlushIntervalMs)
					{
						num = this._urgentFlushIntervalMs - num4;
					}
					break;
				}
				case FlushCallReason.Timer:
					if (this._regularFlushIntervalMs != WebEventBuffer.Infinite)
					{
						flag2 = true;
						num = this.GetNextRegularFlushDueTimeInMs();
					}
					break;
				}
				if (flag)
				{
					if (this._threadsInFlush >= this._maxBufferThreads)
					{
						num3 = 0;
					}
					else
					{
						num3 = Math.Min(this._buffer.Count, max);
					}
				}
				if (flag)
				{
					if (num3 > 0)
					{
						array = new WebBaseEvent[num3];
						for (int i = 0; i < num3; i++)
						{
							array[i] = (WebBaseEvent)this._buffer.Dequeue();
						}
						lastNotification = this._lastFlushTime;
						this._lastFlushTime = utcNow;
						if (reason == FlushCallReason.Timer)
						{
							this._lastScheduledFlushTime = utcNow;
						}
						eventsDiscardedSinceLastNotification = this._discardedSinceLastFlush;
						this._discardedSinceLastFlush = 0;
						if (reason == FlushCallReason.StaticFlush)
						{
							notificationType = EventNotificationType.Flush;
						}
						else
						{
							notificationType = (this._regularTimeoutUsed ? EventNotificationType.Regular : EventNotificationType.Urgent);
						}
					}
					num2 = this._buffer.Count;
					if (num2 >= this._urgentFlushThreshold)
					{
						flag2 = true;
						flag3 = true;
						num = this._urgentFlushIntervalMs;
					}
				}
				this._urgentFlushScheduled = false;
				if (flag2)
				{
					if (flag3)
					{
						long nextRegularFlushDueTimeInMs = this.GetNextRegularFlushDueTimeInMs();
						if (nextRegularFlushDueTimeInMs < num)
						{
							num = nextRegularFlushDueTimeInMs;
							this._regularTimeoutUsed = true;
						}
						else
						{
							this._regularTimeoutUsed = false;
						}
					}
					else
					{
						this._regularTimeoutUsed = true;
					}
					this.SetTimer(num);
					this._urgentFlushScheduled = flag3;
				}
				if (reason == FlushCallReason.Timer && !flag2)
				{
					((IDisposable)this._timer).Dispose();
					this._timer = null;
					this._urgentFlushScheduled = false;
				}
				if (array != null)
				{
					Interlocked.Increment(ref this._threadsInFlush);
				}
			}
			if (array != null)
			{
				using (new ApplicationImpersonationContext())
				{
					try
					{
						WebEventBufferFlushInfo flushInfo = new WebEventBufferFlushInfo(new WebBaseEventCollection(array), notificationType, Interlocked.Increment(ref this._notificationSequence), lastNotification, eventsDiscardedSinceLastNotification, num2);
						this._flushCallback(flushInfo);
					}
					catch (Exception e)
					{
						try
						{
							this._provider.LogException(e);
						}
						catch
						{
						}
					}
					catch
					{
						try
						{
							this._provider.LogException(new Exception(SR.GetString("Provider_Error")));
						}
						catch
						{
						}
					}
				}
				Interlocked.Decrement(ref this._threadsInFlush);
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00040600 File Offset: 0x0003E800
		internal void AddEvent(WebBaseEvent webEvent)
		{
			Queue buffer = this._buffer;
			lock (buffer)
			{
				if (this._buffer.Count == this._maxBufferSize)
				{
					this._buffer.Dequeue();
					this._discardedSinceLastFlush++;
				}
				this._buffer.Enqueue(webEvent);
				if (this._buffer.Count >= this._urgentFlushThreshold)
				{
					this.Flush(this._maxFlushSize, FlushCallReason.UrgentFlushThresholdExceeded);
				}
				this._lastAdd = DateTime.UtcNow;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000406A0 File Offset: 0x0003E8A0
		internal void Shutdown()
		{
			if (this._timer != null)
			{
				this._timer.Dispose();
				this._timer = null;
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000406BC File Offset: 0x0003E8BC
		private string PrintTime(DateTime t)
		{
			return t.ToString("T", DateTimeFormatInfo.InvariantInfo) + "." + t.Millisecond.ToString("d03", CultureInfo.InvariantCulture);
		}

		// Token: 0x040015B4 RID: 5556
		private static long Infinite = long.MaxValue;

		// Token: 0x040015B5 RID: 5557
		private long _burstWaitTimeMs = 2000L;

		// Token: 0x040015B6 RID: 5558
		private BufferedWebEventProvider _provider;

		// Token: 0x040015B7 RID: 5559
		private long _regularFlushIntervalMs;

		// Token: 0x040015B8 RID: 5560
		private int _urgentFlushThreshold;

		// Token: 0x040015B9 RID: 5561
		private int _maxBufferSize;

		// Token: 0x040015BA RID: 5562
		private int _maxFlushSize;

		// Token: 0x040015BB RID: 5563
		private long _urgentFlushIntervalMs;

		// Token: 0x040015BC RID: 5564
		private int _maxBufferThreads;

		// Token: 0x040015BD RID: 5565
		private Queue _buffer;

		// Token: 0x040015BE RID: 5566
		private Timer _timer;

		// Token: 0x040015BF RID: 5567
		private DateTime _lastFlushTime = DateTime.MinValue;

		// Token: 0x040015C0 RID: 5568
		private DateTime _lastScheduledFlushTime = DateTime.MinValue;

		// Token: 0x040015C1 RID: 5569
		private DateTime _lastAdd = DateTime.MinValue;

		// Token: 0x040015C2 RID: 5570
		private DateTime _startTime = DateTime.MinValue;

		// Token: 0x040015C3 RID: 5571
		private bool _urgentFlushScheduled;

		// Token: 0x040015C4 RID: 5572
		private int _discardedSinceLastFlush;

		// Token: 0x040015C5 RID: 5573
		private int _threadsInFlush;

		// Token: 0x040015C6 RID: 5574
		private int _notificationSequence;

		// Token: 0x040015C7 RID: 5575
		private bool _regularTimeoutUsed;

		// Token: 0x040015C8 RID: 5576
		private WebEventBufferFlushCallback _flushCallback;
	}
}
