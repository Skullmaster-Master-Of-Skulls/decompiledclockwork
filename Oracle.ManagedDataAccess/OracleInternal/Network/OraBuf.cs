using System;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x0200015C RID: 348
	internal class OraBuf
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00092B0C File Offset: 0x00090D0C
		private OraArraySegment GetNewOAS(byte[] array, int offset, int count)
		{
			if (this.m_ASegs_cursor >= this.m_ASegs.Length)
			{
				Array.Resize<OraArraySegment>(ref this.m_ASegs, this.m_ASegs.Length * 2);
			}
			OraArraySegment oraArraySegment = this.m_ASegs[this.m_ASegs_cursor];
			if (oraArraySegment == null)
			{
				oraArraySegment = (this.m_ASegs[this.m_ASegs_cursor] = new OraArraySegment(this, array, offset, count));
			}
			else
			{
				oraArraySegment.OB = this;
				oraArraySegment.Array = array;
				oraArraySegment.Offset = offset;
				oraArraySegment.Count = count;
			}
			this.m_ASegs_cursor++;
			return oraArraySegment;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00092B98 File Offset: 0x00090D98
		private void Initialize(OracleCommunication OC, int size, bool ReceiveBuf)
		{
			this.m_OC = OC;
			if (!ReceiveBuf && size > OC.m_sessionCtx.m_sessionDataUnit)
			{
				throw new NetworkException(-6502);
			}
			this.m_size = size;
			this.m_packet = new DataPacket(OC.m_sessionCtx, size);
			this.m_buf = this.m_packet.m_dataBuffer;
			if (!ReceiveBuf)
			{
				this.Add(10);
				this.m_hdrbuf = this.m_buf;
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00092C0C File Offset: 0x00090E0C
		internal OraBuf(OracleCommunication OC, int size)
		{
			lock (OraBuf.m_sync)
			{
				this.m_id = ++OraBuf.m_sId;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
				{
					"(ALLOCATION) (bufid:" + this.m_id + ")"
				});
			}
			this.Initialize(OC, size, false);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00092CB0 File Offset: 0x00090EB0
		internal OraBuf(OracleCommunication OC, int size, bool ReceiveBuf)
		{
			lock (OraBuf.m_sync)
			{
				this.m_id = ++OraBuf.m_sId;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
				{
					"(ALLOCATION) (bufid:" + this.m_id + ")"
				});
			}
			this.Initialize(OC, size, ReceiveBuf);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00092D54 File Offset: 0x00090F54
		internal OraBuf(byte[] Buf)
		{
			lock (OraBuf.m_sync)
			{
				this.m_id = ++OraBuf.m_sId;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BUF, new string[]
				{
					"(ALLOCATION) (bufid:" + this.m_id + ")"
				});
			}
			this.m_buf = Buf;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00092DF4 File Offset: 0x00090FF4
		internal void AddForReceive(byte[] buf, int offset, int count)
		{
			this.GetNewOAS(buf, offset, count);
			this.m_cursor = offset + count;
			this.m_length += count;
			this.m_curlen += count;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00092E28 File Offset: 0x00091028
		internal void AddForReceive(int offset, int count)
		{
			this.GetNewOAS(this.m_buf, offset, count);
			this.m_cursor = offset + count;
			this.m_length += count;
			this.m_curlen += count;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00092E60 File Offset: 0x00091060
		private void AddIt(OraArraySegment NewAS)
		{
			if (this.m_curlen + NewAS.Count > this.m_OC.m_sessionCtx.m_sessionDataUnit)
			{
				this.InitForSend();
				this.m_hdrbuf = new byte[(int)TNSPacketOffsets.NSPDADAT];
				this.m_curlen = 0;
				this.AddIt(this.GetNewOAS(this.m_hdrbuf, 0, (int)TNSPacketOffsets.NSPDADAT));
			}
			this.m_length += NewAS.Count;
			this.m_curlen += NewAS.Count;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00092EE8 File Offset: 0x000910E8
		internal void Add(byte[] array, int offset, int count)
		{
			if (offset < this.m_cursor)
			{
				throw new NetworkException(-6501);
			}
			if (offset + count > this.m_packet.m_totalLength)
			{
				throw new NetworkException(-6502);
			}
			OraArraySegment newOAS = this.GetNewOAS(array, offset, count);
			this.m_cursor = offset + count;
			this.AddIt(newOAS);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00092F40 File Offset: 0x00091140
		internal void Add(int size)
		{
			this.Add(this.m_buf, this.m_cursor, size);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00092F58 File Offset: 0x00091158
		internal void InitForSend()
		{
			DataPacket.InitForSend(this.m_hdrbuf, this.m_curlen, this.m_OC.m_sessionCtx);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00092F78 File Offset: 0x00091178
		internal void ReturnToPool()
		{
			if (this.m_secondary != null)
			{
				this.m_OC.OraBufPool.Put(this.m_secondary.size, this.m_secondary);
				this.m_secondary = null;
			}
			this.m_OC.OraBufPool.Put(this.size, this);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00092FCC File Offset: 0x000911CC
		internal void Clear()
		{
			this.m_ASegs_cursor = 0;
			this.m_length = 0;
			this.m_cursor = 0;
			this.m_curlen = 0;
			this.m_hdrbuf = this.m_buf;
			if (this.m_secondary != null)
			{
				this.m_secondary.Clear();
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0009300C File Offset: 0x0009120C
		internal void ReInit(bool forReceive)
		{
			this.Clear();
			if (!forReceive)
			{
				this.Add(10);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00093020 File Offset: 0x00091220
		internal int the_ByteSegments_Count
		{
			get
			{
				return this.m_ASegs_cursor;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00093028 File Offset: 0x00091228
		internal OraArraySegment[] the_ByteSegments
		{
			get
			{
				return this.m_ASegs;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00093030 File Offset: 0x00091230
		internal int size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00093038 File Offset: 0x00091238
		internal byte[] buf
		{
			get
			{
				return this.m_buf;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00093040 File Offset: 0x00091240
		internal int cursor
		{
			get
			{
				return this.m_cursor;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00093048 File Offset: 0x00091248
		internal int Space
		{
			get
			{
				return this.m_packet.m_totalLength - this.m_cursor;
			}
		}

		// Token: 0x04000F4C RID: 3916
		internal const int RecvSzMultiplier = 4;

		// Token: 0x04000F4D RID: 3917
		internal static int OAS_StartSize = 2;

		// Token: 0x04000F4E RID: 3918
		internal byte[] m_buf;

		// Token: 0x04000F4F RID: 3919
		internal int m_size;

		// Token: 0x04000F50 RID: 3920
		private int m_cursor;

		// Token: 0x04000F51 RID: 3921
		internal int m_curlen;

		// Token: 0x04000F52 RID: 3922
		internal int m_length;

		// Token: 0x04000F53 RID: 3923
		private int m_ASegs_cursor;

		// Token: 0x04000F54 RID: 3924
		private OraArraySegment[] m_ASegs = new OraArraySegment[OraBuf.OAS_StartSize];

		// Token: 0x04000F55 RID: 3925
		private OracleCommunication m_OC;

		// Token: 0x04000F56 RID: 3926
		internal DataPacket m_packet;

		// Token: 0x04000F57 RID: 3927
		private OraBuf m_secondary;

		// Token: 0x04000F58 RID: 3928
		private byte[] m_hdrbuf;

		// Token: 0x04000F59 RID: 3929
		private static object m_sync = new object();

		// Token: 0x04000F5A RID: 3930
		private static int m_sId;

		// Token: 0x04000F5B RID: 3931
		internal int m_id;

		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x06000DDF RID: 3551
		internal delegate void AsyncReceiveCallback(OraBuf OB, int length);
	}
}
