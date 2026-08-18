using System;
using System.Collections;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000031 RID: 49
	public class MemoryAppender : AppenderSkeleton
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00005FAA File Offset: 0x000041AA
		public MemoryAppender()
		{
			this.m_eventsList = new ArrayList();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005FC8 File Offset: 0x000041C8
		public virtual LoggingEvent[] GetEvents()
		{
			LoggingEvent[] result;
			lock (this.m_eventsList.SyncRoot)
			{
				result = (LoggingEvent[])this.m_eventsList.ToArray(typeof(LoggingEvent));
			}
			return result;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006024 File Offset: 0x00004224
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006033 File Offset: 0x00004233
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000604F File Offset: 0x0000424F
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00006057 File Offset: 0x00004257
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

		// Token: 0x060001C2 RID: 450 RVA: 0x00006060 File Offset: 0x00004260
		protected override void Append(LoggingEvent loggingEvent)
		{
			loggingEvent.Fix = this.Fix;
			lock (this.m_eventsList.SyncRoot)
			{
				this.m_eventsList.Add(loggingEvent);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000060B8 File Offset: 0x000042B8
		public virtual void Clear()
		{
			lock (this.m_eventsList.SyncRoot)
			{
				this.m_eventsList.Clear();
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006104 File Offset: 0x00004304
		public virtual LoggingEvent[] PopAllEvents()
		{
			LoggingEvent[] result;
			lock (this.m_eventsList.SyncRoot)
			{
				LoggingEvent[] array = (LoggingEvent[])this.m_eventsList.ToArray(typeof(LoggingEvent));
				this.m_eventsList.Clear();
				result = array;
			}
			return result;
		}

		// Token: 0x040000C7 RID: 199
		protected ArrayList m_eventsList;

		// Token: 0x040000C8 RID: 200
		protected FixFlags m_fixFlags = FixFlags.All;
	}
}
