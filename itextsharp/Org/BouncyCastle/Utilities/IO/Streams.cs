using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020004B0 RID: 1200
	public sealed class Streams
	{
		// Token: 0x0600288B RID: 10379 RVA: 0x000F63A3 File Offset: 0x000F53A3
		private Streams()
		{
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000F63AC File Offset: 0x000F53AC
		public static void Drain(Stream inStr)
		{
			byte[] array = new byte[512];
			while (inStr.Read(array, 0, array.Length) > 0)
			{
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000F63D4 File Offset: 0x000F53D4
		public static byte[] ReadAll(Stream inStr)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAll(inStr, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000F63F4 File Offset: 0x000F53F4
		public static byte[] ReadAllLimited(Stream inStr, int limit)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAllLimited(inStr, (long)limit, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000F6417 File Offset: 0x000F5417
		public static int ReadFully(Stream inStr, byte[] buf)
		{
			return Streams.ReadFully(inStr, buf, 0, buf.Length);
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000F6424 File Offset: 0x000F5424
		public static int ReadFully(Stream inStr, byte[] buf, int off, int len)
		{
			int i;
			int num;
			for (i = 0; i < len; i += num)
			{
				num = inStr.Read(buf, off + i, len - i);
				if (num < 1)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000F6450 File Offset: 0x000F5450
		public static void PipeAll(Stream inStr, Stream outStr)
		{
			byte[] array = new byte[512];
			int count;
			while ((count = inStr.Read(array, 0, array.Length)) > 0)
			{
				outStr.Write(array, 0, count);
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000F6484 File Offset: 0x000F5484
		public static long PipeAllLimited(Stream inStr, long limit, Stream outStr)
		{
			byte[] array = new byte[512];
			long num = 0L;
			int num2;
			while ((num2 = inStr.Read(array, 0, array.Length)) > 0)
			{
				num += (long)num2;
				if (num > limit)
				{
					throw new StreamOverflowException("Data Overflow");
				}
				outStr.Write(array, 0, num2);
			}
			return num;
		}

		// Token: 0x04001CAF RID: 7343
		private const int BufferSize = 512;
	}
}
