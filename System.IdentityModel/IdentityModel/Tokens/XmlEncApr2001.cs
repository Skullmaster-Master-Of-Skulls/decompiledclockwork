using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200019C RID: 412
	internal class XmlEncApr2001 : SecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0003EC45 File Offset: 0x0003CE45
		public XmlEncApr2001(KeyInfoSerializer securityTokenSerializer)
		{
			this.securityTokenSerializer = securityTokenSerializer;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0003EC54 File Offset: 0x0003CE54
		public override void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> keyIdentifierClauseEntries)
		{
			keyIdentifierClauseEntries.Add(new XmlEncApr2001.EncryptedKeyClauseEntry(this.securityTokenSerializer));
		}

		// Token: 0x04000CCC RID: 3276
		private KeyInfoSerializer securityTokenSerializer;

		// Token: 0x0200028E RID: 654
		internal class EncryptedKeyClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x06001340 RID: 4928 RVA: 0x000523A7 File Offset: 0x000505A7
			public EncryptedKeyClauseEntry(KeyInfoSerializer securityTokenSerializer)
			{
				this.securityTokenSerializer = securityTokenSerializer;
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x06001341 RID: 4929 RVA: 0x000523B6 File Offset: 0x000505B6
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.XmlEncryptionDictionary.EncryptedKey;
				}
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x06001342 RID: 4930 RVA: 0x000509E4 File Offset: 0x0004EBE4
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlEncryptionDictionary.Namespace;
				}
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x000523C4 File Offset: 0x000505C4
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				string encryptionMethod = null;
				string carriedKeyName = null;
				SecurityKeyIdentifier encryptingKeyIdentifier = null;
				reader.ReadStartElement(XD.XmlEncryptionDictionary.EncryptedKey, this.NamespaceUri);
				if (reader.IsStartElement(XD.XmlEncryptionDictionary.EncryptionMethod, this.NamespaceUri))
				{
					encryptionMethod = reader.GetAttribute(XD.XmlEncryptionDictionary.AlgorithmAttribute, null);
					bool isEmptyElement = reader.IsEmptyElement;
					reader.ReadStartElement();
					if (!isEmptyElement)
					{
						while (reader.IsStartElement())
						{
							reader.Skip();
						}
						reader.ReadEndElement();
					}
				}
				if (this.securityTokenSerializer.CanReadKeyIdentifier(reader))
				{
					encryptingKeyIdentifier = this.securityTokenSerializer.ReadKeyIdentifier(reader);
				}
				reader.ReadStartElement(XD.XmlEncryptionDictionary.CipherData, this.NamespaceUri);
				reader.ReadStartElement(XD.XmlEncryptionDictionary.CipherValue, this.NamespaceUri);
				byte[] encryptedKey = reader.ReadContentAsBase64();
				reader.ReadEndElement();
				reader.ReadEndElement();
				if (reader.IsStartElement(XD.XmlEncryptionDictionary.CarriedKeyName, this.NamespaceUri))
				{
					reader.ReadStartElement();
					carriedKeyName = reader.ReadString();
					reader.ReadEndElement();
				}
				reader.ReadEndElement();
				return new EncryptedKeyIdentifierClause(encryptedKey, encryptionMethod, encryptingKeyIdentifier, carriedKeyName);
			}

			// Token: 0x06001344 RID: 4932 RVA: 0x000524D5 File Offset: 0x000506D5
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is EncryptedKeyIdentifierClause;
			}

			// Token: 0x06001345 RID: 4933 RVA: 0x000524E0 File Offset: 0x000506E0
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifierClause as EncryptedKeyIdentifierClause;
				writer.WriteStartElement(XD.XmlEncryptionDictionary.Prefix.Value, XD.XmlEncryptionDictionary.EncryptedKey, this.NamespaceUri);
				if (encryptedKeyIdentifierClause.EncryptionMethod != null)
				{
					writer.WriteStartElement(XD.XmlEncryptionDictionary.Prefix.Value, XD.XmlEncryptionDictionary.EncryptionMethod, this.NamespaceUri);
					writer.WriteAttributeString(XD.XmlEncryptionDictionary.AlgorithmAttribute, null, encryptedKeyIdentifierClause.EncryptionMethod);
					if (encryptedKeyIdentifierClause.EncryptionMethod == XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap.Value)
					{
						writer.WriteStartElement("", XD.XmlSignatureDictionary.DigestMethod, XD.XmlSignatureDictionary.Namespace);
						writer.WriteAttributeString(XD.XmlSignatureDictionary.Algorithm, null, "http://www.w3.org/2000/09/xmldsig#sha1");
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				if (encryptedKeyIdentifierClause.EncryptingKeyIdentifier != null)
				{
					this.securityTokenSerializer.WriteKeyIdentifier(writer, encryptedKeyIdentifierClause.EncryptingKeyIdentifier);
				}
				writer.WriteStartElement(XD.XmlEncryptionDictionary.Prefix.Value, XD.XmlEncryptionDictionary.CipherData, this.NamespaceUri);
				writer.WriteStartElement(XD.XmlEncryptionDictionary.Prefix.Value, XD.XmlEncryptionDictionary.CipherValue, this.NamespaceUri);
				byte[] encryptedKey = encryptedKeyIdentifierClause.GetEncryptedKey();
				writer.WriteBase64(encryptedKey, 0, encryptedKey.Length);
				writer.WriteEndElement();
				writer.WriteEndElement();
				if (encryptedKeyIdentifierClause.CarriedKeyName != null)
				{
					writer.WriteElementString(XD.XmlEncryptionDictionary.Prefix.Value, XD.XmlEncryptionDictionary.CarriedKeyName, this.NamespaceUri, encryptedKeyIdentifierClause.CarriedKeyName);
				}
				writer.WriteEndElement();
			}

			// Token: 0x0400112C RID: 4396
			private KeyInfoSerializer securityTokenSerializer;
		}
	}
}
