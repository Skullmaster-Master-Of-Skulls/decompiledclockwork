using System;
using log4net.Appender;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000EF RID: 239
	public class AppenderAttachedImpl : IAppenderAttachable
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x000155B0 File Offset: 0x000137B0
		public int AppendLoopOnAppenders(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_appenderList == null)
			{
				return 0;
			}
			if (this.m_appenderArray == null)
			{
				this.m_appenderArray = this.m_appenderList.ToArray();
			}
			foreach (IAppender appender in this.m_appenderArray)
			{
				try
				{
					appender.DoAppend(loggingEvent);
				}
				catch (Exception exception)
				{
					LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to append to appender [" + appender.Name + "]", exception);
				}
			}
			return this.m_appenderList.Count;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00015650 File Offset: 0x00013850
		public int AppendLoopOnAppenders(LoggingEvent[] loggingEvents)
		{
			if (loggingEvents == null)
			{
				throw new ArgumentNullException("loggingEvents");
			}
			if (loggingEvents.Length == 0)
			{
				throw new ArgumentException("loggingEvents array must not be empty", "loggingEvents");
			}
			if (loggingEvents.Length == 1)
			{
				return this.AppendLoopOnAppenders(loggingEvents[0]);
			}
			if (this.m_appenderList == null)
			{
				return 0;
			}
			if (this.m_appenderArray == null)
			{
				this.m_appenderArray = this.m_appenderList.ToArray();
			}
			foreach (IAppender appender in this.m_appenderArray)
			{
				try
				{
					AppenderAttachedImpl.CallAppend(appender, loggingEvents);
				}
				catch (Exception exception)
				{
					LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to append to appender [" + appender.Name + "]", exception);
				}
			}
			return this.m_appenderList.Count;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00015714 File Offset: 0x00013914
		private static void CallAppend(IAppender appender, LoggingEvent[] loggingEvents)
		{
			IBulkAppender bulkAppender = appender as IBulkAppender;
			if (bulkAppender != null)
			{
				bulkAppender.DoAppend(loggingEvents);
				return;
			}
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				appender.DoAppend(loggingEvent);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00015750 File Offset: 0x00013950
		public void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			this.m_appenderArray = null;
			if (this.m_appenderList == null)
			{
				this.m_appenderList = new AppenderCollection(1);
			}
			if (!this.m_appenderList.Contains(newAppender))
			{
				this.m_appenderList.Add(newAppender);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000157A1 File Offset: 0x000139A1
		public AppenderCollection Appenders
		{
			get
			{
				if (this.m_appenderList == null)
				{
					return AppenderCollection.EmptyCollection;
				}
				return AppenderCollection.ReadOnly(this.m_appenderList);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000157BC File Offset: 0x000139BC
		public IAppender GetAppender(string name)
		{
			if (this.m_appenderList != null && name != null)
			{
				foreach (IAppender appender in this.m_appenderList)
				{
					if (name == appender.Name)
					{
						return appender;
					}
				}
			}
			return null;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001582C File Offset: 0x00013A2C
		public void RemoveAllAppenders()
		{
			if (this.m_appenderList != null)
			{
				foreach (IAppender appender in this.m_appenderList)
				{
					try
					{
						appender.Close();
					}
					catch (Exception exception)
					{
						LogLog.Error(AppenderAttachedImpl.declaringType, "Failed to Close appender [" + appender.Name + "]", exception);
					}
				}
				this.m_appenderList = null;
				this.m_appenderArray = null;
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000158C8 File Offset: 0x00013AC8
		public IAppender RemoveAppender(IAppender appender)
		{
			if (appender != null && this.m_appenderList != null)
			{
				this.m_appenderList.Remove(appender);
				if (this.m_appenderList.Count == 0)
				{
					this.m_appenderList = null;
				}
				this.m_appenderArray = null;
			}
			return appender;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000158FD File Offset: 0x00013AFD
		public IAppender RemoveAppender(string name)
		{
			return this.RemoveAppender(this.GetAppender(name));
		}

		// Token: 0x0400029A RID: 666
		private AppenderCollection m_appenderList;

		// Token: 0x0400029B RID: 667
		private IAppender[] m_appenderArray;

		// Token: 0x0400029C RID: 668
		private static readonly Type declaringType = typeof(AppenderAttachedImpl);
	}
}
