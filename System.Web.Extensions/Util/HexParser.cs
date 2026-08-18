using System;
using System.Globalization;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x02000034 RID: 52
	internal static class HexParser
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public static byte[] Parse(string token)
		{
			byte[] array = new byte[token.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.Parse(token.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000D128 File Offset: 0x0000B328
		public static string ToString(byte[] tokenBytes)
		{
			StringBuilder stringBuilder = new StringBuilder(tokenBytes.Length * 2);
			for (int i = 0; i < tokenBytes.Length; i++)
			{
				stringBuilder.Append(tokenBytes[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}
	}
}
