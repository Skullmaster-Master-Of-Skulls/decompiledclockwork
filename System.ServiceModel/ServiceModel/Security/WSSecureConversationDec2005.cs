using System;
using System.Collections.Generic;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028B RID: 651
	internal class WSSecureConversationDec2005 : WSSecureConversation
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x000443D4 File Offset: 0x000425D4
		public WSSecureConversationDec2005(WSSecurityTokenSerializer tokenSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maxKeyDerivationOffset, int maxKeyDerivationLabelLength, int maxKeyDerivationNonceLength) : base(tokenSerializer, maxKeyDerivationOffset, maxKeyDerivationLabelLength, maxKeyDerivationNonceLength)
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00044454 File Offset: 0x00042654
		public override SecureConversationDictionary SerializerDictionary
		{
			get
			{
				return DXD.SecureConversationDec2005Dictionary;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004445B File Offset: 0x0004265B
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			base.PopulateTokenEntries(tokenEntryList);
			tokenEntryList.Add(new WSSecureConversationDec2005.SecurityContextTokenEntryDec2005(this, this.securityStateEncoder, this.knownClaimTypes));
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0004447C File Offset: 0x0004267C
		public override string DerivationAlgorithm
		{
			get
			{
				return "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
			}
		}

		// Token: 0x04001A0D RID: 6669
		private SecurityStateEncoder securityStateEncoder;

		// Token: 0x04001A0E RID: 6670
		private IList<Type> knownClaimTypes;

		// Token: 0x02000B27 RID: 2855
		private class SecurityContextTokenEntryDec2005 : WSSecureConversation.SecurityContextTokenEntry
		{
			// Token: 0x06006FEB RID: 28651 RVA: 0x0019F6FD File Offset: 0x0019D8FD
			public SecurityContextTokenEntryDec2005(WSSecureConversationDec2005 parent, SecurityStateEncoder securityStateEncoder, IList<Type> knownClaimTypes) : base(parent, securityStateEncoder, knownClaimTypes)
			{
			}

			// Token: 0x06006FEC RID: 28652 RVA: 0x0019F708 File Offset: 0x0019D908
			protected override bool CanReadGeneration(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(DXD.SecureConversationDec2005Dictionary.Instance, DXD.SecureConversationDec2005Dictionary.Namespace);
			}

			// Token: 0x06006FED RID: 28653 RVA: 0x0019F724 File Offset: 0x0019D924
			protected override bool CanReadGeneration(XmlElement element)
			{
				return element.LocalName == DXD.SecureConversationDec2005Dictionary.Instance.Value && element.NamespaceURI == DXD.SecureConversationDec2005Dictionary.Namespace.Value;
			}

			// Token: 0x06006FEE RID: 28654 RVA: 0x0019F75E File Offset: 0x0019D95E
			protected override UniqueId ReadGeneration(XmlDictionaryReader reader)
			{
				return reader.ReadElementContentAsUniqueId();
			}

			// Token: 0x06006FEF RID: 28655 RVA: 0x0019F766 File Offset: 0x0019D966
			protected override UniqueId ReadGeneration(XmlElement element)
			{
				return XmlHelper.ReadTextElementAsUniqueId(element);
			}

			// Token: 0x06006FF0 RID: 28656 RVA: 0x0019F770 File Offset: 0x0019D970
			protected override void WriteGeneration(XmlDictionaryWriter writer, SecurityContextSecurityToken sct)
			{
				if (sct.KeyGeneration != null)
				{
					writer.WriteStartElement(DXD.SecureConversationDec2005Dictionary.Prefix.Value, DXD.SecureConversationDec2005Dictionary.Instance, DXD.SecureConversationDec2005Dictionary.Namespace);
					XmlHelper.WriteStringAsUniqueId(writer, sct.KeyGeneration);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x02000B28 RID: 2856
		public class DriverDec2005 : WSSecureConversation.Driver
		{
			// Token: 0x17001A1E RID: 6686
			// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x0019F7CE File Offset: 0x0019D9CE
			protected override SecureConversationDictionary DriverDictionary
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary;
				}
			}

			// Token: 0x17001A1F RID: 6687
			// (get) Token: 0x06006FF3 RID: 28659 RVA: 0x0019F7D5 File Offset: 0x0019D9D5
			public override XmlDictionaryString CloseAction
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.RequestSecurityContextClose;
				}
			}

			// Token: 0x17001A20 RID: 6688
			// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x0019F7E1 File Offset: 0x0019D9E1
			public override XmlDictionaryString CloseResponseAction
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.RequestSecurityContextCloseResponse;
				}
			}

			// Token: 0x17001A21 RID: 6689
			// (get) Token: 0x06006FF5 RID: 28661 RVA: 0x0019F7ED File Offset: 0x0019D9ED
			public override bool IsSessionSupported
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001A22 RID: 6690
			// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x0019F7F0 File Offset: 0x0019D9F0
			public override XmlDictionaryString RenewAction
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.RequestSecurityContextRenew;
				}
			}

			// Token: 0x17001A23 RID: 6691
			// (get) Token: 0x06006FF7 RID: 28663 RVA: 0x0019F7FC File Offset: 0x0019D9FC
			public override XmlDictionaryString RenewResponseAction
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.RequestSecurityContextRenewResponse;
				}
			}

			// Token: 0x17001A24 RID: 6692
			// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x0019F808 File Offset: 0x0019DA08
			public override XmlDictionaryString Namespace
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.Namespace;
				}
			}

			// Token: 0x17001A25 RID: 6693
			// (get) Token: 0x06006FF9 RID: 28665 RVA: 0x0019F814 File Offset: 0x0019DA14
			public override string TokenTypeUri
			{
				get
				{
					return DXD.SecureConversationDec2005Dictionary.SecurityContextTokenType.Value;
				}
			}
		}
	}
}
