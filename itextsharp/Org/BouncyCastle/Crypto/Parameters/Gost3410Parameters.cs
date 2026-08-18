using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000015 RID: 21
	public class Gost3410Parameters : ICipherParameters
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00005864 File Offset: 0x00004864
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a) : this(p, q, a, null)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005870 File Offset: 0x00004870
		public Gost3410Parameters(BigInteger p, BigInteger q, BigInteger a, Gost3410ValidationParameters validation)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			this.p = p;
			this.q = q;
			this.a = a;
			this.validation = validation;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000058CA File Offset: 0x000048CA
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000058D2 File Offset: 0x000048D2
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000058DA File Offset: 0x000048DA
		public BigInteger A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000058E2 File Offset: 0x000048E2
		public Gost3410ValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000058EC File Offset: 0x000048EC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			Gost3410Parameters gost3410Parameters = obj as Gost3410Parameters;
			return gost3410Parameters != null && this.Equals(gost3410Parameters);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005912 File Offset: 0x00004912
		protected bool Equals(Gost3410Parameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.a.Equals(other.a);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000594D File Offset: 0x0000494D
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.a.GetHashCode();
		}

		// Token: 0x04000045 RID: 69
		private readonly BigInteger p;

		// Token: 0x04000046 RID: 70
		private readonly BigInteger q;

		// Token: 0x04000047 RID: 71
		private readonly BigInteger a;

		// Token: 0x04000048 RID: 72
		private readonly Gost3410ValidationParameters validation;
	}
}
