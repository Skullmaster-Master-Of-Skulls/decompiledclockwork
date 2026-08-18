using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x0200008B RID: 139
	public abstract class PatternLayoutConverter : PatternConverter
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000EC55 File Offset: 0x0000CE55
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000EC5D File Offset: 0x0000CE5D
		public virtual bool IgnoresException
		{
			get
			{
				return this.m_ignoresException;
			}
			set
			{
				this.m_ignoresException = value;
			}
		}

		// Token: 0x0600049D RID: 1181
		protected abstract void Convert(TextWriter writer, LoggingEvent loggingEvent);

		// Token: 0x0600049E RID: 1182 RVA: 0x0000EC68 File Offset: 0x0000CE68
		protected override void Convert(TextWriter writer, object state)
		{
			LoggingEvent loggingEvent = state as LoggingEvent;
			if (loggingEvent != null)
			{
				this.Convert(writer, loggingEvent);
				return;
			}
			throw new ArgumentException("state must be of type [" + typeof(LoggingEvent).FullName + "]", "state");
		}

		// Token: 0x040001FD RID: 509
		private bool m_ignoresException = true;
	}
}
