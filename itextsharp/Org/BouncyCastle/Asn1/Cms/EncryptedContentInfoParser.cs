using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000493 RID: 1171
	public class EncryptedContentInfoParser
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x000EE7B4 File Offset: 0x000ED7B4
		public EncryptedContentInfoParser(Asn1SequenceParser seq)
		{
			this._contentType = (DerObjectIdentifier)seq.ReadObject();
			this._contentEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encryptedContent = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000EE7F4 File Offset: 0x000ED7F4
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000EE7FC File Offset: 0x000ED7FC
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this._contentEncryptionAlgorithm;
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000EE804 File Offset: 0x000ED804
		public IAsn1Convertible GetEncryptedContent(int tag)
		{
			return this._encryptedContent.GetObjectParser(tag, false);
		}

		// Token: 0x04001B36 RID: 6966
		private DerObjectIdentifier _contentType;

		// Token: 0x04001B37 RID: 6967
		private AlgorithmIdentifier _contentEncryptionAlgorithm;

		// Token: 0x04001B38 RID: 6968
		private Asn1TaggedObjectParser _encryptedContent;
	}
}
