using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000123 RID: 291
	public class ECDomainParameters
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x000381B2 File Offset: 0x000371B2
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, BigInteger.One)
		{
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000381C2 File Offset: 0x000371C2
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000381D0 File Offset: 0x000371D0
		public ECDomainParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			if (curve == null)
			{
				throw new ArgumentNullException("curve");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (n == null)
			{
				throw new ArgumentNullException("n");
			}
			if (h == null)
			{
				throw new ArgumentNullException("h");
			}
			this.curve = curve;
			this.g = g;
			this.n = n;
			this.h = h;
			this.seed = Arrays.Clone(seed);
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00038246 File Offset: 0x00037246
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0003824E File Offset: 0x0003724E
		public ECPoint G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00038256 File Offset: 0x00037256
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0003825E File Offset: 0x0003725E
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00038266 File Offset: 0x00037266
		public byte[] GetSeed()
		{
			return Arrays.Clone(this.seed);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00038274 File Offset: 0x00037274
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECDomainParameters ecdomainParameters = obj as ECDomainParameters;
			return ecdomainParameters != null && this.Equals(ecdomainParameters);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0003829C File Offset: 0x0003729C
		protected bool Equals(ECDomainParameters other)
		{
			return this.curve.Equals(other.curve) && this.g.Equals(other.g) && this.n.Equals(other.n) && this.h.Equals(other.h) && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00038308 File Offset: 0x00037308
		public override int GetHashCode()
		{
			return this.curve.GetHashCode() ^ this.g.GetHashCode() ^ this.n.GetHashCode() ^ this.h.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x04000884 RID: 2180
		internal ECCurve curve;

		// Token: 0x04000885 RID: 2181
		internal byte[] seed;

		// Token: 0x04000886 RID: 2182
		internal ECPoint g;

		// Token: 0x04000887 RID: 2183
		internal BigInteger n;

		// Token: 0x04000888 RID: 2184
		internal BigInteger h;
	}
}
