using System;

namespace System.ServiceModel
{
	// Token: 0x0200006B RID: 107
	internal class SecureConversationApr2004Dictionary : SecureConversationDictionary
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000E008 File Offset: 0x0000C208
		public SecureConversationApr2004Dictionary(ServiceModelDictionary dictionary) : base(dictionary)
		{
			this.SecurityContextToken = dictionary.CreateString("SecurityContextToken", 115);
			this.DerivedKeyToken = dictionary.CreateString("DerivedKeyToken", 39);
			this.AlgorithmAttribute = dictionary.CreateString("Algorithm", 8);
			this.Generation = dictionary.CreateString("Generation", 116);
			this.Label = dictionary.CreateString("Label", 117);
			this.Length = dictionary.CreateString("Length", 56);
			this.Nonce = dictionary.CreateString("Nonce", 40);
			this.Offset = dictionary.CreateString("Offset", 118);
			this.Properties = dictionary.CreateString("Properties", 119);
			this.Identifier = dictionary.CreateString("Identifier", 15);
			this.Cookie = dictionary.CreateString("Cookie", 120);
			this.Prefix = dictionary.CreateString("wsc", 121);
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/sc", 122);
			this.DerivedKeyTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/sc/dk", 123);
			this.SecurityContextTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/sc/sct", 124);
			this.SecurityContextTokenReferenceValueType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/sc/sct", 124);
			this.RequestSecurityContextIssuance = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT", 125);
			this.RequestSecurityContextIssuanceResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT", 126);
			this.RenewNeededFaultCode = dictionary.CreateString("RenewNeeded", 127);
			this.BadContextTokenFaultCode = dictionary.CreateString("BadContextToken", 128);
		}
	}
}
