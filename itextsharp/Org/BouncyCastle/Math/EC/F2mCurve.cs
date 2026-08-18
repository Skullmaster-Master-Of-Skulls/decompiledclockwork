using System;
using Org.BouncyCastle.Math.EC.Abc;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200029E RID: 670
	public class F2mCurve : ECCurveBase
	{
		// Token: 0x0600192F RID: 6447 RVA: 0x000936A4 File Offset: 0x000926A4
		public F2mCurve(int m, int k, BigInteger a, BigInteger b) : this(m, k, 0, 0, a, b, null, null)
		{
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000936C0 File Offset: 0x000926C0
		public F2mCurve(int m, int k, BigInteger a, BigInteger b, BigInteger n, BigInteger h) : this(m, k, 0, 0, a, b, n, h)
		{
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000936E0 File Offset: 0x000926E0
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b) : this(m, k1, k2, k3, a, b, null, null)
		{
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00093700 File Offset: 0x00092700
		public F2mCurve(int m, int k1, int k2, int k3, BigInteger a, BigInteger b, BigInteger n, BigInteger h)
		{
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			this.n = n;
			this.h = h;
			this.infinity = new F2mPoint(this, null, null);
			if (k1 == 0)
			{
				throw new ArgumentException("k1 must be > 0");
			}
			if (k2 == 0)
			{
				if (k3 != 0)
				{
					throw new ArgumentException("k3 must be 0 if k2 == 0");
				}
			}
			else
			{
				if (k2 <= k1)
				{
					throw new ArgumentException("k2 must be > k1");
				}
				if (k3 <= k2)
				{
					throw new ArgumentException("k3 must be > k2");
				}
			}
			this.a = this.FromBigInteger(a);
			this.b = this.FromBigInteger(b);
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000937A9 File Offset: 0x000927A9
		public override ECPoint Infinity
		{
			get
			{
				return this.infinity;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x000937B1 File Offset: 0x000927B1
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000937B9 File Offset: 0x000927B9
		public override ECFieldElement FromBigInteger(BigInteger x)
		{
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, x);
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x000937DC File Offset: 0x000927DC
		public bool IsKoblitz
		{
			get
			{
				return this.n != null && this.h != null && (this.a.ToBigInteger().Equals(BigInteger.Zero) || this.a.ToBigInteger().Equals(BigInteger.One)) && this.b.ToBigInteger().Equals(BigInteger.One);
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00093840 File Offset: 0x00092840
		internal sbyte GetMu()
		{
			if (this.mu == 0)
			{
				lock (this)
				{
					if (this.mu == 0)
					{
						this.mu = Tnaf.GetMu(this);
					}
				}
			}
			return this.mu;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00093890 File Offset: 0x00092890
		internal BigInteger[] GetSi()
		{
			if (this.si == null)
			{
				lock (this)
				{
					if (this.si == null)
					{
						this.si = Tnaf.GetSi(this);
					}
				}
			}
			return this.si;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000938E0 File Offset: 0x000928E0
		public override ECPoint CreatePoint(BigInteger X1, BigInteger Y1, bool withCompression)
		{
			return new F2mPoint(this, this.FromBigInteger(X1), this.FromBigInteger(Y1), withCompression);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000938F8 File Offset: 0x000928F8
		protected internal override ECPoint DecompressPoint(int yTilde, BigInteger X1)
		{
			ECFieldElement ecfieldElement = this.FromBigInteger(X1);
			ECFieldElement ecfieldElement2;
			if (ecfieldElement.ToBigInteger().SignValue == 0)
			{
				ecfieldElement2 = (F2mFieldElement)this.b;
				for (int i = 0; i < this.m - 1; i++)
				{
					ecfieldElement2 = ecfieldElement2.Square();
				}
			}
			else
			{
				ECFieldElement beta = ecfieldElement.Add(this.a).Add(this.b.Multiply(ecfieldElement.Square().Invert()));
				ECFieldElement ecfieldElement3 = this.solveQuadradicEquation(beta);
				if (ecfieldElement3 == null)
				{
					throw new ArithmeticException("Invalid point compression");
				}
				int num = ecfieldElement3.ToBigInteger().TestBit(0) ? 1 : 0;
				if (num != yTilde)
				{
					ecfieldElement3 = ecfieldElement3.Add(this.FromBigInteger(BigInteger.One));
				}
				ecfieldElement2 = ecfieldElement.Multiply(ecfieldElement3);
			}
			return new F2mPoint(this, ecfieldElement, ecfieldElement2, true);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000939C4 File Offset: 0x000929C4
		private ECFieldElement solveQuadradicEquation(ECFieldElement beta)
		{
			if (beta.ToBigInteger().SignValue == 0)
			{
				return this.FromBigInteger(BigInteger.Zero);
			}
			ECFieldElement ecfieldElement = null;
			ECFieldElement ecfieldElement2 = this.FromBigInteger(BigInteger.Zero);
			while (ecfieldElement2.ToBigInteger().SignValue == 0)
			{
				ECFieldElement b = this.FromBigInteger(new BigInteger(this.m, new Random()));
				ecfieldElement = this.FromBigInteger(BigInteger.Zero);
				ECFieldElement ecfieldElement3 = beta;
				for (int i = 1; i <= this.m - 1; i++)
				{
					ECFieldElement ecfieldElement4 = ecfieldElement3.Square();
					ecfieldElement = ecfieldElement.Square().Add(ecfieldElement4.Multiply(b));
					ecfieldElement3 = ecfieldElement4.Add(beta);
				}
				if (ecfieldElement3.ToBigInteger().SignValue != 0)
				{
					return null;
				}
				ecfieldElement2 = ecfieldElement.Square().Add(ecfieldElement);
			}
			return ecfieldElement;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00093A88 File Offset: 0x00092A88
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			F2mCurve f2mCurve = obj as F2mCurve;
			return f2mCurve != null && this.Equals(f2mCurve);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00093AB0 File Offset: 0x00092AB0
		protected bool Equals(F2mCurve other)
		{
			return this.m == other.m && this.k1 == other.k1 && this.k2 == other.k2 && this.k3 == other.k3 && base.Equals(other);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00093AFE File Offset: 0x00092AFE
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.m ^ this.k1 ^ this.k2 ^ this.k3;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00093B22 File Offset: 0x00092B22
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00093B2A File Offset: 0x00092B2A
		public bool IsTrinomial()
		{
			return this.k2 == 0 && this.k3 == 0;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00093B3F File Offset: 0x00092B3F
		public int K1
		{
			get
			{
				return this.k1;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00093B47 File Offset: 0x00092B47
		public int K2
		{
			get
			{
				return this.k2;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00093B4F File Offset: 0x00092B4F
		public int K3
		{
			get
			{
				return this.k3;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00093B57 File Offset: 0x00092B57
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00093B5F File Offset: 0x00092B5F
		public BigInteger H
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x040010F2 RID: 4338
		private readonly int m;

		// Token: 0x040010F3 RID: 4339
		private readonly int k1;

		// Token: 0x040010F4 RID: 4340
		private readonly int k2;

		// Token: 0x040010F5 RID: 4341
		private readonly int k3;

		// Token: 0x040010F6 RID: 4342
		private readonly BigInteger n;

		// Token: 0x040010F7 RID: 4343
		private readonly BigInteger h;

		// Token: 0x040010F8 RID: 4344
		private readonly F2mPoint infinity;

		// Token: 0x040010F9 RID: 4345
		private sbyte mu;

		// Token: 0x040010FA RID: 4346
		private BigInteger[] si;
	}
}
