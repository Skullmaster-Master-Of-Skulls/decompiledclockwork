using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.I18N;

namespace OracleInternal.TTC
{
	// Token: 0x0200021E RID: 542
	internal class TTCClob : TTCLob
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x000D5FDC File Offset: 0x000D41DC
		internal TTCClob(MarshallingEngine mEngine) : base(mEngine)
		{
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000D5FE8 File Offset: 0x000D41E8
		internal long Read(byte[] lobLocator, long locatorOffset, long numCharsToRead, bool bVariableWidthChar, out byte[] outBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobBytesRead;
			try
			{
				outBuffer = null;
				base.Initialize();
				byte[] array;
				if (this.m_variableWidthChar)
				{
					array = new byte[(int)numCharsToRead * 2];
				}
				else
				{
					array = new byte[(int)numCharsToRead * 3];
				}
				this.m_lobOperation = 2L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numCharsToRead;
				this.m_bSendLobAmount = true;
				this.m_outBuffer = array;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				outBuffer = array;
				lobBytesRead = this.m_lobBytesRead;
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
			return lobBytesRead;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000D60D8 File Offset: 0x000D42D8
		internal void Read(byte[] lobLocator, long locatorOffset, long numCharsToRead, List<ArraySegment<byte>> dataSegments)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.Initialize();
				this.m_lobOperation = 2L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numCharsToRead;
				this.m_bSendLobAmount = true;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(dataSegments);
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

		// Token: 0x06001431 RID: 5169 RVA: 0x000D6194 File Offset: 0x000D4394
		internal long Write(byte[] lobLocator, bool bIsNClob, short serverNCharSet, long locatorOffset, char[] inBuffer, long inBufferOffset, long numCharsToWrite)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				bool flag = false;
				if ((lobLocator[6] & 128) == 128)
				{
					flag = true;
				}
				if ((lobLocator[7] & 64) == 64)
				{
					this.m_bLittleEndianClob = true;
				}
				byte[] inBuffer2;
				if (flag)
				{
					inBuffer2 = new byte[(int)numCharsToWrite * 2];
					if (this.m_marshallingEngine.DBVersion < 10200 && this.m_bLittleEndianClob)
					{
						Conv instance = Conv.GetInstance(2002);
						inBuffer2 = instance.ConvertCharsToBytes(inBuffer, (int)inBufferOffset, (int)numCharsToWrite, true);
					}
					else
					{
						Conv instance2 = Conv.GetInstance(2000);
						inBuffer2 = instance2.ConvertCharsToBytes(inBuffer, (int)inBufferOffset, (int)numCharsToWrite, true);
					}
				}
				else if (!bIsNClob)
				{
					inBuffer2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertCharsToBytes(inBuffer, (int)inBufferOffset, (int)numCharsToWrite, true);
				}
				else
				{
					inBuffer2 = this.m_marshallingEngine.m_nCharSetConv.ConvertCharsToBytes(inBuffer, (int)inBufferOffset, (int)numCharsToWrite, true);
				}
				base.Initialize();
				this.m_lobOperation = 64L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numCharsToWrite;
				this.m_bSendLobAmount = true;
				this.m_inBuffer = inBuffer2;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				if (flag)
				{
					this.m_lobData.WriteLobData(this.m_inBuffer, 0L, (long)this.m_inBuffer.Length);
				}
				else
				{
					this.m_lobData.WriteLobData(this.m_inBuffer, 0L, (long)this.m_inBuffer.Length);
				}
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x000D637C File Offset: 0x000D457C
		internal long Write(byte[] lobLocator, bool bIsNClob, short serverNCharSet, long locatorOffset, byte[] inBuffer, long inBufferOffset, long numCharsToWrite, bool bVariableWidthChar)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long lobAmount;
			try
			{
				if ((lobLocator[7] & 64) == 64)
				{
					this.m_bLittleEndianClob = true;
				}
				base.Initialize();
				this.m_lobOperation = 64L;
				this.m_sourceLobLocator = lobLocator;
				this.m_sourceOffset = locatorOffset;
				this.m_lobAmount = numCharsToWrite;
				this.m_bSendLobAmount = true;
				this.m_inBuffer = inBuffer;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				if (bVariableWidthChar)
				{
					this.m_lobData.WriteLobData(this.m_inBuffer, 0L, (long)this.m_inBuffer.Length);
				}
				else
				{
					this.m_lobData.WriteLobData(this.m_inBuffer, 0L, (long)this.m_inBuffer.Length);
				}
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				lobAmount = this.m_lobAmount;
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
			return lobAmount;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000D6498 File Offset: 0x000D4698
		internal override byte[] CreateTemporaryLob(bool bCache, bool bNClob, int duration)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[] sourceLobLocator;
			try
			{
				if (12 == duration)
				{
					throw new Exception("Invalid duration in CreateTemporaryLob");
				}
				base.Initialize();
				this.m_lobOperation = 272L;
				this.m_sourceLobLocator = new byte[40];
				this.m_sourceLobLocator[1] = 84;
				this.m_lobAmount = 10L;
				this.m_bSendLobAmount = true;
				if (bNClob)
				{
					this.m_sourceOffset = 2L;
				}
				else
				{
					this.m_sourceOffset = 1L;
				}
				this.m_destinationOffset = 112L;
				this.m_destinationLength = duration;
				this.m_bNullO2U = true;
				this.m_characterSet = (bNClob ? 2000 : 178);
				this.m_lobSCN = new int[1];
				this.m_lobSCN[0] = (bCache ? 1 : 0);
				this.m_lobSCNLength = 1;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				sourceLobLocator = this.m_sourceLobLocator;
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
			return sourceLobLocator;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000D65DC File Offset: 0x000D47DC
		internal bool Open(byte[] lobLocator, int mode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				int mode2 = 2;
				if (mode == 0)
				{
					mode2 = 1;
				}
				result = base.OpenLob(lobLocator, mode2, 32768);
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

		// Token: 0x06001435 RID: 5173 RVA: 0x000D6660 File Offset: 0x000D4860
		internal bool Close(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				result = base.CloseLob(lobLocator, 65536);
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

		// Token: 0x04001654 RID: 5716
		internal bool m_bLittleEndianClob;
	}
}
