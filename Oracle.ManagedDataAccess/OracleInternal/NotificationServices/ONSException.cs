using System;
using System.Runtime.Serialization;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal class ONSException : SystemException
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x0009DA2C File Offset: 0x0009BC2C
		protected internal ONSException(string s) : base(s)
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

		// Token: 0x06000F21 RID: 3873 RVA: 0x0009DA68 File Offset: 0x0009BC68
		protected ONSException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
