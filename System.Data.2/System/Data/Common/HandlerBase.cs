using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000302 RID: 770
	internal static class HandlerBase
	{
		// Token: 0x060030E0 RID: 12512 RVA: 0x0013046C File Offset: 0x0012F86C
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw ADP.ConfigBaseNoChildNodes(node.FirstChild);
			}
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00130490 File Offset: 0x0012F890
		private static void CheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Element != node.NodeType)
			{
				throw ADP.ConfigBaseElementsOnly(node);
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x001304B0 File Offset: 0x0012F8B0
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw ADP.ConfigUnrecognizedAttributes(node);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x001304D4 File Offset: 0x0012F8D4
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Comment == node.NodeType || XmlNodeType.Whitespace == node.NodeType)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x00130500 File Offset: 0x0012F900
		internal static string RemoveAttribute(XmlNode node, string name, bool required, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				if (required)
				{
					throw ADP.ConfigRequiredAttributeMissing(name, node);
				}
				return null;
			}
			else
			{
				string value = xmlNode.Value;
				if (!allowEmpty && value.Length == 0)
				{
					throw ADP.ConfigRequiredAttributeEmpty(name, node);
				}
				return value;
			}
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x00130548 File Offset: 0x0012F948
		internal static DataSet CloneParent(DataSet parentConfig, bool insenstive)
		{
			if (parentConfig == null)
			{
				parentConfig = new DataSet("system.data");
				parentConfig.CaseSensitive = !insenstive;
				parentConfig.Locale = CultureInfo.InvariantCulture;
			}
			else
			{
				parentConfig = parentConfig.Copy();
			}
			return parentConfig;
		}
	}
}
