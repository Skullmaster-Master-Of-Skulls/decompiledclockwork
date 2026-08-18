using System;
using System.IO;
using System.IO.Compression;

namespace ClockWorkWebAPI
{
	// Token: 0x02000010 RID: 16
	public class Compression
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public static byte[] Compress(byte[] buffer)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gzipStream.Write(buffer, 0, buffer.Length);
			}
			memoryStream.Position = 0L;
			MemoryStream memoryStream2 = new MemoryStream();
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, array.Length);
			byte[] array2 = new byte[array.Length + 4];
			Buffer.BlockCopy(array, 0, array2, 4, array.Length);
			Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, array2, 0, 4);
			return array2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006E4C File Offset: 0x0000504C
		public static byte[] Decompress(byte[] gzBuffer)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = BitConverter.ToInt32(gzBuffer, 0);
				memoryStream.Write(gzBuffer, 4, gzBuffer.Length - 4);
				byte[] array = new byte[num];
				memoryStream.Position = 0L;
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					gzipStream.Read(array, 0, array.Length);
				}
				result = array;
			}
			return result;
		}
	}
}
