using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.IdentityModel.Security
{
	// Token: 0x02000108 RID: 264
	internal class WSTrust : SecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001F262 File Offset: 0x0001D462
		public WSTrust(KeyInfoSerializer securityTokenSerializer, TrustDictionary serializerDictionary)
		{
			this.securityTokenSerializer = securityTokenSerializer;
			this.serializerDictionary = serializerDictionary;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001F278 File Offset: 0x0001D478
		public TrustDictionary SerializerDictionary
		{
			get
			{
				return this.serializerDictionary;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001F280 File Offset: 0x0001D480
		public override void PopulateTokenEntries(IList<SecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			tokenEntryList.Add(new WSTrust.BinarySecretTokenEntry(this));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001F28E File Offset: 0x0001D48E
		public override void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> keyIdentifierClauseEntries)
		{
			keyIdentifierClauseEntries.Add(new WSTrust.BinarySecretClauseEntry(this));
			keyIdentifierClauseEntries.Add(new WSTrust.GenericXmlSecurityKeyIdentifierClauseEntry(this));
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		protected static bool CheckElement(XmlElement element, string name, string ns, out string value)
		{
			value = null;
			if (element.LocalName != name || element.NamespaceURI != ns)
			{
				return false;
			}
			if (element.FirstChild is XmlText)
			{
				value = ((XmlText)element.FirstChild).Value;
				return true;
			}
			return false;
		}

		// Token: 0x04000AA6 RID: 2726
		private KeyInfoSerializer securityTokenSerializer;

		// Token: 0x04000AA7 RID: 2727
		private TrustDictionary serializerDictionary;

		// Token: 0x0200025A RID: 602
		private class BinarySecretTokenEntry : SecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06001249 RID: 4681 RVA: 0x00050004 File Offset: 0x0004E204
			public BinarySecretTokenEntry(WSTrust parent)
			{
				this.parent = parent;
			}

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x0600124A RID: 4682 RVA: 0x00050013 File Offset: 0x0004E213
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return this.parent.SerializerDictionary.BinarySecret;
				}
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x0600124B RID: 4683 RVA: 0x00050025 File Offset: 0x0004E225
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return this.parent.SerializerDictionary.Namespace;
				}
			}

			// Token: 0x0600124C RID: 4684 RVA: 0x00050037 File Offset: 0x0004E237
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(BinarySecretSecurityToken)
				};
			}

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x0600124D RID: 4685 RVA: 0x00003459 File Offset: 0x00001659
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x0600124E RID: 4686 RVA: 0x00003459 File Offset: 0x00001659
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0400101A RID: 4122
			private WSTrust parent;
		}

		// Token: 0x0200025B RID: 603
		internal class BinarySecretClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x0600124F RID: 4687 RVA: 0x0005004C File Offset: 0x0004E24C
			public BinarySecretClauseEntry(WSTrust parent)
			{
				this.parent = parent;
				this.otherDictionary = null;
				if (parent.SerializerDictionary is TrustDec2005Dictionary)
				{
					this.otherDictionary = parent.securityTokenSerializer.DictionaryManager.TrustFeb2005Dictionary;
				}
				if (parent.SerializerDictionary is TrustFeb2005Dictionary)
				{
					this.otherDictionary = parent.securityTokenSerializer.DictionaryManager.TrustDec2005Dictionary;
				}
				if (this.otherDictionary == null)
				{
					this.otherDictionary = this.parent.SerializerDictionary;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x06001250 RID: 4688 RVA: 0x000500CC File Offset: 0x0004E2CC
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return this.parent.SerializerDictionary.BinarySecret;
				}
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x06001251 RID: 4689 RVA: 0x000500DE File Offset: 0x0004E2DE
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return this.parent.SerializerDictionary.Namespace;
				}
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x000500F0 File Offset: 0x0004E2F0
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				byte[] key = reader.ReadElementContentAsBase64();
				return new BinarySecretKeyIdentifierClause(key, false);
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x0005010B File Offset: 0x0004E30B
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is BinarySecretKeyIdentifierClause;
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x00050116 File Offset: 0x0004E316
			public override bool CanReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(this.LocalName, this.NamespaceUri) || reader.IsStartElement(this.LocalName, this.otherDictionary.Namespace);
			}

			// Token: 0x06001255 RID: 4693 RVA: 0x00050148 File Offset: 0x0004E348
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				BinarySecretKeyIdentifierClause binarySecretKeyIdentifierClause = keyIdentifierClause as BinarySecretKeyIdentifierClause;
				byte[] keyBytes = binarySecretKeyIdentifierClause.GetKeyBytes();
				writer.WriteStartElement(this.parent.SerializerDictionary.Prefix.Value, this.parent.SerializerDictionary.BinarySecret, this.parent.SerializerDictionary.Namespace);
				writer.WriteBase64(keyBytes, 0, keyBytes.Length);
				writer.WriteEndElement();
			}

			// Token: 0x0400101B RID: 4123
			private WSTrust parent;

			// Token: 0x0400101C RID: 4124
			private TrustDictionary otherDictionary;
		}

		// Token: 0x0200025C RID: 604
		internal class GenericXmlSecurityKeyIdentifierClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x06001256 RID: 4694 RVA: 0x000501AF File Offset: 0x0004E3AF
			public GenericXmlSecurityKeyIdentifierClauseEntry(WSTrust parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x06001257 RID: 4695 RVA: 0x00003459 File Offset: 0x00001659
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x06001258 RID: 4696 RVA: 0x00003459 File Offset: 0x00001659
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x00002D09 File Offset: 0x00000F09
			public override bool CanReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				return false;
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00003459 File Offset: 0x00001659
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				return null;
			}

			// Token: 0x0600125B RID: 4699 RVA: 0x000501BE File Offset: 0x0004E3BE
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is GenericXmlSecurityKeyIdentifierClause;
			}

			// Token: 0x0600125C RID: 4700 RVA: 0x000501CC File Offset: 0x0004E3CC
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				GenericXmlSecurityKeyIdentifierClause genericXmlSecurityKeyIdentifierClause = keyIdentifierClause as GenericXmlSecurityKeyIdentifierClause;
				genericXmlSecurityKeyIdentifierClause.ReferenceXml.WriteTo(writer);
			}

			// Token: 0x0400101D RID: 4125
			private WSTrust parent;
		}
	}
}
