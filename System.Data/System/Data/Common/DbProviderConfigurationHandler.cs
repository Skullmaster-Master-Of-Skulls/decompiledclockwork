using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200013D RID: 317
	public class DbProviderConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x002412B8 File Offset: 0x002406B8
		internal static NameValueCollection CloneParent(NameValueCollection parentConfig)
		{
			if (parentConfig == null)
			{
				parentConfig = new NameValueCollection();
			}
			else
			{
				parentConfig = new NameValueCollection(parentConfig);
			}
			return parentConfig;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x002412E8 File Offset: 0x002406E8
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00241308 File Offset: 0x00240708
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = DbProviderConfigurationHandler.CloneParent(parent as NameValueCollection);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						string a;
						if ((a = name) == null || !(a == "settings"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("settings");
						}
						flag = true;
						DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(obj as NameValueCollection, configContext, xmlNode);
					}
				}
			}
			return obj;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x002413D8 File Offset: 0x002407D8
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				throw ADP.ConfigRequiredAttributeMissing(name, node);
			}
			string value = xmlNode.Value;
			if (value.Length == 0)
			{
				throw ADP.ConfigRequiredAttributeEmpty(name, node);
			}
			return value;
		}

		// Token: 0x04000C5F RID: 3167
		internal const string settings = "settings";

		// Token: 0x0200013E RID: 318
		private sealed class DbProviderDictionarySectionHandler
		{
			// Token: 0x060014CE RID: 5326 RVA: 0x00241418 File Offset: 0x00240818
			internal static NameValueCollection CreateStatic(NameValueCollection config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
				}
				foreach (object obj in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name;
						if ((name = xmlNode.Name) != null)
						{
							if (name == "add")
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
								continue;
							}
							if (name == "remove")
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
								continue;
							}
							if (name == "clear")
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
								continue;
							}
						}
						throw ADP.ConfigUnrecognizedElement(xmlNode);
					}
				}
				return config;
			}

			// Token: 0x060014CF RID: 5327 RVA: 0x002414E8 File Offset: 0x002408E8
			private static void HandleAdd(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				string value = DbProviderConfigurationHandler.RemoveAttribute(child, "value");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Add(name, value);
			}

			// Token: 0x060014D0 RID: 5328 RVA: 0x00241528 File Offset: 0x00240928
			private static void HandleRemove(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Remove(name);
			}

			// Token: 0x060014D1 RID: 5329 RVA: 0x00241558 File Offset: 0x00240958
			private static void HandleClear(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
