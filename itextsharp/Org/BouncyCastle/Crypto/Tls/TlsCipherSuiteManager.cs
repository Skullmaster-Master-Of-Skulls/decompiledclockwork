using System;
using System.IO;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020005A4 RID: 1444
	public class TlsCipherSuiteManager
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x00136DD4 File Offset: 0x00135DD4
		internal static void WriteCipherSuites(Stream outStr)
		{
			int[] array = new int[]
			{
				57,
				56,
				51,
				50,
				22,
				19,
				53,
				47,
				10
			};
			TlsUtilities.WriteUint16(2 * array.Length, outStr);
			for (int i = 0; i < array.Length; i++)
			{
				TlsUtilities.WriteUint16(array[i], outStr);
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x00136E18 File Offset: 0x00135E18
		internal static TlsCipherSuite GetCipherSuite(int number, TlsProtocolHandler handler)
		{
			if (number <= 19)
			{
				if (number == 10)
				{
					return TlsCipherSuiteManager.createDesEdeCipherSuite(24, 1);
				}
				if (number == 19)
				{
					return TlsCipherSuiteManager.createDesEdeCipherSuite(24, 3);
				}
			}
			else
			{
				if (number == 22)
				{
					return TlsCipherSuiteManager.createDesEdeCipherSuite(24, 5);
				}
				switch (number)
				{
				case 47:
					return TlsCipherSuiteManager.createAesCipherSuite(16, 1);
				case 50:
					return TlsCipherSuiteManager.createAesCipherSuite(16, 3);
				case 51:
					return TlsCipherSuiteManager.createAesCipherSuite(16, 5);
				case 53:
					return TlsCipherSuiteManager.createAesCipherSuite(32, 1);
				case 56:
					return TlsCipherSuiteManager.createAesCipherSuite(32, 3);
				case 57:
					return TlsCipherSuiteManager.createAesCipherSuite(32, 5);
				}
			}
			handler.FailWithError(2, 40);
			return null;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00136ED2 File Offset: 0x00135ED2
		private static TlsCipherSuite createAesCipherSuite(int cipherKeySize, short keyExchange)
		{
			return new TlsBlockCipherCipherSuite(TlsCipherSuiteManager.createAesCipher(), TlsCipherSuiteManager.createAesCipher(), new Sha1Digest(), new Sha1Digest(), cipherKeySize, keyExchange);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00136EEF File Offset: 0x00135EEF
		private static TlsCipherSuite createDesEdeCipherSuite(int cipherKeySize, short keyExchange)
		{
			return new TlsBlockCipherCipherSuite(TlsCipherSuiteManager.createDesEdeCipher(), TlsCipherSuiteManager.createDesEdeCipher(), new Sha1Digest(), new Sha1Digest(), cipherKeySize, keyExchange);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00136F0C File Offset: 0x00135F0C
		private static CbcBlockCipher createAesCipher()
		{
			return new CbcBlockCipher(new AesFastEngine());
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00136F18 File Offset: 0x00135F18
		private static CbcBlockCipher createDesEdeCipher()
		{
			return new CbcBlockCipher(new DesEdeEngine());
		}

		// Token: 0x0400223F RID: 8767
		private const int TLS_RSA_WITH_3DES_EDE_CBC_SHA = 10;

		// Token: 0x04002240 RID: 8768
		private const int TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA = 19;

		// Token: 0x04002241 RID: 8769
		private const int TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA = 22;

		// Token: 0x04002242 RID: 8770
		private const int TLS_RSA_WITH_AES_128_CBC_SHA = 47;

		// Token: 0x04002243 RID: 8771
		private const int TLS_DHE_DSS_WITH_AES_128_CBC_SHA = 50;

		// Token: 0x04002244 RID: 8772
		private const int TLS_DHE_RSA_WITH_AES_128_CBC_SHA = 51;

		// Token: 0x04002245 RID: 8773
		private const int TLS_RSA_WITH_AES_256_CBC_SHA = 53;

		// Token: 0x04002246 RID: 8774
		private const int TLS_DHE_DSS_WITH_AES_256_CBC_SHA = 56;

		// Token: 0x04002247 RID: 8775
		private const int TLS_DHE_RSA_WITH_AES_256_CBC_SHA = 57;
	}
}
