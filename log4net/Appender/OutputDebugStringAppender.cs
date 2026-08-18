using System;
using System.Runtime.InteropServices;
using System.Security;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000033 RID: 51
	public class OutputDebugStringAppender : AppenderSkeleton
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x000062D7 File Offset: 0x000044D7
		[SecuritySafeCritical]
		protected override void Append(LoggingEvent loggingEvent)
		{
			OutputDebugStringAppender.OutputDebugString(base.RenderLoggingEvent(loggingEvent));
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000062E5 File Offset: 0x000044E5
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001D5 RID: 469
		[DllImport("Kernel32.dll")]
		protected static extern void OutputDebugString(string message);
	}
}
