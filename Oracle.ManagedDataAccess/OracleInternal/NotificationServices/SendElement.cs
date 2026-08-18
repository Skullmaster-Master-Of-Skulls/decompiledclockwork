using System;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018F RID: 399
	internal class SendElement
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x0009F1A4 File Offset: 0x0009D3A4
		protected internal SendElement(Notification ev)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.e = ev;
			this.s = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0009F1F8 File Offset: 0x0009D3F8
		protected internal SendElement(SubscriptionNotification se)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.e = null;
			this.s = se;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x040011D6 RID: 4566
		protected internal Notification e;

		// Token: 0x040011D7 RID: 4567
		protected internal SubscriptionNotification s;
	}
}
