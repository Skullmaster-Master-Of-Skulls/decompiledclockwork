using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E1 RID: 737
	public class TripleDesSecurityAlgorithmSuite : SecurityAlgorithmSuite
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005BCF4 File Offset: 0x00059EF4
		public override string DefaultCanonicalizationAlgorithm
		{
			get
			{
				return this.DefaultCanonicalizationAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0005BD01 File Offset: 0x00059F01
		public override string DefaultDigestAlgorithm
		{
			get
			{
				return this.DefaultDigestAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0005BD0E File Offset: 0x00059F0E
		public override string DefaultEncryptionAlgorithm
		{
			get
			{
				return this.DefaultEncryptionAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0005BD1B File Offset: 0x00059F1B
		public override int DefaultEncryptionKeyDerivationLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0005BD22 File Offset: 0x00059F22
		public override string DefaultSymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultSymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0005BD2F File Offset: 0x00059F2F
		public override string DefaultAsymmetricKeyWrapAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricKeyWrapAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005BD3C File Offset: 0x00059F3C
		public override string DefaultSymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultSymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0005BD49 File Offset: 0x00059F49
		public override string DefaultAsymmetricSignatureAlgorithm
		{
			get
			{
				return this.DefaultAsymmetricSignatureAlgorithmDictionaryString.Value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0005BD56 File Offset: 0x00059F56
		public override int DefaultSignatureKeyDerivationLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0005BD5D File Offset: 0x00059F5D
		public override int DefaultSymmetricKeyLength
		{
			get
			{
				return 192;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0005BD64 File Offset: 0x00059F64
		public override bool IsSymmetricKeyLengthSupported(int length)
		{
			return length >= 192 && length <= 256;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0005BD7B File Offset: 0x00059F7B
		public override bool IsAsymmetricKeyLengthSupported(int length)
		{
			return length >= 1024 && length <= 4096;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0005BD92 File Offset: 0x00059F92
		internal override XmlDictionaryString DefaultCanonicalizationAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.ExclusiveC14n;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0005BD9E File Offset: 0x00059F9E
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha1Digest;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0005BDAA File Offset: 0x00059FAA
		internal override XmlDictionaryString DefaultEncryptionAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.TripleDesEncryption;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005BDB6 File Offset: 0x00059FB6
		internal override XmlDictionaryString DefaultSymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.TripleDesKeyWrap;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0005BDC2 File Offset: 0x00059FC2
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005BDCE File Offset: 0x00059FCE
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha1Signature;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0005BDDA File Offset: 0x00059FDA
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha1Signature;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0005BDE6 File Offset: 0x00059FE6
		public override string ToString()
		{
			return "TripleDes";
		}
	}
}
