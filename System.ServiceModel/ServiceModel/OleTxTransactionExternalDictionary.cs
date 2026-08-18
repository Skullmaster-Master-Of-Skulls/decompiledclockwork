using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000066 RID: 102
	internal class OleTxTransactionExternalDictionary
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		public OleTxTransactionExternalDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2006/02/tx/oletx", 352);
			this.Prefix = dictionary.CreateString("oletx", 353);
			this.OleTxTransaction = dictionary.CreateString("OleTxTransaction", 354);
			this.PropagationToken = dictionary.CreateString("PropagationToken", 355);
		}

		// Token: 0x0400055F RID: 1375
		public XmlDictionaryString Namespace;

		// Token: 0x04000560 RID: 1376
		public XmlDictionaryString Prefix;

		// Token: 0x04000561 RID: 1377
		public XmlDictionaryString OleTxTransaction;

		// Token: 0x04000562 RID: 1378
		public XmlDictionaryString PropagationToken;
	}
}
