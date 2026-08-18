using System;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000E5 RID: 229
	internal static class XmlHelper
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00019AB0 File Offset: 0x00017CB0
		internal static string GetWhiteSpace(XmlReader reader)
		{
			string text = null;
			StringBuilder stringBuilder = null;
			while (reader.NodeType == XmlNodeType.Whitespace || reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				if (stringBuilder != null)
				{
					stringBuilder.Append(reader.Value);
				}
				else if (text != null)
				{
					stringBuilder = new StringBuilder(text);
					stringBuilder.Append(reader.Value);
					text = null;
				}
				else
				{
					text = reader.Value;
				}
				if (!reader.Read())
				{
					break;
				}
			}
			if (stringBuilder == null)
			{
				return text;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019B1E File Offset: 0x00017D1E
		internal static void OnRequiredAttributeMissing(string attrName, string elementName)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
			{
				attrName,
				elementName
			})));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00019B48 File Offset: 0x00017D48
		internal static string ReadEmptyElementAndRequiredAttribute(XmlDictionaryReader reader, XmlDictionaryString name, XmlDictionaryString namespaceUri, XmlDictionaryString attributeName, out string prefix)
		{
			reader.MoveToStartElement(name, namespaceUri);
			prefix = reader.Prefix;
			bool isEmptyElement = reader.IsEmptyElement;
			string attribute = reader.GetAttribute(attributeName, null);
			if (attribute == null)
			{
				XmlHelper.OnRequiredAttributeMissing(attributeName.Value, null);
			}
			reader.Read();
			if (!isEmptyElement)
			{
				reader.ReadEndElement();
			}
			return attribute;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00019B98 File Offset: 0x00017D98
		internal static string ReadTextElementAsTrimmedString(XmlElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			XmlReader xmlReader = new XmlNodeReader(element);
			xmlReader.MoveToContent();
			return XmlUtil.Trim(xmlReader.ReadElementContentAsString());
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00019BD1 File Offset: 0x00017DD1
		internal static void OnRequiredElementMissing(string elementName, string elementNamespace)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ExpectedElementMissing", new object[]
			{
				elementName,
				elementNamespace
			})));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00019BFA File Offset: 0x00017DFA
		internal static void OnUnexpectedChildNodeError(string parentName, XmlReader r)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
			{
				r.Name,
				r.NodeType,
				parentName
			})));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00019C38 File Offset: 0x00017E38
		internal static void OnUnexpectedChildNodeError(XmlElement parent, XmlNode n)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
			{
				n.Name,
				n.NodeType,
				parent.Name
			})));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00019C84 File Offset: 0x00017E84
		internal static UniqueId GetAttributeAsUniqueId(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			return XmlHelper.GetAttributeAsUniqueId(reader, localName.Value, (ns != null) ? ns.Value : null);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00019CA0 File Offset: 0x00017EA0
		private static UniqueId GetAttributeAsUniqueId(XmlDictionaryReader reader, string name, string ns)
		{
			if (!reader.MoveToAttribute(name, ns))
			{
				return null;
			}
			UniqueId result = reader.ReadContentAsUniqueId();
			reader.MoveToElement();
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00019CC8 File Offset: 0x00017EC8
		public static void WriteAttributeStringAsUniqueId(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString ns, UniqueId id)
		{
			writer.WriteStartAttribute(prefix, localName, ns);
			writer.WriteValue(id);
			writer.WriteEndAttribute();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public static long ReadElementContentAsInt64(XmlDictionaryReader reader)
		{
			reader.ReadFullStartElement();
			long result = reader.ReadContentAsLong();
			reader.ReadEndElement();
			return result;
		}
	}
}
