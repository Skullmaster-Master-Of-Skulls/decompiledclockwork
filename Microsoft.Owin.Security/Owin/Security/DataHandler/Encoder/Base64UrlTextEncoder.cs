using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x0200002D RID: 45
	public class Base64UrlTextEncoder : ITextEncoder
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004360 File Offset: 0x00002560
		public string Encode(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return Convert.ToBase64String(data).TrimEnd(new char[]
			{
				'='
			}).Replace('+', '-').Replace('/', '_');
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000043A5 File Offset: 0x000025A5
		public byte[] Decode(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return Convert.FromBase64String(Base64UrlTextEncoder.Pad(text.Replace('-', '+').Replace('_', '/')));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000043D4 File Offset: 0x000025D4
		private static string Pad(string text)
		{
			int num = 3 - (text.Length + 3) % 4;
			if (num == 0)
			{
				return text;
			}
			return text + new string('=', num);
		}
	}
}
