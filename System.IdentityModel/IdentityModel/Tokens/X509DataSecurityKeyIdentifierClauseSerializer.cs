using System;
using System.Collections.Generic;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000190 RID: 400
	public class X509DataSecurityKeyIdentifierClauseSerializer : SecurityKeyIdentifierClauseSerializer
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x0003D14C File Offset: 0x0003B34C
		public override bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003D171 File Offset: 0x0003B371
		public override bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifierClause");
			}
			return securityKeyIdentifierClause is X509IssuerSerialKeyIdentifierClause || securityKeyIdentifierClause is X509RawDataKeyIdentifierClause || securityKeyIdentifierClause is X509SubjectKeyIdentifierClause;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		public override SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (!this.CanReadKeyIdentifierClause(reader))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					"X509Data",
					"http://www.w3.org/2000/09/xmldsig#"
				}));
			}
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			xmlDictionaryReader.ReadStartElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			List<SecurityKeyIdentifierClause> list = new List<SecurityKeyIdentifierClause>();
			while (xmlDictionaryReader.IsStartElement())
			{
				if (xmlDictionaryReader.IsStartElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#"))
				{
					list.Add(X509DataSecurityKeyIdentifierClauseSerializer.CreateIssuerSerialKeyIdentifierClause(xmlDictionaryReader));
				}
				else if (xmlDictionaryReader.IsStartElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#"))
				{
					list.Add(X509DataSecurityKeyIdentifierClauseSerializer.CreateSubjectKeyIdentifierClause(xmlDictionaryReader));
				}
				else if (xmlDictionaryReader.IsStartElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#"))
				{
					list.Add(X509DataSecurityKeyIdentifierClauseSerializer.CreateRawDataKeyIdentifierClause(xmlDictionaryReader));
				}
				else
				{
					xmlDictionaryReader.Skip();
				}
			}
			xmlDictionaryReader.ReadEndElement();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003D298 File Offset: 0x0003B498
		public override void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifierClause");
			}
			X509IssuerSerialKeyIdentifierClause x509IssuerSerialKeyIdentifierClause = securityKeyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
			if (x509IssuerSerialKeyIdentifierClause != null)
			{
				writer.WriteStartElement("ds", "X509Data", "http://www.w3.org/2000/09/xmldsig#");
				writer.WriteStartElement("ds", "X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
				writer.WriteElementString("ds", "X509IssuerName", "http://www.w3.org/2000/09/xmldsig#", x509IssuerSerialKeyIdentifierClause.IssuerName);
				writer.WriteElementString("ds", "X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#", x509IssuerSerialKeyIdentifierClause.IssuerSerialNumber);
				writer.WriteEndElement();
				writer.WriteEndElement();
				return;
			}
			X509SubjectKeyIdentifierClause x509SubjectKeyIdentifierClause = securityKeyIdentifierClause as X509SubjectKeyIdentifierClause;
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
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause = securityKeyIdentifierClause as X509RawDataKeyIdentifierClause;
			if (x509RawDataKeyIdentifierClause != null)
			{
				writer.WriteStartElement("ds", "X509Data", "http://www.w3.org/2000/09/xmldsig#");
				writer.WriteStartElement("ds", "X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
				byte[] x509RawData = x509RawDataKeyIdentifierClause.GetX509RawData();
				writer.WriteBase64(x509RawData, 0, x509RawData.Length);
				writer.WriteEndElement();
				writer.WriteEndElement();
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("securityKeyIdentifierClause", SR.GetString("ID4259", new object[]
			{
				securityKeyIdentifierClause.GetType()
			}));
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003D414 File Offset: 0x0003B614
		private static SecurityKeyIdentifierClause CreateIssuerSerialKeyIdentifierClause(XmlDictionaryReader dictionaryReader)
		{
			dictionaryReader.ReadStartElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
			if (!dictionaryReader.IsStartElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#"))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3032", new object[]
				{
					dictionaryReader.LocalName,
					dictionaryReader.NamespaceURI,
					"X509IssuerName",
					"http://www.w3.org/2000/09/xmldsig#"
				}));
			}
			string issuerName = dictionaryReader.ReadElementContentAsString("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
			if (!dictionaryReader.IsStartElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#"))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3032", new object[]
				{
					dictionaryReader.LocalName,
					dictionaryReader.NamespaceURI,
					"X509SerialNumber",
					"http://www.w3.org/2000/09/xmldsig#"
				}));
			}
			string issuerSerialNumber = dictionaryReader.ReadElementContentAsString("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
			dictionaryReader.ReadEndElement();
			return new X509IssuerSerialKeyIdentifierClause(issuerName, issuerSerialNumber);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		private static SecurityKeyIdentifierClause CreateSubjectKeyIdentifierClause(XmlDictionaryReader dictionaryReader)
		{
			byte[] array = dictionaryReader.ReadElementContentAsBase64();
			if (array == null || array.Length == 0)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4258", new object[]
				{
					"X509SKI",
					"http://www.w3.org/2000/09/xmldsig#"
				}));
			}
			return new X509SubjectKeyIdentifierClause(array);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003D53C File Offset: 0x0003B73C
		private static SecurityKeyIdentifierClause CreateRawDataKeyIdentifierClause(XmlDictionaryReader dictionaryReader)
		{
			byte[] array = null;
			while (dictionaryReader.IsStartElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#"))
			{
				if (array == null)
				{
					array = dictionaryReader.ReadElementContentAsBase64();
					if (array == null || array.Length == 0)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4258", new object[]
						{
							"X509Certificate",
							"http://www.w3.org/2000/09/xmldsig#"
						}));
					}
				}
				else
				{
					dictionaryReader.Skip();
				}
			}
			return new X509RawDataKeyIdentifierClause(array);
		}
	}
}
