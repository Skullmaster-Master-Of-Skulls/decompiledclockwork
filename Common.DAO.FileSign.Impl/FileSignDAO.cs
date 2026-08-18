using System;
using TechnoPro.Common.DAO.FileSign.Impl.PGP;

namespace TechnoPro.Common.DAO.FileSign.Impl
{
	// Token: 0x02000003 RID: 3
	public class FileSignDAO : IFileSignDAO
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002094 File Offset: 0x00000294
		public byte[] DecryptAndVerify(byte[] EncryptedFile)
		{
			return PgpDecrypt.DecryptAndVerify(this.technopro_pubKey, this.clockwork_privKey, this.clockwork_password, EncryptedFile);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020AE File Offset: 0x000002AE
		public byte[] EncryptAndSign(byte[] DecryptedFile, string TechnoProPrivateKey, string TechnoProPassword, string ClockWorkPublicKey)
		{
			return PgpEncrypt.EncryptAndSign(ClockWorkPublicKey, TechnoProPrivateKey, TechnoProPassword, DecryptedFile);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020BA File Offset: 0x000002BA
		public void DecryptAndVerifyUsingFileSystem(string EncryptedFileName, string OutputDecryptedFileName)
		{
			PgpDecrypt.DecryptAndVerify(this.technopro_pubKey, this.clockwork_privKey, this.clockwork_password, EncryptedFileName, OutputDecryptedFileName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D5 File Offset: 0x000002D5
		public void EncryptAndVerifyUsingFileSystem(string ClockWorkPublicKey, string TechnoProPrivateKey, string TechnoProPassword, string DecryptedFileName, string OutputEncryptedFileName)
		{
			PgpEncrypt.EncryptAndSign(ClockWorkPublicKey, TechnoProPrivateKey, TechnoProPassword, OutputEncryptedFileName, DecryptedFileName);
		}

		// Token: 0x04000001 RID: 1
		private string technopro_pubKey = "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG C# v1.6.1.0\r\n\r\nmI0ETyFJIwEEAKKXyGIhilcGRYb26ey7iUxuBJ99mFwg5evQ7HNn1zgvAwgJP+sb\r\n1BUHGj3QgAE7tmZ5dUM/JtYXuZ/vQN5BSjzHXz0YBhtJNAbRXTxjGCvH6zntP30+\r\npZsCF0GY9J5DtID3dn4Wpm9CErmi4s4nmoxigfMyNbFqv1e+ZIPJPdbtABEBAAG0\r\nAIicBBABAgAGBQJPIUkjAAoJELOF7KJxSZ1Xaz8EAJzjkSRGjewEw7LQ77+hOiGf\r\nf0Tt4UoQr73bj+Y9bzB8/VrO9jL89piuLC8lhNmY4gi7cBNFag/KV0eDbGSqZS2+\r\noUSXpHrrqBaWJbPUYgdC/OytQpHIRUoqmvC3eopMcjB+wC6sYfCIk8dCcZ+DWt1O\r\nzzrFeNRGA96AnuxVmlCQ\r\n=y5Gl\r\n-----END PGP PUBLIC KEY BLOCK-----";

		// Token: 0x04000002 RID: 2
		private string clockwork_privKey = "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: BCPG C# v1.6.1.0\r\n\r\nlQHsBE8hST8BBACWx47QmDa6w/TR+Md96ExckdgjLsPFnrzRSJ/FnSg6LdmeaiYP\r\nXn9TH8r4ve93k7+dHHw7XM6dOl3dV5WMRS1KbO/LADagB18v+2MGTCD2jka4yq7c\r\nbirkNpTzXHDdbuZbs6qlVPrAm3AI5Qkhjw5X8XpWS4gLugTn8HVu7wnoeQARAQAB\r\n/wMDAvWdln5WKPpLYA7imPmGB6NDMFJfRTGy77qL2QTF+oGBfDMX7xXULhpjIfG4\r\nripVoYW5tCwEOdwM6W2+6V+zmO6GZ3KYNEE2CZIGiWcD4K9xex80M5S8ARV7OfNo\r\n+CtfqZN4Hg2wyB9qjkWXWpiCMjuUY35lRbiTcAZttZYGqAf1QtzrYVqWVnkbrTSY\r\nPB75wiBxpaTB1LTR3aFyAQcc5hg8KIj0jGRH9YE063ipCTMQpymqZPXOQ+zyeXE9\r\ndLzOIa2UEymY/SdZ876byRMmWxD2ImGyvf9ssfPIok/p93k/ffJGMyfJBMeWQbA0\r\n08EcY2IlzIOnNpGdtbcyAQOORnALQGzJSgzeuJW5I6VnE8qSLy5X+JDjnxEXY2Hq\r\nsuEqGcaz5w/P7c4hT6WiaXE05P8GxV6OtjIBVrDOHJwnEAEpLTsgavRs7z1pewLf\r\nDhBmE7pm3DeM4ep8CwZktACInAQQAQIABgUCTyFJPwAKCRB78kIY+kOoHQCrA/90\r\nrkuZsbxtmaGxxdCawLSKaIuInmcQu2kTcR/XJnsT5cSb+Lj31g1TbUKLYvHS3T+c\r\nEwXiXiCcalUfa5Sq9iImDJypKcy46Bc3Xnevwr1PZugeBZD/dN62EtNe3ecRzI+Z\r\nJ2SLRl0CCXGny9/Uzrw/Uc4gAPSAWM6nf0QQd8IMiQ==\r\n=LZV+\r\n-----END PGP PRIVATE KEY BLOCK-----";

		// Token: 0x04000003 RID: 3
		private string clockwork_password = "428b5223b8eeb6b02adc06082eaef26dc0d31c1803c7574c413fb7665121a147";
	}
}
