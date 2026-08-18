using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000237 RID: 567
	internal class TTCSwitchSession : TTCFunction
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x000DE884 File Offset: 0x000DCA84
		internal TTCSwitchSession(MarshallingEngine mEngine) : base(mEngine, 107, 0)
		{
			this.m_ttcCode = 17;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000DE898 File Offset: 0x000DCA98
		internal void Write(int sessionId, int serialNum, int opCode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalUB4((long)sessionId);
				this.m_marshallingEngine.MarshalUB4((long)serialNum);
				this.m_marshallingEngine.MarshalUB4((long)opCode);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x04001901 RID: 6401
		internal static int OSESSWS = 1;

		// Token: 0x04001902 RID: 6402
		internal static int OSESGID = 2;

		// Token: 0x04001903 RID: 6403
		internal static int OSESDET = 3;

		// Token: 0x04001904 RID: 6404
		internal static int OSESDEL = 4;

		// Token: 0x04001905 RID: 6405
		internal static int OSESCLN = 5;

		// Token: 0x04001906 RID: 6406
		internal static int OSESINI = 6;

		// Token: 0x04001907 RID: 6407
		internal static int OSESSWB = 7;
	}
}
