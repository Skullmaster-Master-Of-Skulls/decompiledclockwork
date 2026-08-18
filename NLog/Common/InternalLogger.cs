using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using NLog.Internal;
using NLog.Time;

namespace NLog.Common
{
	// Token: 0x02000027 RID: 39
	public static class InternalLogger
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002AB1 File Offset: 0x00000CB1
		static InternalLogger()
		{
			InternalLogger.Reset();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public static void Reset()
		{
			InternalLogger.LogToConsole = InternalLogger.GetSetting<bool>("nlog.internalLogToConsole", "NLOG_INTERNAL_LOG_TO_CONSOLE", false);
			InternalLogger.LogToConsoleError = InternalLogger.GetSetting<bool>("nlog.internalLogToConsoleError", "NLOG_INTERNAL_LOG_TO_CONSOLE_ERROR", false);
			InternalLogger.LogLevel = InternalLogger.GetSetting("nlog.internalLogLevel", "NLOG_INTERNAL_LOG_LEVEL", LogLevel.Info);
			InternalLogger.LogFile = InternalLogger.GetSetting<string>("nlog.internalLogFile", "NLOG_INTERNAL_LOG_FILE", string.Empty);
			InternalLogger.LogToTrace = InternalLogger.GetSetting<bool>("nlog.internalLogToTrace", "NLOG_INTERNAL_LOG_TO_TRACE", false);
			InternalLogger.IncludeTimestamp = InternalLogger.GetSetting<bool>("nlog.internalLogIncludeTimestamp", "NLOG_INTERNAL_INCLUDE_TIMESTAMP", true);
			InternalLogger.Info("NLog internal logger initialized.");
			InternalLogger.ExceptionThrowWhenWriting = false;
			InternalLogger.LogWriter = null;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002B71 File Offset: 0x00000D71
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002B78 File Offset: 0x00000D78
		public static LogLevel LogLevel { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002B87 File Offset: 0x00000D87
		public static bool LogToConsole { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002B8F File Offset: 0x00000D8F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002B96 File Offset: 0x00000D96
		public static bool LogToConsoleError { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002B9E File Offset: 0x00000D9E
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public static bool LogToTrace { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002BAD File Offset: 0x00000DAD
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public static string LogFile
		{
			get
			{
				return InternalLogger._logFile;
			}
			set
			{
				InternalLogger._logFile = value;
				if (!string.IsNullOrEmpty(InternalLogger._logFile))
				{
					InternalLogger.CreateDirectoriesIfNeeded(InternalLogger._logFile);
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002BD2 File Offset: 0x00000DD2
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public static TextWriter LogWriter { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002BE1 File Offset: 0x00000DE1
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public static bool IncludeTimestamp { get; set; }

		// Token: 0x0600007F RID: 127 RVA: 0x00002BF0 File Offset: 0x00000DF0
		[StringFormatMethod("message")]
		public static void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, level, message, args);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002BFB File Offset: 0x00000DFB
		public static void Log(LogLevel level, [Localizable(false)] string message)
		{
			InternalLogger.Write(null, level, message, null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002C06 File Offset: 0x00000E06
		[StringFormatMethod("message")]
		public static void Log(Exception ex, LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, level, message, args);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002C11 File Offset: 0x00000E11
		public static void Log(Exception ex, LogLevel level, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, level, message, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C1C File Offset: 0x00000E1C
		private static void Write([CanBeNull] Exception ex, LogLevel level, string message, [CanBeNull] object[] args)
		{
			if (InternalLogger.IsSeriousException(ex))
			{
				return;
			}
			if (!InternalLogger.LoggingEnabled(level))
			{
				return;
			}
			try
			{
				string value = message;
				if (args != null)
				{
					value = string.Format(CultureInfo.InvariantCulture, message, args);
				}
				StringBuilder stringBuilder = new StringBuilder(message.Length + 32);
				if (InternalLogger.IncludeTimestamp)
				{
					stringBuilder.Append(TimeSource.Current.Time.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture));
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(level);
				stringBuilder.Append(" ");
				stringBuilder.Append(value);
				if (ex != null)
				{
					ex.MarkAsLoggedToInternalLogger();
					stringBuilder.Append(" Exception: ");
					stringBuilder.Append(ex);
				}
				string text = stringBuilder.ToString();
				string logFile = InternalLogger.LogFile;
				if (!string.IsNullOrEmpty(logFile))
				{
					using (StreamWriter streamWriter = File.AppendText(logFile))
					{
						streamWriter.WriteLine(text);
					}
				}
				TextWriter logWriter = InternalLogger.LogWriter;
				if (logWriter != null)
				{
					lock (InternalLogger.LockObject)
					{
						logWriter.WriteLine(text);
					}
				}
				if (InternalLogger.LogToConsole)
				{
					Console.WriteLine(text);
				}
				if (InternalLogger.LogToConsoleError)
				{
					Console.Error.WriteLine(text);
				}
				InternalLogger.WriteToTrace(text);
			}
			catch (Exception exception)
			{
				InternalLogger.ExceptionThrowWhenWriting = true;
				if (exception.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002DBC File Offset: 0x00000FBC
		private static bool IsSeriousException(Exception exception)
		{
			return exception != null && exception.MustBeRethrownImmediately();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002DCC File Offset: 0x00000FCC
		private static bool LoggingEnabled(LogLevel logLevel)
		{
			return !(logLevel == LogLevel.Off) && !(logLevel < InternalLogger.LogLevel) && (!string.IsNullOrEmpty(InternalLogger.LogFile) || InternalLogger.LogToConsole || InternalLogger.LogToConsoleError || InternalLogger.LogToTrace || InternalLogger.LogWriter != null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002E23 File Offset: 0x00001023
		private static void WriteToTrace(string message)
		{
			if (!InternalLogger.LogToTrace)
			{
				return;
			}
			System.Diagnostics.Trace.WriteLine(message, "NLog");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002E38 File Offset: 0x00001038
		public static void LogAssemblyVersion(Assembly assembly)
		{
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
				InternalLogger.Info("{0}. File version: {1}. Product version: {2}.", new object[]
				{
					assembly.FullName,
					versionInfo.FileVersion,
					versionInfo.ProductVersion
				});
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error logging version of assembly {0}.", new object[]
				{
					assembly.FullName
				});
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002EB0 File Offset: 0x000010B0
		private static string GetSettingString(string configName, string envName)
		{
			string text = System.Configuration.ConfigurationManager.AppSettings[configName];
			if (text == null)
			{
				try
				{
					text = Environment.GetEnvironmentVariable(envName);
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrownImmediately())
					{
						throw;
					}
				}
			}
			return text;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002EF4 File Offset: 0x000010F4
		private static LogLevel GetSetting(string configName, string envName, LogLevel defaultValue)
		{
			string settingString = InternalLogger.GetSettingString(configName, envName);
			if (settingString == null)
			{
				return defaultValue;
			}
			LogLevel result;
			try
			{
				result = LogLevel.FromString(settingString);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrownImmediately())
				{
					throw;
				}
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F38 File Offset: 0x00001138
		private static T GetSetting<T>(string configName, string envName, T defaultValue)
		{
			string settingString = InternalLogger.GetSettingString(configName, envName);
			if (settingString == null)
			{
				return defaultValue;
			}
			T result;
			try
			{
				result = (T)((object)Convert.ChangeType(settingString, typeof(T), CultureInfo.InvariantCulture));
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrownImmediately())
				{
					throw;
				}
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F90 File Offset: 0x00001190
		private static void CreateDirectoriesIfNeeded(string filename)
		{
			try
			{
				if (!(InternalLogger.LogLevel == LogLevel.Off))
				{
					string directoryName = Path.GetDirectoryName(filename);
					if (!string.IsNullOrEmpty(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Cannot create needed directories to '{0}'.", new object[]
				{
					filename
				});
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002FFC File Offset: 0x000011FC
		public static bool IsTraceEnabled
		{
			get
			{
				return LogLevel.Trace >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000300D File Offset: 0x0000120D
		public static bool IsDebugEnabled
		{
			get
			{
				return LogLevel.Debug >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000301E File Offset: 0x0000121E
		public static bool IsInfoEnabled
		{
			get
			{
				return LogLevel.Info >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000302F File Offset: 0x0000122F
		public static bool IsWarnEnabled
		{
			get
			{
				return LogLevel.Warn >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003040 File Offset: 0x00001240
		public static bool IsErrorEnabled
		{
			get
			{
				return LogLevel.Error >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003051 File Offset: 0x00001251
		public static bool IsFatalEnabled
		{
			get
			{
				return LogLevel.Fatal >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003062 File Offset: 0x00001262
		[StringFormatMethod("message")]
		public static void Trace([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Trace, message, args);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003071 File Offset: 0x00001271
		public static void Trace([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Trace, message, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003080 File Offset: 0x00001280
		[StringFormatMethod("message")]
		public static void Trace(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Trace, message, args);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000308F File Offset: 0x0000128F
		public static void Trace(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Trace, message, null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000309E File Offset: 0x0000129E
		[StringFormatMethod("message")]
		public static void Debug([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Debug, message, args);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000030AD File Offset: 0x000012AD
		public static void Debug([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Debug, message, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000030BC File Offset: 0x000012BC
		[StringFormatMethod("message")]
		public static void Debug(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Debug, message, args);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000030CB File Offset: 0x000012CB
		public static void Debug(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Debug, message, null);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000030DA File Offset: 0x000012DA
		[StringFormatMethod("message")]
		public static void Info([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Info, message, args);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000030E9 File Offset: 0x000012E9
		public static void Info([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Info, message, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000030F8 File Offset: 0x000012F8
		[StringFormatMethod("message")]
		public static void Info(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Info, message, args);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003107 File Offset: 0x00001307
		public static void Info(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Info, message, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003116 File Offset: 0x00001316
		[StringFormatMethod("message")]
		public static void Warn([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Warn, message, args);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003125 File Offset: 0x00001325
		public static void Warn([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Warn, message, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003134 File Offset: 0x00001334
		[StringFormatMethod("message")]
		public static void Warn(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Warn, message, args);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003143 File Offset: 0x00001343
		public static void Warn(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Warn, message, null);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003152 File Offset: 0x00001352
		[StringFormatMethod("message")]
		public static void Error([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Error, message, args);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003161 File Offset: 0x00001361
		public static void Error([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Error, message, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003170 File Offset: 0x00001370
		[StringFormatMethod("message")]
		public static void Error(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Error, message, args);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000317F File Offset: 0x0000137F
		public static void Error(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Error, message, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000318E File Offset: 0x0000138E
		[StringFormatMethod("message")]
		public static void Fatal([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Fatal, message, args);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000319D File Offset: 0x0000139D
		public static void Fatal([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Fatal, message, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000031AC File Offset: 0x000013AC
		[StringFormatMethod("message")]
		public static void Fatal(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Fatal, message, args);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000031BB File Offset: 0x000013BB
		public static void Fatal(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Fatal, message, null);
		}

		// Token: 0x0400001F RID: 31
		private static readonly object LockObject = new object();

		// Token: 0x04000020 RID: 32
		private static string _logFile;

		// Token: 0x04000021 RID: 33
		internal static bool ExceptionThrowWhenWriting = false;
	}
}
