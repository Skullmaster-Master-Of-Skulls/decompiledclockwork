using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002EB RID: 747
	internal class Basic192Sha256Rsa15SecurityAlgorithmSuite : Basic192Rsa15SecurityAlgorithmSuite
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005BF60 File Offset: 0x0005A160
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005BF6C File Offset: 0x0005A16C
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0005BF78 File Offset: 0x0005A178
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0005BF84 File Offset: 0x0005A184
		public override string ToString()
		{
			return "Basic192Sha256Rsa15";
		}
	}
}
