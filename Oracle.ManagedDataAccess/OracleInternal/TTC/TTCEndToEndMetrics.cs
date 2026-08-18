using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000225 RID: 549
	internal class TTCEndToEndMetrics : TTCFunction
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x000D8FD4 File Offset: 0x000D71D4
		internal TTCEndToEndMetrics(MarshallingEngine mEngine) : base(mEngine, 135, 0)
		{
			this.m_ttcCode = 17;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000D8FEC File Offset: 0x000D71EC
		internal void Write(string[] endToEndMetrics, bool[] endToEndMetricsModified)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				int num = 0;
				if (endToEndMetricsModified[2])
				{
					num |= 16;
				}
				if (endToEndMetricsModified[0])
				{
					num |= 1;
				}
				if (endToEndMetricsModified[1])
				{
					num |= 8;
				}
				if (endToEndMetricsModified[3])
				{
					num |= 256;
				}
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4((long)num);
				byte[] array = null;
				if (endToEndMetricsModified[0])
				{
					this.m_marshallingEngine.MarshalPointer();
					if (!string.IsNullOrEmpty(endToEndMetrics[0]))
					{
						array = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(endToEndMetrics[0], 0, endToEndMetrics[0].Length, true);
						this.m_marshallingEngine.MarshalUB4((long)array.Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				byte[] array2 = null;
				if (endToEndMetricsModified[1])
				{
					this.m_marshallingEngine.MarshalPointer();
					if (!string.IsNullOrEmpty(endToEndMetrics[1]))
					{
						array2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(endToEndMetrics[1], 0, endToEndMetrics[1].Length, true);
						this.m_marshallingEngine.MarshalUB4((long)endToEndMetrics[1].Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				byte[] array3 = null;
				if (endToEndMetricsModified[2])
				{
					this.m_marshallingEngine.MarshalPointer();
					if (!string.IsNullOrEmpty(endToEndMetrics[2]))
					{
						array3 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(endToEndMetrics[2], 0, endToEndMetrics[2].Length, true);
						this.m_marshallingEngine.MarshalUB4((long)endToEndMetrics[2].Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalUB1(0);
				this.m_marshallingEngine.MarshalUB2(0);
				byte[] array4 = null;
				if (endToEndMetricsModified[3])
				{
					this.m_marshallingEngine.MarshalPointer();
					if (!string.IsNullOrEmpty(endToEndMetrics[3]))
					{
						array4 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(endToEndMetrics[3], 0, endToEndMetrics[3].Length, true);
						this.m_marshallingEngine.MarshalUB4((long)endToEndMetrics[3].Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(0L);
				byte[] array5 = null;
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				if (array != null)
				{
					this.m_marshallingEngine.MarshalCHR(array);
				}
				if (array2 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array2);
				}
				if (array3 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array3);
				}
				if (array4 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array4);
				}
				if (array5 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array5);
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

		// Token: 0x04001821 RID: 6177
		internal const short KPDUSR_CID_RESET = 1;

		// Token: 0x04001822 RID: 6178
		internal const short KPDUSR_PROXY_RESET = 2;

		// Token: 0x04001823 RID: 6179
		internal const short KPDUSR_PROXY_TKTSENT = 4;

		// Token: 0x04001824 RID: 6180
		internal const short KPDUSR_MODULE_RESET = 8;

		// Token: 0x04001825 RID: 6181
		internal const short KPDUSR_ACTION_RESET = 16;

		// Token: 0x04001826 RID: 6182
		internal const short KPDUSR_EXECID_RESET = 32;

		// Token: 0x04001827 RID: 6183
		internal const short KPDUSR_EXECSQ_RESET = 64;

		// Token: 0x04001828 RID: 6184
		internal const short KPDUSR_COLLCT_RESET = 128;

		// Token: 0x04001829 RID: 6185
		internal const short KPDUSR_CLINFO_RESET = 256;
	}
}
