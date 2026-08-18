using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020005B4 RID: 1460
	public class RsaPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06003248 RID: 12872 RVA: 0x0013887B File Offset: 0x0013787B
		public RsaPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.n = new MPInteger(bcpgIn);
			this.e = new MPInteger(bcpgIn);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x0013889B File Offset: 0x0013789B
		public RsaPublicBcpgKey(BigInteger n, BigInteger e)
		{
			this.n = new MPInteger(n);
			this.e = new MPInteger(e);
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x001388BB File Offset: 0x001378BB
		public BigInteger PublicExponent
		{
			get
			{
				return this.e.Value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x001388C8 File Offset: 0x001378C8
		public BigInteger Modulus
		{
			get
			{
				return this.n.Value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x001388D5 File Offset: 0x001378D5
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x001388DC File Offset: 0x001378DC
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

		// Token: 0x0600324E RID: 12878 RVA: 0x00138908 File Offset: 0x00137908
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.n,
				this.e
			});
		}

		// Token: 0x04002277 RID: 8823
		private readonly MPInteger n;

		// Token: 0x04002278 RID: 8824
		private readonly MPInteger e;
	}
}
