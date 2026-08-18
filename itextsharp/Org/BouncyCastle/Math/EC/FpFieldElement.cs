using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000184 RID: 388
	public class FpFieldElement : ECFieldElement
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x00057640 File Offset: 0x00056640
		public FpFieldElement(BigInteger q, BigInteger x)
		{
			if (x.CompareTo(q) >= 0)
			{
				throw new ArgumentException("x value too large in field element");
			}
			this.q = q;
			this.x = x;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005766B File Offset: 0x0005666B
		public override BigInteger ToBigInteger()
		{
			return this.x;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00057673 File Offset: 0x00056673
		public override string FieldName
		{
			get
			{
				return "Fp";
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0005767A File Offset: 0x0005667A
		public override int FieldSize
		{
			get
			{
				return this.q.BitLength;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00057687 File Offset: 0x00056687
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0005768F File Offset: 0x0005668F
		public override ECFieldElement Add(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.x.Add(b.ToBigInteger()).Mod(this.q));
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000576B8 File Offset: 0x000566B8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.x.Subtract(b.ToBigInteger()).Mod(this.q));
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000576E1 File Offset: 0x000566E1
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.x.Multiply(b.ToBigInteger()).Mod(this.q));
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0005770A File Offset: 0x0005670A
		public override ECFieldElement Divide(ECFieldElement b)
		{
			return new FpFieldElement(this.q, this.x.Multiply(b.ToBigInteger().ModInverse(this.q)).Mod(this.q));
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005773E File Offset: 0x0005673E
		public override ECFieldElement Negate()
		{
			return new FpFieldElement(this.q, this.x.Negate().Mod(this.q));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00057761 File Offset: 0x00056761
		public override ECFieldElement Square()
		{
			return new FpFieldElement(this.q, this.x.Multiply(this.x).Mod(this.q));
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0005778A File Offset: 0x0005678A
		public override ECFieldElement Invert()
		{
			return new FpFieldElement(this.q, this.x.ModInverse(this.q));
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000577A8 File Offset: 0x000567A8
		public override ECFieldElement Sqrt()
		{
			if (!this.q.TestBit(0))
			{
				throw Platform.CreateNotImplementedException("even value of q");
			}
			if (this.q.TestBit(1))
			{
				ECFieldElement ecfieldElement = new FpFieldElement(this.q, this.x.ModPow(this.q.ShiftRight(2).Add(BigInteger.One), this.q));
				if (!ecfieldElement.Square().Equals(this))
				{
					return null;
				}
				return ecfieldElement;
			}
			else
			{
				BigInteger bigInteger = this.q.Subtract(BigInteger.One);
				BigInteger exponent = bigInteger.ShiftRight(1);
				if (!this.x.ModPow(exponent, this.q).Equals(BigInteger.One))
				{
					return null;
				}
				BigInteger bigInteger2 = bigInteger.ShiftRight(2);
				BigInteger k = bigInteger2.ShiftLeft(1).Add(BigInteger.One);
				BigInteger bigInteger3 = this.x;
				BigInteger bigInteger4 = bigInteger3.ShiftLeft(2).Mod(this.q);
				BigInteger bigInteger7;
				for (;;)
				{
					Random random = new Random();
					BigInteger bigInteger5;
					do
					{
						bigInteger5 = new BigInteger(this.q.BitLength, random);
					}
					while (bigInteger5.CompareTo(this.q) >= 0 || !bigInteger5.Multiply(bigInteger5).Subtract(bigInteger4).ModPow(exponent, this.q).Equals(bigInteger));
					BigInteger[] array = FpFieldElement.fastLucasSequence(this.q, bigInteger5, bigInteger3, k);
					BigInteger bigInteger6 = array[0];
					bigInteger7 = array[1];
					if (bigInteger7.Multiply(bigInteger7).Mod(this.q).Equals(bigInteger4))
					{
						break;
					}
					if (!bigInteger6.Equals(BigInteger.One) && !bigInteger6.Equals(bigInteger))
					{
						goto Block_10;
					}
				}
				if (bigInteger7.TestBit(0))
				{
					bigInteger7 = bigInteger7.Add(this.q);
				}
				bigInteger7 = bigInteger7.ShiftRight(1);
				return new FpFieldElement(this.q, bigInteger7);
				Block_10:
				return null;
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00057970 File Offset: 0x00056970
		private static BigInteger[] fastLucasSequence(BigInteger p, BigInteger P, BigInteger Q, BigInteger k)
		{
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			BigInteger bigInteger = BigInteger.One;
			BigInteger bigInteger2 = BigInteger.Two;
			BigInteger bigInteger3 = P;
			BigInteger bigInteger4 = BigInteger.One;
			BigInteger bigInteger5 = BigInteger.One;
			for (int i = bitLength - 1; i >= lowestSetBit + 1; i--)
			{
				bigInteger4 = bigInteger4.Multiply(bigInteger5).Mod(p);
				if (k.TestBit(i))
				{
					bigInteger5 = bigInteger4.Multiply(Q).Mod(p);
					bigInteger = bigInteger.Multiply(bigInteger3).Mod(p);
					bigInteger2 = bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)).Mod(p);
					bigInteger3 = bigInteger3.Multiply(bigInteger3).Subtract(bigInteger5.ShiftLeft(1)).Mod(p);
				}
				else
				{
					bigInteger5 = bigInteger4;
					bigInteger = bigInteger.Multiply(bigInteger2).Subtract(bigInteger4).Mod(p);
					bigInteger3 = bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)).Mod(p);
					bigInteger2 = bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)).Mod(p);
				}
			}
			bigInteger4 = bigInteger4.Multiply(bigInteger5).Mod(p);
			bigInteger5 = bigInteger4.Multiply(Q).Mod(p);
			bigInteger = bigInteger.Multiply(bigInteger2).Subtract(bigInteger4).Mod(p);
			bigInteger2 = bigInteger3.Multiply(bigInteger2).Subtract(P.Multiply(bigInteger4)).Mod(p);
			bigInteger4 = bigInteger4.Multiply(bigInteger5).Mod(p);
			for (int j = 1; j <= lowestSetBit; j++)
			{
				bigInteger = bigInteger.Multiply(bigInteger2).Mod(p);
				bigInteger2 = bigInteger2.Multiply(bigInteger2).Subtract(bigInteger4.ShiftLeft(1)).Mod(p);
				bigInteger4 = bigInteger4.Multiply(bigInteger4).Mod(p);
			}
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00057B4C File Offset: 0x00056B4C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FpFieldElement fpFieldElement = obj as FpFieldElement;
			return fpFieldElement != null && this.Equals(fpFieldElement);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00057B72 File Offset: 0x00056B72
		protected bool Equals(FpFieldElement other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00057B90 File Offset: 0x00056B90
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04000B0D RID: 2829
		private readonly BigInteger q;

		// Token: 0x04000B0E RID: 2830
		private readonly BigInteger x;
	}
}
