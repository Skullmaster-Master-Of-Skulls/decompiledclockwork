using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200006C RID: 108
	internal class SecureConversationFeb2005Dictionary : SecureConversationDictionary
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000E19C File Offset: 0x0000C39C
		public SecureConversationFeb2005Dictionary(ServiceModelDictionary dictionary) : base(dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc", 38);
			this.DerivedKeyToken = dictionary.CreateString("DerivedKeyToken", 39);
			this.Nonce = dictionary.CreateString("Nonce", 40);
			this.Length = dictionary.CreateString("Length", 56);
			this.SecurityContextToken = dictionary.CreateString("SecurityContextToken", 115);
			this.AlgorithmAttribute = dictionary.CreateString("Algorithm", 8);
			this.Generation = dictionary.CreateString("Generation", 116);
			this.Label = dictionary.CreateString("Label", 117);
			this.Offset = dictionary.CreateString("Offset", 118);
			this.Properties = dictionary.CreateString("Properties", 119);
			this.Identifier = dictionary.CreateString("Identifier", 15);
			this.Cookie = dictionary.CreateString("Cookie", 120);
			this.RenewNeededFaultCode = dictionary.CreateString("RenewNeeded", 127);
			this.BadContextTokenFaultCode = dictionary.CreateString("BadContextToken", 128);
			this.Prefix = dictionary.CreateString("c", 129);
			this.DerivedKeyTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/dk", 130);
			this.SecurityContextTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/sct", 131);
			this.SecurityContextTokenReferenceValueType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/sct", 131);
			this.RequestSecurityContextIssuance = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT", 132);
			this.RequestSecurityContextIssuanceResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT", 133);
			this.RequestSecurityContextRenew = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew", 134);
			this.RequestSecurityContextRenewResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew", 135);
			this.RequestSecurityContextClose = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel", 136);
			this.RequestSecurityContextCloseResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel", 137);
		}

		// Token: 0x040005B9 RID: 1465
		public XmlDictionaryString RequestSecurityContextRenew;

		// Token: 0x040005BA RID: 1466
		public XmlDictionaryString RequestSecurityContextRenewResponse;

		// Token: 0x040005BB RID: 1467
		public XmlDictionaryString RequestSecurityContextClose;

		// Token: 0x040005BC RID: 1468
		public XmlDictionaryString RequestSecurityContextCloseResponse;
	}
}
