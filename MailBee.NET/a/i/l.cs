using System;
using System.Text;
using MailBee.Mime;

namespace a.i
{
	// Token: 0x020001F7 RID: 503
	internal class l
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x00044856 File Offset: 0x00043856
		private l()
		{
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00044860 File Offset: 0x00043860
		public static string a(string A_0, StringConversionMode A_1, Encoding A_2, Encoding A_3, Encoding A_4)
		{
			if (A_1 == StringConversionMode.KeepOriginalByteEncoding)
			{
				byte[] bytes = A_2.GetBytes(A_0);
				return A_4.GetString(bytes, 0, bytes.Length);
			}
			if (A_1 == StringConversionMode.ConvertToWinByteEncoding)
			{
				byte[] bytes2 = A_2.GetBytes(A_0);
				byte[] array = Encoding.Convert(A_2, Encoding.GetEncoding(A_2.WindowsCodePage), bytes2);
				return A_4.GetString(array, 0, array.Length);
			}
			if (A_1 == StringConversionMode.ConvertToDestinationEncoding)
			{
				byte[] bytes3 = A_2.GetBytes(A_0);
				byte[] array2 = Encoding.Convert(A_2, A_3, bytes3);
				return A_4.GetString(array2, 0, array2.Length);
			}
			return A_0;
		}
	}
}
