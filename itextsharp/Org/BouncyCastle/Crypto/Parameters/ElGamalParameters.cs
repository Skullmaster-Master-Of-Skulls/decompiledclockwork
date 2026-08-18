using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000122 RID: 290
	public class ElGamalParameters : ICipherParameters
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x000380D6 File Offset: 0x000370D6
		public ElGamalParameters(BigInteger p, BigInteger g) : this(p, g, 0)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000380E1 File Offset: 0x000370E1
		public ElGamalParameters(BigInteger p, BigInteger g, int l)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.g = g;
			this.l = l;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0003811A File Offset: 0x0003711A
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00038122 File Offset: 0x00037122
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0003812A File Offset: 0x0003712A
		public int L
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00038134 File Offset: 0x00037134
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalParameters elGamalParameters = obj as ElGamalParameters;
			return elGamalParameters != null && this.Equals(elGamalParameters);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003815A File Offset: 0x0003715A
		protected bool Equals(ElGamalParameters other)
		{
			return this.p.Equals(other.p) && this.g.Equals(other.g) && this.l == other.l;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00038192 File Offset: 0x00037192
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.g.GetHashCode() ^ this.l;
		}

		// Token: 0x04000881 RID: 2177
		private readonly BigInteger p;

		// Token: 0x04000882 RID: 2178
		private readonly BigInteger g;

		// Token: 0x04000883 RID: 2179
		private readonly int l;
	}
}
