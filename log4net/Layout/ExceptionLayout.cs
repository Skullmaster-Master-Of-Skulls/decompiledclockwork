using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000AA RID: 170
	public class ExceptionLayout : LayoutSkeleton
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0000FEAF File Offset: 0x0000E0AF
		public ExceptionLayout()
		{
			this.IgnoresException = false;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000FEBE File Offset: 0x0000E0BE
		public override void ActivateOptions()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			writer.Write(loggingEvent.GetExceptionString());
		}
	}
}
