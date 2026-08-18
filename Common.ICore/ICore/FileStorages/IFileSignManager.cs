using System;

namespace TechnoPro.Common.ICore.FileStorages
{
	// Token: 0x0200008D RID: 141
	public interface IFileSignManager
	{
		// Token: 0x060003F0 RID: 1008
		byte[] DecryptAndVerify(byte[] EncryptedFile);

		// Token: 0x060003F1 RID: 1009
		byte[] EncryptAndSign(byte[] DecryptedFile, string TechnoProPrivateKey, string TechnoProPassword, string ClockWorkPublicKey);

		// Token: 0x060003F2 RID: 1010
		void DecryptAndVerifyUsingFileSystem(string EncryptedFileName, string OutputDecryptedFileName);

		// Token: 0x060003F3 RID: 1011
		void EncryptAndVerifyUsingFileSystem(string ClockWorkPublicKey, string TechnoProPrivateKey, string TechnoProPassword, string DecryptedFileName, string OutputEncryptedFileName);

		// Token: 0x060003F4 RID: 1012
		bool VerifySign(byte[] encryptedFile);
	}
}
