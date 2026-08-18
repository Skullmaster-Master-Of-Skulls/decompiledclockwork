using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200029B RID: 667
	public abstract class ECCurve
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001917 RID: 6423
		public abstract int FieldSize { get; }

		// Token: 0x06001918 RID: 6424
		public abstract ECFieldElement FromBigInteger(BigInteger x);

		// Token: 0x06001919 RID: 6425
		public abstract ECPoint CreatePoint(BigInteger x, BigInteger y, bool withCompression);

		// Token: 0x0600191A RID: 6426
		public abstract ECPoint DecodePoint(byte[] encoded);

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600191B RID: 6427
		public abstract ECPoint Infinity { get; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x000933C6 File Offset: 0x000923C6
		public ECFieldElement A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x000933CE File Offset: 0x000923CE
		public ECFieldElement B
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000933D8 File Offset: 0x000923D8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECCurve eccurve = obj as ECCurve;
			return eccurve != null && this.Equals(eccurve);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000933FE File Offset: 0x000923FE
		protected bool Equals(ECCurve other)
		{
			return this.a.Equals(other.a) && this.b.Equals(other.b);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00093426 File Offset: 0x00092426
		public override int GetHashCode()
		{
			return this.a.GetHashCode() ^ this.b.GetHashCode();
		}

		// Token: 0x040010EE RID: 4334
		internal ECFieldElement a;

		// Token: 0x040010EF RID: 4335
		internal ECFieldElement b;
	}
}
