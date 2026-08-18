using System;
using System.Text;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x0200006B RID: 107
	public sealed class Strings
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0001379C File Offset: 0x0001279C
		private Strings()
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000137A4 File Offset: 0x000127A4
		public static string FromByteArray(byte[] bs)
		{
			char[] array = new char[bs.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToChar(bs[i]);
			}
			return new string(array);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000137DC File Offset: 0x000127DC
		public static byte[] ToByteArray(string s)
		{
			byte[] array = new byte[s.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(s[i]);
			}
			return array;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013813 File Offset: 0x00012813
		public static string FromUtf8ByteArray(byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00013824 File Offset: 0x00012824
		public static byte[] ToUtf8ByteArray(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00013831 File Offset: 0x00012831
		public static byte[] ToUtf8ByteArray(char[] cs)
		{
			return Encoding.UTF8.GetBytes(cs);
		}
	}
}
