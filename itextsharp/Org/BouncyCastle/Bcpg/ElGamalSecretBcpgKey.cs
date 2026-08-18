using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020003E6 RID: 998
	public class ElGamalSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x060022A6 RID: 8870 RVA: 0x000D6BF7 File Offset: 0x000D5BF7
		public ElGamalSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.x = new MPInteger(bcpgIn);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000D6C0B File Offset: 0x000D5C0B
		public ElGamalSecretBcpgKey(BigInteger x)
		{
			this.x = new MPInteger(x);
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000D6C1F File Offset: 0x000D5C1F
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000D6C26 File Offset: 0x000D5C26
		public BigInteger X
		{
			get
			{
				return this.x.Value;
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000D6C34 File Offset: 0x000D5C34
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

		// Token: 0x060022AB RID: 8875 RVA: 0x000D6C60 File Offset: 0x000D5C60
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObject(this.x);
		}

		// Token: 0x040017B7 RID: 6071
		internal MPInteger x;
	}
}
