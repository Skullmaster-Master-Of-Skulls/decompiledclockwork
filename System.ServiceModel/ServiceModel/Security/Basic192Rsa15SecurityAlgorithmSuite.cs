using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E3 RID: 739
	internal class Basic192Rsa15SecurityAlgorithmSuite : Basic192SecurityAlgorithmSuite
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0005BE10 File Offset: 0x0005A010
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaV15KeyWrap;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0005BE1C File Offset: 0x0005A01C
		public override string ToString()
		{
			return "Basic192Rsa15";
		}
	}
}
