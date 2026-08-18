using System;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x020003E5 RID: 997
	public class SignatureCreationTime : SignatureSubpacket
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x000D6B54 File Offset: 0x000D5B54
		protected static byte[] TimeToBytes(DateTime time)
		{
			long num = DateTimeUtilities.DateTimeToUnixMs(time) / 1000L;
			return new byte[]
			{
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			};
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000D6B93 File Offset: 0x000D5B93
		public SignatureCreationTime(bool critical, byte[] data) : base(SignatureSubpacketTag.CreationTime, critical, data)
		{
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000D6B9E File Offset: 0x000D5B9E
		public SignatureCreationTime(bool critical, DateTime date) : base(SignatureSubpacketTag.CreationTime, critical, SignatureCreationTime.TimeToBytes(date))
		{
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000D6BB0 File Offset: 0x000D5BB0
		public DateTime GetTime()
		{
			long num = (long)((ulong)((int)this.data[0] << 24 | (int)this.data[1] << 16 | (int)this.data[2] << 8 | (int)this.data[3]));
			return DateTimeUtilities.UnixMsToDateTime(num * 1000L);
		}
	}
}
