using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005E RID: 94
	internal class CoordinationExternal10Dictionary
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		public CoordinationExternal10Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor", 356);
			this.CreateCoordinationContextAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContext", 369);
			this.CreateCoordinationContextResponseAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContextResponse", 370);
			this.RegisterAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor/Register", 371);
			this.RegisterResponseAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor/RegisterResponse", 372);
			this.FaultAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wscoor/fault", 373);
		}

		// Token: 0x04000528 RID: 1320
		public XmlDictionaryString Namespace;

		// Token: 0x04000529 RID: 1321
		public XmlDictionaryString CreateCoordinationContextAction;

		// Token: 0x0400052A RID: 1322
		public XmlDictionaryString CreateCoordinationContextResponseAction;

		// Token: 0x0400052B RID: 1323
		public XmlDictionaryString RegisterAction;

		// Token: 0x0400052C RID: 1324
		public XmlDictionaryString RegisterResponseAction;

		// Token: 0x0400052D RID: 1325
		public XmlDictionaryString FaultAction;
	}
}
