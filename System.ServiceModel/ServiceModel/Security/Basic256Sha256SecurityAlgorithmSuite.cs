using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E6 RID: 742
	internal class Basic256Sha256SecurityAlgorithmSuite : Basic256SecurityAlgorithmSuite
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0005BE61 File Offset: 0x0005A061
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0005BE6D File Offset: 0x0005A06D
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0005BE79 File Offset: 0x0005A079
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0005BE85 File Offset: 0x0005A085
		public override string ToString()
		{
			return "Basic256Sha256";
		}
	}
}
