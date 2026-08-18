using System;

namespace ICSharpCode.SharpZipLib.Checksums
{
	// Token: 0x02000006 RID: 6
	public sealed class Adler32 : IChecksum
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002604 File Offset: 0x00001604
		public long Value
		{
			get
			{
				return (long)((ulong)this.checksum);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000260D File Offset: 0x0000160D
		public Adler32()
		{
			this.Reset();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000261B File Offset: 0x0000161B
		public void Reset()
		{
			this.checksum = 1U;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002624 File Offset: 0x00001624
		public void Update(int value)
		{
			uint num = this.checksum & 65535U;
			uint num2 = this.checksum >> 16;
			num = (num + (uint)(value & 255)) % 65521U;
			num2 = (num + num2) % 65521U;
			this.checksum = (num2 << 16) + num;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000266E File Offset: 0x0000166E
		public void Update(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.Update(buffer, 0, buffer.Length);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000268C File Offset: 0x0000168C
		public void Update(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "cannot be negative");
			}
			if (offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset", "not a valid index into buffer");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", "exceeds buffer size");
			}
			uint num = this.checksum & 65535U;
			uint num2 = this.checksum >> 16;
			while (count > 0)
			{
				int num3 = 3800;
				if (num3 > count)
				{
					num3 = count;
				}
				count -= num3;
				while (--num3 >= 0)
				{
					num += (uint)(buffer[offset++] & byte.MaxValue);
					num2 += num;
				}
				num %= 65521U;
				num2 %= 65521U;
			}
			this.checksum = (num2 << 16 | num);
		}

		// Token: 0x04000015 RID: 21
		private const uint BASE = 65521U;

		// Token: 0x04000016 RID: 22
		private uint checksum;
	}
}
