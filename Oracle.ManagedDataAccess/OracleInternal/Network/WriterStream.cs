using System;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x02000172 RID: 370
	internal class WriterStream
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00097458 File Offset: 0x00095658
		internal WriterStream(SessionContext sessCtx)
		{
			this.m_sessionCtx = sessCtx;
			this.m_dataPacket = new DataPacket(sessCtx);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00097480 File Offset: 0x00095680
		internal void Write(OraBuf OB)
		{
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			if (!this.m_sessionCtx.isNTConnected)
			{
				throw new NetworkException(12614);
			}
			if (OB.the_ByteSegments_Count < 2)
			{
				throw new NetworkException(-6503);
			}
			if (this.m_sessionCtx.cryptoNeeded)
			{
				this.EncryptOraBuf(OB);
			}
			DataPacket.InitForSend(OB.m_buf, OB.m_curlen, this.m_sessionCtx);
			this.m_sessionCtx.m_transportAdapter.Send(OB);
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				this.TraceOB(OB);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0009751C File Offset: 0x0009571C
		private void EncryptOraBuf(OraBuf OB)
		{
			DataIntegrityAlgorithm dataIntegrityAlg = this.m_sessionCtx.m_ano.dataIntegrityAlg;
			EncryptionAlgorithm encryptionAlg = this.m_sessionCtx.encryptionAlg;
			int the_ByteSegments_Count = OB.the_ByteSegments_Count;
			int num = this.m_sessionCtx.m_ano.foldedinkey ? 1 : 0;
			OraArraySegment oraArraySegment = OB.the_ByteSegments[the_ByteSegments_Count - 1];
			if (dataIntegrityAlg != null)
			{
				byte[] array = dataIntegrityAlg.compute(oraArraySegment.Array, oraArraySegment.Offset, oraArraySegment.Count);
				Buffer.BlockCopy(array, 0, oraArraySegment.Array, oraArraySegment.Count + oraArraySegment.Offset, array.Length);
				oraArraySegment.Count += array.Length;
				OB.m_curlen += array.Length;
			}
			if (encryptionAlg != null)
			{
				byte[] array2 = new byte[oraArraySegment.Count];
				Buffer.BlockCopy(oraArraySegment.Array, oraArraySegment.Offset, array2, 0, oraArraySegment.Count);
				byte[] array3 = this.m_sessionCtx.encryptionAlg.encrypt(array2);
				Buffer.BlockCopy(array3, 0, oraArraySegment.Array, oraArraySegment.Offset, array3.Length);
				int num2 = array3.Length - oraArraySegment.Count;
				oraArraySegment.Count += num2;
				OB.m_curlen += num2;
			}
			if (dataIntegrityAlg != null || encryptionAlg != null)
			{
				oraArraySegment.Array[oraArraySegment.Count + oraArraySegment.Offset] = (byte)num;
				oraArraySegment.Count++;
				OB.m_curlen++;
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000976A0 File Offset: 0x000958A0
		private void TraceOB(OraBuf OB)
		{
			OraArraySegment[] the_ByteSegments = OB.the_ByteSegments;
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				for (int i = 0; i < OB.the_ByteSegments_Count; i++)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Send, the_ByteSegments[i].Array, the_ByteSegments[i].Offset, the_ByteSegments[i].Count);
				}
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000976F0 File Offset: 0x000958F0
		internal void Write(byte val)
		{
			this.m_oneByteBuffer[0] = val;
			this.Write(this.m_oneByteBuffer, 0, 1);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0009770C File Offset: 0x0009590C
		internal void Write(byte[] inputBuffer)
		{
			this.Write(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0009771C File Offset: 0x0009591C
		internal void Write(byte[] inputBuffer, int offset, int length)
		{
			int num = 0;
			int dataFlags = DataPacket.NSPDAFZER;
			if (this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(3111);
			}
			while (length > num)
			{
				num += this.m_dataPacket.PutDataInBuffer(inputBuffer, offset + num, length - num);
				if (this.m_dataPacket.m_isBufferFull)
				{
					dataFlags = ((length > num) ? DataPacket.NSPDAFMOR : DataPacket.NSPDAFZER);
					this.m_dataPacket.Send(dataFlags);
				}
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0009778C File Offset: 0x0009598C
		internal void Flush()
		{
			if (this.m_dataPacket.m_availableBytesToSend > 0)
			{
				this.m_dataPacket.Send(DataPacket.NSPDAFZER);
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000977AC File Offset: 0x000959AC
		internal void DiscardData()
		{
			this.m_dataPacket.Initialize();
		}

		// Token: 0x040010BC RID: 4284
		protected SessionContext m_sessionCtx;

		// Token: 0x040010BD RID: 4285
		internal DataPacket m_dataPacket;

		// Token: 0x040010BE RID: 4286
		private byte[] m_oneByteBuffer = new byte[1];
	}
}
