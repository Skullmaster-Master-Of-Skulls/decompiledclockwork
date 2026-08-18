using System;

namespace NLog.Internal
{
	// Token: 0x02000096 RID: 150
	internal class LoggerConfiguration
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000A618 File Offset: 0x00008818
		public LoggerConfiguration(TargetWithFilterChain[] targetsByLevel, bool exceptionLoggingOldStyle = false)
		{
			this.targetsByLevel = targetsByLevel;
			this.ExceptionLoggingOldStyle = exceptionLoggingOldStyle;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000A62E File Offset: 0x0000882E
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0000A636 File Offset: 0x00008836
		[Obsolete("This option will be removed in NLog 5")]
		public bool ExceptionLoggingOldStyle { get; private set; }

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000A63F File Offset: 0x0000883F
		public TargetWithFilterChain GetTargetsForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return null;
			}
			return this.targetsByLevel[level.Ordinal];
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000A65D File Offset: 0x0000885D
		public bool IsEnabled(LogLevel level)
		{
			return !(level == LogLevel.Off) && this.targetsByLevel[level.Ordinal] != null;
		}

		// Token: 0x040000FA RID: 250
		private readonly TargetWithFilterChain[] targetsByLevel;
	}
}
