using System;
using System.Runtime.Serialization;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	internal class SubscriptionException : SystemException
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x0009FF7C File Offset: 0x0009E17C
		protected internal SubscriptionException(string s) : base(s)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0009FFB8 File Offset: 0x0009E1B8
		protected SubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
