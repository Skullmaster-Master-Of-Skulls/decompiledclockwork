using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002EC RID: 748
	internal class Basic128Sha256Rsa15SecurityAlgorithmSuite : Basic128Rsa15SecurityAlgorithmSuite
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005BF93 File Offset: 0x0005A193
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005BF9F File Offset: 0x0005A19F
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005BFAB File Offset: 0x0005A1AB
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0005BFB7 File Offset: 0x0005A1B7
		public override string ToString()
		{
			return "Basic128Sha256Rsa15";
		}
	}
}
