using System;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000185 RID: 389
	internal class QueueElement
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x0009B794 File Offset: 0x00099994
		public QueueElement(object o)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.obj = o;
			this.next = null;
			this.priority = 10;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0009B7F0 File Offset: 0x000999F0
		public QueueElement(object o, int p)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.obj = o;
			this.next = null;
			this.priority = p;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x04001169 RID: 4457
		public object obj;

		// Token: 0x0400116A RID: 4458
		public QueueElement next;

		// Token: 0x0400116B RID: 4459
		public int priority;
	}
}
