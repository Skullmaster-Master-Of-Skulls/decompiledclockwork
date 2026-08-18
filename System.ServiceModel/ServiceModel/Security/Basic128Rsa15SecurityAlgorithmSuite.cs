using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E2 RID: 738
	internal class Basic128Rsa15SecurityAlgorithmSuite : Basic128SecurityAlgorithmSuite
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005BDF5 File Offset: 0x00059FF5
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaV15KeyWrap;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0005BE01 File Offset: 0x0005A001
		public override string ToString()
		{
			return "Basic128Rsa15";
		}
	}
}
