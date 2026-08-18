using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000032 RID: 50
	public class Exportable : SignatureSubpacket
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00008CC0 File Offset: 0x00007CC0
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

		// Token: 0x06000156 RID: 342 RVA: 0x00008CDE File Offset: 0x00007CDE
		public Exportable(bool critical, byte[] data) : base(SignatureSubpacketTag.Exportable, critical, data)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008CE9 File Offset: 0x00007CE9
		public Exportable(bool critical, bool isExportable) : base(SignatureSubpacketTag.Exportable, critical, Exportable.BooleanToByteArray(isExportable))
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008CF9 File Offset: 0x00007CF9
		public bool IsExportable()
		{
			return this.data[0] != 0;
		}
	}
}
