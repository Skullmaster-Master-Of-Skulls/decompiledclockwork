using System;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200021D RID: 541
	internal class TTCCancel : TTCFunction
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x000D5F10 File Offset: 0x000D4110
		internal TTCCancel(MarshallingEngine mEngine) : base(mEngine, 120, 0)
		{
			this.m_ttcCode = 17;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x000D5F24 File Offset: 0x000D4124
		internal void Write(ArrayList cursorIdList)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				int count = cursorIdList.Count;
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalUB4((long)count);
				for (int i = 0; i < count; i++)
				{
					this.m_marshallingEngine.MarshalUB4((long)cursorIdList[i]);
				}
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
