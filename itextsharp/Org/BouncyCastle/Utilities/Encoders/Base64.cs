using System;
using System.IO;
using System.Text;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x0200022E RID: 558
	public sealed class Base64
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x0007DCE0 File Offset: 0x0007CCE0
		private Base64()
		{
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0007DCE8 File Offset: 0x0007CCE8
		public static byte[] Encode(byte[] data)
		{
			string s = Convert.ToBase64String(data, 0, data.Length);
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0007DD0C File Offset: 0x0007CD0C
		public static int Encode(byte[] data, Stream outStream)
		{
			string s = Convert.ToBase64String(data, 0, data.Length);
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			outStream.Write(bytes, 0, bytes.Length);
			return bytes.Length;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0007DD40 File Offset: 0x0007CD40
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			string s = Convert.ToBase64String(data, off, length);
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			outStream.Write(bytes, 0, bytes.Length);
			return bytes.Length;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0007DD70 File Offset: 0x0007CD70
		public static byte[] Decode(byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data, 0, data.Length);
			return Convert.FromBase64String(@string);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0007DD93 File Offset: 0x0007CD93
		public static byte[] Decode(string data)
		{
			return Convert.FromBase64String(data);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0007DD9C File Offset: 0x0007CD9C
		public static int Decode(string data, Stream outStream)
		{
			byte[] array = Base64.Decode(data);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}
	}
}
