using System;
using System.Collections;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000007 RID: 7
	public abstract class BufferingAppenderSkeleton : AppenderSkeleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002647 File Offset: 0x00000847
		protected BufferingAppenderSkeleton() : this(true)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002650 File Offset: 0x00000850
		protected BufferingAppenderSkeleton(bool eventMustBeFixed)
		{
			this.m_eventMustBeFixed = eventMustBeFixed;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002675 File Offset: 0x00000875
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000267D File Offset: 0x0000087D
		public bool Lossy
		{
			get
			{
				return this.m_lossy;
			}
			set
			{
				this.m_lossy = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002686 File Offset: 0x00000886
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000268E File Offset: 0x0000088E
		public int BufferSize
		{
			get
			{
				return this.m_bufferSize;
			}
			set
			{
				this.m_bufferSize = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002697 File Offset: 0x00000897
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000269F File Offset: 0x0000089F
		public ITriggeringEventEvaluator Evaluator
		{
			get
			{
				return this.m_evaluator;
			}
			set
			{
				this.m_evaluator = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000026B0 File Offset: 0x000008B0
		public ITriggeringEventEvaluator LossyEvaluator
		{
			get
			{
				return this.m_lossyEvaluator;
			}
			set
			{
				this.m_lossyEvaluator = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026B9 File Offset: 0x000008B9
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026C8 File Offset: 0x000008C8
		[Obsolete("Use Fix property")]
		public virtual bool OnlyFixPartialEventData
		{
			get
			{
				return this.Fix == FixFlags.Partial;
			}
			set
			{
				if (value)
				{
					this.Fix = FixFlags.Partial;
					return;
				}
				this.Fix = FixFlags.All;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026EC File Offset: 0x000008EC
		public virtual FixFlags Fix
		{
			get
			{
				return this.m_fixFlags;
			}
			set
			{
				this.m_fixFlags = value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026F5 File Offset: 0x000008F5
		public override bool Flush(int millisecondsTimeout)
		{
			this.Flush();
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026FE File Offset: 0x000008FE
		public virtual void Flush()
		{
			this.Flush(false);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002708 File Offset: 0x00000908
		public virtual void Flush(bool flushLossyBuffer)
		{
			lock (this)
			{
				if (this.m_cb != null && this.m_cb.Length > 0)
				{
					if (this.m_lossy)
					{
						if (flushLossyBuffer)
						{
							if (this.m_lossyEvaluator != null)
							{
								LoggingEvent[] array = this.m_cb.PopAll();
								ArrayList arrayList = new ArrayList(array.Length);
								foreach (LoggingEvent loggingEvent in array)
								{
									if (this.m_lossyEvaluator.IsTriggeringEvent(loggingEvent))
									{
										arrayList.Add(loggingEvent);
									}
								}
								if (arrayList.Count > 0)
								{
									this.SendBuffer((LoggingEvent[])arrayList.ToArray(typeof(LoggingEvent)));
								}
							}
							else
							{
								this.m_cb.Clear();
							}
						}
					}
					else
					{
						this.SendFromBuffer(null, this.m_cb);
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027FC File Offset: 0x000009FC
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.m_lossy && this.m_evaluator == null)
			{
				this.ErrorHandler.Error("Appender [" + base.Name + "] is Lossy but has no Evaluator. The buffer will never be sent!");
			}
			if (this.m_bufferSize > 1)
			{
				this.m_cb = new CyclicBuffer(this.m_bufferSize);
				return;
			}
			this.m_cb = null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002861 File Offset: 0x00000A61
		protected override void OnClose()
		{
			this.Flush(true);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000286C File Offset: 0x00000A6C
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_cb == null || this.m_bufferSize <= 1)
			{
				if (!this.m_lossy || (this.m_evaluator != null && this.m_evaluator.IsTriggeringEvent(loggingEvent)) || (this.m_lossyEvaluator != null && this.m_lossyEvaluator.IsTriggeringEvent(loggingEvent)))
				{
					if (this.m_eventMustBeFixed)
					{
						loggingEvent.Fix = this.Fix;
					}
					this.SendBuffer(new LoggingEvent[]
					{
						loggingEvent
					});
					return;
				}
			}
			else
			{
				loggingEvent.Fix = this.Fix;
				LoggingEvent loggingEvent2 = this.m_cb.Append(loggingEvent);
				if (loggingEvent2 != null)
				{
					if (!this.m_lossy)
					{
						this.SendFromBuffer(loggingEvent2, this.m_cb);
						return;
					}
					if (this.m_lossyEvaluator == null || !this.m_lossyEvaluator.IsTriggeringEvent(loggingEvent2))
					{
						loggingEvent2 = null;
					}
					if (this.m_evaluator != null && this.m_evaluator.IsTriggeringEvent(loggingEvent))
					{
						this.SendFromBuffer(loggingEvent2, this.m_cb);
						return;
					}
					if (loggingEvent2 != null)
					{
						this.SendBuffer(new LoggingEvent[]
						{
							loggingEvent2
						});
						return;
					}
				}
				else if (this.m_evaluator != null && this.m_evaluator.IsTriggeringEvent(loggingEvent))
				{
					this.SendFromBuffer(null, this.m_cb);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002994 File Offset: 0x00000B94
		protected virtual void SendFromBuffer(LoggingEvent firstLoggingEvent, CyclicBuffer buffer)
		{
			LoggingEvent[] array = buffer.PopAll();
			if (firstLoggingEvent == null)
			{
				this.SendBuffer(array);
				return;
			}
			if (array.Length == 0)
			{
				this.SendBuffer(new LoggingEvent[]
				{
					firstLoggingEvent
				});
				return;
			}
			LoggingEvent[] array2 = new LoggingEvent[array.Length + 1];
			Array.Copy(array, 0, array2, 1, array.Length);
			array2[0] = firstLoggingEvent;
			this.SendBuffer(array2);
		}

		// Token: 0x06000039 RID: 57
		protected abstract void SendBuffer(LoggingEvent[] events);

		// Token: 0x0400000D RID: 13
		private const int DEFAULT_BUFFER_SIZE = 512;

		// Token: 0x0400000E RID: 14
		private int m_bufferSize = 512;

		// Token: 0x0400000F RID: 15
		private CyclicBuffer m_cb;

		// Token: 0x04000010 RID: 16
		private ITriggeringEventEvaluator m_evaluator;

		// Token: 0x04000011 RID: 17
		private bool m_lossy;

		// Token: 0x04000012 RID: 18
		private ITriggeringEventEvaluator m_lossyEvaluator;

		// Token: 0x04000013 RID: 19
		private FixFlags m_fixFlags = FixFlags.All;

		// Token: 0x04000014 RID: 20
		private readonly bool m_eventMustBeFixed;
	}
}
