using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000230 RID: 560
	internal class TTCReExecuteSql : TTCFunction
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x000DD430 File Offset: 0x000DB630
		internal TTCReExecuteSql(MarshallingEngine mEngine) : base(mEngine, 4, 0)
		{
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000DD43C File Offset: 0x000DB63C
		internal void WriteMessage(short ttcCallCode, int cursorId, int exerof, int execFlags, long numIterations, bool bArrayBinding, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindParamsHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_functionCode = ttcCallCode;
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalSWORD(cursorId);
				this.m_marshallingEngine.MarshalSWORD((int)numIterations);
				this.m_marshallingEngine.MarshalSWORD(exerof);
				this.m_marshallingEngine.MarshalSWORD(execFlags);
				object[] paramValueArray = marshalBindParamsHelper.m_paramValueArray;
				if (paramValueArray != null && paramValueArray.Length > 0)
				{
					if (bArrayBinding)
					{
						TTCExecuteSql.MarshalValuesForArrayBind(this.m_marshallingEngine, (int)numIterations, 0, ref marshalBindParamsHelper);
					}
					else
					{
						TTCExecuteSql.MarshalBindValues(this.m_marshallingEngine, ref marshalBindParamsHelper);
					}
				}
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
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

		// Token: 0x040018E3 RID: 6371
		internal const int EXE_COMMIT_ON_SUCCESS = 1;
	}
}
