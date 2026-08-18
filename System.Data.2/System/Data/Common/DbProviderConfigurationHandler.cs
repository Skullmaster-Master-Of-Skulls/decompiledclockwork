using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002F6 RID: 758
	public class DbProviderConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x0600306C RID: 12396 RVA: 0x0012E4C0 File Offset: 0x0012D8C0
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

		// Token: 0x0600306D RID: 12397 RVA: 0x0012E4E4 File Offset: 0x0012D8E4
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0012E4FC File Offset: 0x0012D8FC
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
						if (!(name == "settings"))
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

		// Token: 0x0600306F RID: 12399 RVA: 0x0012E5C0 File Offset: 0x0012D9C0
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

		// Token: 0x04001D34 RID: 7476
		internal const string settings = "settings";

		// Token: 0x0200043A RID: 1082
		private sealed class DbProviderDictionarySectionHandler
		{
			// Token: 0x06003648 RID: 13896 RVA: 0x001497A4 File Offset: 0x00148BA4
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
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									throw ADP.ConfigUnrecognizedElement(xmlNode);
								}
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
							}
							else
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
							}
						}
						else
						{
							DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
						}
					}
				}
				return config;
			}

			// Token: 0x06003649 RID: 13897 RVA: 0x00149868 File Offset: 0x00148C68
			private static void HandleAdd(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				string value = DbProviderConfigurationHandler.RemoveAttribute(child, "value");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Add(name, value);
			}

			// Token: 0x0600364A RID: 13898 RVA: 0x001498A4 File Offset: 0x00148CA4
			private static void HandleRemove(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Remove(name);
			}

			// Token: 0x0600364B RID: 13899 RVA: 0x001498D0 File Offset: 0x00148CD0
			private static void HandleClear(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
