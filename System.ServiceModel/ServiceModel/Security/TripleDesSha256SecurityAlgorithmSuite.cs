using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E9 RID: 745
	internal class TripleDesSha256SecurityAlgorithmSuite : TripleDesSecurityAlgorithmSuite
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0005BEFA File Offset: 0x0005A0FA
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0005BF06 File Offset: 0x0005A106
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0005BF12 File Offset: 0x0005A112
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005BF1E File Offset: 0x0005A11E
		public override string ToString()
		{
			return "TripleDesSha256";
		}
	}
}
