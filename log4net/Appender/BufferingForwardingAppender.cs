using System;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000016 RID: 22
	public class BufferingForwardingAppender : BufferingAppenderSkeleton, IAppenderAttachable
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00003E24 File Offset: 0x00002024
		protected override void OnClose()
		{
			lock (this)
			{
				base.OnClose();
				if (this.m_appenderAttachedImpl != null)
				{
					this.m_appenderAttachedImpl.RemoveAllAppenders();
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003E74 File Offset: 0x00002074
		protected override void SendBuffer(LoggingEvent[] events)
		{
			if (this.m_appenderAttachedImpl != null)
			{
				this.m_appenderAttachedImpl.AppendLoopOnAppenders(events);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003E8C File Offset: 0x0000208C
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003EF0 File Offset: 0x000020F0
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

		// Token: 0x060000DD RID: 221 RVA: 0x00003F44 File Offset: 0x00002144
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

		// Token: 0x060000DE RID: 222 RVA: 0x00003F98 File Offset: 0x00002198
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

		// Token: 0x060000DF RID: 223 RVA: 0x00003FE8 File Offset: 0x000021E8
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

		// Token: 0x060000E0 RID: 224 RVA: 0x0000403C File Offset: 0x0000223C
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

		// Token: 0x04000050 RID: 80
		private AppenderAttachedImpl m_appenderAttachedImpl;
	}
}
