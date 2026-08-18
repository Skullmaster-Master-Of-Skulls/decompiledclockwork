using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x0200020F RID: 527
	public sealed class ReedSolomonEncoder
	{
		// Token: 0x0600142A RID: 5162 RVA: 0x00073588 File Offset: 0x00072588
		public ReedSolomonEncoder(GF256 field)
		{
			if (!GF256.QR_CODE_FIELD.Equals(field))
			{
				throw new ArgumentException("Only QR Code is supported at this time");
			}
			this.field = field;
			this.cachedGenerators = new List<GF256Poly>();
			this.cachedGenerators.Add(new GF256Poly(field, new int[]
			{
				1
			}));
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000735E4 File Offset: 0x000725E4
		private GF256Poly BuildGenerator(int degree)
		{
			if (degree >= this.cachedGenerators.Count)
			{
				GF256Poly gf256Poly = this.cachedGenerators[this.cachedGenerators.Count - 1];
				for (int i = this.cachedGenerators.Count; i <= degree; i++)
				{
					GF256Poly gf256Poly2 = gf256Poly.Multiply(new GF256Poly(this.field, new int[]
					{
						1,
						this.field.Exp(i - 1)
					}));
					this.cachedGenerators.Add(gf256Poly2);
					gf256Poly = gf256Poly2;
				}
			}
			return this.cachedGenerators[degree];
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00073678 File Offset: 0x00072678
		public void Encode(int[] toEncode, int ecBytes)
		{
			if (ecBytes == 0)
			{
				throw new ArgumentException("No error correction bytes");
			}
			int num = toEncode.Length - ecBytes;
			if (num <= 0)
			{
				throw new ArgumentException("No data bytes provided");
			}
			GF256Poly other = this.BuildGenerator(ecBytes);
			int[] array = new int[num];
			Array.Copy(toEncode, 0, array, 0, num);
			GF256Poly gf256Poly = new GF256Poly(this.field, array);
			gf256Poly = gf256Poly.MultiplyByMonomial(ecBytes, 1);
			GF256Poly gf256Poly2 = gf256Poly.Divide(other)[1];
			int[] coefficients = gf256Poly2.GetCoefficients();
			int num2 = ecBytes - coefficients.Length;
			for (int i = 0; i < num2; i++)
			{
				toEncode[num + i] = 0;
			}
			Array.Copy(coefficients, 0, toEncode, num + num2, coefficients.Length);
		}

		// Token: 0x04000DE2 RID: 3554
		private GF256 field;

		// Token: 0x04000DE3 RID: 3555
		private List<GF256Poly> cachedGenerators;
	}
}
