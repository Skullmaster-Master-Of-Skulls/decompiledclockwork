using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200022C RID: 556
	internal class TTCLobData : TTCMessage
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x000DC320 File Offset: 0x000DA520
		internal TTCLobData(MarshallingEngine mEngine) : base(mEngine, 14)
		{
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000DC32C File Offset: 0x000DA52C
		internal void WriteLobData(byte[] inBuffer, long inBufferOffset, long numBytes)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteTTCCode();
				this.m_marshallingEngine.MarshalCLR(inBuffer, (int)inBufferOffset, (int)numBytes);
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

		// Token: 0x06001481 RID: 5249 RVA: 0x000DC3B0 File Offset: 0x000DA5B0
		internal long ReadLobData(byte[] outBuffer, long outBufferOffset)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long num = 0L;
			int num2 = 0;
			long num3 = outBufferOffset;
			long result;
			try
			{
				int num4 = 0;
				while (num4 != 4)
				{
					switch (num4)
					{
					case 0:
						num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
						if (num2 == 254)
						{
							num4 = 2;
						}
						else
						{
							num4 = 1;
						}
						break;
					case 1:
						this.m_marshallingEngine.GetNBytes(outBuffer, (int)num3, num2);
						num += (long)num2;
						num4 = 4;
						break;
					case 2:
						if (this.m_marshallingEngine.m_bUseBigCLRChunks)
						{
							num2 = this.m_marshallingEngine.UnmarshalSB4();
						}
						else
						{
							num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
						}
						if (num2 > 0)
						{
							num4 = 3;
						}
						else
						{
							num4 = 4;
						}
						break;
					case 3:
						this.m_marshallingEngine.GetNBytes(outBuffer, (int)num3, num2);
						num += (long)num2;
						num3 += (long)num2;
						num4 = 2;
						break;
					}
				}
				result = num;
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
			return result;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000DC4E0 File Offset: 0x000DA6E0
		internal long ReadLobDataForArray()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long num = 0L;
			int num2 = 0;
			long result;
			try
			{
				int num3 = 0;
				while (num3 != 4)
				{
					switch (num3)
					{
					case 0:
						num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
						if (num2 == 254)
						{
							num3 = 2;
						}
						else
						{
							num3 = 1;
						}
						break;
					case 1:
						this.m_marshallingEngine.GetNBytes_ScanOnly(num2);
						num += (long)num2;
						num3 = 4;
						break;
					case 2:
						if (this.m_marshallingEngine.m_bUseBigCLRChunks)
						{
							num2 = this.m_marshallingEngine.UnmarshalSB4();
						}
						else
						{
							num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
						}
						if (num2 > 0)
						{
							num3 = 3;
						}
						else
						{
							num3 = 4;
						}
						break;
					case 3:
						this.m_marshallingEngine.GetNBytes_ScanOnly(num2);
						num += (long)num2;
						num3 = 2;
						break;
					}
				}
				result = num;
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
			return result;
		}

		// Token: 0x040018B0 RID: 6320
		internal const int LOBD_STATE0 = 0;

		// Token: 0x040018B1 RID: 6321
		internal const int LOBD_STATE1 = 1;

		// Token: 0x040018B2 RID: 6322
		internal const int LOBD_STATE2 = 2;

		// Token: 0x040018B3 RID: 6323
		internal const int LOBD_STATE3 = 3;

		// Token: 0x040018B4 RID: 6324
		internal const int LOBD_STATE_EXIT = 4;

		// Token: 0x040018B5 RID: 6325
		internal const short TTCG_LNG = 254;

		// Token: 0x040018B6 RID: 6326
		internal const short LOBDATALENGTH = 252;
	}
}
