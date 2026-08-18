using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000232 RID: 562
	internal class TTCRowHeader : TTCMessage
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x000DDB84 File Offset: 0x000DBD84
		internal TTCRowHeader(MarshallingEngine mEngine) : base(mEngine, 6)
		{
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000DDB90 File Offset: 0x000DBD90
		internal void ReadMessage(TTCRowData rowData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_flags = this.m_marshallingEngine.UnmarshalUB1(false);
				this.m_noOfRequests = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_iterationNumber = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_noOfRequests += this.m_iterationNumber * 256;
				this.m_noOfIterations = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_uacBufferLength = this.m_marshallingEngine.UnmarshalUB2(false);
				byte[] array = this.m_marshallingEngine.UnmarshalDALC(false, this.m_marshallingEngine.retLen);
				if (array != null)
				{
					byte[] array2 = new byte[this.m_marshallingEngine.retLen[0]];
					Buffer.BlockCopy(array, 0, array2, 0, this.m_marshallingEngine.retLen[0]);
					rowData.SetBitVector(array2);
				}
				this.m_marshallingEngine.UnmarshalDALC(true, null);
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

		// Token: 0x0600149E RID: 5278 RVA: 0x000DDCCC File Offset: 0x000DBECC
		internal void ReInitialize()
		{
			this.m_flags = 0;
			this.m_noOfRequests = 0;
			this.m_iterationNumber = 0;
			this.m_noOfIterations = 0;
			this.m_uacBufferLength = 0;
		}

		// Token: 0x040018EA RID: 6378
		private short m_flags;

		// Token: 0x040018EB RID: 6379
		internal int m_noOfRequests;

		// Token: 0x040018EC RID: 6380
		internal int m_iterationNumber;

		// Token: 0x040018ED RID: 6381
		internal int m_noOfIterations;

		// Token: 0x040018EE RID: 6382
		private int m_uacBufferLength;
	}
}
