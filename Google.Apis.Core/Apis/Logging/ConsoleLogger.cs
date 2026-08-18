using System;
using Google.Apis.Util;

namespace Google.Apis.Logging
{
	// Token: 0x02000019 RID: 25
	public sealed class ConsoleLogger : BaseLogger, ILogger
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000035AA File Offset: 0x000017AA
		public ConsoleLogger(LogLevel minimumLogLevel, bool logToStdOut = false, IClock clock = null) : this(minimumLogLevel, logToStdOut, clock, null)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000035B6 File Offset: 0x000017B6
		private ConsoleLogger(LogLevel minimumLogLevel, bool logToStdOut, IClock clock, Type forType) : base(minimumLogLevel, clock, forType)
		{
			this.LogToStdOut = logToStdOut;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000035C9 File Offset: 0x000017C9
		public bool LogToStdOut { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x000035D1 File Offset: 0x000017D1
		protected override ILogger BuildNewLogger(Type type)
		{
			return new ConsoleLogger(base.MinimumLogLevel, this.LogToStdOut, base.Clock, type);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035EB File Offset: 0x000017EB
		protected override void Log(LogLevel logLevel, string formattedMessage)
		{
			(this.LogToStdOut ? Console.Out : Console.Error).WriteLine(formattedMessage);
		}
	}
}
