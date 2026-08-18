using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E8 RID: 744
	internal class Basic128Sha256SecurityAlgorithmSuite : Basic128SecurityAlgorithmSuite
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0005BEC7 File Offset: 0x0005A0C7
		internal override XmlDictionaryString DefaultDigestAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.Sha256Digest;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0005BED3 File Offset: 0x0005A0D3
		internal override XmlDictionaryString DefaultSymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.HmacSha256Signature;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0005BEDF File Offset: 0x0005A0DF
		internal override XmlDictionaryString DefaultAsymmetricSignatureAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaSha256Signature;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0005BEEB File Offset: 0x0005A0EB
		public override string ToString()
		{
			return "Basic128Sha256";
		}
	}
}
