using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000270 RID: 624
	internal sealed class GF256Poly
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x00086C60 File Offset: 0x00085C60
		internal GF256Poly(GF256 field, int[] coefficients)
		{
			if (coefficients == null || coefficients.Length == 0)
			{
				throw new ArgumentException();
			}
			this.field = field;
			int num = coefficients.Length;
			if (num <= 1 || coefficients[0] != 0)
			{
				this.coefficients = coefficients;
				return;
			}
			int num2 = 1;
			while (num2 < num && coefficients[num2] == 0)
			{
				num2++;
			}
			if (num2 == num)
			{
				this.coefficients = field.GetZero().coefficients;
				return;
			}
			this.coefficients = new int[num - num2];
			Array.Copy(coefficients, num2, this.coefficients, 0, this.coefficients.Length);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00086CE8 File Offset: 0x00085CE8
		internal int[] GetCoefficients()
		{
			return this.coefficients;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00086CF0 File Offset: 0x00085CF0
		internal int GetDegree()
		{
			return this.coefficients.Length - 1;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00086CFC File Offset: 0x00085CFC
		internal bool IsZero()
		{
			return this.coefficients[0] == 0;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00086D09 File Offset: 0x00085D09
		internal int GetCoefficient(int degree)
		{
			return this.coefficients[this.coefficients.Length - 1 - degree];
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00086D20 File Offset: 0x00085D20
		internal int EvaluateAt(int a)
		{
			if (a == 0)
			{
				return this.GetCoefficient(0);
			}
			int num = this.coefficients.Length;
			if (a == 1)
			{
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					num2 = GF256.AddOrSubtract(num2, this.coefficients[i]);
				}
				return num2;
			}
			int num3 = this.coefficients[0];
			for (int j = 1; j < num; j++)
			{
				num3 = GF256.AddOrSubtract(this.field.Multiply(a, num3), this.coefficients[j]);
			}
			return num3;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00086D9C File Offset: 0x00085D9C
		internal GF256Poly AddOrSubtract(GF256Poly other)
		{
			if (!this.field.Equals(other.field))
			{
				throw new ArgumentException("GF256Polys do not have same GF256 field");
			}
			if (this.IsZero())
			{
				return other;
			}
			if (other.IsZero())
			{
				return this;
			}
			int[] array = this.coefficients;
			int[] array2 = other.coefficients;
			if (array.Length > array2.Length)
			{
				int[] array3 = array;
				array = array2;
				array2 = array3;
			}
			int[] array4 = new int[array2.Length];
			int num = array2.Length - array.Length;
			Array.Copy(array2, 0, array4, 0, num);
			for (int i = num; i < array2.Length; i++)
			{
				array4[i] = GF256.AddOrSubtract(array[i - num], array2[i]);
			}
			return new GF256Poly(this.field, array4);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00086E48 File Offset: 0x00085E48
		internal GF256Poly Multiply(GF256Poly other)
		{
			if (!this.field.Equals(other.field))
			{
				throw new ArgumentException("GF256Polys do not have same GF256 field");
			}
			if (this.IsZero() || other.IsZero())
			{
				return this.field.GetZero();
			}
			int[] array = this.coefficients;
			int num = array.Length;
			int[] array2 = other.coefficients;
			int num2 = array2.Length;
			int[] array3 = new int[num + num2 - 1];
			for (int i = 0; i < num; i++)
			{
				int a = array[i];
				for (int j = 0; j < num2; j++)
				{
					array3[i + j] = GF256.AddOrSubtract(array3[i + j], this.field.Multiply(a, array2[j]));
				}
			}
			return new GF256Poly(this.field, array3);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00086F0C File Offset: 0x00085F0C
		internal GF256Poly Multiply(int scalar)
		{
			if (scalar == 0)
			{
				return this.field.GetZero();
			}
			if (scalar == 1)
			{
				return this;
			}
			int num = this.coefficients.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.field.Multiply(this.coefficients[i], scalar);
			}
			return new GF256Poly(this.field, array);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00086F70 File Offset: 0x00085F70
		internal GF256Poly MultiplyByMonomial(int degree, int coefficient)
		{
			if (degree < 0)
			{
				throw new ArgumentException();
			}
			if (coefficient == 0)
			{
				return this.field.GetZero();
			}
			int num = this.coefficients.Length;
			int[] array = new int[num + degree];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.field.Multiply(this.coefficients[i], coefficient);
			}
			return new GF256Poly(this.field, array);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00086FD8 File Offset: 0x00085FD8
		internal GF256Poly[] Divide(GF256Poly other)
		{
			if (!this.field.Equals(other.field))
			{
				throw new ArgumentException("GF256Polys do not have same GF256 field");
			}
			if (other.IsZero())
			{
				throw new DivideByZeroException("Divide by 0");
			}
			GF256Poly gf256Poly = this.field.GetZero();
			GF256Poly gf256Poly2 = this;
			int coefficient = other.GetCoefficient(other.GetDegree());
			int b = this.field.Inverse(coefficient);
			while (gf256Poly2.GetDegree() >= other.GetDegree() && !gf256Poly2.IsZero())
			{
				int degree = gf256Poly2.GetDegree() - other.GetDegree();
				int coefficient2 = this.field.Multiply(gf256Poly2.GetCoefficient(gf256Poly2.GetDegree()), b);
				GF256Poly other2 = other.MultiplyByMonomial(degree, coefficient2);
				GF256Poly other3 = this.field.BuildMonomial(degree, coefficient2);
				gf256Poly = gf256Poly.AddOrSubtract(other3);
				gf256Poly2 = gf256Poly2.AddOrSubtract(other2);
			}
			return new GF256Poly[]
			{
				gf256Poly,
				gf256Poly2
			};
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000870C4 File Offset: 0x000860C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(8 * this.GetDegree());
			for (int i = this.GetDegree(); i >= 0; i--)
			{
				int num = this.GetCoefficient(i);
				if (num != 0)
				{
					if (num < 0)
					{
						stringBuilder.Append(" - ");
						num = -num;
					}
					else if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" + ");
					}
					if (i == 0 || num != 1)
					{
						int num2 = this.field.Log(num);
						if (num2 == 0)
						{
							stringBuilder.Append('1');
						}
						else if (num2 == 1)
						{
							stringBuilder.Append('a');
						}
						else
						{
							stringBuilder.Append("a^");
							stringBuilder.Append(num2);
						}
					}
					if (i != 0)
					{
						if (i == 1)
						{
							stringBuilder.Append('x');
						}
						else
						{
							stringBuilder.Append("x^");
							stringBuilder.Append(i);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001009 RID: 4105
		private GF256 field;

		// Token: 0x0400100A RID: 4106
		private int[] coefficients;
	}
}
