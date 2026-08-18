using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060F RID: 1551
	public class F2mPoint : ECPointBase
	{
		// Token: 0x060034CB RID: 13515 RVA: 0x001481E5 File Offset: 0x001471E5
		public F2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y) : this(curve, x, y, false)
		{
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x001481F4 File Offset: 0x001471F4
		public F2mPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
			if ((x != null && y == null) || (x == null && y != null))
			{
				throw new ArgumentException("Exactly one of the field elements is null");
			}
			if (x != null)
			{
				F2mFieldElement.CheckFieldElements(this.x, this.y);
				F2mFieldElement.CheckFieldElements(this.x, this.curve.A);
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x0014824D File Offset: 0x0014724D
		[Obsolete("Use ECCurve.Infinity property")]
		public F2mPoint(ECCurve curve) : this(curve, null, null)
		{
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x00148258 File Offset: 0x00147258
		protected internal override bool YTilde
		{
			get
			{
				return base.X.ToBigInteger().SignValue != 0 && base.Y.Multiply(base.X.Invert()).ToBigInteger().TestBit(0);
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x0014828F File Offset: 0x0014728F
		private static void CheckPoints(ECPoint a, ECPoint b)
		{
			if (!a.curve.Equals(b.curve))
			{
				throw new ArgumentException("Only points on the same curve can be added or subtracted");
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x001482AF File Offset: 0x001472AF
		public override ECPoint Add(ECPoint b)
		{
			F2mPoint.CheckPoints(this, b);
			return this.AddSimple((F2mPoint)b);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x001482C4 File Offset: 0x001472C4
		internal F2mPoint AddSimple(F2mPoint b)
		{
			if (base.IsInfinity)
			{
				return b;
			}
			if (b.IsInfinity)
			{
				return this;
			}
			F2mFieldElement f2mFieldElement = (F2mFieldElement)b.X;
			F2mFieldElement f2mFieldElement2 = (F2mFieldElement)b.Y;
			if (!this.x.Equals(f2mFieldElement))
			{
				ECFieldElement b2 = this.x.Add(f2mFieldElement);
				F2mFieldElement f2mFieldElement3 = (F2mFieldElement)this.y.Add(f2mFieldElement2).Divide(b2);
				F2mFieldElement f2mFieldElement4 = (F2mFieldElement)f2mFieldElement3.Square().Add(f2mFieldElement3).Add(b2).Add(this.curve.A);
				F2mFieldElement y = (F2mFieldElement)f2mFieldElement3.Multiply(this.x.Add(f2mFieldElement4)).Add(f2mFieldElement4).Add(this.y);
				return new F2mPoint(this.curve, f2mFieldElement4, y, this.withCompression);
			}
			if (this.y.Equals(f2mFieldElement2))
			{
				return (F2mPoint)this.Twice();
			}
			return (F2mPoint)this.curve.Infinity;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x001483C5 File Offset: 0x001473C5
		public override ECPoint Subtract(ECPoint b)
		{
			F2mPoint.CheckPoints(this, b);
			return this.SubtractSimple((F2mPoint)b);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x001483DA File Offset: 0x001473DA
		internal F2mPoint SubtractSimple(F2mPoint b)
		{
			if (b.IsInfinity)
			{
				return this;
			}
			return this.AddSimple((F2mPoint)b.Negate());
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x001483F8 File Offset: 0x001473F8
		public override ECPoint Twice()
		{
			if (base.IsInfinity)
			{
				return this;
			}
			if (this.x.ToBigInteger().SignValue == 0)
			{
				return this.curve.Infinity;
			}
			F2mFieldElement f2mFieldElement = (F2mFieldElement)this.x.Add(this.y.Divide(this.x));
			F2mFieldElement f2mFieldElement2 = (F2mFieldElement)f2mFieldElement.Square().Add(f2mFieldElement).Add(this.curve.A);
			ECFieldElement b = this.curve.FromBigInteger(BigInteger.One);
			F2mFieldElement y = (F2mFieldElement)this.x.Square().Add(f2mFieldElement2.Multiply(f2mFieldElement.Add(b)));
			return new F2mPoint(this.curve, f2mFieldElement2, y, this.withCompression);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x001484B8 File Offset: 0x001474B8
		public override ECPoint Negate()
		{
			return new F2mPoint(this.curve, this.x, this.x.Add(this.y), this.withCompression);
		}
	}
}
