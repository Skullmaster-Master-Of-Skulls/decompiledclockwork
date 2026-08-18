using System;
using System.IO;
using System.IO.Compression;

namespace ImportExportClassLibrary
{
	// Token: 0x02000021 RID: 33
	public class Compression
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00005450 File Offset: 0x00004450
		public static byte[] Compress(byte[] buffer)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gzipStream.Write(buffer, 0, buffer.Length);
			}
			memoryStream.Position = 0L;
			new MemoryStream();
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, array.Length);
			byte[] array2 = new byte[array.Length + 4];
			Buffer.BlockCopy(array, 0, array2, 4, array.Length);
			Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, array2, 0, 4);
			return array2;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000054E4 File Offset: 0x000044E4
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
