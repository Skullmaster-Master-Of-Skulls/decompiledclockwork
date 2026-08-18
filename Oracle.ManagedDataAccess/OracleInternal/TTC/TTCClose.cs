using System;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200021F RID: 543
	internal class TTCClose : TTCFunction
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x000D66DC File Offset: 0x000D48DC
		internal TTCClose(MarshallingEngine mEngine) : base(mEngine, 105, 0)
		{
			this.m_ttcCode = 17;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000D66F0 File Offset: 0x000D48F0
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
