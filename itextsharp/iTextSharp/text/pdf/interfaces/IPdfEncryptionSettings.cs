using System;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x02000058 RID: 88
	public interface IPdfEncryptionSettings
	{
		// Token: 0x0600029E RID: 670
		void SetEncryption(byte[] userPassword, byte[] ownerPassword, int permissions, int encryptionType);

		// Token: 0x0600029F RID: 671
		void SetEncryption(X509Certificate[] certs, int[] permissions, int encryptionType);
	}
}
