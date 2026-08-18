using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000078 RID: 120
	internal class Message11Dictionary
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public Message11Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/soap/envelope/", 481);
			this.Actor = dictionary.CreateString("actor", 482);
			this.FaultCode = dictionary.CreateString("faultcode", 483);
			this.FaultString = dictionary.CreateString("faultstring", 484);
			this.FaultActor = dictionary.CreateString("faultactor", 485);
			this.FaultDetail = dictionary.CreateString("detail", 486);
			this.FaultNamespace = dictionary.CreateString("", 81);
		}

		// Token: 0x040006B8 RID: 1720
		public XmlDictionaryString Namespace;

		// Token: 0x040006B9 RID: 1721
		public XmlDictionaryString Actor;

		// Token: 0x040006BA RID: 1722
		public XmlDictionaryString FaultCode;

		// Token: 0x040006BB RID: 1723
		public XmlDictionaryString FaultString;

		// Token: 0x040006BC RID: 1724
		public XmlDictionaryString FaultActor;

		// Token: 0x040006BD RID: 1725
		public XmlDictionaryString FaultDetail;

		// Token: 0x040006BE RID: 1726
		public XmlDictionaryString FaultNamespace;
	}
}
