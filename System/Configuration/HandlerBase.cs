using System;
using System.Globalization;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F8 RID: 1784
	internal class HandlerBase
	{
		// Token: 0x06003714 RID: 14100 RVA: 0x000EA4E1 File Offset: 0x000E94E1
		private HandlerBase()
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000EA4EC File Offset: 0x000E94EC
		private static XmlNode GetAndRemoveAttribute(XmlNode node, string attrib, bool fRequired)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attrib);
			if (fRequired && xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_missing_required_attribute", new object[]
				{
					attrib,
					node.Name
				}), node);
			}
			return xmlNode;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000EA534 File Offset: 0x000E9534
		private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = andRemoveAttribute.Value;
			}
			return andRemoveAttribute;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000EA556 File Offset: 0x000E9556
		internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000EA564 File Offset: 0x000E9564
		private static XmlNode GetAndRemoveBooleanAttributeInternal(XmlNode node, string attrib, bool fRequired, ref bool val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				try
				{
					val = bool.Parse(andRemoveAttribute.Value);
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString(SR.GetString("Config_invalid_boolean_attribute", new object[]
					{
						andRemoveAttribute.Name
					})), inner, andRemoveAttribute);
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000EA5C8 File Offset: 0x000E95C8
		internal static XmlNode GetAndRemoveBooleanAttribute(XmlNode node, string attrib, ref bool val)
		{
			return HandlerBase.GetAndRemoveBooleanAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000EA5D4 File Offset: 0x000E95D4
		private static XmlNode GetAndRemoveIntegerAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				if (andRemoveAttribute.Value.Trim() != andRemoveAttribute.Value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_invalid_integer_attribute", new object[]
					{
						andRemoveAttribute.Name
					}), andRemoveAttribute);
				}
				try
				{
					val = int.Parse(andRemoveAttribute.Value, CultureInfo.InvariantCulture);
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_invalid_integer_attribute", new object[]
					{
						andRemoveAttribute.Name
					}), inner, andRemoveAttribute);
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000EA670 File Offset: 0x000E9670
		internal static XmlNode GetAndRemoveIntegerAttribute(XmlNode node, string attrib, ref int val)
		{
			return HandlerBase.GetAndRemoveIntegerAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000EA67C File Offset: 0x000E967C
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
				{
					node.Attributes[0].Name
				}), node);
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000EA6C4 File Offset: 0x000E96C4
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode != null)
			{
				return xmlNode.Value;
			}
			return null;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000EA6E9 File Offset: 0x000E96E9
		internal static string RemoveRequiredAttribute(XmlNode node, string name)
		{
			return HandlerBase.RemoveRequiredAttribute(node, name, false);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000EA6F4 File Offset: 0x000E96F4
		internal static string RemoveRequiredAttribute(XmlNode node, string name, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_missing", new object[]
				{
					name
				}), node);
			}
			if (string.IsNullOrEmpty(xmlNode.Value) && !allowEmpty)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_empty", new object[]
				{
					name
				}), node);
			}
			return xmlNode.Value;
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000EA761 File Offset: 0x000E9761
		internal static void CheckForNonElement(XmlNode node)
		{
			if (node.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
			}
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000EA77D File Offset: 0x000E977D
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000EA79B File Offset: 0x000E979B
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), node.FirstChild);
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000EA7BB File Offset: 0x000E97BB
		internal static void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
		}
	}
}
