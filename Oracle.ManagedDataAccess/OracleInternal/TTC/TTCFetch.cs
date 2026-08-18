using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200022A RID: 554
	internal class TTCFetch : TTCFunction
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x000DC1DC File Offset: 0x000DA3DC
		internal TTCFetch(MarshallingEngine mEngine) : base(mEngine, 5, 0)
		{
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000DC1E8 File Offset: 0x000DA3E8
		internal void WriteMessage(int cursorId, int noOfRowsToFetch)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalSWORD(cursorId);
				this.m_marshallingEngine.MarshalSWORD(noOfRowsToFetch);
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
	}
}
