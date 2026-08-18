using System;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060C RID: 1548
	public abstract class ECPoint
	{
		// Token: 0x060034B0 RID: 13488 RVA: 0x00147DAB File Offset: 0x00146DAB
		protected internal ECPoint(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			this.curve = curve;
			this.x = x;
			this.y = y;
			this.withCompression = withCompression;
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x00147DDE File Offset: 0x00146DDE
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x00147DE6 File Offset: 0x00146DE6
		public ECFieldElement X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x00147DEE File Offset: 0x00146DEE
		public ECFieldElement Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x00147DF6 File Offset: 0x00146DF6
		public bool IsInfinity
		{
			get
			{
				return this.x == null && this.y == null;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x00147E0B File Offset: 0x00146E0B
		public bool IsCompressed
		{
			get
			{
				return this.withCompression;
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x00147E14 File Offset: 0x00146E14
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPoint ecpoint = obj as ECPoint;
			if (ecpoint == null)
			{
				return false;
			}
			if (this.IsInfinity)
			{
				return ecpoint.IsInfinity;
			}
			return this.x.Equals(ecpoint.x) && this.y.Equals(ecpoint.y);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x00147E68 File Offset: 0x00146E68
		public override int GetHashCode()
		{
			if (this.IsInfinity)
			{
				return 0;
			}
			return this.x.GetHashCode() ^ this.y.GetHashCode();
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x00147E8B File Offset: 0x00146E8B
		internal void SetPreCompInfo(PreCompInfo preCompInfo)
		{
			this.preCompInfo = preCompInfo;
		}

		// Token: 0x060034B9 RID: 13497
		public abstract byte[] GetEncoded();

		// Token: 0x060034BA RID: 13498
		public abstract ECPoint Add(ECPoint b);

		// Token: 0x060034BB RID: 13499
		public abstract ECPoint Subtract(ECPoint b);

		// Token: 0x060034BC RID: 13500
		public abstract ECPoint Negate();

		// Token: 0x060034BD RID: 13501
		public abstract ECPoint Twice();

		// Token: 0x060034BE RID: 13502
		public abstract ECPoint Multiply(BigInteger b);

		// Token: 0x060034BF RID: 13503 RVA: 0x00147E94 File Offset: 0x00146E94
		internal virtual void AssertECMultiplier()
		{
			if (this.multiplier == null)
			{
				lock (this)
				{
					if (this.multiplier == null)
					{
						this.multiplier = new FpNafMultiplier();
					}
				}
			}
		}

		// Token: 0x0400236A RID: 9066
		internal readonly ECCurve curve;

		// Token: 0x0400236B RID: 9067
		internal readonly ECFieldElement x;

		// Token: 0x0400236C RID: 9068
		internal readonly ECFieldElement y;

		// Token: 0x0400236D RID: 9069
		internal readonly bool withCompression;

		// Token: 0x0400236E RID: 9070
		internal ECMultiplier multiplier;

		// Token: 0x0400236F RID: 9071
		internal PreCompInfo preCompInfo;
	}
}
