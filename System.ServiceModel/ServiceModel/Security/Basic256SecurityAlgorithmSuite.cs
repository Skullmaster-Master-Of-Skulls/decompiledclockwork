using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DE RID: 734
	public class Basic256SecurityAlgorithmSuite : SecurityAlgorithmSuite
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0005B9FE File Offset: 0x00059BFE
		public override string DefaultCanonicalizationAlgorithm
		{
			get
			{
				return this.DefaultCanonicalizationAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0005BA0B File Offset: 0x00059C0B
		public override string DefaultDigestAlgorithm
		{
			get
			{
				return this.DefaultDigestAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0005BA18 File Offset: 0x00059C18
		public override string DefaultEncryptionAlgorithm
		{
			get
			{
				return this.DefaultEncryptionAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005BA25 File Offset: 0x00059C25
		public override int DefaultEncryptionKeyDerivationLength
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005BA2C File Offset: 0x00059C2C
		public override string DefaultSymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultSymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005BA39 File Offset: 0x00059C39
		public override string DefaultAsymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005BA46 File Offset: 0x00059C46
		public override string DefaultSymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultSymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0005BA53 File Offset: 0x00059C53
		public override string DefaultAsymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0005BA60 File Offset: 0x00059C60
		public override int DefaultSignatureKeyDerivationLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005BA67 File Offset: 0x00059C67
		public override int DefaultSymmetricKeyLength
		{
			get
			{
				return 256;
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0005BA6E File Offset: 0x00059C6E
		public override bool IsSymmetricKeyLengthSupported(int length)
		{
			return length == 256;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0005BA78 File Offset: 0x00059C78
		public override bool IsAsymmetricKeyLengthSupported(int length)
		{
			return length >= 1024 && length <= 4096;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005BA8F File Offset: 0x00059C8F
		internal override XmlDictionaryString DefaultCanonicalizationAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.ExclusiveC14n;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0005BA9B File Offset: 0x00059C9B
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha1Digest;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0005BAA7 File Offset: 0x00059CA7
		internal override XmlDictionaryString DefaultEncryptionAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes256Encryption;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0005BAB3 File Offset: 0x00059CB3
		internal override XmlDictionaryString DefaultSymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes256KeyWrap;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005BABF File Offset: 0x00059CBF
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0005BACB File Offset: 0x00059CCB
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha1Signature;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0005BAD7 File Offset: 0x00059CD7
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha1Signature;
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0005BAE3 File Offset: 0x00059CE3
		public override string ToString()
		{
			return "Basic256";
		}
	}
}
