using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006E9 RID: 1769
	internal static class HandlerBase
	{
		// Token: 0x060054FF RID: 21759 RVA: 0x00129344 File Offset: 0x00127544
		private static XmlNode GetAndRemoveAttribute(XmlNode node, string attrib, bool fRequired)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attrib);
			if (fRequired && xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Missing_required_attribute", new object[]
				{
					attrib,
					node.Name
				}), node);
			}
			return xmlNode;
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0012938C File Offset: 0x0012758C
		private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = andRemoveAttribute.Value;
			}
			return andRemoveAttribute;
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x001293AE File Offset: 0x001275AE
		internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x001293B9 File Offset: 0x001275B9
		internal static XmlNode GetAndRemoveRequiredStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, true, ref val);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x001293C4 File Offset: 0x001275C4
		internal static XmlNode GetAndRemoveNonEmptyStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveNonEmptyStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x001293CF File Offset: 0x001275CF
		internal static XmlNode GetAndRemoveRequiredNonEmptyStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveNonEmptyStringAttributeInternal(node, attrib, true, ref val);
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x001293DC File Offset: 0x001275DC
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

		// Token: 0x06005506 RID: 21766 RVA: 0x0012941C File Offset: 0x0012761C
		private static XmlNode GetAndRemoveBooleanAttributeInternal(XmlNode node, string attrib, bool fRequired, ref bool val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				if (andRemoveAttribute.Value == "true")
				{
					val = true;
				}
				else
				{
					if (!(andRemoveAttribute.Value == "false"))
					{
						throw new ConfigurationErrorsException(SR.GetString("Invalid_boolean_attribute", new object[]
						{
							andRemoveAttribute.Name
						}), andRemoveAttribute);
					}
					val = false;
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00129484 File Offset: 0x00127684
		internal static XmlNode GetAndRemoveBooleanAttribute(XmlNode node, string attrib, ref bool val)
		{
			return HandlerBase.GetAndRemoveBooleanAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00129490 File Offset: 0x00127690
		private static XmlNode GetAndRemoveIntegerAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				if (andRemoveAttribute.Value.Trim() != andRemoveAttribute.Value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_integer_attribute", new object[]
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
					throw new ConfigurationErrorsException(SR.GetString("Invalid_integer_attribute", new object[]
					{
						andRemoveAttribute.Name
					}), inner, andRemoveAttribute);
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0012952C File Offset: 0x0012772C
		private static XmlNode GetAndRemovePositiveAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveIntegerAttributeInternal = HandlerBase.GetAndRemoveIntegerAttributeInternal(node, attrib, fRequired, ref val);
			if (andRemoveIntegerAttributeInternal != null && val <= 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_positive_integer_attribute", new object[]
				{
					attrib
				}), andRemoveIntegerAttributeInternal);
			}
			return andRemoveIntegerAttributeInternal;
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00129567 File Offset: 0x00127767
		internal static XmlNode GetAndRemovePositiveIntegerAttribute(XmlNode node, string attrib, ref int val)
		{
			return HandlerBase.GetAndRemovePositiveAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00129574 File Offset: 0x00127774
		private static XmlNode GetAndRemoveTypeAttributeInternal(XmlNode node, string attrib, bool fRequired, ref Type val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = ConfigUtil.GetType(andRemoveAttribute.Value, andRemoveAttribute);
			}
			return andRemoveAttribute;
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0012959C File Offset: 0x0012779C
		internal static XmlNode GetAndRemoveTypeAttribute(XmlNode node, string attrib, ref Type val)
		{
			return HandlerBase.GetAndRemoveTypeAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x001295A8 File Offset: 0x001277A8
		internal static void CheckForbiddenAttribute(XmlNode node, string attrib)
		{
			XmlAttribute xmlAttribute = node.Attributes[attrib];
			if (xmlAttribute != null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
				{
					attrib
				}), xmlAttribute);
			}
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x001295E0 File Offset: 0x001277E0
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

		// Token: 0x0600550F RID: 21775 RVA: 0x00129630 File Offset: 0x00127830
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode != null)
			{
				return xmlNode.Value;
			}
			return null;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00129655 File Offset: 0x00127855
		internal static string RemoveRequiredAttribute(XmlNode node, string name)
		{
			return HandlerBase.RemoveRequiredAttribute(node, name, false);
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00129660 File Offset: 0x00127860
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
			if (xmlNode.Value.Length == 0 && !allowEmpty)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_empty", new object[]
				{
					name
				}), node);
			}
			return xmlNode.Value;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x001296CC File Offset: 0x001278CC
		internal static void CheckForNonCommentChildNodes(XmlNode node)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), xmlNode);
				}
			}
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x00129738 File Offset: 0x00127938
		internal static void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0012974A File Offset: 0x0012794A
		internal static void CheckAssignableType(XmlNode node, Type baseType, Type type)
		{
			if (!baseType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}), node);
			}
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0012977E File Offset: 0x0012797E
		internal static void CheckAssignableType(string filename, int lineNumber, Type baseType, Type type)
		{
			if (!baseType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}), filename, lineNumber);
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x001297B3 File Offset: 0x001279B3
		internal static bool IsServerConfiguration(object context)
		{
			return context is HttpConfigurationContext;
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001297C0 File Offset: 0x001279C0
		internal static bool CheckAndReadRegistryValue(ref string value, bool throwIfError)
		{
			if (value == null)
			{
				return true;
			}
			if (!StringUtil.StringStartsWithIgnoreCase(value, "registry:"))
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (UnsafeNativeMethods.GetCredentialFromRegistry(value, stringBuilder, 1024) == 0)
			{
				value = stringBuilder.ToString();
				return true;
			}
			if (throwIfError)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_registry_config"));
			}
			return false;
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00129820 File Offset: 0x00127A20
		internal static bool CheckAndReadConnectionString(ref string connectionString, bool throwIfError)
		{
			ConnectionStringSettings connectionStringSettings = RuntimeConfig.GetConfig().ConnectionStrings.ConnectionStrings[connectionString];
			if (connectionStringSettings != null && connectionStringSettings.ConnectionString != null && connectionStringSettings.ConnectionString.Length > 0)
			{
				connectionString = connectionStringSettings.ConnectionString;
			}
			return HandlerBase.CheckAndReadRegistryValue(ref connectionString, throwIfError);
		}
	}
}
