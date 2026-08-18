using System;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200017C RID: 380
	internal class CallBackThread : SupportClass.ThreadClass
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00098BD4 File Offset: 0x00096DD4
		protected internal CallBackThread(Subscriber subs, CallBack cbo, Notification not)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.shutdown = false;
			base.IsBackground = true;
			this.s = subs;
			this.cb = cbo;
			this.cbmode = 2;
			this.n = not;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00098C44 File Offset: 0x00096E44
		public override void Run()
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00098C48 File Offset: 0x00096E48
		protected internal virtual void set_shutdown()
		{
			this.shutdown = true;
		}

		// Token: 0x040010EB RID: 4331
		private const int ONETHREAD = 1;

		// Token: 0x040010EC RID: 4332
		private const int THREADPERCB = 2;

		// Token: 0x040010ED RID: 4333
		private Subscriber s;

		// Token: 0x040010EE RID: 4334
		private CallBack cb;

		// Token: 0x040010EF RID: 4335
		private int cbmode;

		// Token: 0x040010F0 RID: 4336
		private Notification n;

		// Token: 0x040010F1 RID: 4337
		private bool shutdown;
	}
}
