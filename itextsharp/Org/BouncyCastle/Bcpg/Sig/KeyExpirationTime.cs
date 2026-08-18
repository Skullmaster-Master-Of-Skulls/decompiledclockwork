using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x020005B3 RID: 1459
	public class KeyExpirationTime : SignatureSubpacket
	{
		// Token: 0x06003244 RID: 12868 RVA: 0x001387D4 File Offset: 0x001377D4
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

		// Token: 0x06003245 RID: 12869 RVA: 0x00138805 File Offset: 0x00137805
		public KeyExpirationTime(bool critical, byte[] data) : base(SignatureSubpacketTag.KeyExpireTime, critical, data)
		{
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x00138811 File Offset: 0x00137811
		public KeyExpirationTime(bool critical, long seconds) : base(SignatureSubpacketTag.KeyExpireTime, critical, KeyExpirationTime.TimeToBytes(seconds))
		{
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x00138824 File Offset: 0x00137824
		public long Time
		{
			get
			{
				return (long)(this.data[0] & byte.MaxValue) << 24 | (long)(this.data[1] & byte.MaxValue) << 16 | (long)(this.data[2] & byte.MaxValue) << 8 | (long)((ulong)this.data[3] & 255UL);
			}
		}
	}
}
