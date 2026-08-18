using System;

namespace iTextSharp.text.pdf.crypto
{
	// Token: 0x0200045E RID: 1118
	public sealed class IVGenerator
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x000E4564 File Offset: 0x000E3564
		static IVGenerator()
		{
			byte[] array = new byte[8];
			long num = DateTime.Now.Ticks;
			for (int num2 = 0; num2 != 8; num2++)
			{
				array[num2] = (byte)num;
				num = (long)((ulong)num >> 8);
			}
			IVGenerator.rc4.PrepareARCFOURKey(array);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000E45B0 File Offset: 0x000E35B0
		private IVGenerator()
		{
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000E45B8 File Offset: 0x000E35B8
		public static byte[] GetIV()
		{
			return IVGenerator.GetIV(16);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000E45C4 File Offset: 0x000E35C4
		public static byte[] GetIV(int len)
		{
			byte[] array = new byte[len];
			lock (IVGenerator.rc4)
			{
				IVGenerator.rc4.EncryptARCFOUR(array);
			}
			return array;
		}

		// Token: 0x04001A40 RID: 6720
		private static ARCFOUREncryption rc4 = new ARCFOUREncryption();
	}
}
