using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000057 RID: 87
	internal class AddressingDictionary
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public AddressingDictionary(ServiceModelDictionary dictionary)
		{
			this.Action = dictionary.CreateString("Action", 5);
			this.To = dictionary.CreateString("To", 6);
			this.RelatesTo = dictionary.CreateString("RelatesTo", 9);
			this.MessageId = dictionary.CreateString("MessageID", 13);
			this.Address = dictionary.CreateString("Address", 21);
			this.ReplyTo = dictionary.CreateString("ReplyTo", 22);
			this.Empty = dictionary.CreateString("", 81);
			this.From = dictionary.CreateString("From", 82);
			this.FaultTo = dictionary.CreateString("FaultTo", 83);
			this.EndpointReference = dictionary.CreateString("EndpointReference", 84);
			this.PortType = dictionary.CreateString("PortType", 85);
			this.ServiceName = dictionary.CreateString("ServiceName", 86);
			this.PortName = dictionary.CreateString("PortName", 87);
			this.ReferenceProperties = dictionary.CreateString("ReferenceProperties", 88);
			this.RelationshipType = dictionary.CreateString("RelationshipType", 89);
			this.Reply = dictionary.CreateString("Reply", 90);
			this.Prefix = dictionary.CreateString("a", 91);
			this.IdentityExtensionNamespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2006/02/addressingidentity", 92);
			this.Identity = dictionary.CreateString("Identity", 93);
			this.Spn = dictionary.CreateString("Spn", 94);
			this.Upn = dictionary.CreateString("Upn", 95);
			this.Rsa = dictionary.CreateString("Rsa", 96);
			this.Dns = dictionary.CreateString("Dns", 97);
			this.X509v3Certificate = dictionary.CreateString("X509v3Certificate", 98);
			this.ReferenceParameters = dictionary.CreateString("ReferenceParameters", 100);
			this.IsReferenceParameter = dictionary.CreateString("IsReferenceParameter", 101);
		}

		// Token: 0x040004D3 RID: 1235
		public XmlDictionaryString Action;

		// Token: 0x040004D4 RID: 1236
		public XmlDictionaryString To;

		// Token: 0x040004D5 RID: 1237
		public XmlDictionaryString RelatesTo;

		// Token: 0x040004D6 RID: 1238
		public XmlDictionaryString MessageId;

		// Token: 0x040004D7 RID: 1239
		public XmlDictionaryString Address;

		// Token: 0x040004D8 RID: 1240
		public XmlDictionaryString ReplyTo;

		// Token: 0x040004D9 RID: 1241
		public XmlDictionaryString Empty;

		// Token: 0x040004DA RID: 1242
		public XmlDictionaryString From;

		// Token: 0x040004DB RID: 1243
		public XmlDictionaryString FaultTo;

		// Token: 0x040004DC RID: 1244
		public XmlDictionaryString EndpointReference;

		// Token: 0x040004DD RID: 1245
		public XmlDictionaryString PortType;

		// Token: 0x040004DE RID: 1246
		public XmlDictionaryString ServiceName;

		// Token: 0x040004DF RID: 1247
		public XmlDictionaryString PortName;

		// Token: 0x040004E0 RID: 1248
		public XmlDictionaryString ReferenceProperties;

		// Token: 0x040004E1 RID: 1249
		public XmlDictionaryString RelationshipType;

		// Token: 0x040004E2 RID: 1250
		public XmlDictionaryString Reply;

		// Token: 0x040004E3 RID: 1251
		public XmlDictionaryString Prefix;

		// Token: 0x040004E4 RID: 1252
		public XmlDictionaryString IdentityExtensionNamespace;

		// Token: 0x040004E5 RID: 1253
		public XmlDictionaryString Identity;

		// Token: 0x040004E6 RID: 1254
		public XmlDictionaryString Spn;

		// Token: 0x040004E7 RID: 1255
		public XmlDictionaryString Upn;

		// Token: 0x040004E8 RID: 1256
		public XmlDictionaryString Rsa;

		// Token: 0x040004E9 RID: 1257
		public XmlDictionaryString Dns;

		// Token: 0x040004EA RID: 1258
		public XmlDictionaryString X509v3Certificate;

		// Token: 0x040004EB RID: 1259
		public XmlDictionaryString ReferenceParameters;

		// Token: 0x040004EC RID: 1260
		public XmlDictionaryString IsReferenceParameter;
	}
}
