using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060E RID: 1550
	public class FpPoint : ECPointBase
	{
		// Token: 0x060034C4 RID: 13508 RVA: 0x00147FC2 File Offset: 0x00146FC2
		public FpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x00147FCE File Offset: 0x00146FCE
		public FpPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if ((x != null && y == null) || (x == null && y != null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x00147FF2 File Offset: 0x00146FF2
		protected internal override bool YTilde
		{
			get
			{
				return base.Y.ToBigInteger().TestBit(0);
			}
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x00148008 File Offset: 0x00147008
		public override ECPoint Add(ECPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this;
			}
			if (!this.x.Equals(b.x))
			{
				ECFieldElement ecfieldElement = b.y.Subtract(this.y).Divide(b.x.Subtract(this.x));
				ECFieldElement ecfieldElement2 = ecfieldElement.Square().Subtract(this.x).Subtract(b.x);
				ECFieldElement y = ecfieldElement.Multiply(this.x.Subtract(ecfieldElement2)).Subtract(this.y);
				return new FpPoint(this.curve, ecfieldElement2, y);
			}
			if (this.y.Equals(b.y))
			{
				return this.Twice();
			}
			return this.curve.Infinity;
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x001480D4 File Offset: 0x001470D4
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			if (this.y.ToBigInteger().SignValue == 0)
			{
				return this.curve.Infinity;
			}
			ECFieldElement b = this.curve.FromBigInteger(BigInteger.Two);
			ECFieldElement b2 = this.curve.FromBigInteger(BigInteger.Three);
			ECFieldElement ecfieldElement = this.x.Square().Multiply(b2).Add(this.curve.a).Divide(this.y.Multiply(b));
			ECFieldElement ecfieldElement2 = ecfieldElement.Square().Subtract(this.x.Multiply(b));
			ECFieldElement y = ecfieldElement.Multiply(this.x.Subtract(ecfieldElement2)).Subtract(this.y);
			return new FpPoint(this.curve, ecfieldElement2, y, this.withCompression);
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x001481A9 File Offset: 0x001471A9
		public override ECPoint Subtract(ECPoint b)
		{
			if (b.IsInfinity)
			{
				return this;
			}
			return this.Add(b.Negate());
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x001481C1 File Offset: 0x001471C1
		public override ECPoint Negate()
		{
			return new FpPoint(this.curve, this.x, this.y.Negate(), this.withCompression);
		}
	}
}
