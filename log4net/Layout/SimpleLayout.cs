using System;
using System.IO;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000B2 RID: 178
	public class SimpleLayout : LayoutSkeleton
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		public SimpleLayout()
		{
			this.IgnoresException = true;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000FFC2 File Offset: 0x0000E1C2
		public override void ActivateOptions()
		{
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			writer.Write(loggingEvent.Level.DisplayName);
			writer.Write(" - ");
			loggingEvent.WriteRenderedMessage(writer);
			writer.WriteLine();
		}
	}
}
