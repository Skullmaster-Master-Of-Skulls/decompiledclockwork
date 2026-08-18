using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace NLog.Fluent
{
	// Token: 0x02000065 RID: 101
	public static class Log
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000088A4 File Offset: 0x00006AA4
		public static LogBuilder Level(LogLevel logLevel, [CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(logLevel, callerFilePath);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000088AD File Offset: 0x00006AAD
		public static LogBuilder Trace([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Trace, callerFilePath);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000088BA File Offset: 0x00006ABA
		public static LogBuilder Debug([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Debug, callerFilePath);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000088C7 File Offset: 0x00006AC7
		public static LogBuilder Info([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Info, callerFilePath);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000088D4 File Offset: 0x00006AD4
		public static LogBuilder Warn([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Warn, callerFilePath);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000088E1 File Offset: 0x00006AE1
		public static LogBuilder Error([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Error, callerFilePath);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000088EE File Offset: 0x00006AEE
		public static LogBuilder Fatal([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Fatal, callerFilePath);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000088FC File Offset: 0x00006AFC
		private static LogBuilder Create(LogLevel logLevel, string callerFilePath)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath ?? string.Empty);
			ILogger logger = string.IsNullOrWhiteSpace(fileNameWithoutExtension) ? Log._logger : LogManager.GetLogger(fileNameWithoutExtension);
			LogBuilder logBuilder = new LogBuilder(logger, logLevel);
			if (callerFilePath != null)
			{
				logBuilder.Property("CallerFilePath", callerFilePath);
			}
			return logBuilder;
		}

		// Token: 0x040000CD RID: 205
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
	}
}
