using System;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000356 RID: 854
	internal static class XmlHelper
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x000743EC File Offset: 0x000725EC
		internal static void AddNamespaceDeclaration(XmlDictionaryWriter writer, string prefix, XmlDictionaryString ns)
		{
			string text = writer.LookupPrefix(ns.Value);
			if (text == null || text != prefix)
			{
				writer.WriteXmlnsAttribute(prefix, ns);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0007441C File Offset: 0x0007261C
		internal static string EnsureNamespaceDefined(XmlDictionaryWriter writer, XmlDictionaryString ns, string defaultPrefix)
		{
			string text = writer.LookupPrefix(ns.Value);
			if (text == null)
			{
				writer.WriteXmlnsAttribute(defaultPrefix, ns);
				text = defaultPrefix;
			}
			return text;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00074444 File Offset: 0x00072644
		internal static XmlQualifiedName GetAttributeValueAsQName(XmlReader reader, string attributeName)
		{
			string attribute = reader.GetAttribute(attributeName);
			if (attribute == null)
			{
				return null;
			}
			return XmlHelper.GetValueAsQName(reader, attribute);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00074468 File Offset: 0x00072668
		internal static XmlElement GetChildElement(XmlElement parent)
		{
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			XmlElement xmlElement = null;
			for (int i = 0; i < parent.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = parent.ChildNodes[i];
				if (xmlNode.NodeType != XmlNodeType.Whitespace && xmlNode.NodeType != XmlNodeType.Comment)
				{
					if (xmlNode.NodeType == XmlNodeType.Element && xmlElement == null)
					{
						xmlElement = (XmlElement)xmlNode;
					}
					else
					{
						XmlHelper.OnUnexpectedChildNodeError(parent, xmlNode);
					}
				}
			}
			if (xmlElement == null)
			{
				XmlHelper.OnChildNodeTypeMissing(parent, XmlNodeType.Element);
			}
			return xmlElement;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000744E8 File Offset: 0x000726E8
		internal static XmlElement GetChildElement(XmlElement parent, string childLocalName, string childNamespace)
		{
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			for (int i = 0; i < parent.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = parent.ChildNodes[i];
				if (xmlNode.NodeType != XmlNodeType.Whitespace && xmlNode.NodeType != XmlNodeType.Comment)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						if (xmlNode.LocalName == childLocalName && xmlNode.NamespaceURI == childNamespace)
						{
							return (XmlElement)xmlNode;
						}
					}
					else
					{
						XmlHelper.OnUnexpectedChildNodeError(parent, xmlNode);
					}
				}
			}
			return null;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00074574 File Offset: 0x00072774
		internal static XmlQualifiedName GetValueAsQName(XmlReader reader, string value)
		{
			string text;
			string name;
			XmlHelper.SplitIntoPrefixAndName(value, out text, out name);
			string text2 = reader.LookupNamespace(text);
			if (text2 == null && text.Length > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CouldNotFindNamespaceForPrefix", new object[]
				{
					text
				})));
			}
			return new XmlQualifiedName(name, text2);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000745CC File Offset: 0x000727CC
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

		// Token: 0x06001F4A RID: 8010 RVA: 0x0007463A File Offset: 0x0007283A
		internal static bool IsWhitespaceOrComment(XmlReader reader)
		{
			return reader.NodeType == XmlNodeType.Comment || reader.NodeType == XmlNodeType.Whitespace;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00074654 File Offset: 0x00072854
		internal static void OnChildNodeTypeMissing(string parentName, XmlNodeType expectedNodeType)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ChildNodeTypeMissing", new object[]
			{
				parentName,
				expectedNodeType
			})));
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00074682 File Offset: 0x00072882
		internal static void OnChildNodeTypeMissing(XmlElement parent, XmlNodeType expectedNodeType)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ChildNodeTypeMissing", new object[]
			{
				parent.Name,
				expectedNodeType
			})));
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000746B5 File Offset: 0x000728B5
		internal static void OnEmptyElementError(XmlReader r)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("EmptyXmlElementError", new object[]
			{
				r.Name
			})));
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000746DF File Offset: 0x000728DF
		internal static void OnEmptyElementError(XmlElement e)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("EmptyXmlElementError", new object[]
			{
				e.Name
			})));
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00074709 File Offset: 0x00072909
		internal static void OnEOF()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedEndOfFile")));
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00074724 File Offset: 0x00072924
		internal static void OnNamespaceMissing(string prefix)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CouldNotFindNamespaceForPrefix", new object[]
			{
				prefix
			})));
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00074749 File Offset: 0x00072949
		internal static void OnRequiredAttributeMissing(string attrName, string elementName)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
			{
				attrName,
				elementName
			})));
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00074772 File Offset: 0x00072972
		internal static void OnRequiredElementMissing(string elementName, string elementNamespace)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ExpectedElementMissing", new object[]
			{
				elementName,
				elementNamespace
			})));
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0007479B File Offset: 0x0007299B
		internal static void OnUnexpectedChildNodeError(string parentName, XmlReader r)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
			{
				r.Name,
				r.NodeType,
				parentName
			})));
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000747D8 File Offset: 0x000729D8
		internal static void OnUnexpectedChildNodeError(XmlElement parent, XmlNode n)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnexpectedXmlChildNode", new object[]
			{
				n.Name,
				n.NodeType,
				parent.Name
			})));
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00074824 File Offset: 0x00072A24
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

		// Token: 0x06001F56 RID: 8022 RVA: 0x00074874 File Offset: 0x00072A74
		internal static string GetRequiredNonEmptyAttribute(XmlDictionaryReader reader, XmlDictionaryString name, XmlDictionaryString ns)
		{
			string attribute = reader.GetAttribute(name, ns);
			if (attribute == null || attribute.Length == 0)
			{
				XmlHelper.OnRequiredAttributeMissing(name.Value, (reader == null) ? null : reader.Name);
			}
			return attribute;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000748B0 File Offset: 0x00072AB0
		internal static byte[] GetRequiredBase64Attribute(XmlDictionaryReader reader, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (!reader.MoveToAttribute(name.Value, (ns == null) ? null : ns.Value))
			{
				XmlHelper.OnRequiredAttributeMissing(name.Value, (ns == null) ? null : ns.Value);
			}
			byte[] array = reader.ReadContentAsBase64();
			if (array == null || array.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("EmptyBase64Attribute", new object[]
				{
					name,
					ns
				})));
			}
			return array;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00074928 File Offset: 0x00072B28
		internal static string ReadTextElementAsTrimmedString(XmlElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			string result;
			using (XmlReader xmlReader = new XmlNodeReader(element))
			{
				xmlReader.MoveToContent();
				result = XmlUtil.Trim(xmlReader.ReadElementContentAsString());
			}
			return result;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00074980 File Offset: 0x00072B80
		internal static void SplitIntoPrefixAndName(string qName, out string prefix, out string name)
		{
			string[] array = qName.Split(new char[]
			{
				':'
			});
			if (array.Length > 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("InvalidQName"));
			}
			if (array.Length == 2)
			{
				prefix = array[0].Trim();
				name = array[1].Trim();
				return;
			}
			prefix = string.Empty;
			name = qName.Trim();
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000749E4 File Offset: 0x00072BE4
		internal static void ValidateIdPrefix(string idPrefix)
		{
			if (idPrefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("idPrefix"));
			}
			if (idPrefix.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("idPrefix", SR.GetString("ValueMustBeGreaterThanZero")));
			}
			if (!char.IsLetter(idPrefix[0]) && idPrefix[0] != '_')
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("idPrefix", SR.GetString("InValidateIdPrefix", new object[]
				{
					idPrefix[0]
				})));
			}
			for (int i = 1; i < idPrefix.Length; i++)
			{
				char c = idPrefix[i];
				if (!char.IsLetter(c) && !char.IsNumber(c) && c != '.' && c != '_' && c != '-')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("idPrefix", SR.GetString("InValidateId", new object[]
					{
						idPrefix[i]
					})));
				}
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00074AE9 File Offset: 0x00072CE9
		internal static UniqueId GetAttributeAsUniqueId(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			return XmlHelper.GetAttributeAsUniqueId(reader, localName.Value, (ns != null) ? ns.Value : null);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00074B04 File Offset: 0x00072D04
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

		// Token: 0x06001F5D RID: 8029 RVA: 0x00074B2C File Offset: 0x00072D2C
		public static void WriteAttributeStringAsUniqueId(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString ns, UniqueId id)
		{
			writer.WriteStartAttribute(prefix, localName, ns);
			writer.WriteValue(id);
			writer.WriteEndAttribute();
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00074B45 File Offset: 0x00072D45
		public static void WriteElementStringAsUniqueId(XmlWriter writer, string localName, UniqueId id)
		{
			writer.WriteStartElement(localName);
			writer.WriteValue(id);
			writer.WriteEndElement();
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00074B5B File Offset: 0x00072D5B
		public static void WriteElementStringAsUniqueId(XmlDictionaryWriter writer, XmlDictionaryString localName, XmlDictionaryString ns, UniqueId id)
		{
			writer.WriteStartElement(localName, ns);
			writer.WriteValue(id);
			writer.WriteEndElement();
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00074B72 File Offset: 0x00072D72
		public static void WriteElementContentAsInt64(XmlDictionaryWriter writer, XmlDictionaryString localName, XmlDictionaryString ns, long value)
		{
			writer.WriteStartElement(localName, ns);
			writer.WriteValue(value);
			writer.WriteEndElement();
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00074B8C File Offset: 0x00072D8C
		public static long ReadElementContentAsInt64(XmlDictionaryReader reader)
		{
			reader.ReadFullStartElement();
			long result = reader.ReadContentAsLong();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00074BAD File Offset: 0x00072DAD
		public static void WriteStringAsUniqueId(XmlDictionaryWriter writer, UniqueId id)
		{
			writer.WriteValue(id);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00074BB8 File Offset: 0x00072DB8
		public static UniqueId ReadElementStringAsUniqueId(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (reader.IsStartElement(localName, ns) && reader.IsEmptyElement)
			{
				reader.Read();
				return new UniqueId(string.Empty);
			}
			reader.ReadStartElement(localName, ns);
			UniqueId result = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00074C00 File Offset: 0x00072E00
		public static UniqueId ReadElementStringAsUniqueId(XmlDictionaryReader reader)
		{
			if (reader.IsStartElement() && reader.IsEmptyElement)
			{
				reader.Read();
				return new UniqueId(string.Empty);
			}
			reader.ReadStartElement();
			UniqueId result = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00074C43 File Offset: 0x00072E43
		public static UniqueId ReadTextElementAsUniqueId(XmlElement element)
		{
			return new UniqueId(XmlHelper.ReadTextElementAsTrimmedString(element));
		}
	}
}
