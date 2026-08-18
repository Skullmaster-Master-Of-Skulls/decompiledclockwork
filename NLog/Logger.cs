using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000122 RID: 290
	[CLSCompliant(true)]
	public class Logger : ILogger, ILoggerBase, ISuppress
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x000136F3 File Offset: 0x000118F3
		protected internal Logger()
		{
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000862 RID: 2146 RVA: 0x0001370C File Offset: 0x0001190C
		// (remove) Token: 0x06000863 RID: 2147 RVA: 0x00013744 File Offset: 0x00011944
		public event EventHandler<EventArgs> LoggerReconfigured;

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00013779 File Offset: 0x00011979
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00013781 File Offset: 0x00011981
		public string Name { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001378A File Offset: 0x0001198A
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00013792 File Offset: 0x00011992
		public LogFactory Factory { get; private set; }

		// Token: 0x06000868 RID: 2152 RVA: 0x0001379B File Offset: 0x0001199B
		public bool IsEnabled(LogLevel level)
		{
			if (level == null)
			{
				throw new InvalidOperationException("Log level must be defined");
			}
			return this.GetTargetsForLevel(level) != null;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000137BE File Offset: 0x000119BE
		public void Log(LogEventInfo logEvent)
		{
			if (this.IsEnabled(logEvent.Level))
			{
				this.WriteToTargets(logEvent);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000137D5 File Offset: 0x000119D5
		public void Log(Type wrapperType, LogEventInfo logEvent)
		{
			if (this.IsEnabled(logEvent.Level))
			{
				this.WriteToTargets(wrapperType, logEvent);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000137ED File Offset: 0x000119ED
		public void Log<T>(LogLevel level, T value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<T>(level, null, value);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00013801 File Offset: 0x00011A01
		public void Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<T>(level, formatProvider, value);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00013815 File Offset: 0x00011A15
		public void Log(LogLevel level, LogMessageGenerator messageFunc)
		{
			if (this.IsEnabled(level))
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(level, null, messageFunc());
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001383C File Offset: 0x00011A3C
		[Obsolete("Use Log(LogLevel, String, Exception) method instead.")]
		public void LogException(LogLevel level, [Localizable(false)] string message, Exception exception)
		{
			this.Log(level, message, exception);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00013847 File Offset: 0x00011A47
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, args);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001385D File Offset: 0x00011A5D
		public void Log(LogLevel level, [Localizable(false)] string message)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, null, message);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00013871 File Offset: 0x00011A71
		public void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, args);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00013885 File Offset: 0x00011A85
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)")]
		public void Log(LogLevel level, [Localizable(false)] string message, Exception exception)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, exception);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00013899 File Offset: 0x00011A99
		public void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, exception, message, args);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000138AF File Offset: 0x00011AAF
		public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, exception, formatProvider, message, args);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000138C8 File Offset: 0x00011AC8
		[StringFormatMethod("message")]
		public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000138FC File Offset: 0x00011AFC
		[StringFormatMethod("message")]
		public void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001392C File Offset: 0x00011B2C
		public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00013968 File Offset: 0x00011B68
		[StringFormatMethod("message")]
		public void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000139A4 File Offset: 0x00011BA4
		public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000139EC File Offset: 0x00011BEC
		[StringFormatMethod("message")]
		public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00013A30 File Offset: 0x00011C30
		internal void WriteToTargets(LogLevel level, Exception ex, [Localizable(false)] string message, object[] args)
		{
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), this.PrepareLogEventInfo(LogEventInfo.Create(level, this.Name, ex, this.Factory.DefaultCultureInfo, message, args)), this.Factory);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00013A78 File Offset: 0x00011C78
		internal void WriteToTargets(LogLevel level, Exception ex, IFormatProvider formatProvider, [Localizable(false)] string message, object[] args)
		{
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), this.PrepareLogEventInfo(LogEventInfo.Create(level, this.Name, ex, formatProvider, message, args)), this.Factory);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00013AB5 File Offset: 0x00011CB5
		private LogEventInfo PrepareLogEventInfo(LogEventInfo logEvent)
		{
			if (logEvent.FormatProvider == null)
			{
				logEvent.FormatProvider = this.Factory.DefaultCultureInfo;
			}
			return logEvent;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00013AD4 File Offset: 0x00011CD4
		public void Swallow(Action action)
		{
			try
			{
				action();
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00013B04 File Offset: 0x00011D04
		public T Swallow<T>(Func<T> func)
		{
			return this.Swallow<T>(func, default(T));
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00013B24 File Offset: 0x00011D24
		public T Swallow<T>(Func<T> func, T fallback)
		{
			T result;
			try
			{
				result = func();
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
				result = fallback;
			}
			return result;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00013C50 File Offset: 0x00011E50
		public async void Swallow(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00013D8C File Offset: 0x00011F8C
		public async Task SwallowAsync(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00013ED8 File Offset: 0x000120D8
		public async Task SwallowAsync(Func<Task> asyncAction)
		{
			try
			{
				await asyncAction();
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001400C File Offset: 0x0001220C
		public async Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
		{
			return await this.SwallowAsync<TResult>(asyncFunc, default(TResult));
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00014160 File Offset: 0x00012360
		public async Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
		{
			TResult result;
			try
			{
				result = await asyncFunc();
			}
			catch (Exception value)
			{
				this.Error<Exception>(value);
				result = fallback;
			}
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000141B6 File Offset: 0x000123B6
		internal void Initialize(string name, LoggerConfiguration loggerConfiguration, LogFactory factory)
		{
			this.Name = name;
			this.Factory = factory;
			this.SetConfiguration(loggerConfiguration);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000141CD File Offset: 0x000123CD
		internal void WriteToTargets(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, object[] args)
		{
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), this.PrepareLogEventInfo(LogEventInfo.Create(level, this.Name, formatProvider, message, args)), this.Factory);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00014200 File Offset: 0x00012400
		internal void WriteToTargets(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message)
		{
			LogEventInfo logEvent = LogEventInfo.Create(level, this.Name, formatProvider, message, null);
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), this.PrepareLogEventInfo(logEvent), this.Factory);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001423C File Offset: 0x0001243C
		internal void WriteToTargets<T>(LogLevel level, IFormatProvider formatProvider, T value)
		{
			LogEventInfo logEventInfo = this.PrepareLogEventInfo(LogEventInfo.Create(level, this.Name, formatProvider, value));
			Exception ex = value as Exception;
			if (ex != null)
			{
				logEventInfo.Exception = ex;
			}
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), logEventInfo, this.Factory);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00014292 File Offset: 0x00012492
		[Obsolete("Use WriteToTargets(Exception ex, LogLevel level, IFormatProvider formatProvider, string message, object[] args) method instead.")]
		internal void WriteToTargets(LogLevel level, [Localizable(false)] string message, Exception ex)
		{
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(level), this.PrepareLogEventInfo(LogEventInfo.Create(level, this.Name, message, ex)), this.Factory);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000142C0 File Offset: 0x000124C0
		internal void WriteToTargets(LogLevel level, [Localizable(false)] string message, object[] args)
		{
			this.WriteToTargets(level, this.Factory.DefaultCultureInfo, message, args);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000142D6 File Offset: 0x000124D6
		internal void WriteToTargets(LogEventInfo logEvent)
		{
			LoggerImpl.Write(this.loggerType, this.GetTargetsForLevel(logEvent.Level), this.PrepareLogEventInfo(logEvent), this.Factory);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000142FC File Offset: 0x000124FC
		internal void WriteToTargets(Type wrapperType, LogEventInfo logEvent)
		{
			LoggerImpl.Write(wrapperType ?? this.loggerType, this.GetTargetsForLevel(logEvent.Level), this.PrepareLogEventInfo(logEvent), this.Factory);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00014328 File Offset: 0x00012528
		internal void SetConfiguration(LoggerConfiguration newConfiguration)
		{
			this.configuration = newConfiguration;
			this.isTraceEnabled = newConfiguration.IsEnabled(LogLevel.Trace);
			this.isDebugEnabled = newConfiguration.IsEnabled(LogLevel.Debug);
			this.isInfoEnabled = newConfiguration.IsEnabled(LogLevel.Info);
			this.isWarnEnabled = newConfiguration.IsEnabled(LogLevel.Warn);
			this.isErrorEnabled = newConfiguration.IsEnabled(LogLevel.Error);
			this.isFatalEnabled = newConfiguration.IsEnabled(LogLevel.Fatal);
			EventHandler<EventArgs> loggerReconfigured = this.LoggerReconfigured;
			if (loggerReconfigured != null)
			{
				loggerReconfigured(this, new EventArgs());
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000143C6 File Offset: 0x000125C6
		private TargetWithFilterChain GetTargetsForLevel(LogLevel level)
		{
			return this.configuration.GetTargetsForLevel(level);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000143D6 File Offset: 0x000125D6
		public bool IsTraceEnabled
		{
			get
			{
				return this.isTraceEnabled;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000143E0 File Offset: 0x000125E0
		public bool IsDebugEnabled
		{
			get
			{
				return this.isDebugEnabled;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000143EA File Offset: 0x000125EA
		public bool IsInfoEnabled
		{
			get
			{
				return this.isInfoEnabled;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000143F4 File Offset: 0x000125F4
		public bool IsWarnEnabled
		{
			get
			{
				return this.isWarnEnabled;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000143FE File Offset: 0x000125FE
		public bool IsErrorEnabled
		{
			get
			{
				return this.isErrorEnabled;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00014408 File Offset: 0x00012608
		public bool IsFatalEnabled
		{
			get
			{
				return this.isFatalEnabled;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00014412 File Offset: 0x00012612
		public void Trace<T>(T value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Trace, null, value);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00014429 File Offset: 0x00012629
		public void Trace<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Trace, formatProvider, value);
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00014440 File Offset: 0x00012640
		public void Trace(LogMessageGenerator messageFunc)
		{
			if (this.IsTraceEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Trace, null, messageFunc());
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001446A File Offset: 0x0001266A
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead.")]
		public void TraceException([Localizable(false)] string message, Exception exception)
		{
			this.Trace(message, exception);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00014474 File Offset: 0x00012674
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, args);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001448C File Offset: 0x0001268C
		public void Trace([Localizable(false)] string message)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, null, message);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000144A3 File Offset: 0x000126A3
		[StringFormatMethod("message")]
		public void Trace([Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, args);
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000144BA File Offset: 0x000126BA
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead.")]
		public void Trace([Localizable(false)] string message, Exception exception)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, exception);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000144D1 File Offset: 0x000126D1
		public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, message, args);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000144E9 File Offset: 0x000126E9
		[StringFormatMethod("message")]
		public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00014504 File Offset: 0x00012704
		[StringFormatMethod("message")]
		public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00014538 File Offset: 0x00012738
		[StringFormatMethod("message")]
		public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsTraceEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Trace(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00014594 File Offset: 0x00012794
		[StringFormatMethod("message")]
		public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000145D4 File Offset: 0x000127D4
		[StringFormatMethod("message")]
		public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00014610 File Offset: 0x00012810
		[StringFormatMethod("message")]
		public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00014658 File Offset: 0x00012858
		[StringFormatMethod("message")]
		public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001469D File Offset: 0x0001289D
		public void Debug<T>(T value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Debug, null, value);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000146B4 File Offset: 0x000128B4
		public void Debug<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Debug, formatProvider, value);
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000146CB File Offset: 0x000128CB
		public void Debug(LogMessageGenerator messageFunc)
		{
			if (this.IsDebugEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Debug, null, messageFunc());
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000146F5 File Offset: 0x000128F5
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead.")]
		public void DebugException([Localizable(false)] string message, Exception exception)
		{
			this.Debug(message, exception);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000146FF File Offset: 0x000128FF
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, args);
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00014717 File Offset: 0x00012917
		public void Debug([Localizable(false)] string message)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, null, message);
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001472E File Offset: 0x0001292E
		[StringFormatMethod("message")]
		public void Debug([Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, args);
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00014745 File Offset: 0x00012945
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead.")]
		public void Debug([Localizable(false)] string message, Exception exception)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, exception);
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001475C File Offset: 0x0001295C
		public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, message, args);
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00014774 File Offset: 0x00012974
		[StringFormatMethod("message")]
		public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00014790 File Offset: 0x00012990
		[StringFormatMethod("message")]
		public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000147C4 File Offset: 0x000129C4
		[StringFormatMethod("message")]
		public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsDebugEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Debug(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00014820 File Offset: 0x00012A20
		[StringFormatMethod("message")]
		public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00014860 File Offset: 0x00012A60
		[StringFormatMethod("message")]
		public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001489C File Offset: 0x00012A9C
		[StringFormatMethod("message")]
		public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000148E4 File Offset: 0x00012AE4
		[StringFormatMethod("message")]
		public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00014929 File Offset: 0x00012B29
		public void Info<T>(T value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Info, null, value);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00014940 File Offset: 0x00012B40
		public void Info<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Info, formatProvider, value);
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00014957 File Offset: 0x00012B57
		public void Info(LogMessageGenerator messageFunc)
		{
			if (this.IsInfoEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Info, null, messageFunc());
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00014981 File Offset: 0x00012B81
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead.")]
		public void InfoException([Localizable(false)] string message, Exception exception)
		{
			this.Info(message, exception);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001498B File Offset: 0x00012B8B
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, args);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000149A3 File Offset: 0x00012BA3
		public void Info([Localizable(false)] string message)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, null, message);
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000149BA File Offset: 0x00012BBA
		[StringFormatMethod("message")]
		public void Info([Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, args);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000149D1 File Offset: 0x00012BD1
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead.")]
		public void Info([Localizable(false)] string message, Exception exception)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, exception);
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000149E8 File Offset: 0x00012BE8
		public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, message, args);
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00014A00 File Offset: 0x00012C00
		[StringFormatMethod("message")]
		public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00014A1C File Offset: 0x00012C1C
		[StringFormatMethod("message")]
		public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00014A50 File Offset: 0x00012C50
		[StringFormatMethod("message")]
		public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsInfoEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Info(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00014AAC File Offset: 0x00012CAC
		[StringFormatMethod("message")]
		public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00014AEC File Offset: 0x00012CEC
		[StringFormatMethod("message")]
		public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00014B28 File Offset: 0x00012D28
		[StringFormatMethod("message")]
		public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00014B70 File Offset: 0x00012D70
		[StringFormatMethod("message")]
		public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00014BB5 File Offset: 0x00012DB5
		public void Warn<T>(T value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Warn, null, value);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00014BCC File Offset: 0x00012DCC
		public void Warn<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Warn, formatProvider, value);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00014BE3 File Offset: 0x00012DE3
		public void Warn(LogMessageGenerator messageFunc)
		{
			if (this.IsWarnEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Warn, null, messageFunc());
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00014C0D File Offset: 0x00012E0D
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead.")]
		public void WarnException([Localizable(false)] string message, Exception exception)
		{
			this.Warn(message, exception);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00014C17 File Offset: 0x00012E17
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, args);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00014C2F File Offset: 0x00012E2F
		public void Warn([Localizable(false)] string message)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, null, message);
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00014C46 File Offset: 0x00012E46
		[StringFormatMethod("message")]
		public void Warn([Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, args);
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00014C5D File Offset: 0x00012E5D
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead.")]
		public void Warn([Localizable(false)] string message, Exception exception)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, exception);
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00014C74 File Offset: 0x00012E74
		public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, message, args);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00014C8C File Offset: 0x00012E8C
		[StringFormatMethod("message")]
		public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00014CA8 File Offset: 0x00012EA8
		[StringFormatMethod("message")]
		public void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00014CDC File Offset: 0x00012EDC
		[StringFormatMethod("message")]
		public void Warn<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsWarnEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Warn(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00014D38 File Offset: 0x00012F38
		[StringFormatMethod("message")]
		public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00014D78 File Offset: 0x00012F78
		[StringFormatMethod("message")]
		public void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00014DB4 File Offset: 0x00012FB4
		[StringFormatMethod("message")]
		public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00014DFC File Offset: 0x00012FFC
		[StringFormatMethod("message")]
		public void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00014E41 File Offset: 0x00013041
		public void Error<T>(T value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Error, null, value);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00014E58 File Offset: 0x00013058
		public void Error<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Error, formatProvider, value);
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00014E6F File Offset: 0x0001306F
		public void Error(LogMessageGenerator messageFunc)
		{
			if (this.IsErrorEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Error, null, messageFunc());
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00014E99 File Offset: 0x00013099
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead.")]
		public void ErrorException([Localizable(false)] string message, Exception exception)
		{
			this.Error(message, exception);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00014EA3 File Offset: 0x000130A3
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, args);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00014EBB File Offset: 0x000130BB
		public void Error([Localizable(false)] string message)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, null, message);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00014ED2 File Offset: 0x000130D2
		[StringFormatMethod("message")]
		public void Error([Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, args);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00014EE9 File Offset: 0x000130E9
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead.")]
		public void Error([Localizable(false)] string message, Exception exception)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, exception);
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00014F00 File Offset: 0x00013100
		public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, message, args);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00014F18 File Offset: 0x00013118
		[StringFormatMethod("message")]
		public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00014F34 File Offset: 0x00013134
		[StringFormatMethod("message")]
		public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00014F68 File Offset: 0x00013168
		[StringFormatMethod("message")]
		public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsErrorEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Error(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00014FC4 File Offset: 0x000131C4
		[StringFormatMethod("message")]
		public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00015004 File Offset: 0x00013204
		[StringFormatMethod("message")]
		public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00015040 File Offset: 0x00013240
		[StringFormatMethod("message")]
		public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00015088 File Offset: 0x00013288
		[StringFormatMethod("message")]
		public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000150CD File Offset: 0x000132CD
		public void Fatal<T>(T value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Fatal, null, value);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000150E4 File Offset: 0x000132E4
		public void Fatal<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Fatal, formatProvider, value);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000150FB File Offset: 0x000132FB
		public void Fatal(LogMessageGenerator messageFunc)
		{
			if (this.IsFatalEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Fatal, null, messageFunc());
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00015125 File Offset: 0x00013325
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead.")]
		public void FatalException([Localizable(false)] string message, Exception exception)
		{
			this.Fatal(message, exception);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001512F File Offset: 0x0001332F
		[StringFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, args);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00015147 File Offset: 0x00013347
		public void Fatal([Localizable(false)] string message)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, null, message);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001515E File Offset: 0x0001335E
		[StringFormatMethod("message")]
		public void Fatal([Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, args);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00015175 File Offset: 0x00013375
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead.")]
		public void Fatal([Localizable(false)] string message, Exception exception)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, exception);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001518C File Offset: 0x0001338C
		public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, message, args);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000151A4 File Offset: 0x000133A4
		[StringFormatMethod("message")]
		public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000151C0 File Offset: 0x000133C0
		[StringFormatMethod("message")]
		public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000151F4 File Offset: 0x000133F4
		[StringFormatMethod("message")]
		public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsFatalEnabled)
			{
				if (this.configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Fatal(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00015250 File Offset: 0x00013450
		[StringFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00015290 File Offset: 0x00013490
		[StringFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument1,
					argument2
				});
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000152CC File Offset: 0x000134CC
		[StringFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00015314 File Offset: 0x00013514
		[StringFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument1,
					argument2,
					argument3
				});
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00015359 File Offset: 0x00013559
		[Conditional("DEBUG")]
		public void ConditionalDebug<T>(T value)
		{
			this.Debug<T>(value);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00015362 File Offset: 0x00013562
		[Conditional("DEBUG")]
		public void ConditionalDebug<T>(IFormatProvider formatProvider, T value)
		{
			this.Debug<T>(formatProvider, value);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001536C File Offset: 0x0001356C
		[Conditional("DEBUG")]
		public void ConditionalDebug(LogMessageGenerator messageFunc)
		{
			this.Debug(messageFunc);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00015375 File Offset: 0x00013575
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(Exception exception, string message, params object[] args)
		{
			this.Debug(exception, message, args);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00015380 File Offset: 0x00013580
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Debug(exception, formatProvider, message, args);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001538D File Offset: 0x0001358D
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Debug(formatProvider, message, args);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00015398 File Offset: 0x00013598
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message)
		{
			this.Debug(message);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000153A1 File Offset: 0x000135A1
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, params object[] args)
		{
			this.Debug(message, args);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000153AB File Offset: 0x000135AB
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
		{
			this.Debug<TArgument>(formatProvider, message, argument);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000153B6 File Offset: 0x000135B6
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug<TArgument>(string message, TArgument argument)
		{
			this.Debug<TArgument>(message, argument);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000153C0 File Offset: 0x000135C0
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Debug<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000153CD File Offset: 0x000135CD
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Debug<TArgument1, TArgument2>(message, argument1, argument2);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000153D8 File Offset: 0x000135D8
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Debug<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000153E7 File Offset: 0x000135E7
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Debug<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000153F4 File Offset: 0x000135F4
		[Conditional("DEBUG")]
		public void ConditionalDebug(object value)
		{
			this.Debug(value);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000153FD File Offset: 0x000135FD
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, object value)
		{
			this.Debug(formatProvider, value);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00015407 File Offset: 0x00013607
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, object arg1, object arg2)
		{
			this.Debug(message, arg1, arg2);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00015412 File Offset: 0x00013612
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, object arg1, object arg2, object arg3)
		{
			this.Debug(message, arg1, arg2, arg3);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001541F File Offset: 0x0001361F
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, bool argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001542A File Offset: 0x0001362A
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, bool argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00015434 File Offset: 0x00013634
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, char argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001543F File Offset: 0x0001363F
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, char argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00015449 File Offset: 0x00013649
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, byte argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00015454 File Offset: 0x00013654
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, byte argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001545E File Offset: 0x0001365E
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, string argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00015469 File Offset: 0x00013669
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, string argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00015473 File Offset: 0x00013673
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, int argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001547E File Offset: 0x0001367E
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, int argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00015488 File Offset: 0x00013688
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, long argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00015493 File Offset: 0x00013693
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, long argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001549D File Offset: 0x0001369D
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, float argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000154A8 File Offset: 0x000136A8
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, float argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000154B2 File Offset: 0x000136B2
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, double argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000154BD File Offset: 0x000136BD
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalDebug(string message, double argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000154C7 File Offset: 0x000136C7
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, decimal argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000154D2 File Offset: 0x000136D2
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, decimal argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000154DC File Offset: 0x000136DC
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, object argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000154E7 File Offset: 0x000136E7
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message, object argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000154F1 File Offset: 0x000136F1
		[Conditional("DEBUG")]
		public void ConditionalTrace<T>(T value)
		{
			this.Trace<T>(value);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000154FA File Offset: 0x000136FA
		[Conditional("DEBUG")]
		public void ConditionalTrace<T>(IFormatProvider formatProvider, T value)
		{
			this.Trace<T>(formatProvider, value);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00015504 File Offset: 0x00013704
		[Conditional("DEBUG")]
		public void ConditionalTrace(LogMessageGenerator messageFunc)
		{
			this.Trace(messageFunc);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001550D File Offset: 0x0001370D
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(Exception exception, string message, params object[] args)
		{
			this.Trace(exception, message, args);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00015518 File Offset: 0x00013718
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Trace(exception, formatProvider, message, args);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00015525 File Offset: 0x00013725
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Trace(formatProvider, message, args);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00015530 File Offset: 0x00013730
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message)
		{
			this.Trace(message);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00015539 File Offset: 0x00013739
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, params object[] args)
		{
			this.Trace(message, args);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00015543 File Offset: 0x00013743
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
		{
			this.Trace<TArgument>(formatProvider, message, argument);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001554E File Offset: 0x0001374E
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace<TArgument>(string message, TArgument argument)
		{
			this.Trace<TArgument>(message, argument);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00015558 File Offset: 0x00013758
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Trace<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00015565 File Offset: 0x00013765
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Trace<TArgument1, TArgument2>(message, argument1, argument2);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00015570 File Offset: 0x00013770
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Trace<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001557F File Offset: 0x0001377F
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Trace<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001558C File Offset: 0x0001378C
		[Conditional("DEBUG")]
		public void ConditionalTrace(object value)
		{
			this.Trace(value);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00015595 File Offset: 0x00013795
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, object value)
		{
			this.Trace(formatProvider, value);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001559F File Offset: 0x0001379F
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, object arg1, object arg2)
		{
			this.Trace(message, arg1, arg2);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000155AA File Offset: 0x000137AA
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(string message, object arg1, object arg2, object arg3)
		{
			this.Trace(message, arg1, arg2, arg3);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000155B7 File Offset: 0x000137B7
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, bool argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000155C2 File Offset: 0x000137C2
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(string message, bool argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000155CC File Offset: 0x000137CC
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, char argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000155D7 File Offset: 0x000137D7
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(string message, char argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000155E1 File Offset: 0x000137E1
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, byte argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000155EC File Offset: 0x000137EC
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, byte argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000155F6 File Offset: 0x000137F6
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, string argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00015601 File Offset: 0x00013801
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, string argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001560B File Offset: 0x0001380B
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, int argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00015616 File Offset: 0x00013816
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, int argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00015620 File Offset: 0x00013820
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, long argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001562B File Offset: 0x0001382B
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(string message, long argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00015635 File Offset: 0x00013835
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, float argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00015640 File Offset: 0x00013840
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, float argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001564A File Offset: 0x0001384A
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, double argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00015655 File Offset: 0x00013855
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(string message, double argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001565F File Offset: 0x0001385F
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, decimal argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001566A File Offset: 0x0001386A
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, decimal argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00015674 File Offset: 0x00013874
		[Conditional("DEBUG")]
		[StringFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, object argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001567F File Offset: 0x0001387F
		[StringFormatMethod("message")]
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, object argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001568C File Offset: 0x0001388C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, object value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000156BC File Offset: 0x000138BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, object value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000156EC File Offset: 0x000138EC
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, object arg1, object arg2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001571C File Offset: 0x0001391C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00015750 File Offset: 0x00013950
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00015784 File Offset: 0x00013984
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, bool argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000157B4 File Offset: 0x000139B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000157E8 File Offset: 0x000139E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, char argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00015818 File Offset: 0x00013A18
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001584C File Offset: 0x00013A4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, byte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001587C File Offset: 0x00013A7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000158A8 File Offset: 0x00013AA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, string argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000158D4 File Offset: 0x00013AD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00015908 File Offset: 0x00013B08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, int argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00015938 File Offset: 0x00013B38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001596C File Offset: 0x00013B6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, long argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001599C File Offset: 0x00013B9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000159D0 File Offset: 0x00013BD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, float argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00015A00 File Offset: 0x00013C00
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00015A34 File Offset: 0x00013C34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, double argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00015A64 File Offset: 0x00013C64
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00015A98 File Offset: 0x00013C98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, decimal argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00015AC8 File Offset: 0x00013CC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00015AF4 File Offset: 0x00013CF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, object argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00015B20 File Offset: 0x00013D20
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00015B54 File Offset: 0x00013D54
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Log(LogLevel level, string message, sbyte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00015B84 File Offset: 0x00013D84
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00015BB8 File Offset: 0x00013DB8
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, uint argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00015BE8 File Offset: 0x00013DE8
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00015C1C File Offset: 0x00013E1C
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, ulong argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00015C4C File Offset: 0x00013E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(object value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00015C80 File Offset: 0x00013E80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, object value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00015CB4 File Offset: 0x00013EB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(string message, object arg1, object arg2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00015CE8 File Offset: 0x00013EE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00015D20 File Offset: 0x00013F20
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00015D54 File Offset: 0x00013F54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, bool argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00015D88 File Offset: 0x00013F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00015DBC File Offset: 0x00013FBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, char argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00015DF0 File Offset: 0x00013FF0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00015E24 File Offset: 0x00014024
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, byte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00015E58 File Offset: 0x00014058
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00015E88 File Offset: 0x00014088
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, string argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00015EB8 File Offset: 0x000140B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00015EEC File Offset: 0x000140EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, int argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00015F20 File Offset: 0x00014120
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00015F54 File Offset: 0x00014154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, long argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00015F88 File Offset: 0x00014188
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00015FBC File Offset: 0x000141BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, float argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00015FF0 File Offset: 0x000141F0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00016024 File Offset: 0x00014224
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, double argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00016058 File Offset: 0x00014258
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001608C File Offset: 0x0001428C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, decimal argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000160C0 File Offset: 0x000142C0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000160F0 File Offset: 0x000142F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, object argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00016120 File Offset: 0x00014320
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00016154 File Offset: 0x00014354
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, sbyte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00016188 File Offset: 0x00014388
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000161BC File Offset: 0x000143BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Trace(string message, uint argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000161F0 File Offset: 0x000143F0
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00016224 File Offset: 0x00014424
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, ulong argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00016258 File Offset: 0x00014458
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(object value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001628C File Offset: 0x0001448C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, object value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000162C0 File Offset: 0x000144C0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, object arg1, object arg2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000162F4 File Offset: 0x000144F4
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001632C File Offset: 0x0001452C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00016360 File Offset: 0x00014560
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, bool argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00016394 File Offset: 0x00014594
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000163C8 File Offset: 0x000145C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, char argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000163FC File Offset: 0x000145FC
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00016430 File Offset: 0x00014630
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, byte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00016464 File Offset: 0x00014664
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00016494 File Offset: 0x00014694
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, string argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000164C4 File Offset: 0x000146C4
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000164F8 File Offset: 0x000146F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, int argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001652C File Offset: 0x0001472C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00016560 File Offset: 0x00014760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, long argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00016594 File Offset: 0x00014794
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000165C8 File Offset: 0x000147C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, float argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000165FC File Offset: 0x000147FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00016630 File Offset: 0x00014830
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, double argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00016664 File Offset: 0x00014864
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00016698 File Offset: 0x00014898
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, decimal argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000166CC File Offset: 0x000148CC
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000166FC File Offset: 0x000148FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, object argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001672C File Offset: 0x0001492C
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00016760 File Offset: 0x00014960
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Debug(string message, sbyte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00016794 File Offset: 0x00014994
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000167C8 File Offset: 0x000149C8
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Debug(string message, uint argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000167FC File Offset: 0x000149FC
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00016830 File Offset: 0x00014A30
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Debug(string message, ulong argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00016864 File Offset: 0x00014A64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(object value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00016898 File Offset: 0x00014A98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, object value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000168CC File Offset: 0x00014ACC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(string message, object arg1, object arg2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00016900 File Offset: 0x00014B00
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00016938 File Offset: 0x00014B38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001696C File Offset: 0x00014B6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, bool argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000169A0 File Offset: 0x00014BA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000169D4 File Offset: 0x00014BD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, char argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00016A08 File Offset: 0x00014C08
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00016A3C File Offset: 0x00014C3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, byte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00016A70 File Offset: 0x00014C70
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00016AA0 File Offset: 0x00014CA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, string argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00016AD0 File Offset: 0x00014CD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00016B04 File Offset: 0x00014D04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, int argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00016B38 File Offset: 0x00014D38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00016B6C File Offset: 0x00014D6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, long argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00016BA0 File Offset: 0x00014DA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00016BD4 File Offset: 0x00014DD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, float argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00016C08 File Offset: 0x00014E08
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00016C3C File Offset: 0x00014E3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, double argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00016C70 File Offset: 0x00014E70
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00016CA4 File Offset: 0x00014EA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, decimal argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00016CD8 File Offset: 0x00014ED8
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00016D08 File Offset: 0x00014F08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, object argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00016D38 File Offset: 0x00014F38
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00016D6C File Offset: 0x00014F6C
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, sbyte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00016DA0 File Offset: 0x00014FA0
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00016DD4 File Offset: 0x00014FD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		public void Info(string message, uint argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00016E08 File Offset: 0x00015008
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00016E3C File Offset: 0x0001503C
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Info(string message, ulong argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00016E70 File Offset: 0x00015070
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(object value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00016EA4 File Offset: 0x000150A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, object value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00016ED8 File Offset: 0x000150D8
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, object arg1, object arg2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00016F0C File Offset: 0x0001510C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00016F44 File Offset: 0x00015144
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00016F78 File Offset: 0x00015178
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, bool argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00016FAC File Offset: 0x000151AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00016FE0 File Offset: 0x000151E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, char argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00017014 File Offset: 0x00015214
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00017048 File Offset: 0x00015248
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, byte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001707C File Offset: 0x0001527C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000170AC File Offset: 0x000152AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, string argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000170DC File Offset: 0x000152DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00017110 File Offset: 0x00015310
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, int argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00017144 File Offset: 0x00015344
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00017178 File Offset: 0x00015378
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, long argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000171AC File Offset: 0x000153AC
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000171E0 File Offset: 0x000153E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, float argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00017214 File Offset: 0x00015414
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00017248 File Offset: 0x00015448
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, double argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001727C File Offset: 0x0001547C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000172B0 File Offset: 0x000154B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, decimal argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000172E4 File Offset: 0x000154E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00017314 File Offset: 0x00015514
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, object argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00017344 File Offset: 0x00015544
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00017378 File Offset: 0x00015578
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Warn(string message, sbyte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000173AC File Offset: 0x000155AC
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000173E0 File Offset: 0x000155E0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Warn(string message, uint argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00017414 File Offset: 0x00015614
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00017448 File Offset: 0x00015648
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Warn(string message, ulong argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001747C File Offset: 0x0001567C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(object value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000174B0 File Offset: 0x000156B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, object value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000174E4 File Offset: 0x000156E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(string message, object arg1, object arg2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00017518 File Offset: 0x00015718
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00017550 File Offset: 0x00015750
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00017584 File Offset: 0x00015784
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, bool argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000175B8 File Offset: 0x000157B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000175EC File Offset: 0x000157EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, char argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00017620 File Offset: 0x00015820
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00017654 File Offset: 0x00015854
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, byte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00017688 File Offset: 0x00015888
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000176B8 File Offset: 0x000158B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, string argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000176E8 File Offset: 0x000158E8
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001771C File Offset: 0x0001591C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, int argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00017750 File Offset: 0x00015950
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00017784 File Offset: 0x00015984
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, long argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000177B8 File Offset: 0x000159B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000177EC File Offset: 0x000159EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, float argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00017820 File Offset: 0x00015A20
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00017854 File Offset: 0x00015A54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, double argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00017888 File Offset: 0x00015A88
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000178BC File Offset: 0x00015ABC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, decimal argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000178F0 File Offset: 0x00015AF0
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00017920 File Offset: 0x00015B20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, object argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00017950 File Offset: 0x00015B50
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Error(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00017984 File Offset: 0x00015B84
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Error(string message, sbyte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000179B8 File Offset: 0x00015BB8
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000179EC File Offset: 0x00015BEC
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, uint argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00017A20 File Offset: 0x00015C20
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00017A54 File Offset: 0x00015C54
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Error(string message, ulong argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00017A88 File Offset: 0x00015C88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(object value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00017ABC File Offset: 0x00015CBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, object value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, "{0}", new object[]
				{
					value
				});
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00017AF0 File Offset: 0x00015CF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Fatal(string message, object arg1, object arg2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					arg1,
					arg2
				});
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00017B24 File Offset: 0x00015D24
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					arg1,
					arg2,
					arg3
				});
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00017B5C File Offset: 0x00015D5C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00017B90 File Offset: 0x00015D90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, bool argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00017BC4 File Offset: 0x00015DC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00017BF8 File Offset: 0x00015DF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, char argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00017C2C File Offset: 0x00015E2C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00017C60 File Offset: 0x00015E60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, byte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00017C94 File Offset: 0x00015E94
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00017CC4 File Offset: 0x00015EC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, string argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00017CF4 File Offset: 0x00015EF4
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00017D28 File Offset: 0x00015F28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, int argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00017D5C File Offset: 0x00015F5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00017D90 File Offset: 0x00015F90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, long argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00017DC4 File Offset: 0x00015FC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00017DF8 File Offset: 0x00015FF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, float argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00017E2C File Offset: 0x0001602C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00017E60 File Offset: 0x00016060
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, double argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00017E94 File Offset: 0x00016094
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00017EC8 File Offset: 0x000160C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, decimal argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00017EFC File Offset: 0x000160FC
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00017F2C File Offset: 0x0001612C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, object argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00017F5C File Offset: 0x0001615C
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00017F90 File Offset: 0x00016190
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, sbyte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00017FC4 File Offset: 0x000161C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[StringFormatMethod("message")]
		[CLSCompliant(false)]
		public void Fatal(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00017FF8 File Offset: 0x000161F8
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, uint argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001802C File Offset: 0x0001622C
		[StringFormatMethod("message")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00018060 File Offset: 0x00016260
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		[StringFormatMethod("message")]
		public void Fatal(string message, ulong argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[]
				{
					argument
				});
			}
		}

		// Token: 0x04000282 RID: 642
		private readonly Type loggerType = typeof(Logger);

		// Token: 0x04000283 RID: 643
		private volatile LoggerConfiguration configuration;

		// Token: 0x04000284 RID: 644
		private volatile bool isTraceEnabled;

		// Token: 0x04000285 RID: 645
		private volatile bool isDebugEnabled;

		// Token: 0x04000286 RID: 646
		private volatile bool isInfoEnabled;

		// Token: 0x04000287 RID: 647
		private volatile bool isWarnEnabled;

		// Token: 0x04000288 RID: 648
		private volatile bool isErrorEnabled;

		// Token: 0x04000289 RID: 649
		private volatile bool isFatalEnabled;
	}
}
