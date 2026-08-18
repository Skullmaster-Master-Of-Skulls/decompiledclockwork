using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002E5 RID: 741
	internal class TripleDesRsa15SecurityAlgorithmSuite : TripleDesSecurityAlgorithmSuite
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005BE46 File Offset: 0x0005A046
		internal override XmlDictionaryString DefaultAsymmetricKeyWrapAlgorithmDictionaryString
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.RsaV15KeyWrap;
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0005BE52 File Offset: 0x0005A052
		public override string ToString()
		{
			return "TripleDesRsa15";
		}
	}
}
