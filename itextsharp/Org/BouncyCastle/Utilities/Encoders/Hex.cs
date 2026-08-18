using System;
using System.IO;
using System.Text;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020003C9 RID: 969
	public sealed class Hex
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x000CCD99 File Offset: 0x000CBD99
		private Hex()
		{
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000CCDA4 File Offset: 0x000CBDA4
		public static string ToHexString(byte[] data)
		{
			byte[] array = Hex.Encode(data, 0, data.Length);
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000CCDCC File Offset: 0x000CBDCC
		public static string ToHexString(byte[] data, int off, int length)
		{
			byte[] array = Hex.Encode(data, off, length);
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000CCDF1 File Offset: 0x000CBDF1
		public static byte[] Encode(byte[] data)
		{
			return Hex.Encode(data, 0, data.Length);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000CCE00 File Offset: 0x000CBE00
		public static byte[] Encode(byte[] data, int off, int length)
		{
			MemoryStream memoryStream = new MemoryStream(length * 2);
			Hex.encoder.Encode(data, off, length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000CCE2B File Offset: 0x000CBE2B
		public static int Encode(byte[] data, Stream outStream)
		{
			return Hex.encoder.Encode(data, 0, data.Length, outStream);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000CCE3D File Offset: 0x000CBE3D
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			return Hex.encoder.Encode(data, off, length, outStream);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000CCE50 File Offset: 0x000CBE50
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.Decode(data, 0, data.Length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000CCE84 File Offset: 0x000CBE84
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.DecodeString(data, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000CCEB4 File Offset: 0x000CBEB4
		public static int Decode(string data, Stream outStream)
		{
			return Hex.encoder.DecodeString(data, outStream);
		}

		// Token: 0x04001744 RID: 5956
		private static readonly IEncoder encoder = new HexEncoder();
	}
}
