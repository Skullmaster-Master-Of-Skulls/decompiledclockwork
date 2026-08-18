using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E7 RID: 743
	internal class Basic192Sha256SecurityAlgorithmSuite : Basic192SecurityAlgorithmSuite
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0005BE94 File Offset: 0x0005A094
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0005BEA0 File Offset: 0x0005A0A0
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0005BEAC File Offset: 0x0005A0AC
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0005BEB8 File Offset: 0x0005A0B8
		public override string ToString()
		{
			return "Basic192Sha256";
		}
	}
}
