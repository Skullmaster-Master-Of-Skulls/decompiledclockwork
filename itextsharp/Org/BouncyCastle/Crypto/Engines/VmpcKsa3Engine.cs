using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000617 RID: 1559
	public class VmpcKsa3Engine : VmpcEngine
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x0014972D File Offset: 0x0014872D
		public override string AlgorithmName
		{
			get
			{
				return "VMPC-KSA3";
			}
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x00149734 File Offset: 0x00148734
		protected override void InitKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			for (int l = 0; l < 768; l++)
			{
				this.s = this.P[(int)(this.s + this.P[l & 255] + keyBytes[l % keyBytes.Length] & byte.MaxValue)];
				byte b3 = this.P[l & 255];
				this.P[l & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			this.n = 0;
		}
	}
}
