using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x0200002B RID: 43
	public interface ITextEncoder
	{
		// Token: 0x060000B2 RID: 178
		string Encode(byte[] data);

		// Token: 0x060000B3 RID: 179
		byte[] Decode(string text);
	}
}
