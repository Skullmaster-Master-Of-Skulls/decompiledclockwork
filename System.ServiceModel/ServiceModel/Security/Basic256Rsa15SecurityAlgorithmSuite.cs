using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E4 RID: 740
	internal class Basic256Rsa15SecurityAlgorithmSuite : Basic256SecurityAlgorithmSuite
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005BE2B File Offset: 0x0005A02B
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaV15KeyWrap;
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0005BE37 File Offset: 0x0005A037
		public override string ToString()
		{
			return "Basic256Rsa15";
		}
	}
}
