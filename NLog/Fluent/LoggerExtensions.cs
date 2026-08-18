using System;

namespace NLog.Fluent
{
	// Token: 0x02000067 RID: 103
	public static class LoggerExtensions
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00008D1C File Offset: 0x00006F1C
		[CLSCompliant(false)]
		public static LogBuilder Log(this ILogger logger, LogLevel logLevel)
		{
			return new LogBuilder(logger, logLevel);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008D34 File Offset: 0x00006F34
		[CLSCompliant(false)]
		public static LogBuilder Trace(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Trace);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008D50 File Offset: 0x00006F50
		[CLSCompliant(false)]
		public static LogBuilder Debug(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Debug);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008D6C File Offset: 0x00006F6C
		[CLSCompliant(false)]
		public static LogBuilder Info(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Info);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008D88 File Offset: 0x00006F88
		[CLSCompliant(false)]
		public static LogBuilder Warn(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Warn);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008DA4 File Offset: 0x00006FA4
		[CLSCompliant(false)]
		public static LogBuilder Error(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Error);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008DC0 File Offset: 0x00006FC0
		[CLSCompliant(false)]
		public static LogBuilder Fatal(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Fatal);
		}
	}
}
