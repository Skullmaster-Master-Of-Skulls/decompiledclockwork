using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000472 RID: 1138
	internal class TlsNullCipherSuite : TlsCipherSuite
	{
		// Token: 0x060026CB RID: 9931 RVA: 0x000EAC70 File Offset: 0x000E9C70
		internal override void Init(TlsProtocolHandler handler, byte[] ms, byte[] cr, byte[] sr)
		{
			throw new TlsException("Sorry, init of TLS_NULL_WITH_NULL_NULL is forbidden");
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000EAC7C File Offset: 0x000E9C7C
		internal override byte[] EncodePlaintext(short type, byte[] plaintext, int offset, int len)
		{
			byte[] array = new byte[len];
			Array.Copy(plaintext, offset, array, 0, len);
			return array;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000EACA0 File Offset: 0x000E9CA0
		internal override byte[] DecodeCiphertext(short type, byte[] plaintext, int offset, int len)
		{
			byte[] array = new byte[len];
			Array.Copy(plaintext, offset, array, 0, len);
			return array;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000EACC1 File Offset: 0x000E9CC1
		internal override short KeyExchangeAlgorithm
		{
			get
			{
				return 0;
			}
		}
	}
}
