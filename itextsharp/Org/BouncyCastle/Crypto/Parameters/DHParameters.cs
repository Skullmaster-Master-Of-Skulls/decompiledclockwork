using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C0 RID: 1216
	public class DHParameters : ICipherParameters
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x000FC860 File Offset: 0x000FB860
		private static int GetDefaultMParam(int lParam)
		{
			if (lParam == 0)
			{
				return 160;
			}
			return Math.Min(lParam, 160);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000FC876 File Offset: 0x000FB876
		public DHParameters(BigInteger p, BigInteger g) : this(p, g, null, 0)
		{
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000FC882 File Offset: 0x000FB882
		public DHParameters(BigInteger p, BigInteger g, BigInteger q) : this(p, g, q, 0)
		{
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000FC88E File Offset: 0x000FB88E
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int l) : this(p, g, q, DHParameters.GetDefaultMParam(l), l, null, null)
		{
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000FC8A4 File Offset: 0x000FB8A4
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l) : this(p, g, q, m, l, null, null)
		{
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000FC8B5 File Offset: 0x000FB8B5
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, BigInteger j, DHValidationParameters validation) : this(p, g, q, 160, 0, j, validation)
		{
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000FC8CC File Offset: 0x000FB8CC
		public DHParameters(BigInteger p, BigInteger g, BigInteger q, int m, int l, BigInteger j, DHValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (!p.TestBit(0))
			{
				throw new ArgumentException("field must be an odd prime", "p");
			}
			if (g.CompareTo(BigInteger.Two) < 0 || g.CompareTo(p.Subtract(BigInteger.Two)) > 0)
			{
				throw new ArgumentException("generator must in the range [2, p - 2]", "g");
			}
			if (q != null && q.BitLength >= p.BitLength)
			{
				throw new ArgumentException("q too big to be a factor of (p-1)", "q");
			}
			if (m >= p.BitLength)
			{
				throw new ArgumentException("m value must be < bitlength of p", "m");
			}
			if (l != 0)
			{
				if (l >= p.BitLength)
				{
					throw new ArgumentException("when l value specified, it must be less than bitlength(p)", "l");
				}
				if (l < m)
				{
					throw new ArgumentException("when l value specified, it may not be less than m value", "l");
				}
			}
			if (j != null && j.CompareTo(BigInteger.Two) < 0)
			{
				throw new ArgumentException("subgroup factor must be >= 2", "j");
			}
			this.p = p;
			this.g = g;
			this.q = q;
			this.m = m;
			this.l = l;
			this.j = j;
			this.validation = validation;
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x000FCA0D File Offset: 0x000FBA0D
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000FCA15 File Offset: 0x000FBA15
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000FCA1D File Offset: 0x000FBA1D
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000FCA25 File Offset: 0x000FBA25
		public BigInteger J
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x000FCA2D File Offset: 0x000FBA2D
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000FCA35 File Offset: 0x000FBA35
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000FCA3D File Offset: 0x000FBA3D
		public DHValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000FCA48 File Offset: 0x000FBA48
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHParameters dhparameters = obj as DHParameters;
			return dhparameters != null && this.Equals(dhparameters);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000FCA6E File Offset: 0x000FBA6E
		protected bool Equals(DHParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && object.Equals(this.q, other.q);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000FCAAC File Offset: 0x000FBAAC
		public override int GetHashCode()
		{
			int num = this.p.GetHashCode() ^ this.g.GetHashCode();
			if (this.q != null)
			{
				num ^= this.q.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001CF2 RID: 7410
		private const int DefaultMinimumLength = 160;

		// Token: 0x04001CF3 RID: 7411
		private readonly BigInteger p;

		// Token: 0x04001CF4 RID: 7412
		private readonly BigInteger g;

		// Token: 0x04001CF5 RID: 7413
		private readonly BigInteger q;

		// Token: 0x04001CF6 RID: 7414
		private readonly BigInteger j;

		// Token: 0x04001CF7 RID: 7415
		private readonly int m;

		// Token: 0x04001CF8 RID: 7416
		private readonly int l;

		// Token: 0x04001CF9 RID: 7417
		private readonly DHValidationParameters validation;
	}
}
