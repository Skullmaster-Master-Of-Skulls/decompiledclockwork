using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x0200061E RID: 1566
	public class Revocable : SignatureSubpacket
	{
		// Token: 0x0600353C RID: 13628 RVA: 0x0014A8D0 File Offset: 0x001498D0
		private static byte[] BooleanToByteArray(bool value)
		{
			byte[] array = new byte[1];
			if (value)
			{
				array[0] = 1;
				return array;
			}
			return array;
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x0014A8EE File Offset: 0x001498EE
		public Revocable(bool critical, byte[] data) : base(SignatureSubpacketTag.Revocable, critical, data)
		{
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x0014A8F9 File Offset: 0x001498F9
		public Revocable(bool critical, bool isRevocable) : base(SignatureSubpacketTag.Revocable, critical, Revocable.BooleanToByteArray(isRevocable))
		{
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x0014A909 File Offset: 0x00149909
		public bool IsRevocable()
		{
			return this.data[0] != 0;
		}
	}
}
