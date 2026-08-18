using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005F RID: 95
	internal class DotNetAddressingDictionary
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000D55C File Offset: 0x0000B75C
		public DotNetAddressingDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/06/addressingex", 108);
			this.RedirectTo = dictionary.CreateString("RedirectTo", 109);
			this.Via = dictionary.CreateString("Via", 110);
		}

		// Token: 0x0400052E RID: 1326
		public XmlDictionaryString Namespace;

		// Token: 0x0400052F RID: 1327
		public XmlDictionaryString RedirectTo;

		// Token: 0x04000530 RID: 1328
		public XmlDictionaryString Via;
	}
}
