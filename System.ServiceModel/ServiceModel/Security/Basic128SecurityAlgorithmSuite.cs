using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E0 RID: 736
	public class Basic128SecurityAlgorithmSuite : SecurityAlgorithmSuite
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0005BBF3 File Offset: 0x00059DF3
		public override string DefaultCanonicalizationAlgorithm
		{
			get
			{
				return this.DefaultCanonicalizationAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0005BC00 File Offset: 0x00059E00
		public override string DefaultDigestAlgorithm
		{
			get
			{
				return this.DefaultDigestAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005BC0D File Offset: 0x00059E0D
		public override string DefaultEncryptionAlgorithm
		{
			get
			{
				return this.DefaultEncryptionAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0005BC1A File Offset: 0x00059E1A
		public override int DefaultEncryptionKeyDerivationLength
		{
			get
			{
				return 128;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005BC21 File Offset: 0x00059E21
		public override string DefaultSymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultSymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0005BC2E File Offset: 0x00059E2E
		public override string DefaultAsymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0005BC3B File Offset: 0x00059E3B
		public override string DefaultSymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultSymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0005BC48 File Offset: 0x00059E48
		public override string DefaultAsymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0005BC55 File Offset: 0x00059E55
		public override int DefaultSignatureKeyDerivationLength
		{
			get
			{
				return 128;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0005BC5C File Offset: 0x00059E5C
		public override int DefaultSymmetricKeyLength
		{
			get
			{
				return 128;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0005BC63 File Offset: 0x00059E63
		public override bool IsSymmetricKeyLengthSupported(int length)
		{
			return length >= 128 && length <= 256;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0005BC7A File Offset: 0x00059E7A
		public override bool IsAsymmetricKeyLengthSupported(int length)
		{
			return length >= 1024 && length <= 4096;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0005BC91 File Offset: 0x00059E91
		internal override XmlDictionaryString DefaultCanonicalizationAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.ExclusiveC14n;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005BC9D File Offset: 0x00059E9D
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha1Digest;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0005BCA9 File Offset: 0x00059EA9
		internal override XmlDictionaryString DefaultEncryptionAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes128Encryption;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005BCB5 File Offset: 0x00059EB5
		internal override XmlDictionaryString DefaultSymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes128KeyWrap;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0005BCC1 File Offset: 0x00059EC1
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0005BCCD File Offset: 0x00059ECD
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha1Signature;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0005BCD9 File Offset: 0x00059ED9
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha1Signature;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0005BCE5 File Offset: 0x00059EE5
		public override string ToString()
		{
			return "Basic128";
		}
	}
}
