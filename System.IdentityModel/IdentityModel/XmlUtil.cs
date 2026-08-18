using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000E7 RID: 231
	internal static class XmlUtil
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00019D05 File Offset: 0x00017F05
		public static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n';
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019D20 File Offset: 0x00017F20
		public static string TrimEnd(string s)
		{
			int num = s.Length;
			while (num > 0 && XmlUtil.IsWhitespace(s[num - 1]))
			{
				num--;
			}
			if (num != s.Length)
			{
				return s.Substring(0, num);
			}
			return s;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019D64 File Offset: 0x00017F64
		public static string TrimStart(string s)
		{
			int num = 0;
			while (num < s.Length && XmlUtil.IsWhitespace(s[num]))
			{
				num++;
			}
			if (num != 0)
			{
				return s.Substring(num);
			}
			return s;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019D9C File Offset: 0x00017F9C
		public static string Trim(string s)
		{
			int num = 0;
			while (num < s.Length && XmlUtil.IsWhitespace(s[num]))
			{
				num++;
			}
			if (num >= s.Length)
			{
				return string.Empty;
			}
			int num2 = s.Length;
			while (num2 > 0 && XmlUtil.IsWhitespace(s[num2 - 1]))
			{
				num2--;
			}
			if (num != 0 || num2 != s.Length)
			{
				return s.Substring(num, num2 - num);
			}
			return s;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00019E10 File Offset: 0x00018010
		public static XmlQualifiedName GetXsiType(XmlReader reader)
		{
			string attribute = reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
			reader.MoveToElement();
			if (string.IsNullOrEmpty(attribute))
			{
				return null;
			}
			return XmlUtil.ResolveQName(reader, attribute);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00019E46 File Offset: 0x00018046
		public static bool EqualsQName(XmlQualifiedName qname, string localName, string namespaceUri)
		{
			return null != qname && StringComparer.Ordinal.Equals(localName, qname.Name) && StringComparer.Ordinal.Equals(namespaceUri, qname.Namespace);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00019E78 File Offset: 0x00018078
		public static bool IsNil(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			return !string.IsNullOrEmpty(attribute) && XmlConvert.ToBoolean(attribute);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019EA6 File Offset: 0x000180A6
		public static string NormalizeEmptyString(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return s;
			}
			return null;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019EB4 File Offset: 0x000180B4
		public static XmlQualifiedName ResolveQName(XmlReader reader, string qstring)
		{
			string name = qstring;
			string prefix = string.Empty;
			int num = qstring.IndexOf(':');
			if (num > -1)
			{
				prefix = qstring.Substring(0, num);
				name = qstring.Substring(num + 1, qstring.Length - (num + 1));
			}
			string ns = reader.LookupNamespace(prefix);
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00019F04 File Offset: 0x00018104
		public static void ValidateXsiType(XmlReader reader, string expectedTypeName, string expectedTypeNamespace)
		{
			XmlUtil.ValidateXsiType(reader, expectedTypeName, expectedTypeNamespace, false);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019F10 File Offset: 0x00018110
		public static void ValidateXsiType(XmlReader reader, string expectedTypeName, string expectedTypeNamespace, bool requireDeclaration)
		{
			XmlQualifiedName xsiType = XmlUtil.GetXsiType(reader);
			if (null == xsiType)
			{
				if (requireDeclaration)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4104", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}));
				}
			}
			else if (!StringComparer.Ordinal.Equals(expectedTypeNamespace, xsiType.Namespace) || !StringComparer.Ordinal.Equals(expectedTypeName, xsiType.Name))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4102", new object[]
				{
					expectedTypeName,
					expectedTypeNamespace,
					xsiType.Name,
					xsiType.Namespace
				}));
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00019FB4 File Offset: 0x000181B4
		public static string SerializeSecurityKeyIdentifier(SecurityKeyIdentifier ski, SecurityTokenSerializer tokenSerializer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					OmitXmlDeclaration = true
				}))
				{
					tokenSerializer.WriteKeyIdentifierClause(xmlWriter, ski[0]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001A030 File Offset: 0x00018230
		public static bool IsValidXmlIDValue(string val)
		{
			return !string.IsNullOrEmpty(val) && ((val[0] >= 'A' && val[0] <= 'Z') || (val[0] >= 'a' && val[0] <= 'z') || val[0] == '_' || val[0] == ':');
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A08B File Offset: 0x0001828B
		public static void WriteElementStringAsUniqueId(XmlDictionaryWriter writer, XmlDictionaryString localName, XmlDictionaryString ns, string id)
		{
			writer.WriteStartElement(localName, ns);
			writer.WriteValue(id);
			writer.WriteEndElement();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001A0A2 File Offset: 0x000182A2
		public static void WriteElementContentAsInt64(XmlDictionaryWriter writer, XmlDictionaryString localName, XmlDictionaryString ns, long value)
		{
			writer.WriteStartElement(localName, ns);
			writer.WriteValue(value);
			writer.WriteEndElement();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001A0BC File Offset: 0x000182BC
		public static long ReadElementContentAsInt64(XmlDictionaryReader reader)
		{
			reader.ReadFullStartElement();
			long result = reader.ReadContentAsLong();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public static List<XmlElement> GetXmlElements(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("nodeList");
			}
			List<XmlElement> list = new List<XmlElement>();
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					list.Add(xmlElement);
				}
			}
			return list;
		}

		// Token: 0x04000798 RID: 1944
		public const string XmlNs = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000799 RID: 1945
		public const string XmlNsNs = "http://www.w3.org/2000/xmlns/";

		// Token: 0x0400079A RID: 1946
		public const string LanguagePrefix = "xml";

		// Token: 0x0400079B RID: 1947
		public const string LanguageLocalname = "lang";

		// Token: 0x0400079C RID: 1948
		public const string LanguageAttribute = "xml:lang";
	}
}
