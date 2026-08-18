using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x0200039A RID: 922
	public sealed class GF256
	{
		// Token: 0x06001FEB RID: 8171 RVA: 0x000BEB4C File Offset: 0x000BDB4C
		private GF256(int primitive)
		{
			this.expTable = new int[256];
			this.logTable = new int[256];
			int num = 1;
			for (int i = 0; i < 256; i++)
			{
				this.expTable[i] = num;
				num <<= 1;
				if (num >= 256)
				{
					num ^= primitive;
				}
			}
			for (int j = 0; j < 255; j++)
			{
				this.logTable[this.expTable[j]] = j;
			}
			int[] coefficients = new int[1];
			this.zero = new GF256Poly(this, coefficients);
			this.one = new GF256Poly(this, new int[]
			{
				1
			});
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000BEBF9 File Offset: 0x000BDBF9
		internal GF256Poly GetZero()
		{
			return this.zero;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000BEC01 File Offset: 0x000BDC01
		internal GF256Poly GetOne()
		{
			return this.one;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000BEC0C File Offset: 0x000BDC0C
		internal GF256Poly BuildMonomial(int degree, int coefficient)
		{
			if (degree < 0)
			{
				throw new ArgumentException();
			}
			if (coefficient == 0)
			{
				return this.zero;
			}
			int[] array = new int[degree + 1];
			array[0] = coefficient;
			return new GF256Poly(this, array);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000BEC41 File Offset: 0x000BDC41
		internal static int AddOrSubtract(int a, int b)
		{
			return a ^ b;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000BEC46 File Offset: 0x000BDC46
		internal int Exp(int a)
		{
			return this.expTable[a];
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000BEC50 File Offset: 0x000BDC50
		internal int Log(int a)
		{
			if (a == 0)
			{
				throw new ArgumentException();
			}
			return this.logTable[a];
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000BEC63 File Offset: 0x000BDC63
		internal int Inverse(int a)
		{
			if (a == 0)
			{
				throw new ArithmeticException();
			}
			return this.expTable[255 - this.logTable[a]];
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000BEC83 File Offset: 0x000BDC83
		internal int Multiply(int a, int b)
		{
			if (a == 0 || b == 0)
			{
				return 0;
			}
			if (a == 1)
			{
				return b;
			}
			if (b == 1)
			{
				return a;
			}
			return this.expTable[(this.logTable[a] + this.logTable[b]) % 255];
		}

		// Token: 0x040015FD RID: 5629
		public static readonly GF256 QR_CODE_FIELD = new GF256(285);

		// Token: 0x040015FE RID: 5630
		public static readonly GF256 DATA_MATRIX_FIELD = new GF256(301);

		// Token: 0x040015FF RID: 5631
		private int[] expTable;

		// Token: 0x04001600 RID: 5632
		private int[] logTable;

		// Token: 0x04001601 RID: 5633
		private GF256Poly zero;

		// Token: 0x04001602 RID: 5634
		private GF256Poly one;
	}
}
