using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008B3 RID: 2227
	[ComVisible(true)]
	public class SignatureDescription
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x001223E9 File Offset: 0x001213E9
		public SignatureDescription()
		{
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x001223F4 File Offset: 0x001213F4
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x00122459 File Offset: 0x00121459
		// (set) Token: 0x060050EC RID: 20716 RVA: 0x00122461 File Offset: 0x00121461
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x0012246A File Offset: 0x0012146A
		// (set) Token: 0x060050EE RID: 20718 RVA: 0x00122472 File Offset: 0x00121472
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x060050EF RID: 20719 RVA: 0x0012247B File Offset: 0x0012147B
		// (set) Token: 0x060050F0 RID: 20720 RVA: 0x00122483 File Offset: 0x00121483
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0012248C File Offset: 0x0012148C
		// (set) Token: 0x060050F2 RID: 20722 RVA: 0x00122494 File Offset: 0x00121494
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x001224A0 File Offset: 0x001214A0
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x001224C8 File Offset: 0x001214C8
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x001224EE File Offset: 0x001214EE
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x0400298A RID: 10634
		private string _strKey;

		// Token: 0x0400298B RID: 10635
		private string _strDigest;

		// Token: 0x0400298C RID: 10636
		private string _strFormatter;

		// Token: 0x0400298D RID: 10637
		private string _strDeformatter;
	}
}
