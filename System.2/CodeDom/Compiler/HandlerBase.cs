using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066F RID: 1647
	internal static class HandlerBase
	{
		// Token: 0x06003BAA RID: 15274 RVA: 0x000F6E18 File Offset: 0x000F5018
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

		// Token: 0x06003BAB RID: 15275 RVA: 0x000F6E60 File Offset: 0x000F5060
		private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = andRemoveAttribute.Value;
			}
			return andRemoveAttribute;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000F6E82 File Offset: 0x000F5082
		internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000F6E8D File Offset: 0x000F508D
		internal static XmlNode GetAndRemoveRequiredNonEmptyStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveNonEmptyStringAttributeInternal(node, attrib, true, ref val);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000F6E98 File Offset: 0x000F5098
		private static XmlNode GetAndRemoveNonEmptyStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveStringAttributeInternal = HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, fRequired, ref val);
			if (andRemoveStringAttributeInternal != null && val.Length == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Empty_attribute", new object[]
				{
					attrib
				}), andRemoveStringAttributeInternal);
			}
			return andRemoveStringAttributeInternal;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000F6ED8 File Offset: 0x000F50D8
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

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000F6F74 File Offset: 0x000F5174
		private static XmlNode GetAndRemoveNonNegativeAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveIntegerAttributeInternal = HandlerBase.GetAndRemoveIntegerAttributeInternal(node, attrib, fRequired, ref val);
			if (andRemoveIntegerAttributeInternal != null && val < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_nonnegative_integer_attribute", new object[]
				{
					attrib
				}), andRemoveIntegerAttributeInternal);
			}
			return andRemoveIntegerAttributeInternal;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000F6FAF File Offset: 0x000F51AF
		internal static XmlNode GetAndRemoveNonNegativeIntegerAttribute(XmlNode node, string attrib, ref int val)
		{
			return HandlerBase.GetAndRemoveNonNegativeAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000F6FBC File Offset: 0x000F51BC
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
				{
					node.Attributes[0].Name
				}), node.Attributes[0]);
			}
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000F700C File Offset: 0x000F520C
		internal static void CheckForNonElement(XmlNode node)
		{
			if (node.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
			}
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000F7028 File Offset: 0x000F5228
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000F7046 File Offset: 0x000F5246
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), node.FirstChild);
			}
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000F7066 File Offset: 0x000F5266
		internal static void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
		}
	}
}
