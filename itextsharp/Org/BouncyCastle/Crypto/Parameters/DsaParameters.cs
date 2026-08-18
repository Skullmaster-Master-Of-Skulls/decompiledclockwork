using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000016 RID: 22
	public class DsaParameters : ICipherParameters
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00005972 File Offset: 0x00004972
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g) : this(p, q, g, null)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005980 File Offset: 0x00004980
		public DsaParameters(BigInteger p, BigInteger q, BigInteger g, DsaValidationParameters parameters)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			this.p = p;
			this.q = q;
			this.g = g;
			this.validation = parameters;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000059DA File Offset: 0x000049DA
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000059E2 File Offset: 0x000049E2
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000059EA File Offset: 0x000049EA
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000059F2 File Offset: 0x000049F2
		public DsaValidationParameters ValidationParameters
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000059FC File Offset: 0x000049FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaParameters dsaParameters = obj as DsaParameters;
			return dsaParameters != null && this.Equals(dsaParameters);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005A22 File Offset: 0x00004A22
		protected bool Equals(DsaParameters other)
		{
			return this.p.Equals(other.p) && this.q.Equals(other.q) && this.g.Equals(other.g);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005A5D File Offset: 0x00004A5D
		public override int GetHashCode()
		{
			return this.p.GetHashCode() ^ this.q.GetHashCode() ^ this.g.GetHashCode();
		}

		// Token: 0x04000049 RID: 73
		private readonly BigInteger p;

		// Token: 0x0400004A RID: 74
		private readonly BigInteger q;

		// Token: 0x0400004B RID: 75
		private readonly BigInteger g;

		// Token: 0x0400004C RID: 76
		private readonly DsaValidationParameters validation;
	}
}
