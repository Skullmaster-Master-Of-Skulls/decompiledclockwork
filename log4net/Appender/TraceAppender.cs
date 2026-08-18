using System;
using System.Diagnostics;
using log4net.Core;
using log4net.Layout;

namespace log4net.Appender
{
	// Token: 0x02000047 RID: 71
	public class TraceAppender : AppenderSkeleton
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public TraceAppender()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008D13 File Offset: 0x00006F13
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public TraceAppender(ILayout layout)
		{
			this.Layout = layout;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008D39 File Offset: 0x00006F39
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00008D41 File Offset: 0x00006F41
		public bool ImmediateFlush
		{
			get
			{
				return this.m_immediateFlush;
			}
			set
			{
				this.m_immediateFlush = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00008D4A File Offset: 0x00006F4A
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00008D52 File Offset: 0x00006F52
		public PatternLayout Category
		{
			get
			{
				return this.m_category;
			}
			set
			{
				this.m_category = value;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008D5B File Offset: 0x00006F5B
		protected override void Append(LoggingEvent loggingEvent)
		{
			Trace.Write(base.RenderLoggingEvent(loggingEvent), this.m_category.Format(loggingEvent));
			if (this.m_immediateFlush)
			{
				Trace.Flush();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00008D82 File Offset: 0x00006F82
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008D85 File Offset: 0x00006F85
		public override bool Flush(int millisecondsTimeout)
		{
			if (this.m_immediateFlush)
			{
				return true;
			}
			Trace.Flush();
			return true;
		}

		// Token: 0x04000142 RID: 322
		private bool m_immediateFlush = true;

		// Token: 0x04000143 RID: 323
		private PatternLayout m_category = new PatternLayout("%logger");
	}
}
