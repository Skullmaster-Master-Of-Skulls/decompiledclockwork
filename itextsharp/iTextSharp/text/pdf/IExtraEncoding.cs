using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200049F RID: 1183
	public interface IExtraEncoding
	{
		// Token: 0x0600281E RID: 10270
		byte[] CharToByte(string text, string encoding);

		// Token: 0x0600281F RID: 10271
		byte[] CharToByte(char char1, string encoding);

		// Token: 0x06002820 RID: 10272
		string ByteToChar(byte[] b, string encoding);
	}
}
