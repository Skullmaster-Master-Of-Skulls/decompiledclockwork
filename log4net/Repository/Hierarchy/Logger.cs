using System;
using System.Security;
using log4net.Appender;
using log4net.Core;
using log4net.Util;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000C5 RID: 197
	public abstract class Logger : IAppenderAttachable, ILogger
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00011887 File Offset: 0x0000FA87
		protected Logger(string name)
		{
			this.m_name = string.Intern(name);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000118AD File Offset: 0x0000FAAD
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000118B5 File Offset: 0x0000FAB5
		public virtual Logger Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000118BE File Offset: 0x0000FABE
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x000118C6 File Offset: 0x0000FAC6
		public virtual bool Additivity
		{
			get
			{
				return this.m_additive;
			}
			set
			{
				this.m_additive = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public virtual Level EffectiveLevel
		{
			get
			{
				for (Logger logger = this; logger != null; logger = logger.m_parent)
				{
					Level level = logger.m_level;
					if (level != null)
					{
						return level;
					}
				}
				return null;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000118F8 File Offset: 0x0000FAF8
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00011900 File Offset: 0x0000FB00
		public virtual Hierarchy Hierarchy
		{
			get
			{
				return this.m_hierarchy;
			}
			set
			{
				this.m_hierarchy = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00011909 File Offset: 0x0000FB09
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00011911 File Offset: 0x0000FB11
		public virtual Level Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001191C File Offset: 0x0000FB1C
		public virtual void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl == null)
				{
					this.m_appenderAttachedImpl = new AppenderAttachedImpl();
				}
				this.m_appenderAttachedImpl.AddAppender(newAppender);
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00011980 File Offset: 0x0000FB80
		public virtual AppenderCollection Appenders
		{
			get
			{
				this.m_appenderLock.AcquireReaderLock();
				AppenderCollection result;
				try
				{
					if (this.m_appenderAttachedImpl == null)
					{
						result = AppenderCollection.EmptyCollection;
					}
					else
					{
						result = this.m_appenderAttachedImpl.Appenders;
					}
				}
				finally
				{
					this.m_appenderLock.ReleaseReaderLock();
				}
				return result;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public virtual IAppender GetAppender(string name)
		{
			this.m_appenderLock.AcquireReaderLock();
			IAppender result;
			try
			{
				if (this.m_appenderAttachedImpl == null || name == null)
				{
					result = null;
				}
				else
				{
					result = this.m_appenderAttachedImpl.GetAppender(name);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00011A28 File Offset: 0x0000FC28
		public virtual void RemoveAllAppenders()
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
					this.m_appenderAttachedImpl = null;
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00011A78 File Offset: 0x0000FC78
		public virtual IAppender RemoveAppender(IAppender appender)
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (appender != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(appender);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
			return null;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011ACC File Offset: 0x0000FCCC
		public virtual IAppender RemoveAppender(string name)
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (name != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(name);
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
			return null;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00011B20 File Offset: 0x0000FD20
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00011B28 File Offset: 0x0000FD28
		public virtual void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
		{
			try
			{
				if (this.IsEnabledFor(level))
				{
					this.ForcedLog((callerStackBoundaryDeclaringType != null) ? callerStackBoundaryDeclaringType : Logger.declaringType, level, message, exception);
				}
			}
			catch (Exception exception2)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", exception2);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00011B80 File Offset: 0x0000FD80
		public virtual void Log(LoggingEvent logEvent)
		{
			try
			{
				if (logEvent != null && this.IsEnabledFor(logEvent.Level))
				{
					this.ForcedLog(logEvent);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", exception);
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public virtual bool IsEnabledFor(Level level)
		{
			try
			{
				if (level != null)
				{
					if (this.m_hierarchy.IsDisabled(level))
					{
						return false;
					}
					return level >= this.EffectiveLevel;
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(Logger.declaringType, "Exception while logging", exception);
			}
			return false;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public ILoggerRepository Repository
		{
			get
			{
				return this.m_hierarchy;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011C34 File Offset: 0x0000FE34
		protected virtual void CallAppenders(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			int num = 0;
			for (Logger logger = this; logger != null; logger = logger.m_parent)
			{
				if (logger.m_appenderAttachedImpl != null)
				{
					logger.m_appenderLock.AcquireReaderLock();
					try
					{
						if (logger.m_appenderAttachedImpl != null)
						{
							num += logger.m_appenderAttachedImpl.AppendLoopOnAppenders(loggingEvent);
						}
					}
					finally
					{
						logger.m_appenderLock.ReleaseReaderLock();
					}
				}
				if (!logger.m_additive)
				{
					break;
				}
			}
			if (!this.m_hierarchy.EmittedNoAppenderWarning && num == 0)
			{
				this.m_hierarchy.EmittedNoAppenderWarning = true;
				LogLog.Debug(Logger.declaringType, string.Concat(new string[]
				{
					"No appenders could be found for logger [",
					this.Name,
					"] repository [",
					this.Repository.Name,
					"]"
				}));
				LogLog.Debug(Logger.declaringType, "Please initialize the log4net system properly.");
				try
				{
					LogLog.Debug(Logger.declaringType, "    Current AppDomain context information: ");
					LogLog.Debug(Logger.declaringType, "       BaseDirectory   : " + SystemInfo.ApplicationBaseDirectory);
					LogLog.Debug(Logger.declaringType, "       FriendlyName    : " + AppDomain.CurrentDomain.FriendlyName);
					LogLog.Debug(Logger.declaringType, "       DynamicDirectory: " + AppDomain.CurrentDomain.DynamicDirectory);
				}
				catch (SecurityException)
				{
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public virtual void CloseNestedAppenders()
		{
			this.m_appenderLock.AcquireWriterLock();
			try
			{
				if (this.m_appenderAttachedImpl != null)
				{
					AppenderCollection appenders = this.m_appenderAttachedImpl.Appenders;
					foreach (IAppender appender in appenders)
					{
						if (appender is IAppenderAttachable)
						{
							appender.Close();
						}
					}
				}
			}
			finally
			{
				this.m_appenderLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00011E2C File Offset: 0x0001002C
		public virtual void Log(Level level, object message, Exception exception)
		{
			if (this.IsEnabledFor(level))
			{
				this.ForcedLog(Logger.declaringType, level, message, exception);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00011E45 File Offset: 0x00010045
		protected virtual void ForcedLog(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
		{
			this.CallAppenders(new LoggingEvent(callerStackBoundaryDeclaringType, this.Hierarchy, this.Name, level, message, exception));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00011E63 File Offset: 0x00010063
		protected virtual void ForcedLog(LoggingEvent logEvent)
		{
			logEvent.EnsureRepository(this.Hierarchy);
			this.CallAppenders(logEvent);
		}

		// Token: 0x0400024B RID: 587
		private static readonly Type declaringType = typeof(Logger);

		// Token: 0x0400024C RID: 588
		private readonly string m_name;

		// Token: 0x0400024D RID: 589
		private Level m_level;

		// Token: 0x0400024E RID: 590
		private Logger m_parent;

		// Token: 0x0400024F RID: 591
		private Hierarchy m_hierarchy;

		// Token: 0x04000250 RID: 592
		private AppenderAttachedImpl m_appenderAttachedImpl;

		// Token: 0x04000251 RID: 593
		private bool m_additive = true;

		// Token: 0x04000252 RID: 594
		private readonly ReaderWriterLock m_appenderLock = new ReaderWriterLock();
	}
}
