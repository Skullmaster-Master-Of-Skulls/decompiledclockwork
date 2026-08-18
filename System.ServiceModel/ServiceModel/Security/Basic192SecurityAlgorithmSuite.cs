using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DF RID: 735
	public class Basic192SecurityAlgorithmSuite : SecurityAlgorithmSuite
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0005BAF2 File Offset: 0x00059CF2
		public override string DefaultCanonicalizationAlgorithm
		{
			get
			{
				return this.DefaultCanonicalizationAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005BAFF File Offset: 0x00059CFF
		public override string DefaultDigestAlgorithm
		{
			get
			{
				return this.DefaultDigestAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0005BB0C File Offset: 0x00059D0C
		public override string DefaultEncryptionAlgorithm
		{
			get
			{
				return this.DefaultEncryptionAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005BB19 File Offset: 0x00059D19
		public override int DefaultEncryptionKeyDerivationLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0005BB20 File Offset: 0x00059D20
		public override string DefaultSymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultSymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0005BB2D File Offset: 0x00059D2D
		public override string DefaultAsymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005BB3A File Offset: 0x00059D3A
		public override string DefaultSymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultSymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0005BB47 File Offset: 0x00059D47
		public override string DefaultAsymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005BB54 File Offset: 0x00059D54
		public override int DefaultSignatureKeyDerivationLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0005BB5B File Offset: 0x00059D5B
		public override int DefaultSymmetricKeyLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0005BB62 File Offset: 0x00059D62
		public override bool IsSymmetricKeyLengthSupported(int length)
		{
			return length >= 192 && length <= 256;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0005BB79 File Offset: 0x00059D79
		public override bool IsAsymmetricKeyLengthSupported(int length)
		{
			return length >= 1024 && length <= 4096;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0005BB90 File Offset: 0x00059D90
		internal override XmlDictionaryString DefaultCanonicalizationAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.ExclusiveC14n;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0005BB9C File Offset: 0x00059D9C
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha1Digest;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0005BBA8 File Offset: 0x00059DA8
		internal override XmlDictionaryString DefaultEncryptionAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes192Encryption;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		internal override XmlDictionaryString DefaultSymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Aes192KeyWrap;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0005BBCC File Offset: 0x00059DCC
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha1Signature;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha1Signature;
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005BBE4 File Offset: 0x00059DE4
		public override string ToString()
		{
			return "Basic192";
		}
	}
}
