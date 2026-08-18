using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000085 RID: 133
	public class IesParameters : ICipherParameters
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00016412 File Offset: 0x00015412
		public IesParameters(byte[] derivation, byte[] encoding, int macKeySize)
		{
			this.derivation = derivation;
			this.encoding = encoding;
			this.macKeySize = macKeySize;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001642F File Offset: 0x0001542F
		public byte[] GetDerivationV()
		{
			return this.derivation;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00016437 File Offset: 0x00015437
		public byte[] GetEncodingV()
		{
			return this.encoding;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001643F File Offset: 0x0001543F
		public int MacKeySize
		{
			get
			{
				return this.macKeySize;
			}
		}

		// Token: 0x0400021E RID: 542
		private byte[] derivation;

		// Token: 0x0400021F RID: 543
		private byte[] encoding;

		// Token: 0x04000220 RID: 544
		private int macKeySize;
	}
}
