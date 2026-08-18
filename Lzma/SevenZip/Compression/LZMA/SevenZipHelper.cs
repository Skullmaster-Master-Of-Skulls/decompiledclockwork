using System;
using System.IO;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x0200001D RID: 29
	public static class SevenZipHelper
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00006118 File Offset: 0x00004318
		public static byte[] Compress(byte[] inputBytes)
		{
			MemoryStream memoryStream = new MemoryStream(inputBytes);
			MemoryStream memoryStream2 = new MemoryStream();
			Encoder encoder = new Encoder();
			encoder.SetCoderProperties(SevenZipHelper.propIDs, SevenZipHelper.properties);
			encoder.WriteCoderProperties(memoryStream2);
			long length = memoryStream.Length;
			for (int i = 0; i < 8; i++)
			{
				memoryStream2.WriteByte((byte)(length >> 8 * i));
			}
			encoder.Code(memoryStream, memoryStream2, -1L, -1L, null);
			return memoryStream2.ToArray();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000618C File Offset: 0x0000438C
		public static byte[] Decompress(byte[] inputBytes)
		{
			MemoryStream memoryStream = new MemoryStream(inputBytes);
			Decoder decoder = new Decoder();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			MemoryStream memoryStream2 = new MemoryStream();
			byte[] array = new byte[5];
			if (memoryStream.Read(array, 0, 5) != 5)
			{
				throw new Exception("input .lzma is too short");
			}
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				int num2 = memoryStream.ReadByte();
				if (num2 < 0)
				{
					throw new Exception("Can't Read 1");
				}
				num |= (long)((long)((ulong)((byte)num2)) << 8 * i);
			}
			decoder.SetDecoderProperties(array);
			long inSize = memoryStream.Length - memoryStream.Position;
			decoder.Code(memoryStream, memoryStream2, inSize, num, null);
			return memoryStream2.ToArray();
		}

		// Token: 0x040000B3 RID: 179
		private static int dictionary = 8388608;

		// Token: 0x040000B4 RID: 180
		private static bool eos = false;

		// Token: 0x040000B5 RID: 181
		private static CoderPropID[] propIDs = new CoderPropID[]
		{
			CoderPropID.DictionarySize,
			CoderPropID.PosStateBits,
			CoderPropID.LitContextBits,
			CoderPropID.LitPosBits,
			CoderPropID.Algorithm,
			CoderPropID.NumFastBytes,
			CoderPropID.MatchFinder,
			CoderPropID.EndMarker
		};

		// Token: 0x040000B6 RID: 182
		private static object[] properties = new object[]
		{
			SevenZipHelper.dictionary,
			2,
			3,
			0,
			2,
			128,
			"bt4",
			SevenZipHelper.eos
		};
	}
}
