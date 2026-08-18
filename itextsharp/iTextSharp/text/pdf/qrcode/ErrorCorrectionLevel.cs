using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000494 RID: 1172
	public sealed class ErrorCorrectionLevel
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x000EE813 File Offset: 0x000ED813
		private ErrorCorrectionLevel(int ordinal, int bits, string name)
		{
			this.ordinal = ordinal;
			this.bits = bits;
			this.name = name;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000EE830 File Offset: 0x000ED830
		public int Ordinal()
		{
			return this.ordinal;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000EE838 File Offset: 0x000ED838
		public int GetBits()
		{
			return this.bits;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000EE840 File Offset: 0x000ED840
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000EE848 File Offset: 0x000ED848
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000EE850 File Offset: 0x000ED850
		public static ErrorCorrectionLevel ForBits(int bits)
		{
			if (bits < 0 || bits >= ErrorCorrectionLevel.FOR_BITS.Length)
			{
				throw new IndexOutOfRangeException();
			}
			return ErrorCorrectionLevel.FOR_BITS[bits];
		}

		// Token: 0x04001B39 RID: 6969
		public static readonly ErrorCorrectionLevel L = new ErrorCorrectionLevel(0, 1, "L");

		// Token: 0x04001B3A RID: 6970
		public static readonly ErrorCorrectionLevel M = new ErrorCorrectionLevel(1, 0, "M");

		// Token: 0x04001B3B RID: 6971
		public static readonly ErrorCorrectionLevel Q = new ErrorCorrectionLevel(2, 3, "Q");

		// Token: 0x04001B3C RID: 6972
		public static readonly ErrorCorrectionLevel H = new ErrorCorrectionLevel(3, 2, "H");

		// Token: 0x04001B3D RID: 6973
		private static readonly ErrorCorrectionLevel[] FOR_BITS = new ErrorCorrectionLevel[]
		{
			ErrorCorrectionLevel.M,
			ErrorCorrectionLevel.L,
			ErrorCorrectionLevel.H,
			ErrorCorrectionLevel.Q
		};

		// Token: 0x04001B3E RID: 6974
		private int ordinal;

		// Token: 0x04001B3F RID: 6975
		private int bits;

		// Token: 0x04001B40 RID: 6976
		private string name;
	}
}
