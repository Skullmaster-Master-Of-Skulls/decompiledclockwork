using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002EA RID: 746
	internal class Basic256Sha256Rsa15SecurityAlgorithmSuite : Basic256Rsa15SecurityAlgorithmSuite
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005BF2D File Offset: 0x0005A12D
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005BF39 File Offset: 0x0005A139
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005BF45 File Offset: 0x0005A145
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005BF51 File Offset: 0x0005A151
		public override string ToString()
		{
			return "Basic256Sha256Rsa15";
		}
	}
}
