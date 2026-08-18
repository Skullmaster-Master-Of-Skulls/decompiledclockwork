using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x020001FE RID: 510
	public class PrimaryUserId : SignatureSubpacket
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x00071F8C File Offset: 0x00070F8C
		private static byte[] BooleanToByteArray(bool val)
		{
			byte[] array = new byte[1];
			if (val)
			{
				array[0] = 1;
				return array;
			}
			return array;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00071FAA File Offset: 0x00070FAA
		public PrimaryUserId(bool critical, byte[] data) : base(SignatureSubpacketTag.PrimaryUserId, critical, data)
		{
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00071FB6 File Offset: 0x00070FB6
		public PrimaryUserId(bool critical, bool isPrimaryUserId) : base(SignatureSubpacketTag.PrimaryUserId, critical, PrimaryUserId.BooleanToByteArray(isPrimaryUserId))
		{
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00071FC7 File Offset: 0x00070FC7
		public bool IsPrimaryUserId()
		{
			return this.data[0] != 0;
		}
	}
}
