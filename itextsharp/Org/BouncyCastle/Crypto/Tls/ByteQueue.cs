using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020002A0 RID: 672
	public class ByteQueue
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x00093FA8 File Offset: 0x00092FA8
		public static int NextTwoPow(int i)
		{
			i |= i >> 1;
			i |= i >> 2;
			i |= i >> 4;
			i |= i >> 8;
			i |= i >> 16;
			return i + 1;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00093FD4 File Offset: 0x00092FD4
		public void Read(byte[] buf, int offset, int len, int skip)
		{
			if (this.available - skip < len)
			{
				throw new TlsException("Not enough data to read");
			}
			if (buf.Length - offset < len)
			{
				throw new TlsException(string.Concat(new object[]
				{
					"Buffer size of ",
					buf.Length,
					" is too small for a read of ",
					len,
					" bytes"
				}));
			}
			Array.Copy(this.databuf, this.skipped + skip, buf, offset, len);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00094058 File Offset: 0x00093058
		public void AddData(byte[] data, int offset, int len)
		{
			if (this.skipped + this.available + len > this.databuf.Length)
			{
				byte[] destinationArray = new byte[ByteQueue.NextTwoPow(data.Length)];
				Array.Copy(this.databuf, this.skipped, destinationArray, 0, this.available);
				this.skipped = 0;
				this.databuf = destinationArray;
			}
			Array.Copy(data, offset, this.databuf, this.skipped + this.available, len);
			this.available += len;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000940DC File Offset: 0x000930DC
		public void RemoveData(int i)
		{
			if (i > this.available)
			{
				throw new TlsException(string.Concat(new object[]
				{
					"Cannot remove ",
					i,
					" bytes, only got ",
					this.available
				}));
			}
			this.available -= i;
			this.skipped += i;
			if (this.skipped > this.databuf.Length / 2)
			{
				Array.Copy(this.databuf, this.skipped, this.databuf, 0, this.available);
				this.skipped = 0;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0009417F File Offset: 0x0009317F
		public int Available
		{
			get
			{
				return this.available;
			}
		}

		// Token: 0x040010FB RID: 4347
		private const int InitBufSize = 1024;

		// Token: 0x040010FC RID: 4348
		private byte[] databuf = new byte[1024];

		// Token: 0x040010FD RID: 4349
		private int skipped;

		// Token: 0x040010FE RID: 4350
		private int available;
	}
}
