using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C8 RID: 200
	internal class SecureConversationFeb2005Dictionary : SecureConversationDictionary
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00016A44 File Offset: 0x00014C44
		public SecureConversationFeb2005Dictionary(IdentityModelDictionary dictionary) : base(dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc", 172);
			this.DerivedKeyToken = dictionary.CreateString("DerivedKeyToken", 173);
			this.Nonce = dictionary.CreateString("Nonce", 120);
			this.Length = dictionary.CreateString("Length", 174);
			this.SecurityContextToken = dictionary.CreateString("SecurityContextToken", 175);
			this.AlgorithmAttribute = dictionary.CreateString("Algorithm", 0);
			this.Generation = dictionary.CreateString("Generation", 176);
			this.Label = dictionary.CreateString("Label", 177);
			this.Offset = dictionary.CreateString("Offset", 178);
			this.Properties = dictionary.CreateString("Properties", 179);
			this.Identifier = dictionary.CreateString("Identifier", 180);
			this.Cookie = dictionary.CreateString("Cookie", 181);
			this.RenewNeededFaultCode = dictionary.CreateString("RenewNeeded", 182);
			this.BadContextTokenFaultCode = dictionary.CreateString("BadContextToken", 183);
			this.Prefix = dictionary.CreateString("c", 184);
			this.DerivedKeyTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/dk", 185);
			this.SecurityContextTokenType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/sct", 186);
			this.SecurityContextTokenReferenceValueType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/sct", 186);
			this.RequestSecurityContextIssuance = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT", 187);
			this.RequestSecurityContextIssuanceResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT", 188);
			this.RequestSecurityContextRenew = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew", 189);
			this.RequestSecurityContextRenewResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew", 190);
			this.RequestSecurityContextClose = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel", 191);
			this.RequestSecurityContextCloseResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel", 192);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00016C64 File Offset: 0x00014E64
		public SecureConversationFeb2005Dictionary(IXmlDictionary dictionary) : base(dictionary)
		{
			this.Namespace = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/sc");
			this.DerivedKeyToken = this.LookupDictionaryString(dictionary, "DerivedKeyToken");
			this.Nonce = this.LookupDictionaryString(dictionary, "Nonce");
			this.Length = this.LookupDictionaryString(dictionary, "Length");
			this.SecurityContextToken = this.LookupDictionaryString(dictionary, "SecurityContextToken");
			this.AlgorithmAttribute = this.LookupDictionaryString(dictionary, "Algorithm");
			this.Generation = this.LookupDictionaryString(dictionary, "Generation");
			this.Label = this.LookupDictionaryString(dictionary, "Label");
			this.Offset = this.LookupDictionaryString(dictionary, "Offset");
			this.Properties = this.LookupDictionaryString(dictionary, "Properties");
			this.Identifier = this.LookupDictionaryString(dictionary, "Identifier");
			this.Cookie = this.LookupDictionaryString(dictionary, "Cookie");
			this.RenewNeededFaultCode = this.LookupDictionaryString(dictionary, "RenewNeeded");
			this.BadContextTokenFaultCode = this.LookupDictionaryString(dictionary, "BadContextToken");
			this.Prefix = this.LookupDictionaryString(dictionary, "c");
			this.DerivedKeyTokenType = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/sc/dk");
			this.SecurityContextTokenType = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/sc/sct");
			this.SecurityContextTokenReferenceValueType = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/sc/sct");
			this.RequestSecurityContextIssuance = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT");
			this.RequestSecurityContextIssuanceResponse = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT");
			this.RequestSecurityContextRenew = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew");
			this.RequestSecurityContextRenewResponse = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew");
			this.RequestSecurityContextClose = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel");
			this.RequestSecurityContextCloseResponse = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel");
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016E28 File Offset: 0x00015028
		private XmlDictionaryString LookupDictionaryString(IXmlDictionary dictionary, string value)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(value, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("XDCannotFindValueInDictionaryString", new object[]
				{
					value
				}));
			}
			return result;
		}
	}
}
