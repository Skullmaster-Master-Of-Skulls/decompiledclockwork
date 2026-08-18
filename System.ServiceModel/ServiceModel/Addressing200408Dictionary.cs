using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000059 RID: 89
	internal class Addressing200408Dictionary
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		public Addressing200408Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/08/addressing", 105);
			this.Anonymous = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous", 106);
			this.FaultAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/08/addressing/fault", 107);
		}

		// Token: 0x040004F3 RID: 1267
		public XmlDictionaryString Namespace;

		// Token: 0x040004F4 RID: 1268
		public XmlDictionaryString Anonymous;

		// Token: 0x040004F5 RID: 1269
		public XmlDictionaryString FaultAction;
	}
}
