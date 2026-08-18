using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000894 RID: 2196
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		// Token: 0x06004FE7 RID: 20455 RVA: 0x00115FC8 File Offset: 0x00114FC8
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06004FE8 RID: 20456 RVA: 0x00116005 File Offset: 0x00115005
		// (set) Token: 0x06004FE9 RID: 20457 RVA: 0x0011601C File Offset: 0x0011501C
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
			set
			{
				if (value > this.KeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				if (value == 0)
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				if (value < 40)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKS40"));
				}
				if (base.ValidKeySize(value))
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x00116082 File Offset: 0x00115082
		// (set) Token: 0x06004FEB RID: 20459 RVA: 0x0011608A File Offset: 0x0011508A
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				base.KeySize = value;
			}
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x001160AC File Offset: 0x001150AC
		public new static RC2 Create()
		{
			return RC2.Create("System.Security.Cryptography.RC2");
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x001160B8 File Offset: 0x001150B8
		public new static RC2 Create(string AlgName)
		{
			return (RC2)CryptoConfig.CreateFromName(AlgName);
		}

		// Token: 0x04002925 RID: 10533
		protected int EffectiveKeySizeValue;

		// Token: 0x04002926 RID: 10534
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04002927 RID: 10535
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}
