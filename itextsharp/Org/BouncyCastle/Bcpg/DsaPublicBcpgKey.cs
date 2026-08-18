using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000303 RID: 771
	public class DsaPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001C32 RID: 7218 RVA: 0x000A8B41 File Offset: 0x000A7B41
		public DsaPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.p = new MPInteger(bcpgIn);
			this.q = new MPInteger(bcpgIn);
			this.g = new MPInteger(bcpgIn);
			this.y = new MPInteger(bcpgIn);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000A8B79 File Offset: 0x000A7B79
		public DsaPublicBcpgKey(BigInteger p, BigInteger q, BigInteger g, BigInteger y)
		{
			this.p = new MPInteger(p);
			this.q = new MPInteger(q);
			this.g = new MPInteger(g);
			this.y = new MPInteger(y);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x000A8BB2 File Offset: 0x000A7BB2
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000A8BBC File Offset: 0x000A7BBC
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

		// Token: 0x06001C36 RID: 7222 RVA: 0x000A8BE8 File Offset: 0x000A7BE8
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.p,
				this.q,
				this.g,
				this.y
			});
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x000A8C27 File Offset: 0x000A7C27
		public BigInteger G
		{
			get
			{
				return this.g.Value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000A8C34 File Offset: 0x000A7C34
		public BigInteger P
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000A8C41 File Offset: 0x000A7C41
		public BigInteger Q
		{
			get
			{
				return this.q.Value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000A8C4E File Offset: 0x000A7C4E
		public BigInteger Y
		{
			get
			{
				return this.y.Value;
			}
		}

		// Token: 0x04001359 RID: 4953
		private readonly MPInteger p;

		// Token: 0x0400135A RID: 4954
		private readonly MPInteger q;

		// Token: 0x0400135B RID: 4955
		private readonly MPInteger g;

		// Token: 0x0400135C RID: 4956
		private readonly MPInteger y;
	}
}
