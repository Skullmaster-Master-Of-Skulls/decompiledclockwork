using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008A3 RID: 2211
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		// Token: 0x0600506E RID: 20590 RVA: 0x00119DDC File Offset: 0x00118DDC
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00119E1C File Offset: 0x00118E1C
		public new static Rijndael Create()
		{
			return Rijndael.Create("System.Security.Cryptography.Rijndael");
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x00119E28 File Offset: 0x00118E28
		public new static Rijndael Create(string algName)
		{
			return (Rijndael)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04002959 RID: 10585
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x0400295A RID: 10586
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
