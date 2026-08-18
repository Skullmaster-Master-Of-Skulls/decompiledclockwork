using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005A RID: 90
	internal class AddressingNoneDictionary
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000D034 File Offset: 0x0000B234
		public AddressingNoneDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2005/05/addressing/none", 439);
		}

		// Token: 0x040004F6 RID: 1270
		public XmlDictionaryString Namespace;
	}
}
