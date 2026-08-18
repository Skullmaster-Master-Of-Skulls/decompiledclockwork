using System;

namespace TechnoPro.Common.DAO.FileSign.Impl
{
	// Token: 0x02000004 RID: 4
	public interface IFileSignDAO
	{
		// Token: 0x06000009 RID: 9
		byte[] DecryptAndVerify(byte[] EncryptedFile);

		// Token: 0x0600000A RID: 10
		byte[] EncryptAndSign(byte[] DecryptedFile, string TechnoProPrivateKey, string TechnoProPassword, string ClockWorkPublicKey);

		// Token: 0x0600000B RID: 11
		void DecryptAndVerifyUsingFileSystem(string EncryptedFileName, string OutputDecryptedFileName);

		// Token: 0x0600000C RID: 12
		void EncryptAndVerifyUsingFileSystem(string ClockWorkPublicKey, string TechnoProPrivateKey, string TechnoProPassword, string DecryptedFileName, string OutputEncryptedFileName);
	}
}
