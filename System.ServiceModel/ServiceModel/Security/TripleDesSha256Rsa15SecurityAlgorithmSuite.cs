using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002ED RID: 749
	internal class TripleDesSha256Rsa15SecurityAlgorithmSuite : TripleDesRsa15SecurityAlgorithmSuite
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0005BFC6 File Offset: 0x0005A1C6
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005BFD2 File Offset: 0x0005A1D2
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0005BFDE File Offset: 0x0005A1DE
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0005BFEA File Offset: 0x0005A1EA
		public override string ToString()
		{
			return "TripleDesSha256Rsa15";
		}
	}
}
