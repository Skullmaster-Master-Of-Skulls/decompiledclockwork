using System;
using System.Diagnostics;

namespace NLog.Targets
{
	// Token: 0x0200016F RID: 367
	[Target("Trace")]
	public sealed class TraceTarget : TargetWithLayout
	{
		// Token: 0x06000DDB RID: 3547 RVA: 0x00021677 File Offset: 0x0001F877
		public TraceTarget()
		{
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002167F File Offset: 0x0001F87F
		public TraceTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00021690 File Offset: 0x0001F890
		protected override void Write(LogEventInfo logEvent)
		{
			if (logEvent.Level <= LogLevel.Debug)
			{
				Trace.WriteLine(this.Layout.Render(logEvent));
				return;
			}
			if (logEvent.Level == LogLevel.Info)
			{
				Trace.TraceInformation(this.Layout.Render(logEvent));
				return;
			}
			if (logEvent.Level == LogLevel.Warn)
			{
				Trace.TraceWarning(this.Layout.Render(logEvent));
				return;
			}
			if (logEvent.Level == LogLevel.Error)
			{
				Trace.TraceError(this.Layout.Render(logEvent));
				return;
			}
			if (logEvent.Level >= LogLevel.Fatal)
			{
				Trace.Fail(this.Layout.Render(logEvent));
				return;
			}
			Trace.WriteLine(this.Layout.Render(logEvent));
		}
	}
}
