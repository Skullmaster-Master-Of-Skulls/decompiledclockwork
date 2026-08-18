using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace NLog.Config
{
	// Token: 0x0200004C RID: 76
	public sealed class InstallationContext : IDisposable
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00005566 File Offset: 0x00003766
		public InstallationContext() : this(TextWriter.Null)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005573 File Offset: 0x00003773
		public InstallationContext(TextWriter logOutput)
		{
			this.LogOutput = logOutput;
			this.Parameters = new Dictionary<string, string>();
			this.LogLevel = LogLevel.Info;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005598 File Offset: 0x00003798
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000055A0 File Offset: 0x000037A0
		public LogLevel LogLevel { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000055A9 File Offset: 0x000037A9
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000055B1 File Offset: 0x000037B1
		public bool IgnoreFailures { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000055BA File Offset: 0x000037BA
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000055C2 File Offset: 0x000037C2
		public IDictionary<string, string> Parameters { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000055CB File Offset: 0x000037CB
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000055D3 File Offset: 0x000037D3
		public TextWriter LogOutput { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x000055DC File Offset: 0x000037DC
		public void Trace([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Trace, message, arguments);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000055EB File Offset: 0x000037EB
		public void Debug([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Debug, message, arguments);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000055FA File Offset: 0x000037FA
		public void Info([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Info, message, arguments);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005609 File Offset: 0x00003809
		public void Warning([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Warn, message, arguments);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005618 File Offset: 0x00003818
		public void Error([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Error, message, arguments);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005627 File Offset: 0x00003827
		public void Dispose()
		{
			if (this.LogOutput != null)
			{
				this.LogOutput.Close();
				this.LogOutput = null;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005644 File Offset: 0x00003844
		public LogEventInfo CreateLogEvent()
		{
			LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
			foreach (KeyValuePair<string, string> keyValuePair in this.Parameters)
			{
				logEventInfo.Properties.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return logEventInfo;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000056AC File Offset: 0x000038AC
		private void Log(LogLevel logLevel, [Localizable(false)] string message, object[] arguments)
		{
			if (logLevel >= this.LogLevel)
			{
				if (arguments != null && arguments.Length > 0)
				{
					message = string.Format(CultureInfo.InvariantCulture, message, arguments);
				}
				ConsoleColor foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = InstallationContext.logLevel2ConsoleColor[logLevel];
				try
				{
					this.LogOutput.WriteLine(message);
				}
				finally
				{
					Console.ForegroundColor = foregroundColor;
				}
			}
		}

		// Token: 0x04000087 RID: 135
		private static readonly Dictionary<LogLevel, ConsoleColor> logLevel2ConsoleColor = new Dictionary<LogLevel, ConsoleColor>
		{
			{
				LogLevel.Trace,
				ConsoleColor.DarkGray
			},
			{
				LogLevel.Debug,
				ConsoleColor.Gray
			},
			{
				LogLevel.Info,
				ConsoleColor.White
			},
			{
				LogLevel.Warn,
				ConsoleColor.Yellow
			},
			{
				LogLevel.Error,
				ConsoleColor.Red
			},
			{
				LogLevel.Fatal,
				ConsoleColor.DarkRed
			}
		};
	}
}
