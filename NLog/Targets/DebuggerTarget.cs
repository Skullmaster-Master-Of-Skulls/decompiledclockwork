using System;
using System.Diagnostics;

namespace NLog.Targets
{
	// Token: 0x02000156 RID: 342
	[Target("Debugger")]
	public sealed class DebuggerTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x0001CE1A File Offset: 0x0001B01A
		public DebuggerTarget()
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0001CE22 File Offset: 0x0001B022
		public DebuggerTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001CE31 File Offset: 0x0001B031
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (base.Header != null)
			{
				Debugger.Log(LogLevel.Off.Ordinal, string.Empty, base.Header.Render(LogEventInfo.CreateNullEvent()) + "\n");
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0001CE6F File Offset: 0x0001B06F
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				Debugger.Log(LogLevel.Off.Ordinal, string.Empty, base.Footer.Render(LogEventInfo.CreateNullEvent()) + "\n");
			}
			base.CloseTarget();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0001CEAD File Offset: 0x0001B0AD
		protected override void Write(LogEventInfo logEvent)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(logEvent.Level.Ordinal, logEvent.LoggerName, this.Layout.Render(logEvent) + "\n");
			}
		}
	}
}
