using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200021B RID: 539
	internal class TTCBFile : TTCLob
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x000D5964 File Offset: 0x000D3B64
		internal TTCBFile(MarshallingEngine mEngine) : base(mEngine)
		{
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000D5970 File Offset: 0x000D3B70
		internal override byte[] CreateTemporaryLob(bool bCache, bool bNClob, int duration)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000D5978 File Offset: 0x000D3B78
		internal byte[] SetDirFileName(string directoryName, string fileName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[] result;
			try
			{
				int num = (directoryName != null) ? directoryName.Length : 0;
				int num2 = (fileName != null) ? fileName.Length : 0;
				int num3 = 16 + num + num2 + 4;
				byte[] array = new byte[num3];
				array[16] = (byte)(num / 256);
				array[17] = (byte)(num % 256);
				int num4 = 0;
				if (directoryName != null)
				{
					byte[] array2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(directoryName, 0, directoryName.Length, true);
					num4 = array2.Length;
					for (int i = 0; i < num4; i++)
					{
						array[18 + i] = array2[i];
					}
				}
				int num5 = 18 + num4;
				array[num5] = (byte)(num2 / 256);
				array[num5 + 1] = (byte)(num2 % 256);
				if (fileName != null)
				{
					byte[] array3 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(fileName, 0, fileName.Length, true);
					num4 = array3.Length;
					for (int j = 0; j < num4; j++)
					{
						array[num5 + 2 + j] = array3[j];
					}
				}
				array[0] = (byte)((num3 - 2) / 256);
				array[1] = (byte)((num3 - 2) % 256);
				array[4] = 8;
				array[8] = 0;
				array[9] = 1;
				array[2] = 0;
				array[3] = 1;
				array[10] = (array[11] = 0);
				array[5] = 8;
				result = array;
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

		// Token: 0x06001425 RID: 5157 RVA: 0x000D5B34 File Offset: 0x000D3D34
		internal bool Open(byte[] lobLocator, int mode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				result = base.OpenLob(lobLocator, 11, 256);
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

		// Token: 0x06001426 RID: 5158 RVA: 0x000D5BB0 File Offset: 0x000D3DB0
		internal bool Close(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				result = base.CloseLob(lobLocator, 512);
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

		// Token: 0x06001427 RID: 5159 RVA: 0x000D5C2C File Offset: 0x000D3E2C
		internal bool Exists(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool bLobNull;
			try
			{
				base.Initialize();
				this.m_sourceLobLocator = lobLocator;
				this.m_lobOperation = 2048L;
				this.m_bNullO2U = true;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				bLobNull = this.m_bLobNull;
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
			return bLobNull;
		}

		// Token: 0x04001643 RID: 5699
		internal const int KOLBLXFXL = 16;

		// Token: 0x04001644 RID: 5700
		internal const int KOLBLNAML = 2;

		// Token: 0x04001645 RID: 5701
		internal const int KOLBLNMXL = 255;

		// Token: 0x04001646 RID: 5702
		internal const int KOLBLLENL = 2;

		// Token: 0x04001647 RID: 5703
		internal const int KOLBLVSNL = 2;

		// Token: 0x04001648 RID: 5704
		internal const int KOLBLFLGL = 4;

		// Token: 0x04001649 RID: 5705
		internal const int KOLBLBYTL = 2;

		// Token: 0x0400164A RID: 5706
		internal const int KOLBLFIDB = 10;

		// Token: 0x0400164B RID: 5707
		internal const int KOLBLXSPRB = 12;

		// Token: 0x0400164C RID: 5708
		internal const int KOLBLDRLB = 16;

		// Token: 0x0400164D RID: 5709
		internal const int KOLBLDIRB = 18;

		// Token: 0x0400164E RID: 5710
		internal const int KOLBLLENB = 0;

		// Token: 0x0400164F RID: 5711
		internal const int KOLBLVSNB = 2;

		// Token: 0x04001650 RID: 5712
		internal new const int KOLBLFLGB = 4;

		// Token: 0x04001651 RID: 5713
		internal const int KOLBLBYTB = 8;

		// Token: 0x04001652 RID: 5714
		internal const byte KOLBLBFIL = 8;

		// Token: 0x04001653 RID: 5715
		internal const byte KOLBLINI = 8;
	}
}
