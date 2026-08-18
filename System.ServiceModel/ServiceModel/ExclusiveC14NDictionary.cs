using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000063 RID: 99
	internal class ExclusiveC14NDictionary
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000D798 File Offset: 0x0000B998
		public ExclusiveC14NDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#", 111);
			this.PrefixList = dictionary.CreateString("PrefixList", 112);
			this.InclusiveNamespaces = dictionary.CreateString("InclusiveNamespaces", 113);
			this.Prefix = dictionary.CreateString("ec", 114);
		}

		// Token: 0x04000546 RID: 1350
		public XmlDictionaryString Namespace;

		// Token: 0x04000547 RID: 1351
		public XmlDictionaryString PrefixList;

		// Token: 0x04000548 RID: 1352
		public XmlDictionaryString InclusiveNamespaces;

		// Token: 0x04000549 RID: 1353
		public XmlDictionaryString Prefix;
	}
}
