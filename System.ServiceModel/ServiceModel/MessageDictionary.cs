using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000064 RID: 100
	internal class MessageDictionary
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		public MessageDictionary(ServiceModelDictionary dictionary)
		{
			this.MustUnderstand = dictionary.CreateString("mustUnderstand", 0);
			this.Envelope = dictionary.CreateString("Envelope", 1);
			this.Header = dictionary.CreateString("Header", 4);
			this.Body = dictionary.CreateString("Body", 7);
			this.Prefix = dictionary.CreateString("s", 66);
			this.Fault = dictionary.CreateString("Fault", 67);
			this.MustUnderstandFault = dictionary.CreateString("MustUnderstand", 68);
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2005/05/envelope/none", 440);
		}

		// Token: 0x0400054A RID: 1354
		public XmlDictionaryString MustUnderstand;

		// Token: 0x0400054B RID: 1355
		public XmlDictionaryString Envelope;

		// Token: 0x0400054C RID: 1356
		public XmlDictionaryString Header;

		// Token: 0x0400054D RID: 1357
		public XmlDictionaryString Body;

		// Token: 0x0400054E RID: 1358
		public XmlDictionaryString Prefix;

		// Token: 0x0400054F RID: 1359
		public XmlDictionaryString Fault;

		// Token: 0x04000550 RID: 1360
		public XmlDictionaryString MustUnderstandFault;

		// Token: 0x04000551 RID: 1361
		public XmlDictionaryString Namespace;
	}
}
