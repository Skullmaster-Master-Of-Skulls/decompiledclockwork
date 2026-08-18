using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200019B RID: 411
	internal class XmlDsigSep2000 : SecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x0003EC00 File Offset: 0x0003CE00
		public XmlDsigSep2000(KeyInfoSerializer securityTokenSerializer)
		{
			this.securityTokenSerializer = securityTokenSerializer;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003EC0F File Offset: 0x0003CE0F
		public override void PopulateKeyIdentifierEntries(IList<SecurityTokenSerializer.KeyIdentifierEntry> keyIdentifierEntries)
		{
			keyIdentifierEntries.Add(new XmlDsigSep2000.KeyInfoEntry(this.securityTokenSerializer));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003EC22 File Offset: 0x0003CE22
		public override void PopulateKeyIdentifierClauseEntries(IList<SecurityTokenSerializer.KeyIdentifierClauseEntry> keyIdentifierClauseEntries)
		{
			keyIdentifierClauseEntries.Add(new XmlDsigSep2000.KeyNameClauseEntry());
			keyIdentifierClauseEntries.Add(new XmlDsigSep2000.KeyValueClauseEntry());
			keyIdentifierClauseEntries.Add(new XmlDsigSep2000.X509CertificateClauseEntry());
		}

		// Token: 0x04000CCB RID: 3275
		private KeyInfoSerializer securityTokenSerializer;

		// Token: 0x0200028A RID: 650
		internal class KeyInfoEntry : SecurityTokenSerializer.KeyIdentifierEntry
		{
			// Token: 0x06001328 RID: 4904 RVA: 0x00051D16 File Offset: 0x0004FF16
			public KeyInfoEntry(KeyInfoSerializer securityTokenSerializer)
			{
				this.securityTokenSerializer = securityTokenSerializer;
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x06001329 RID: 4905 RVA: 0x00051D25 File Offset: 0x0004FF25
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.XmlSignatureDictionary.KeyInfo;
				}
			}

			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x0600132A RID: 4906 RVA: 0x00051D31 File Offset: 0x0004FF31
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlSignatureDictionary.Namespace;
				}
			}

			// Token: 0x0600132B RID: 4907 RVA: 0x00051D40 File Offset: 0x0004FF40
			public override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlDictionaryReader reader)
			{
				reader.ReadStartElement(this.LocalName, this.NamespaceUri);
				SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier();
				while (reader.IsStartElement())
				{
					SecurityKeyIdentifierClause securityKeyIdentifierClause = this.securityTokenSerializer.ReadKeyIdentifierClause(reader);
					if (securityKeyIdentifierClause == null)
					{
						reader.Skip();
					}
					else
					{
						securityKeyIdentifier.Add(securityKeyIdentifierClause);
					}
				}
				if (securityKeyIdentifier.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorDeserializingKeyIdentifierClause")));
				}
				reader.ReadEndElement();
				return securityKeyIdentifier;
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x00002434 File Offset: 0x00000634
			public override bool SupportsCore(SecurityKeyIdentifier keyIdentifier)
			{
				return true;
			}

			// Token: 0x0600132D RID: 4909 RVA: 0x00051DB8 File Offset: 0x0004FFB8
			public override void WriteKeyIdentifierCore(XmlDictionaryWriter writer, SecurityKeyIdentifier keyIdentifier)
			{
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, this.LocalName, this.NamespaceUri);
				bool flag = false;
				foreach (SecurityKeyIdentifierClause keyIdentifierClause in keyIdentifier)
				{
					this.securityTokenSerializer.InnerSecurityTokenSerializer.WriteKeyIdentifierClause(writer, keyIdentifierClause);
					flag = true;
				}
				writer.WriteEndElement();
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("NoKeyInfoClausesToWrite")));
				}
			}

			// Token: 0x0400112B RID: 4395
			private KeyInfoSerializer securityTokenSerializer;
		}

		// Token: 0x0200028B RID: 651
		internal class KeyNameClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x0600132E RID: 4910 RVA: 0x00051E54 File Offset: 0x00050054
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.XmlSignatureDictionary.KeyName;
				}
			}

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x0600132F RID: 4911 RVA: 0x00051D31 File Offset: 0x0004FF31
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlSignatureDictionary.Namespace;
				}
			}

			// Token: 0x06001330 RID: 4912 RVA: 0x00051E60 File Offset: 0x00050060
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				reader.ReadStartElement(XD.XmlSignatureDictionary.KeyName, this.NamespaceUri);
				string keyName = reader.ReadString();
				reader.ReadEndElement();
				return new KeyNameIdentifierClause(keyName);
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x00051E96 File Offset: 0x00050096
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is KeyNameIdentifierClause;
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x00051EA4 File Offset: 0x000500A4
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				KeyNameIdentifierClause keyNameIdentifierClause = keyIdentifierClause as KeyNameIdentifierClause;
				writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.KeyName, this.NamespaceUri, keyNameIdentifierClause.KeyName);
			}
		}

		// Token: 0x0200028C RID: 652
		internal class KeyValueClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x00051EEB File Offset: 0x000500EB
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.XmlSignatureDictionary.KeyValue;
				}
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x06001335 RID: 4917 RVA: 0x00051D31 File Offset: 0x0004FF31
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlSignatureDictionary.Namespace;
				}
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x00051EF8 File Offset: 0x000500F8
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				reader.ReadStartElement(XD.XmlSignatureDictionary.KeyValue, this.NamespaceUri);
				reader.ReadStartElement(XD.XmlSignatureDictionary.RsaKeyValue, this.NamespaceUri);
				reader.ReadStartElement(XD.XmlSignatureDictionary.Modulus, this.NamespaceUri);
				byte[] modulus = Convert.FromBase64String(reader.ReadString());
				reader.ReadEndElement();
				reader.ReadStartElement(XD.XmlSignatureDictionary.Exponent, this.NamespaceUri);
				byte[] exponent = Convert.FromBase64String(reader.ReadString());
				reader.ReadEndElement();
				reader.ReadEndElement();
				reader.ReadEndElement();
				RSA rsa = new RSACryptoServiceProvider();
				rsa.ImportParameters(new RSAParameters
				{
					Modulus = modulus,
					Exponent = exponent
				});
				return new RsaKeyIdentifierClause(rsa);
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x00051FB8 File Offset: 0x000501B8
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is RsaKeyIdentifierClause;
			}

			// Token: 0x06001338 RID: 4920 RVA: 0x00051FC4 File Offset: 0x000501C4
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				RsaKeyIdentifierClause rsaKeyIdentifierClause = keyIdentifierClause as RsaKeyIdentifierClause;
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.KeyValue, this.NamespaceUri);
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.RsaKeyValue, this.NamespaceUri);
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.Modulus, this.NamespaceUri);
				rsaKeyIdentifierClause.WriteModulusAsBase64(writer);
				writer.WriteEndElement();
				writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.Exponent, this.NamespaceUri);
				rsaKeyIdentifierClause.WriteExponentAsBase64(writer);
				writer.WriteEndElement();
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x0200028D RID: 653
		internal class X509CertificateClauseEntry : SecurityTokenSerializer.KeyIdentifierClauseEntry
		{
			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x0600133A RID: 4922 RVA: 0x00052092 File Offset: 0x00050292
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return XD.XmlSignatureDictionary.X509Data;
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x0600133B RID: 4923 RVA: 0x00051D31 File Offset: 0x0004FF31
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return XD.XmlSignatureDictionary.Namespace;
				}
			}

			// Token: 0x0600133C RID: 4924 RVA: 0x000520A0 File Offset: 0x000502A0
			public override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlDictionaryReader reader)
			{
				SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
				reader.ReadStartElement(XD.XmlSignatureDictionary.X509Data, this.NamespaceUri);
				while (reader.IsStartElement())
				{
					if (securityKeyIdentifierClause == null && reader.IsStartElement(XD.XmlSignatureDictionary.X509Certificate, this.NamespaceUri))
					{
						X509Certificate2 certificate = null;
						if (!SecurityUtils.TryCreateX509CertificateFromRawData(reader.ReadElementContentAsBase64(), out certificate))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("InvalidX509RawData")));
						}
						securityKeyIdentifierClause = new X509RawDataKeyIdentifierClause(certificate);
					}
					else if (securityKeyIdentifierClause == null && reader.IsStartElement("X509SKI", this.NamespaceUri.ToString()))
					{
						securityKeyIdentifierClause = new X509SubjectKeyIdentifierClause(reader.ReadElementContentAsBase64());
					}
					else if (securityKeyIdentifierClause == null && reader.IsStartElement(XD.XmlSignatureDictionary.X509IssuerSerial, XD.XmlSignatureDictionary.Namespace))
					{
						reader.ReadStartElement(XD.XmlSignatureDictionary.X509IssuerSerial, XD.XmlSignatureDictionary.Namespace);
						reader.ReadStartElement(XD.XmlSignatureDictionary.X509IssuerName, XD.XmlSignatureDictionary.Namespace);
						string issuerName = reader.ReadContentAsString();
						reader.ReadEndElement();
						reader.ReadStartElement(XD.XmlSignatureDictionary.X509SerialNumber, XD.XmlSignatureDictionary.Namespace);
						string issuerSerialNumber = reader.ReadContentAsString();
						reader.ReadEndElement();
						reader.ReadEndElement();
						securityKeyIdentifierClause = new X509IssuerSerialKeyIdentifierClause(issuerName, issuerSerialNumber);
					}
					else
					{
						reader.Skip();
					}
				}
				reader.ReadEndElement();
				return securityKeyIdentifierClause;
			}

			// Token: 0x0600133D RID: 4925 RVA: 0x000521FA File Offset: 0x000503FA
			public override bool SupportsCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return keyIdentifierClause is X509RawDataKeyIdentifierClause;
			}

			// Token: 0x0600133E RID: 4926 RVA: 0x00052208 File Offset: 0x00050408
			public override void WriteKeyIdentifierClauseCore(XmlDictionaryWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = keyIdentifierClause as X509RawDataKeyIdentifierClause;
				if (x509RawDataKeyIdentifierClause != null)
				{
					writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509Data, this.NamespaceUri);
					writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509Certificate, this.NamespaceUri);
					byte[] x509RawData = x509RawDataKeyIdentifierClause.GetX509RawData();
					writer.WriteBase64(x509RawData, 0, x509RawData.Length);
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
				X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = keyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
				if (x509IssuerSerialKeyIdentifierClause != null)
				{
					writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509Data, XD.XmlSignatureDictionary.Namespace);
					writer.WriteStartElement(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509IssuerSerial, XD.XmlSignatureDictionary.Namespace);
					writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509IssuerName, XD.XmlSignatureDictionary.Namespace, x509IssuerSerialKeyIdentifierClause.IssuerName);
					writer.WriteElementString(XD.XmlSignatureDictionary.Prefix.Value, XD.XmlSignatureDictionary.X509SerialNumber, XD.XmlSignatureDictionary.Namespace, x509IssuerSerialKeyIdentifierClause.IssuerSerialNumber);
					writer.WriteEndElement();
					writer.WriteEndElement();
					return;
				}
				X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause = keyIdentifierClause as X509SubjectKeyIdentifierClause;
				if (x509SubjectKeyIdentifierClause != null)
				{
					writer.WriteStartElement("ds", "X509Data", "http://www.w3.org/2000/09/xmldsig#");
					writer.WriteStartElement("ds", "X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					byte[] x509SubjectKeyIdentifier = x509SubjectKeyIdentifierClause.GetX509SubjectKeyIdentifier();
					writer.WriteBase64(x509SubjectKeyIdentifier, 0, x509SubjectKeyIdentifier.Length);
					writer.WriteEndElement();
					writer.WriteEndElement();
					return;
				}
			}
		}
	}
}
