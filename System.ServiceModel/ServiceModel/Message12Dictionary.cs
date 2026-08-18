using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000065 RID: 101
	internal class Message12Dictionary
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public Message12Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://www.w3.org/2003/05/soap-envelope", 2);
			this.Role = dictionary.CreateString("role", 69);
			this.Relay = dictionary.CreateString("relay", 70);
			this.FaultCode = dictionary.CreateString("Code", 71);
			this.FaultReason = dictionary.CreateString("Reason", 72);
			this.FaultText = dictionary.CreateString("Text", 73);
			this.FaultNode = dictionary.CreateString("Node", 74);
			this.FaultRole = dictionary.CreateString("Role", 75);
			this.FaultDetail = dictionary.CreateString("Detail", 76);
			this.FaultValue = dictionary.CreateString("Value", 77);
			this.FaultSubcode = dictionary.CreateString("Subcode", 78);
			this.NotUnderstood = dictionary.CreateString("NotUnderstood", 79);
			this.QName = dictionary.CreateString("qname", 80);
		}

		// Token: 0x04000552 RID: 1362
		public XmlDictionaryString Namespace;

		// Token: 0x04000553 RID: 1363
		public XmlDictionaryString Role;

		// Token: 0x04000554 RID: 1364
		public XmlDictionaryString Relay;

		// Token: 0x04000555 RID: 1365
		public XmlDictionaryString FaultCode;

		// Token: 0x04000556 RID: 1366
		public XmlDictionaryString FaultReason;

		// Token: 0x04000557 RID: 1367
		public XmlDictionaryString FaultText;

		// Token: 0x04000558 RID: 1368
		public XmlDictionaryString FaultNode;

		// Token: 0x04000559 RID: 1369
		public XmlDictionaryString FaultRole;

		// Token: 0x0400055A RID: 1370
		public XmlDictionaryString FaultDetail;

		// Token: 0x0400055B RID: 1371
		public XmlDictionaryString FaultValue;

		// Token: 0x0400055C RID: 1372
		public XmlDictionaryString FaultSubcode;

		// Token: 0x0400055D RID: 1373
		public XmlDictionaryString NotUnderstood;

		// Token: 0x0400055E RID: 1374
		public XmlDictionaryString QName;
	}
}
