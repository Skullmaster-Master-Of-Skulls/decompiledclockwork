using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C9 RID: 201
	[LayoutRenderer("callsite-linenumber")]
	[ThreadAgnostic]
	public class CallSiteLineNumberLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000D32F File Offset: 0x0000B52F
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000D337 File Offset: 0x0000B537
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000D340 File Offset: 0x0000B540
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				return StackTraceUsage.WithSource;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000D344 File Offset: 0x0000B544
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			StackFrame stackFrame = (logEvent.StackTrace != null) ? logEvent.StackTrace.GetFrame(logEvent.UserStackFrameNumber + this.SkipFrames) : null;
			if (stackFrame != null)
			{
				int fileLineNumber = stackFrame.GetFileLineNumber();
				builder.Append(fileLineNumber);
			}
		}
	}
}
