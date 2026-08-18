using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020003D9 RID: 985
	public class DHKdfParameters : IDerivationParameters
	{
		// Token: 0x06002260 RID: 8800 RVA: 0x000D5E21 File Offset: 0x000D4E21
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z) : this(algorithm, keySize, z, null)
		{
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000D5E2D File Offset: 0x000D4E2D
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z, byte[] extraInfo)
		{
			this.algorithm = algorithm;
			this.keySize = keySize;
			this.z = z;
			this.extraInfo = extraInfo;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000D5E52 File Offset: 0x000D4E52
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000D5E5A File Offset: 0x000D4E5A
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000D5E62 File Offset: 0x000D4E62
		public byte[] GetZ()
		{
			return this.z;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000D5E6A File Offset: 0x000D4E6A
		public byte[] GetExtraInfo()
		{
			return this.extraInfo;
		}

		// Token: 0x0400179B RID: 6043
		private readonly DerObjectIdentifier algorithm;

		// Token: 0x0400179C RID: 6044
		private readonly int keySize;

		// Token: 0x0400179D RID: 6045
		private readonly byte[] z;

		// Token: 0x0400179E RID: 6046
		private readonly byte[] extraInfo;
	}
}
