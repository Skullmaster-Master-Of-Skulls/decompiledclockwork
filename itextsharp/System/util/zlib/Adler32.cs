using System;

namespace System.util.zlib
{
	// Token: 0x020003C1 RID: 961
	internal sealed class Adler32
	{
		// Token: 0x06002183 RID: 8579 RVA: 0x000CA038 File Offset: 0x000C9038
		internal long adler32(long adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1L;
			}
			long num = adler & 65535L;
			long num2 = adler >> 16 & 65535L;
			while (len > 0)
			{
				int i = (len < 5552) ? len : 5552;
				len -= i;
				while (i >= 16)
				{
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					num += (long)(buf[index++] & byte.MaxValue);
					num2 += num;
					i -= 16;
				}
				if (i != 0)
				{
					do
					{
						num += (long)(buf[index++] & byte.MaxValue);
						num2 += num;
					}
					while (--i != 0);
				}
				num %= 65521L;
				num2 %= 65521L;
			}
			return num2 << 16 | num;
		}

		// Token: 0x04001713 RID: 5907
		private const int BASE = 65521;

		// Token: 0x04001714 RID: 5908
		private const int NMAX = 5552;
	}
}
