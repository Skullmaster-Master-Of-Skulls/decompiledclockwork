using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D3 RID: 2259
	internal static class Oids
	{
		// Token: 0x04002A5E RID: 10846
		internal static readonly byte[] Pkcs7Data = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			7,
			1
		};

		// Token: 0x04002A5F RID: 10847
		internal static readonly byte[] Pkcs7Encrypted = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			7,
			6
		};

		// Token: 0x04002A60 RID: 10848
		internal static readonly byte[] Pkcs12ShroudedKeyBag = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			12,
			10,
			1,
			2
		};

		// Token: 0x04002A61 RID: 10849
		internal static readonly byte[] PasswordBasedEncryptionScheme2 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			13
		};

		// Token: 0x04002A62 RID: 10850
		internal static readonly byte[] Pbkdf2 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			12
		};

		// Token: 0x04002A63 RID: 10851
		internal static readonly byte[] PbeWithMD5AndDESCBC = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			3
		};

		// Token: 0x04002A64 RID: 10852
		internal static readonly byte[] PbeWithMD5AndRC2CBC = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			6
		};

		// Token: 0x04002A65 RID: 10853
		internal static readonly byte[] PbeWithSha1AndDESCBC = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			10
		};

		// Token: 0x04002A66 RID: 10854
		internal static readonly byte[] PbeWithSha1AndRC2CBC = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			5,
			11
		};

		// Token: 0x04002A67 RID: 10855
		internal static readonly byte[] Pkcs12PbeWithShaAnd3Key3Des = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			12,
			1,
			3
		};

		// Token: 0x04002A68 RID: 10856
		internal static readonly byte[] Pkcs12PbeWithShaAnd2Key3Des = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			12,
			1,
			4
		};

		// Token: 0x04002A69 RID: 10857
		internal static readonly byte[] Pkcs12PbeWithShaAnd128BitRC2 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			12,
			1,
			5
		};

		// Token: 0x04002A6A RID: 10858
		internal static readonly byte[] Pkcs12PbeWithShaAnd40BitRC2 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			12,
			1,
			6
		};

		// Token: 0x04002A6B RID: 10859
		internal static readonly byte[] Aes128Cbc = new byte[]
		{
			96,
			134,
			72,
			1,
			101,
			3,
			4,
			1,
			2
		};

		// Token: 0x04002A6C RID: 10860
		internal static readonly byte[] Aes192Cbc = new byte[]
		{
			96,
			134,
			72,
			1,
			101,
			3,
			4,
			1,
			22
		};

		// Token: 0x04002A6D RID: 10861
		internal static readonly byte[] Aes256Cbc = new byte[]
		{
			96,
			134,
			72,
			1,
			101,
			3,
			4,
			1,
			42
		};

		// Token: 0x04002A6E RID: 10862
		internal static readonly byte[] TripleDesCbc = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			3,
			7
		};

		// Token: 0x04002A6F RID: 10863
		internal static readonly byte[] Rc2Cbc = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			3,
			2
		};

		// Token: 0x04002A70 RID: 10864
		internal static readonly byte[] DesCbc = new byte[]
		{
			43,
			14,
			3,
			2,
			7
		};

		// Token: 0x04002A71 RID: 10865
		internal static readonly byte[] HmacWithSha1 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			2,
			7
		};

		// Token: 0x04002A72 RID: 10866
		internal static readonly byte[] HmacWithSha256 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			2,
			9
		};

		// Token: 0x04002A73 RID: 10867
		internal static readonly byte[] HmacWithSha384 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			2,
			10
		};

		// Token: 0x04002A74 RID: 10868
		internal static readonly byte[] HmacWithSha512 = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			2,
			11
		};
	}
}
