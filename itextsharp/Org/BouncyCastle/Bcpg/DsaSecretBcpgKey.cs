using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000480 RID: 1152
	public class DsaSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x0600270F RID: 9999 RVA: 0x000EC8BF File Offset: 0x000EB8BF
		public DsaSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.x = new MPInteger(bcpgIn);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000EC8D3 File Offset: 0x000EB8D3
		public DsaSecretBcpgKey(BigInteger x)
		{
			this.x = new MPInteger(x);
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x000EC8E7 File Offset: 0x000EB8E7
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000EC8F0 File Offset: 0x000EB8F0
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

		// Token: 0x06002713 RID: 10003 RVA: 0x000EC91C File Offset: 0x000EB91C
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObject(this.x);
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x000EC92A File Offset: 0x000EB92A
		public BigInteger X
		{
			get
			{
				return this.x.Value;
			}
		}

		// Token: 0x04001ADA RID: 6874
		internal MPInteger x;
	}
}
