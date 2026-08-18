using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000140 RID: 320
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x002417D8 File Offset: 0x00240BD8
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderFactoriesConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x002417F8 File Offset: 0x00240BF8
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = HandlerBase.CloneParent(parent as DataSet, false);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						string a;
						if ((a = name) == null || !(a == "DbProviderFactories"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("DbProviderFactories");
						}
						flag = true;
						DbProviderFactoriesConfigurationHandler.HandleProviders(obj as DataSet, configContext, xmlNode, name);
					}
				}
			}
			return obj;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x002418C8 File Offset: 0x00240CC8
		private static void HandleProviders(DataSet config, object configContext, XmlNode section, string sectionName)
		{
			DataTableCollection tables = config.Tables;
			DataTable dataTable = tables[sectionName];
			bool flag = null != dataTable;
			dataTable = DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(dataTable, configContext, section);
			if (!flag)
			{
				tables.Add(dataTable);
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00241908 File Offset: 0x00240D08
		internal static DataTable CreateProviderDataTable()
		{
			DataColumn dataColumn = new DataColumn("Name", typeof(string));
			dataColumn.ReadOnly = true;
			DataColumn dataColumn2 = new DataColumn("Description", typeof(string));
			dataColumn2.ReadOnly = true;
			DataColumn dataColumn3 = new DataColumn("InvariantName", typeof(string));
			dataColumn3.ReadOnly = true;
			DataColumn dataColumn4 = new DataColumn("AssemblyQualifiedName", typeof(string));
			dataColumn4.ReadOnly = true;
			DataColumn[] primaryKey = new DataColumn[]
			{
				dataColumn3
			};
			DataColumn[] columns = new DataColumn[]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4
			};
			DataTable dataTable = new DataTable("DbProviderFactories");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.AddRange(columns);
			dataTable.PrimaryKey = primaryKey;
			return dataTable;
		}

		// Token: 0x04000C66 RID: 3174
		internal const string sectionName = "system.data";

		// Token: 0x04000C67 RID: 3175
		internal const string providerGroup = "DbProviderFactories";

		// Token: 0x02000141 RID: 321
		private static class DbProviderDictionarySectionHandler
		{
			// Token: 0x060014DE RID: 5342 RVA: 0x002419E8 File Offset: 0x00240DE8
			internal static DataTable CreateStatic(DataTable config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
					if (config == null)
					{
						config = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
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
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
									continue;
								}
								if (name == "remove")
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
									continue;
								}
								if (name == "clear")
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
									continue;
								}
							}
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
					}
					config.AcceptChanges();
				}
				return config;
			}

			// Token: 0x060014DF RID: 5343 RVA: 0x00241AC8 File Offset: 0x00240EC8
			private static void HandleAdd(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				DataRow dataRow = config.NewRow();
				dataRow[0] = HandlerBase.RemoveAttribute(child, "name", true, false);
				dataRow[1] = HandlerBase.RemoveAttribute(child, "description", true, false);
				dataRow[2] = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				dataRow[3] = HandlerBase.RemoveAttribute(child, "type", true, false);
				HandlerBase.RemoveAttribute(child, "support", false, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Rows.Add(dataRow);
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x00241B58 File Offset: 0x00240F58
			private static void HandleRemove(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				string key = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				DataRow dataRow = config.Rows.Find(key);
				if (dataRow != null)
				{
					dataRow.Delete();
				}
			}

			// Token: 0x060014E1 RID: 5345 RVA: 0x00241B98 File Offset: 0x00240F98
			private static void HandleClear(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
