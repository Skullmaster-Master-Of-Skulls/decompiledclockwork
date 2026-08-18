using System;
using System.Globalization;
using log4net.Repository;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000072 RID: 114
	public class LogImpl : LoggerWrapperImpl, ILog, ILoggerWrapper
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000D600 File Offset: 0x0000B800
		public LogImpl(ILogger logger) : base(logger)
		{
			logger.Repository.ConfigurationChanged += this.LoggerRepositoryConfigurationChanged;
			this.ReloadLevels(logger.Repository);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D62C File Offset: 0x0000B82C
		protected virtual void ReloadLevels(ILoggerRepository repository)
		{
			LevelMap levelMap = repository.LevelMap;
			this.m_levelDebug = levelMap.LookupWithDefault(Level.Debug);
			this.m_levelInfo = levelMap.LookupWithDefault(Level.Info);
			this.m_levelWarn = levelMap.LookupWithDefault(Level.Warn);
			this.m_levelError = levelMap.LookupWithDefault(Level.Error);
			this.m_levelFatal = levelMap.LookupWithDefault(Level.Fatal);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D695 File Offset: 0x0000B895
		public virtual void Debug(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, message, null);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		public virtual void Debug(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, message, exception);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D6C9 File Offset: 0x0000B8C9
		public virtual void DebugFormat(string format, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public virtual void DebugFormat(string format, object arg0)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0
				}), null);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000D73C File Offset: 0x0000B93C
		public virtual void DebugFormat(string format, object arg0, object arg1)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1
				}), null);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D784 File Offset: 0x0000B984
		public virtual void DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1,
					arg2
				}), null);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public virtual void DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelDebug, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
		public virtual void Info(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, message, null);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D813 File Offset: 0x0000BA13
		public virtual void Info(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, message, exception);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D82D File Offset: 0x0000BA2D
		public virtual void InfoFormat(string format, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public virtual void InfoFormat(string format, object arg0)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0
				}), null);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		public virtual void InfoFormat(string format, object arg0, object arg1)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1
				}), null);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public virtual void InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1,
					arg2
				}), null);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000D934 File Offset: 0x0000BB34
		public virtual void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelInfo, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D95D File Offset: 0x0000BB5D
		public virtual void Warn(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, message, null);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000D977 File Offset: 0x0000BB77
		public virtual void Warn(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, message, exception);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D991 File Offset: 0x0000BB91
		public virtual void WarnFormat(string format, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public virtual void WarnFormat(string format, object arg0)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0
				}), null);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public virtual void WarnFormat(string format, object arg0, object arg1)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1
				}), null);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public virtual void WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1,
					arg2
				}), null);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public virtual void WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelWarn, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000DAC1 File Offset: 0x0000BCC1
		public virtual void Error(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, message, null);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000DADB File Offset: 0x0000BCDB
		public virtual void Error(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, message, exception);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
		public virtual void ErrorFormat(string format, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public virtual void ErrorFormat(string format, object arg0)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0
				}), null);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public virtual void ErrorFormat(string format, object arg0, object arg1)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1
				}), null);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public virtual void ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1,
					arg2
				}), null);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public virtual void ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelError, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000DC25 File Offset: 0x0000BE25
		public virtual void Fatal(object message)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, message, null);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DC3F File Offset: 0x0000BE3F
		public virtual void Fatal(object message, Exception exception)
		{
			this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, message, exception);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DC59 File Offset: 0x0000BE59
		public virtual void FatalFormat(string format, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public virtual void FatalFormat(string format, object arg0)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0
				}), null);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DCCC File Offset: 0x0000BECC
		public virtual void FatalFormat(string format, object arg0, object arg1)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1
				}), null);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public virtual void FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, new object[]
				{
					arg0,
					arg1,
					arg2
				}), null);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public virtual void FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.Logger.Log(LogImpl.ThisDeclaringType, this.m_levelFatal, new SystemStringFormat(provider, format, args), null);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000DD89 File Offset: 0x0000BF89
		public virtual bool IsDebugEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelDebug);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		public virtual bool IsInfoEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelInfo);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		public virtual bool IsWarnEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelWarn);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000DDC2 File Offset: 0x0000BFC2
		public virtual bool IsErrorEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelError);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		public virtual bool IsFatalEnabled
		{
			get
			{
				return this.Logger.IsEnabledFor(this.m_levelFatal);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		private void LoggerRepositoryConfigurationChanged(object sender, EventArgs e)
		{
			ILoggerRepository loggerRepository = sender as ILoggerRepository;
			if (loggerRepository != null)
			{
				this.ReloadLevels(loggerRepository);
			}
		}

		// Token: 0x040001C2 RID: 450
		private static readonly Type ThisDeclaringType = typeof(LogImpl);

		// Token: 0x040001C3 RID: 451
		private Level m_levelDebug;

		// Token: 0x040001C4 RID: 452
		private Level m_levelInfo;

		// Token: 0x040001C5 RID: 453
		private Level m_levelWarn;

		// Token: 0x040001C6 RID: 454
		private Level m_levelError;

		// Token: 0x040001C7 RID: 455
		private Level m_levelFatal;
	}
}
