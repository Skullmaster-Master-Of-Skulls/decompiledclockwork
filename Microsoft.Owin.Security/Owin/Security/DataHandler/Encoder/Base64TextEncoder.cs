using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x0200002C RID: 44
	public class Base64TextEncoder : ITextEncoder
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004345 File Offset: 0x00002545
		public string Encode(byte[] data)
		{
			return Convert.ToBase64String(data);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000434D File Offset: 0x0000254D
		public byte[] Decode(string text)
		{
			return Convert.FromBase64String(text);
		}
	}
}
