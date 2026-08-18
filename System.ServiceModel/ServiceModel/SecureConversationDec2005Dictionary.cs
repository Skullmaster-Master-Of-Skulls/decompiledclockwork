using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000040 RID: 64
	internal class SecureConversationDec2005Dictionary : SecureConversationDictionary
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00009910 File Offset: 0x00007B10
		public SecureConversationDec2005Dictionary(XmlDictionary dictionary)
		{
			this.SecurityContextToken = dictionary.Add("SecurityContextToken");
			this.AlgorithmAttribute = dictionary.Add("Algorithm");
			this.Generation = dictionary.Add("Generation");
			this.Label = dictionary.Add("Label");
			this.Offset = dictionary.Add("Offset");
			this.Properties = dictionary.Add("Properties");
			this.Identifier = dictionary.Add("Identifier");
			this.Cookie = dictionary.Add("Cookie");
			this.RenewNeededFaultCode = dictionary.Add("RenewNeeded");
			this.BadContextTokenFaultCode = dictionary.Add("BadContextToken");
			this.Prefix = dictionary.Add("sc");
			this.DerivedKeyTokenType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk");
			this.SecurityContextTokenType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct");
			this.SecurityContextTokenReferenceValueType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct");
			this.RequestSecurityContextIssuance = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT");
			this.RequestSecurityContextIssuanceResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT");
			this.RequestSecurityContextRenew = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Renew");
			this.RequestSecurityContextRenewResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Renew");
			this.RequestSecurityContextClose = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/SCT/Cancel");
			this.RequestSecurityContextCloseResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/SCT/Cancel");
			this.Namespace = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");
			this.DerivedKeyToken = dictionary.Add("DerivedKeyToken");
			this.Nonce = dictionary.Add("Nonce");
			this.Length = dictionary.Add("Length");
			this.Instance = dictionary.Add("Instance");
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public void PopulateSecureConversationDec2005()
		{
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.SecurityContextToken);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.AlgorithmAttribute);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Generation);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Label);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Offset);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Properties);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Identifier);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Cookie);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RenewNeededFaultCode);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.BadContextTokenFaultCode);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Prefix);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.DerivedKeyTokenType);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.SecurityContextTokenType);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.SecurityContextTokenReferenceValueType);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextIssuance);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextIssuanceResponse);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextRenew);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextRenewResponse);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextClose);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.RequestSecurityContextCloseResponse);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Namespace);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.DerivedKeyToken);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Nonce);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Length);
			this.SecureConversationDictionaryStrings.Add(DXD.SecureConversationDec2005Dictionary.Instance);
		}

		// Token: 0x040001D7 RID: 471
		public XmlDictionaryString RequestSecurityContextRenew;

		// Token: 0x040001D8 RID: 472
		public XmlDictionaryString RequestSecurityContextRenewResponse;

		// Token: 0x040001D9 RID: 473
		public XmlDictionaryString RequestSecurityContextClose;

		// Token: 0x040001DA RID: 474
		public XmlDictionaryString RequestSecurityContextCloseResponse;

		// Token: 0x040001DB RID: 475
		public XmlDictionaryString Instance;

		// Token: 0x040001DC RID: 476
		public List<XmlDictionaryString> SecureConversationDictionaryStrings = new List<XmlDictionaryString>();
	}
}
