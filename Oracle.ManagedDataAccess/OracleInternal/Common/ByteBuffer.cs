using System;

namespace OracleInternal.Common
{
	// Token: 0x02000081 RID: 129
	internal class ByteBuffer
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0003A124 File Offset: 0x00038324
		internal ByteBuffer()
		{
			this.m_byteBuffer = null;
			this.m_limit = (this.m_capacity = (this.m_position = 0));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0003A158 File Offset: 0x00038358
		internal ByteBuffer(int capacity)
		{
			this.m_byteBuffer = new byte[capacity];
			this.m_capacity = capacity;
			this.m_limit = (this.m_position = 0);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0003A190 File Offset: 0x00038390
		internal bool HasRemaining
		{
			get
			{
				return this.m_limit > this.m_position;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0003A1A4 File Offset: 0x000383A4
		internal int Remaining
		{
			get
			{
				return this.m_limit - this.m_position;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0003A1B4 File Offset: 0x000383B4
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0003A1BC File Offset: 0x000383BC
		internal int Limit
		{
			get
			{
				return this.m_limit;
			}
			set
			{
				this.m_limit = value;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0003A1C8 File Offset: 0x000383C8
		internal void GetBufferRef(out byte[] userBuff, out int offset, int length)
		{
			if (length <= this.Remaining)
			{
				userBuff = this.m_byteBuffer;
				offset = this.m_position;
				this.m_position += length;
				return;
			}
			throw new ArgumentOutOfRangeException("length");
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0003A1FC File Offset: 0x000383FC
		internal void GetBuffer(byte[] userBuff, int offset, int length)
		{
			if (length <= this.Remaining)
			{
				Array.Copy(this.m_byteBuffer, this.m_position, userBuff, offset, length);
				this.m_position += length;
				return;
			}
			throw new ArgumentOutOfRangeException("length");
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0003A234 File Offset: 0x00038434
		internal byte GetByte()
		{
			return this.m_byteBuffer[this.m_position++];
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0003A25C File Offset: 0x0003845C
		internal short GetShort()
		{
			int num = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			int num2 = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			return (short)((num << 8 | num2) & 65535);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003A2BC File Offset: 0x000384BC
		internal int GetInt()
		{
			int num = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			int num2 = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			int num3 = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			int num4 = (int)(this.m_byteBuffer[this.m_position++] & byte.MaxValue);
			return num << 24 | num2 << 16 | num3 << 8 | num4;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0003A364 File Offset: 0x00038564
		internal long GetLong()
		{
			long num = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num2 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num3 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num4 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num5 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num6 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num7 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			long num8 = (long)((ulong)this.m_byteBuffer[this.m_position++] & 255UL);
			return num << 56 | num2 << 48 | num3 << 40 | num4 << 32 | num5 << 24 | num6 << 16 | num7 << 8 | num8;
		}

		// Token: 0x0400078C RID: 1932
		internal byte[] m_byteBuffer;

		// Token: 0x0400078D RID: 1933
		internal int m_capacity;

		// Token: 0x0400078E RID: 1934
		internal int m_position;

		// Token: 0x0400078F RID: 1935
		internal int m_limit;
	}
}
