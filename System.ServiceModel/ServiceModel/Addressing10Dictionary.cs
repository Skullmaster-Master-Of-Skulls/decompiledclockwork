using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000058 RID: 88
	internal class Addressing10Dictionary
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000CF64 File Offset: 0x0000B164
		public Addressing10Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2005/08/addressing", 3);
			this.Anonymous = dictionary.CreateString("http://www.w3.org/2005/08/addressing/anonymous", 10);
			this.FaultAction = dictionary.CreateString("http://www.w3.org/2005/08/addressing/fault", 99);
			this.ReplyRelationship = dictionary.CreateString("http://www.w3.org/2005/08/addressing/reply", 102);
			this.NoneAddress = dictionary.CreateString("http://www.w3.org/2005/08/addressing/none", 103);
			this.Metadata = dictionary.CreateString("Metadata", 104);
		}

		// Token: 0x040004ED RID: 1261
		public XmlDictionaryString Namespace;

		// Token: 0x040004EE RID: 1262
		public XmlDictionaryString Anonymous;

		// Token: 0x040004EF RID: 1263
		public XmlDictionaryString FaultAction;

		// Token: 0x040004F0 RID: 1264
		public XmlDictionaryString ReplyRelationship;

		// Token: 0x040004F1 RID: 1265
		public XmlDictionaryString NoneAddress;

		// Token: 0x040004F2 RID: 1266
		public XmlDictionaryString Metadata;
	}
}
