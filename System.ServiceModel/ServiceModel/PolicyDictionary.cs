using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000068 RID: 104
	internal class PolicyDictionary
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000DADF File Offset: 0x0000BCDF
		public PolicyDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2002/12/policy", 428);
		}

		// Token: 0x0400056B RID: 1387
		public XmlDictionaryString Namespace;
	}
}
