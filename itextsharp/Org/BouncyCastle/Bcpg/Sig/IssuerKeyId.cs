using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200019D RID: 413
	public class IssuerKeyId : SignatureSubpacket
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005C5F0 File Offset: 0x0005B5F0
		protected static byte[] KeyIdToBytes(long keyId)
		{
			return new byte[]
			{
				(byte)(keyId >> 56),
				(byte)(keyId >> 48),
				(byte)(keyId >> 40),
				(byte)(keyId >> 32),
				(byte)(keyId >> 24),
				(byte)(keyId >> 16),
				(byte)(keyId >> 8),
				(byte)keyId
			};
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005C641 File Offset: 0x0005B641
		public IssuerKeyId(bool critical, byte[] data) : base(SignatureSubpacketTag.IssuerKeyId, critical, data)
		{
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005C64D File Offset: 0x0005B64D
		public IssuerKeyId(bool critical, long keyId) : base(SignatureSubpacketTag.IssuerKeyId, critical, IssuerKeyId.KeyIdToBytes(keyId))
		{
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0005C660 File Offset: 0x0005B660
		public long KeyId
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 56 | (long)(this.data[1] & byte.MaxValue) << 48 | (long)(this.data[2] & byte.MaxValue) << 40 | (long)(this.data[3] & byte.MaxValue) << 32 | (long)(this.data[4] & byte.MaxValue) << 24 | (long)(this.data[5] & byte.MaxValue) << 16 | (long)(this.data[6] & byte.MaxValue) << 8 | (long)((ulong)this.data[7] & 255UL);
			}
		}
	}
}
