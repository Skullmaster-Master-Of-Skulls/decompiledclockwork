using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E0 RID: 992
	internal static class XmlSerializerHelper
	{
		// Token: 0x06002566 RID: 9574 RVA: 0x00085E78 File Offset: 0x00084078
		internal static XmlReflectionMember GetXmlReflectionMember(MessagePartDescription part, bool isRpc, bool isEncoded, bool isWrapped)
		{
			string ns = isRpc ? null : part.Namespace;
			ICustomAttributeProvider additionalAttributesProvider = null;
			if (isEncoded || part.AdditionalAttributesProvider is MemberInfo)
			{
				additionalAttributesProvider = part.AdditionalAttributesProvider;
			}
			XmlName memberName = string.IsNullOrEmpty(part.UniquePartName) ? null : new XmlName(part.UniquePartName, true);
			XmlName xmlName = part.XmlName;
			return XmlSerializerHelper.GetXmlReflectionMember(memberName, xmlName, ns, part.Type, additionalAttributesProvider, part.Multiple, isEncoded, isWrapped);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00085EE8 File Offset: 0x000840E8
		internal static XmlReflectionMember GetXmlReflectionMember(XmlName memberName, XmlName elementName, string ns, Type type, ICustomAttributeProvider additionalAttributesProvider, bool isMultiple, bool isEncoded, bool isWrapped)
		{
			if (isEncoded && isMultiple)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMultiplePartsNotAllowedInEncoded", new object[]
				{
					elementName.DecodedName,
					ns
				})));
			}
			XmlReflectionMember xmlReflectionMember = new XmlReflectionMember();
			xmlReflectionMember.MemberName = (memberName ?? elementName).DecodedName;
			xmlReflectionMember.MemberType = type;
			if (xmlReflectionMember.MemberType.IsByRef)
			{
				xmlReflectionMember.MemberType = xmlReflectionMember.MemberType.GetElementType();
			}
			if (isMultiple)
			{
				xmlReflectionMember.MemberType = xmlReflectionMember.MemberType.MakeArrayType();
			}
			if (additionalAttributesProvider != null)
			{
				if (isEncoded)
				{
					xmlReflectionMember.SoapAttributes = new SoapAttributes(additionalAttributesProvider);
				}
				else
				{
					xmlReflectionMember.XmlAttributes = new XmlAttributes(additionalAttributesProvider);
				}
			}
			if (isEncoded)
			{
				if (xmlReflectionMember.SoapAttributes == null)
				{
					xmlReflectionMember.SoapAttributes = new SoapAttributes();
				}
				else
				{
					Type type2 = null;
					if (xmlReflectionMember.SoapAttributes.SoapAttribute != null)
					{
						type2 = typeof(SoapAttributeAttribute);
					}
					else if (xmlReflectionMember.SoapAttributes.SoapIgnore)
					{
						type2 = typeof(SoapIgnoreAttribute);
					}
					else if (xmlReflectionMember.SoapAttributes.SoapType != null)
					{
						type2 = typeof(SoapTypeAttribute);
					}
					if (type2 != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidSoapAttribute", new object[]
						{
							type2,
							elementName.DecodedName
						})));
					}
				}
				if (xmlReflectionMember.SoapAttributes.SoapElement == null)
				{
					xmlReflectionMember.SoapAttributes.SoapElement = new SoapElementAttribute(elementName.DecodedName);
				}
			}
			else
			{
				if (xmlReflectionMember.XmlAttributes == null)
				{
					xmlReflectionMember.XmlAttributes = new XmlAttributes();
				}
				else
				{
					Type type3 = null;
					if (xmlReflectionMember.XmlAttributes.XmlAttribute != null)
					{
						type3 = typeof(XmlAttributeAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.XmlAnyAttribute != null && !isWrapped)
					{
						type3 = typeof(XmlAnyAttributeAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.XmlChoiceIdentifier != null)
					{
						type3 = typeof(XmlChoiceIdentifierAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.XmlIgnore)
					{
						type3 = typeof(XmlIgnoreAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.Xmlns)
					{
						type3 = typeof(XmlNamespaceDeclarationsAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.XmlText != null)
					{
						type3 = typeof(XmlTextAttribute);
					}
					else if (xmlReflectionMember.XmlAttributes.XmlEnum != null)
					{
						type3 = typeof(XmlEnumAttribute);
					}
					if (type3 != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(isWrapped ? "SFxInvalidXmlAttributeInWrapped" : "SFxInvalidXmlAttributeInBare", new object[]
						{
							type3,
							elementName.DecodedName
						})));
					}
					if (xmlReflectionMember.XmlAttributes.XmlArray != null && isMultiple)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxXmlArrayNotAllowedForMultiple", new object[]
						{
							elementName.DecodedName,
							ns
						})));
					}
				}
				bool isArray = xmlReflectionMember.MemberType.IsArray;
				if ((isArray && !isMultiple && xmlReflectionMember.MemberType != typeof(byte[])) || (!isArray && typeof(IEnumerable).IsAssignableFrom(xmlReflectionMember.MemberType) && xmlReflectionMember.MemberType != typeof(string) && !typeof(XmlNode).IsAssignableFrom(xmlReflectionMember.MemberType) && !typeof(IXmlSerializable).IsAssignableFrom(xmlReflectionMember.MemberType)))
				{
					if (xmlReflectionMember.XmlAttributes.XmlArray != null)
					{
						if (xmlReflectionMember.XmlAttributes.XmlArray.ElementName == string.Empty)
						{
							xmlReflectionMember.XmlAttributes.XmlArray.ElementName = elementName.DecodedName;
						}
						if (xmlReflectionMember.XmlAttributes.XmlArray.Namespace == null)
						{
							xmlReflectionMember.XmlAttributes.XmlArray.Namespace = ns;
						}
					}
					else if (XmlSerializerHelper.HasNoXmlParameterAttributes(xmlReflectionMember.XmlAttributes))
					{
						xmlReflectionMember.XmlAttributes.XmlArray = new XmlArrayAttribute();
						xmlReflectionMember.XmlAttributes.XmlArray.ElementName = elementName.DecodedName;
						xmlReflectionMember.XmlAttributes.XmlArray.Namespace = ns;
					}
				}
				else if (xmlReflectionMember.XmlAttributes.XmlElements == null || xmlReflectionMember.XmlAttributes.XmlElements.Count == 0)
				{
					if (XmlSerializerHelper.HasNoXmlParameterAttributes(xmlReflectionMember.XmlAttributes))
					{
						XmlElementAttribute xmlElementAttribute = new XmlElementAttribute();
						xmlElementAttribute.ElementName = elementName.DecodedName;
						xmlElementAttribute.Namespace = ns;
						xmlReflectionMember.XmlAttributes.XmlElements.Add(xmlElementAttribute);
					}
				}
				else
				{
					foreach (object obj in xmlReflectionMember.XmlAttributes.XmlElements)
					{
						XmlElementAttribute xmlElementAttribute2 = (XmlElementAttribute)obj;
						if (xmlElementAttribute2.ElementName == string.Empty)
						{
							xmlElementAttribute2.ElementName = elementName.DecodedName;
						}
						if (xmlElementAttribute2.Namespace == null)
						{
							xmlElementAttribute2.Namespace = ns;
						}
					}
				}
			}
			return xmlReflectionMember;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000863F8 File Offset: 0x000845F8
		private static bool HasNoXmlParameterAttributes(XmlAttributes xmlAttributes)
		{
			return xmlAttributes.XmlAnyAttribute == null && (xmlAttributes.XmlAnyElements == null || xmlAttributes.XmlAnyElements.Count == 0) && xmlAttributes.XmlArray == null && xmlAttributes.XmlAttribute == null && !xmlAttributes.XmlIgnore && xmlAttributes.XmlText == null && xmlAttributes.XmlChoiceIdentifier == null && (xmlAttributes.XmlElements == null || xmlAttributes.XmlElements.Count == 0) && !xmlAttributes.Xmlns;
		}
	}
}
