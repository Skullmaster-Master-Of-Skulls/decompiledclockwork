using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000167 RID: 359
	public interface ITSAClient
	{
		// Token: 0x06000DAD RID: 3501
		int GetTokenSizeEstimate();

		// Token: 0x06000DAE RID: 3502
		byte[] GetTimeStampToken(PdfPKCS7 caller, byte[] imprint);
	}
}
