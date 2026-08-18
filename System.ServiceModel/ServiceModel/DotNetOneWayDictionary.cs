using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000061 RID: 97
	internal class DotNetOneWayDictionary
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000D72E File Offset: 0x0000B92E
		public DotNetOneWayDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2005/05/routing", 437);
			this.HeaderName = dictionary.CreateString("PacketRoutable", 438);
		}

		// Token: 0x04000542 RID: 1346
		public XmlDictionaryString Namespace;

		// Token: 0x04000543 RID: 1347
		public XmlDictionaryString HeaderName;
	}
}
