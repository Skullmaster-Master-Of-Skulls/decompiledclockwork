using System;
using System.IO;
using System.IO.Compression;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E1 RID: 2529
	internal static class MessageEncoderCompressionHandler
	{
		// Token: 0x060063DF RID: 25567 RVA: 0x00174E2C File Offset: 0x0017302C
		internal static void DecompressBuffer(ref ArraySegment<byte> buffer, BufferManager bufferManager, CompressionFormat compressionFormat, long maxReceivedMessageSize)
		{
			MemoryStream stream = new MemoryStream(buffer.Array, buffer.Offset, buffer.Count);
			int maxSize = (int)Math.Min(maxReceivedMessageSize, 2147483647L);
			using (BufferManagerOutputStream bufferManagerOutputStream = new BufferManagerOutputStream("MaxReceivedMessageSizeExceeded", 1024, maxSize, bufferManager))
			{
				bufferManagerOutputStream.Write(buffer.Array, 0, buffer.Offset);
				byte[] buffer2 = bufferManager.TakeBuffer(1024);
				try
				{
					using (Stream stream2 = (compressionFormat == CompressionFormat.GZip) ? new GZipStream(stream, CompressionMode.Decompress) : new DeflateStream(stream, CompressionMode.Decompress))
					{
						for (;;)
						{
							int num = stream2.Read(buffer2, 0, 1024);
							if (num <= 0)
							{
								break;
							}
							bufferManagerOutputStream.Write(buffer2, 0, num);
						}
					}
				}
				finally
				{
					bufferManager.ReturnBuffer(buffer2);
				}
				int num2 = 0;
				byte[] array = bufferManagerOutputStream.ToArray(out num2);
				bufferManager.ReturnBuffer(buffer.Array);
				buffer = new ArraySegment<byte>(array, buffer.Offset, num2 - buffer.Offset);
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x00174F48 File Offset: 0x00173148
		internal static void CompressBuffer(ref ArraySegment<byte> buffer, BufferManager bufferManager, CompressionFormat compressionFormat)
		{
			using (BufferManagerOutputStream bufferManagerOutputStream = new BufferManagerOutputStream("MaxSentMessageSizeExceeded", 1024, int.MaxValue, bufferManager))
			{
				bufferManagerOutputStream.Write(buffer.Array, 0, buffer.Offset);
				using (Stream stream = (compressionFormat == CompressionFormat.GZip) ? new GZipStream(bufferManagerOutputStream, CompressionMode.Compress, true) : new DeflateStream(bufferManagerOutputStream, CompressionMode.Compress, true))
				{
					stream.Write(buffer.Array, buffer.Offset, buffer.Count);
				}
				int num = 0;
				byte[] array = bufferManagerOutputStream.ToArray(out num);
				bufferManager.ReturnBuffer(buffer.Array);
				buffer = new ArraySegment<byte>(array, buffer.Offset, num - buffer.Offset);
			}
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x00175014 File Offset: 0x00173214
		internal static Stream GetDecompressStream(Stream compressedStream, CompressionFormat compressionFormat)
		{
			if (compressionFormat != CompressionFormat.GZip)
			{
				return new DeflateStream(compressedStream, CompressionMode.Decompress, false);
			}
			return new GZipStream(compressedStream, CompressionMode.Decompress, false);
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x0017502B File Offset: 0x0017322B
		internal static Stream GetCompressStream(Stream uncompressedStream, CompressionFormat compressionFormat)
		{
			if (compressionFormat != CompressionFormat.GZip)
			{
				return new DeflateStream(uncompressedStream, CompressionMode.Compress, true);
			}
			return new GZipStream(uncompressedStream, CompressionMode.Compress, true);
		}

		// Token: 0x0400399C RID: 14748
		internal const string GZipContentEncoding = "gzip";

		// Token: 0x0400399D RID: 14749
		internal const string DeflateContentEncoding = "deflate";

		// Token: 0x0400399E RID: 14750
		private const int DecompressBlockSize = 1024;
	}
}
