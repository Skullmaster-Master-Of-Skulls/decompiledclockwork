using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000074 RID: 116
	internal class UtilityDictionary
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000F520 File Offset: 0x0000D720
		public UtilityDictionary(ServiceModelDictionary dictionary)
		{
			this.IdAttribute = dictionary.CreateString("Id", 14);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", 51);
			this.Timestamp = dictionary.CreateString("Timestamp", 53);
			this.CreatedElement = dictionary.CreateString("Created", 54);
			this.ExpiresElement = dictionary.CreateString("Expires", 55);
			this.Prefix = dictionary.CreateString("u", 305);
			this.UniqueEndpointHeaderName = dictionary.CreateString("ChannelInstance", 306);
			this.UniqueEndpointHeaderNamespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2005/02/duplex", 307);
		}

		// Token: 0x0400065B RID: 1627
		public XmlDictionaryString IdAttribute;

		// Token: 0x0400065C RID: 1628
		public XmlDictionaryString Namespace;

		// Token: 0x0400065D RID: 1629
		public XmlDictionaryString Timestamp;

		// Token: 0x0400065E RID: 1630
		public XmlDictionaryString CreatedElement;

		// Token: 0x0400065F RID: 1631
		public XmlDictionaryString ExpiresElement;

		// Token: 0x04000660 RID: 1632
		public XmlDictionaryString Prefix;

		// Token: 0x04000661 RID: 1633
		public XmlDictionaryString UniqueEndpointHeaderName;

		// Token: 0x04000662 RID: 1634
		public XmlDictionaryString UniqueEndpointHeaderNamespace;
	}
}
