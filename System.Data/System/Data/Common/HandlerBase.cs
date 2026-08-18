using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200014D RID: 333
	internal static class HandlerBase
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x002436F8 File Offset: 0x00242AF8
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw ADP.ConfigBaseNoChildNodes(node.FirstChild);
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00243728 File Offset: 0x00242B28
		private static void CheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Element != node.NodeType)
			{
				throw ADP.ConfigBaseElementsOnly(node);
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00243748 File Offset: 0x00242B48
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw ADP.ConfigUnrecognizedAttributes(node);
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00243778 File Offset: 0x00242B78
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Comment == node.NodeType || XmlNodeType.Whitespace == node.NodeType)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x002437A8 File Offset: 0x00242BA8
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

		// Token: 0x0600154D RID: 5453 RVA: 0x002437F8 File Offset: 0x00242BF8
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
