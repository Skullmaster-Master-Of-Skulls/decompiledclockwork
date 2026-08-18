using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000098 RID: 152
	public class RsaSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001A7F8 File Offset: 0x000197F8
		public RsaSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.d = new MPInteger(bcpgIn);
			this.p = new MPInteger(bcpgIn);
			this.q = new MPInteger(bcpgIn);
			this.u = new MPInteger(bcpgIn);
			this.expP = this.d.Value.Remainder(this.p.Value.Subtract(BigInteger.One));
			this.expQ = this.d.Value.Remainder(this.q.Value.Subtract(BigInteger.One));
			this.crt = this.q.Value.ModInverse(this.p.Value);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001A8B4 File Offset: 0x000198B4
		public RsaSecretBcpgKey(BigInteger d, BigInteger p, BigInteger q)
		{
			int num = p.CompareTo(q);
			if (num >= 0)
			{
				if (num == 0)
				{
					throw new ArgumentException("p and q cannot be equal");
				}
				BigInteger bigInteger = p;
				p = q;
				q = bigInteger;
			}
			this.d = new MPInteger(d);
			this.p = new MPInteger(p);
			this.q = new MPInteger(q);
			this.u = new MPInteger(p.ModInverse(q));
			this.expP = d.Remainder(p.Subtract(BigInteger.One));
			this.expQ = d.Remainder(q.Subtract(BigInteger.One));
			this.crt = q.ModInverse(p);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001A95A File Offset: 0x0001995A
		public BigInteger Modulus
		{
			get
			{
				return this.p.Value.Multiply(this.q.Value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001A977 File Offset: 0x00019977
		public BigInteger PrivateExponent
		{
			get
			{
				return this.d.Value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001A984 File Offset: 0x00019984
		public BigInteger PrimeP
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001A991 File Offset: 0x00019991
		public BigInteger PrimeQ
		{
			get
			{
				return this.q.Value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001A99E File Offset: 0x0001999E
		public BigInteger PrimeExponentP
		{
			get
			{
				return this.expP;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001A9A6 File Offset: 0x000199A6
		public BigInteger PrimeExponentQ
		{
			get
			{
				return this.expQ;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001A9AE File Offset: 0x000199AE
		public BigInteger CrtCoefficient
		{
			get
			{
				return this.crt;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001A9B6 File Offset: 0x000199B6
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001A9C0 File Offset: 0x000199C0
		public override byte[] GetEncoded()
		{
			byte[] result;
			try
			{
				result = base.GetEncoded();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001A9EC File Offset: 0x000199EC
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.d,
				this.p,
				this.q,
				this.u
			});
		}

		// Token: 0x0400026D RID: 621
		private readonly MPInteger d;

		// Token: 0x0400026E RID: 622
		private readonly MPInteger p;

		// Token: 0x0400026F RID: 623
		private readonly MPInteger q;

		// Token: 0x04000270 RID: 624
		private readonly MPInteger u;

		// Token: 0x04000271 RID: 625
		private readonly BigInteger expP;

		// Token: 0x04000272 RID: 626
		private readonly BigInteger expQ;

		// Token: 0x04000273 RID: 627
		private readonly BigInteger crt;
	}
}
