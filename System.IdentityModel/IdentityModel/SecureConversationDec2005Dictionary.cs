using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C7 RID: 199
	internal class SecureConversationDec2005Dictionary : SecureConversationDictionary
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00016600 File Offset: 0x00014800
		public SecureConversationDec2005Dictionary(IdentityModelDictionary dictionary) : base(dictionary)
		{
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
			this.Prefix = dictionary.CreateString("sc", 268);
			this.DerivedKeyTokenType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk", 269);
			this.SecurityContextTokenType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct", 270);
			this.SecurityContextTokenReferenceValueType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct", 270);
			this.RequestSecurityContextIssuance = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT", 271);
			this.RequestSecurityContextIssuanceResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT", 272);
			this.RequestSecurityContextRenew = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Renew", 273);
			this.RequestSecurityContextRenewResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Renew", 274);
			this.RequestSecurityContextClose = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Cancel", 275);
			this.RequestSecurityContextCloseResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Cancel", 276);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512", 277);
			this.DerivedKeyToken = dictionary.CreateString("DerivedKeyToken", 173);
			this.Nonce = dictionary.CreateString("Nonce", 120);
			this.Length = dictionary.CreateString("Length", 174);
			this.Instance = dictionary.CreateString("Instance", 278);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00016834 File Offset: 0x00014A34
		public SecureConversationDec2005Dictionary(IXmlDictionary dictionary) : base(dictionary)
		{
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
			this.Prefix = this.LookupDictionaryString(dictionary, "sc");
			this.DerivedKeyTokenType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk");
			this.SecurityContextTokenType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct");
			this.SecurityContextTokenReferenceValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct");
			this.RequestSecurityContextIssuance = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT");
			this.RequestSecurityContextIssuanceResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT");
			this.RequestSecurityContextRenew = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Renew");
			this.RequestSecurityContextRenewResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Renew");
			this.RequestSecurityContextClose = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Cancel");
			this.RequestSecurityContextCloseResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Cancel");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");
			this.DerivedKeyToken = this.LookupDictionaryString(dictionary, "DerivedKeyToken");
			this.Nonce = this.LookupDictionaryString(dictionary, "Nonce");
			this.Length = this.LookupDictionaryString(dictionary, "Length");
			this.Instance = this.LookupDictionaryString(dictionary, "Instance");
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00016A0C File Offset: 0x00014C0C
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
