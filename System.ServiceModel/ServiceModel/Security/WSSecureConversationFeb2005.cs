using System;
using System.Collections.Generic;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028C RID: 652
	internal class WSSecureConversationFeb2005 : WSSecureConversation
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x00044484 File Offset: 0x00042684
		public WSSecureConversationFeb2005(WSSecurityTokenSerializer tokenSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maxKeyDerivationOffset, int maxKeyDerivationLabelLength, int maxKeyDerivationNonceLength) : base(tokenSerializer, maxKeyDerivationOffset, maxKeyDerivationLabelLength, maxKeyDerivationNonceLength)
		{
			if (securityStateEncoder != null)
			{
				this.securityStateEncoder = securityStateEncoder;
			}
			else
			{
				this.securityStateEncoder = new DataProtectionSecurityStateEncoder();
			}
			this.knownClaimTypes = new List<Type>();
			if (knownTypes != null)
			{
				foreach (Type item in knownTypes)
				{
					this.knownClaimTypes.Add(item);
				}
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00044504 File Offset: 0x00042704
		public override SecureConversationDictionary SerializerDictionary
		{
			get
			{
				return XD.SecureConversationFeb2005Dictionary;
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004450B File Offset: 0x0004270B
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			base.PopulateTokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecureConversationFeb2005.SecurityContextTokenEntryFeb2005(this, this.securityStateEncoder, this.knownClaimTypes));
		}

		// Token: 0x04001A0F RID: 6671
		private SecurityStateEncoder securityStateEncoder;

		// Token: 0x04001A10 RID: 6672
		private IList<Type> knownClaimTypes;

		// Token: 0x02000B29 RID: 2857
		private class SecurityContextTokenEntryFeb2005 : WSSecureConversation.SecurityContextTokenEntry
		{
			// Token: 0x06006FFA RID: 28666 RVA: 0x0019F825 File Offset: 0x0019DA25
			public SecurityContextTokenEntryFeb2005(WSSecureConversationFeb2005 parent, SecurityStateEncoder securityStateEncoder, IList<Type> knownClaimTypes) : base(parent, securityStateEncoder, knownClaimTypes)
			{
			}

			// Token: 0x06006FFB RID: 28667 RVA: 0x0019F830 File Offset: 0x0019DA30
			protected override bool CanReadGeneration(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(DXD.SecureConversationDec2005Dictionary.Instance, XD.SecureConversationFeb2005Dictionary.Namespace);
			}

			// Token: 0x06006FFC RID: 28668 RVA: 0x0019F84C File Offset: 0x0019DA4C
			protected override bool CanReadGeneration(XmlElement element)
			{
				return element.LocalName == DXD.SecureConversationDec2005Dictionary.Instance.Value && element.NamespaceURI == XD.SecureConversationFeb2005Dictionary.Namespace.Value;
			}

			// Token: 0x06006FFD RID: 28669 RVA: 0x0019F886 File Offset: 0x0019DA86
			protected override UniqueId ReadGeneration(XmlDictionaryReader reader)
			{
				return reader.ReadElementContentAsUniqueId();
			}

			// Token: 0x06006FFE RID: 28670 RVA: 0x0019F88E File Offset: 0x0019DA8E
			protected override UniqueId ReadGeneration(XmlElement element)
			{
				return XmlHelper.ReadTextElementAsUniqueId(element);
			}

			// Token: 0x06006FFF RID: 28671 RVA: 0x0019F898 File Offset: 0x0019DA98
			protected override void WriteGeneration(XmlDictionaryWriter writer, SecurityContextSecurityToken sct)
			{
				if (sct.KeyGeneration != null)
				{
					writer.WriteStartElement(XD.SecureConversationFeb2005Dictionary.Prefix.Value, DXD.SecureConversationDec2005Dictionary.Instance, XD.SecureConversationFeb2005Dictionary.Namespace);
					XmlHelper.WriteStringAsUniqueId(writer, sct.KeyGeneration);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x02000B2A RID: 2858
		public class DriverFeb2005 : WSSecureConversation.Driver
		{
			// Token: 0x17001A26 RID: 6694
			// (get) Token: 0x06007001 RID: 28673 RVA: 0x0019F8F6 File Offset: 0x0019DAF6
			protected override SecureConversationDictionary DriverDictionary
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary;
				}
			}

			// Token: 0x17001A27 RID: 6695
			// (get) Token: 0x06007002 RID: 28674 RVA: 0x0019F8FD File Offset: 0x0019DAFD
			public override XmlDictionaryString CloseAction
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.RequestSecurityContextClose;
				}
			}

			// Token: 0x17001A28 RID: 6696
			// (get) Token: 0x06007003 RID: 28675 RVA: 0x0019F909 File Offset: 0x0019DB09
			public override XmlDictionaryString CloseResponseAction
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.RequestSecurityContextCloseResponse;
				}
			}

			// Token: 0x17001A29 RID: 6697
			// (get) Token: 0x06007004 RID: 28676 RVA: 0x0019F915 File Offset: 0x0019DB15
			public override bool IsSessionSupported
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001A2A RID: 6698
			// (get) Token: 0x06007005 RID: 28677 RVA: 0x0019F918 File Offset: 0x0019DB18
			public override XmlDictionaryString RenewAction
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.RequestSecurityContextRenew;
				}
			}

			// Token: 0x17001A2B RID: 6699
			// (get) Token: 0x06007006 RID: 28678 RVA: 0x0019F924 File Offset: 0x0019DB24
			public override XmlDictionaryString RenewResponseAction
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.RequestSecurityContextRenewResponse;
				}
			}

			// Token: 0x17001A2C RID: 6700
			// (get) Token: 0x06007007 RID: 28679 RVA: 0x0019F930 File Offset: 0x0019DB30
			public override XmlDictionaryString Namespace
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.Namespace;
				}
			}

			// Token: 0x17001A2D RID: 6701
			// (get) Token: 0x06007008 RID: 28680 RVA: 0x0019F93C File Offset: 0x0019DB3C
			public override string TokenTypeUri
			{
				get
				{
					return XD.SecureConversationFeb2005Dictionary.SecurityContextTokenType.Value;
				}
			}
		}
	}
}
