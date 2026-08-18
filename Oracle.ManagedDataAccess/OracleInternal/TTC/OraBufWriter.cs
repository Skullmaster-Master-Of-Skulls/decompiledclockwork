using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x02000216 RID: 534
	internal class OraBufWriter
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x000D1C50 File Offset: 0x000CFE50
		internal OraBufWriter(MarshallingEngine mEngine, WriterStream writerStream, OracleCommunication oracleComm)
		{
			this.m_marshallingEngine = mEngine;
			this.m_writerStream = writerStream;
			this.m_oracleComm = oracleComm;
			this.Initialize();
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000D1C80 File Offset: 0x000CFE80
		internal void Initialize()
		{
			if (this.m_currentOB == null)
			{
				this.m_currentOB = this.m_oracleComm.OraBufPool.Get(this.m_oracleComm.SDU, this.m_oracleComm, false);
			}
			else
			{
				this.m_currentOB.ReInit(false);
			}
			this.m_currentObBuffer = this.m_currentOB.buf;
			this.m_startIdxForDataSegment = (this.m_positionInCurrentOB = this.m_currentOB.cursor);
			this.m_lengthForDataSegment = 0;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000D1D00 File Offset: 0x000CFF00
		internal void Write(byte val)
		{
			if (this.m_currentOB.Space - this.m_positionInCurrentOB > 0)
			{
				this.m_currentObBuffer[this.m_positionInCurrentOB++] = val;
				this.m_lengthForDataSegment++;
				return;
			}
			this.m_oneByteBuffer[0] = val;
			this.Write(this.m_oneByteBuffer, 0, 1);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000D1D64 File Offset: 0x000CFF64
		internal void Write(byte[] inputBuffer)
		{
			this.Write(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000D1D74 File Offset: 0x000CFF74
		internal void Write(byte[] inputBuffer, int offset, int length)
		{
			this.WriteDataToOraBuf(inputBuffer, offset, length);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000D1D80 File Offset: 0x000CFF80
		internal void WriteLengthAndDataToOraBuf(bool bWritePrefixLength, int prefixLength, byte[] userBuffer, int offset, int length)
		{
			bool flag = false;
			int num = this.m_currentOB.Space - this.m_positionInCurrentOB;
			if (num > 0 && bWritePrefixLength)
			{
				this.m_currentObBuffer[this.m_positionInCurrentOB++] = (byte)(prefixLength & 255);
				this.m_lengthForDataSegment++;
				flag = true;
			}
			for (;;)
			{
				int num2 = this.m_currentOB.Space - this.m_positionInCurrentOB;
				if (num2 > 0)
				{
					int num3 = (length <= num2) ? length : num2;
					Buffer.BlockCopy(userBuffer, offset, this.m_currentObBuffer, this.m_positionInCurrentOB, num3);
					this.m_positionInCurrentOB += num3;
					this.m_lengthForDataSegment += num3;
					length -= num3;
					if (length == 0)
					{
						break;
					}
					offset += num3;
				}
				else
				{
					this.FlushData();
					if (bWritePrefixLength && !flag)
					{
						this.m_currentObBuffer[this.m_positionInCurrentOB++] = (byte)(prefixLength & 255);
						this.m_lengthForDataSegment++;
						flag = true;
					}
				}
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000D1E8C File Offset: 0x000D008C
		internal int WriteDataToOraBuf(byte[] userBuffer, int offset, int length)
		{
			int num = 0;
			for (;;)
			{
				int num2 = this.m_currentOB.Space - this.m_positionInCurrentOB;
				if (num2 > 0)
				{
					int num3 = (length <= num2) ? length : num2;
					Buffer.BlockCopy(userBuffer, offset, this.m_currentObBuffer, this.m_positionInCurrentOB, num3);
					this.m_positionInCurrentOB += num3;
					num += num3;
					this.m_lengthForDataSegment += num3;
					length -= num3;
					if (length == 0)
					{
						break;
					}
					offset += num3;
				}
				else
				{
					this.FlushData();
				}
			}
			return num;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000D1F0C File Offset: 0x000D010C
		internal void FlushData()
		{
			try
			{
				if (this.m_currentOB != null)
				{
					this.m_currentOB.Add(this.m_currentObBuffer, this.m_startIdxForDataSegment, this.m_lengthForDataSegment);
					this.m_writerStream.Write(this.m_currentOB);
				}
			}
			catch (NetworkException ex)
			{
				if (ex.ErrorCode != 3111)
				{
					throw;
				}
				this.Initialize();
				this.m_marshallingEngine.ProcessReset();
				if (this.m_marshallingEngine.TTCErrorObject.ErrorCode != 0)
				{
					byte[] errorMessage = this.m_marshallingEngine.TTCErrorObject.ErrorMessage;
					throw new OracleException(this.m_marshallingEngine.TTCErrorObject.ErrorCode, string.Empty, string.Empty, this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(errorMessage, 0, errorMessage.Length, null, true));
				}
			}
			finally
			{
				this.Initialize();
			}
		}

		// Token: 0x04001500 RID: 5376
		private MarshallingEngine m_marshallingEngine;

		// Token: 0x04001501 RID: 5377
		private WriterStream m_writerStream;

		// Token: 0x04001502 RID: 5378
		private byte[] m_oneByteBuffer = new byte[1];

		// Token: 0x04001503 RID: 5379
		internal OracleCommunication m_oracleComm;

		// Token: 0x04001504 RID: 5380
		internal OraBuf m_currentOB;

		// Token: 0x04001505 RID: 5381
		internal byte[] m_currentObBuffer;

		// Token: 0x04001506 RID: 5382
		internal int m_positionInCurrentOB;

		// Token: 0x04001507 RID: 5383
		internal int m_startIdxForDataSegment;

		// Token: 0x04001508 RID: 5384
		internal int m_lengthForDataSegment;
	}
}
