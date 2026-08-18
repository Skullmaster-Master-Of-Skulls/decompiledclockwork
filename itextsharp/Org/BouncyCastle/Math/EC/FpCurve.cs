using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200029D RID: 669
	public class FpCurve : ECCurveBase
	{
		// Token: 0x06001925 RID: 6437 RVA: 0x0009354D File Offset: 0x0009254D
		public FpCurve(BigInteger q, BigInteger a, BigInteger b)
		{
			this.q = q;
			this.a = this.FromBigInteger(a);
			this.b = this.FromBigInteger(b);
			this.infinity = new FpPoint(this, null, null);
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00093584 File Offset: 0x00092584
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0009358C File Offset: 0x0009258C
		public override ECPoint Infinity
		{
			get
			{
				return this.infinity;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00093594 File Offset: 0x00092594
		public override int FieldSize
		{
			get
			{
				return this.q.BitLength;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000935A1 File Offset: 0x000925A1
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new FpFieldElement(this.q, x);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000935AF File Offset: 0x000925AF
		public override ECPoint CreatePoint(BigInteger X1, BigInteger Y1, bool withCompression)
		{
			return new FpPoint(this, this.FromBigInteger(X1), this.FromBigInteger(Y1), withCompression);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000935C8 File Offset: 0x000925C8
		protected internal override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2 = ecfieldElement.Multiply(ecfieldElement.Square().Add(this.a)).Add(this.b);
			ECFieldElement ecfieldElement3 = ecfieldElement2.Sqrt();
			if (ecfieldElement3 == null)
			{
				throw new ArithmeticException("Invalid point compression");
			}
			BigInteger bigInteger = ecfieldElement3.ToBigInteger();
			int num = bigInteger.TestBit(0) ? 1 : 0;
			if (num != yTilde)
			{
				ecfieldElement3 = this.FromBigInteger(this.q.Subtract(bigInteger));
			}
			return new FpPoint(this, ecfieldElement, ecfieldElement3, true);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0009364C File Offset: 0x0009264C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FpCurve fpCurve = obj as FpCurve;
			return fpCurve != null && this.Equals(fpCurve);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00093672 File Offset: 0x00092672
		protected bool Equals(FpCurve other)
		{
			return base.Equals(other) && this.q.Equals(other.q);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00093690 File Offset: 0x00092690
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.q.GetHashCode();
		}

		// Token: 0x040010F0 RID: 4336
		private readonly BigInteger q;

		// Token: 0x040010F1 RID: 4337
		private readonly FpPoint infinity;
	}
}
