using System;
using System.Diagnostics;
using log4net.Core;
using log4net.Layout;

namespace log4net.Appender
{
	// Token: 0x0200001E RID: 30
	public class DebugAppender : AppenderSkeleton
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004383 File Offset: 0x00002583
		public DebugAppender()
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000043A2 File Offset: 0x000025A2
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public DebugAppender(ILayout layout)
		{
			this.Layout = layout;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000043C8 File Offset: 0x000025C8
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000043D0 File Offset: 0x000025D0
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000043D9 File Offset: 0x000025D9
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000043E1 File Offset: 0x000025E1
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

		// Token: 0x06000103 RID: 259 RVA: 0x000043EA File Offset: 0x000025EA
		public override bool Flush(int millisecondsTimeout)
		{
			if (this.m_immediateFlush)
			{
				return true;
			}
			Debug.Flush();
			return true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000043FC File Offset: 0x000025FC
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_category == null)
			{
				Debug.Write(base.RenderLoggingEvent(loggingEvent));
			}
			else
			{
				string text = this.m_category.Format(loggingEvent);
				if (string.IsNullOrEmpty(text))
				{
					Debug.Write(base.RenderLoggingEvent(loggingEvent));
				}
				else
				{
					Debug.Write(base.RenderLoggingEvent(loggingEvent), text);
				}
			}
			if (this.m_immediateFlush)
			{
				Debug.Flush();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000445C File Offset: 0x0000265C
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000073 RID: 115
		private bool m_immediateFlush = true;

		// Token: 0x04000074 RID: 116
		private PatternLayout m_category = new PatternLayout("%logger");
	}
}
