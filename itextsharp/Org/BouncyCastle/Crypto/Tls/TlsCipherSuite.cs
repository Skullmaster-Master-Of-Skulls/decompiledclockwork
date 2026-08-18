using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200000E RID: 14
	internal abstract class TlsCipherSuite
	{
		// Token: 0x06000063 RID: 99
		internal abstract void Init(TlsProtocolHandler handler, byte[] ms, byte[] cr, byte[] sr);

		// Token: 0x06000064 RID: 100
		internal abstract byte[] EncodePlaintext(short type, byte[] plaintext, int offset, int len);

		// Token: 0x06000065 RID: 101
		internal abstract byte[] DecodeCiphertext(short type, byte[] plaintext, int offset, int len);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000066 RID: 102
		internal abstract short KeyExchangeAlgorithm { get; }

		// Token: 0x04000023 RID: 35
		internal const short KE_RSA = 1;

		// Token: 0x04000024 RID: 36
		internal const short KE_RSA_EXPORT = 2;

		// Token: 0x04000025 RID: 37
		internal const short KE_DHE_DSS = 3;

		// Token: 0x04000026 RID: 38
		internal const short KE_DHE_DSS_EXPORT = 4;

		// Token: 0x04000027 RID: 39
		internal const short KE_DHE_RSA = 5;

		// Token: 0x04000028 RID: 40
		internal const short KE_DHE_RSA_EXPORT = 6;

		// Token: 0x04000029 RID: 41
		internal const short KE_DH_DSS = 7;

		// Token: 0x0400002A RID: 42
		internal const short KE_DH_RSA = 8;

		// Token: 0x0400002B RID: 43
		internal const short KE_DH_anon = 9;

		// Token: 0x0400002C RID: 44
		internal const short KE_SRP = 10;

		// Token: 0x0400002D RID: 45
		internal const short KE_SRP_RSA = 11;

		// Token: 0x0400002E RID: 46
		internal const short KE_SRP_DSS = 12;
	}
}
