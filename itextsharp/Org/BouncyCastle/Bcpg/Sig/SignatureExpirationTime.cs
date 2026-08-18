using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200035B RID: 859
	public class SignatureExpirationTime : SignatureSubpacket
	{
		// Token: 0x06001ED4 RID: 7892 RVA: 0x000B9CC0 File Offset: 0x000B8CC0
		protected static byte[] TimeToBytes(long t)
		{
			return new byte[]
			{
				(byte)(t >> 24),
				(byte)(t >> 16),
				(byte)(t >> 8),
				(byte)t
			};
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000B9CF1 File Offset: 0x000B8CF1
		public SignatureExpirationTime(bool critical, byte[] data) : base(SignatureSubpacketTag.ExpireTime, critical, data)
		{
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000B9CFC File Offset: 0x000B8CFC
		public SignatureExpirationTime(bool critical, long seconds) : base(SignatureSubpacketTag.ExpireTime, critical, SignatureExpirationTime.TimeToBytes(seconds))
		{
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000B9D0C File Offset: 0x000B8D0C
		public long Time
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 24 | (long)(this.data[1] & byte.MaxValue) << 16 | (long)(this.data[2] & byte.MaxValue) << 8 | (long)((ulong)this.data[3] & 255UL);
			}
		}
	}
}
