using System;
using System.Web;
using log4net.Core;
using log4net.Layout;

namespace log4net.Appender
{
	// Token: 0x02000014 RID: 20
	public class AspNetTraceAppender : AppenderSkeleton
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003D8C File Offset: 0x00001F8C
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (HttpContext.Current != null && HttpContext.Current.Trace.IsEnabled)
			{
				if (loggingEvent.Level >= Level.Warn)
				{
					HttpContext.Current.Trace.Warn(this.m_category.Format(loggingEvent), base.RenderLoggingEvent(loggingEvent));
					return;
				}
				HttpContext.Current.Trace.Write(this.m_category.Format(loggingEvent), base.RenderLoggingEvent(loggingEvent));
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003E08 File Offset: 0x00002008
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003E0B File Offset: 0x0000200B
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003E13 File Offset: 0x00002013
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

		// Token: 0x0400004F RID: 79
		private PatternLayout m_category = new PatternLayout("%logger");
	}
}
