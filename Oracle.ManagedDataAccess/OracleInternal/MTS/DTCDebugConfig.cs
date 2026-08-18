using System;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x02000123 RID: 291
	internal class DTCDebugConfig
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0008AAE4 File Offset: 0x00088CE4
		static DTCDebugConfig()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("DTC_DEBUG_EVENT");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				try
				{
					DTCDebugConfig.s_DTCDbgEvt = (DTCDebugEvent)int.Parse(environmentVariable);
				}
				catch (Exception)
				{
					DTCDebugConfig.s_DTCDbgEvt = (DTCDebugEvent)0;
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"DTC Debug Event is set to " + DTCDebugConfig.s_DTCDbgEvt
				});
			}
		}

		// Token: 0x04000D5E RID: 3422
		internal const string DTC_DEBUG_EVENT_TAG = "DTC_DEBUG_EVENT";

		// Token: 0x04000D5F RID: 3423
		internal const uint DTC_DEFAULT_ABORT_TIMEOUT = 2U;

		// Token: 0x04000D60 RID: 3424
		internal static DTCDebugEvent s_DTCDbgEvt;
	}
}
