using System;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200002A RID: 42
	public class ForwardingAppender : AppenderSkeleton, IAppenderAttachable
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00005A18 File Offset: 0x00003C18
		protected override void OnClose()
		{
			lock (this)
			{
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005A60 File Offset: 0x00003C60
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_appenderAttachedImpl != null)
			{
				this.m_appenderAttachedImpl.AppendLoopOnAppenders(loggingEvent);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005A77 File Offset: 0x00003C77
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			if (this.m_appenderAttachedImpl != null)
			{
				this.m_appenderAttachedImpl.AppendLoopOnAppenders(loggingEvents);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005A90 File Offset: 0x00003C90
		public virtual void AddAppender(IAppender newAppender)
		{
			if (newAppender == null)
			{
				throw new ArgumentNullException("newAppender");
			}
			lock (this)
			{
				if (this.m_appenderAttachedImpl == null)
				{
					this.m_appenderAttachedImpl = new AppenderAttachedImpl();
				}
				this.m_appenderAttachedImpl.AddAppender(newAppender);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public virtual AppenderCollection Appenders
		{
			get
			{
				AppenderCollection result;
				lock (this)
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
				return result;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005B48 File Offset: 0x00003D48
		public virtual IAppender GetAppender(string name)
		{
			IAppender result;
			lock (this)
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
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005B9C File Offset: 0x00003D9C
		public virtual void RemoveAllAppenders()
		{
			lock (this)
			{
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
					this.m_appenderAttachedImpl = null;
				}
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005BEC File Offset: 0x00003DEC
		public virtual IAppender RemoveAppender(IAppender appender)
		{
			lock (this)
			{
				if (appender != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(appender);
				}
			}
			return null;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005C40 File Offset: 0x00003E40
		public virtual IAppender RemoveAppender(string name)
		{
			lock (this)
			{
				if (name != null && this.m_appenderAttachedImpl != null)
				{
					return this.m_appenderAttachedImpl.RemoveAppender(name);
				}
			}
			return null;
		}

		// Token: 0x04000097 RID: 151
		private AppenderAttachedImpl m_appenderAttachedImpl;
	}
}
