using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002F9 RID: 761
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x0012EC48 File Offset: 0x0012E048
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderFactoriesConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x0012EC60 File Offset: 0x0012E060
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
						if (!(name == "DbProviderFactories"))
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

		// Token: 0x06003082 RID: 12418 RVA: 0x0012ED28 File Offset: 0x0012E128
		private static void HandleProviders(DataSet config, object configContext, XmlNode section, string sectionName)
		{
			DataTableCollection tables = config.Tables;
			DataTable dataTable = tables[sectionName];
			bool flag = dataTable != null;
			dataTable = DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(dataTable, configContext, section);
			if (!flag)
			{
				tables.Add(dataTable);
			}
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0012ED5C File Offset: 0x0012E15C
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

		// Token: 0x04001D42 RID: 7490
		internal const string sectionName = "system.data";

		// Token: 0x04001D43 RID: 7491
		internal const string providerGroup = "DbProviderFactories";

		// Token: 0x04001D44 RID: 7492
		internal const string odbcProviderName = "Odbc Data Provider";

		// Token: 0x04001D45 RID: 7493
		internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";

		// Token: 0x04001D46 RID: 7494
		internal const string oledbProviderName = "OleDb Data Provider";

		// Token: 0x04001D47 RID: 7495
		internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";

		// Token: 0x04001D48 RID: 7496
		internal const string oracleclientProviderName = "OracleClient Data Provider";

		// Token: 0x04001D49 RID: 7497
		internal const string oracleclientProviderNamespace = "System.Data.OracleClient";

		// Token: 0x04001D4A RID: 7498
		internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";

		// Token: 0x04001D4B RID: 7499
		internal const string sqlclientProviderName = "SqlClient Data Provider";

		// Token: 0x04001D4C RID: 7500
		internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";

		// Token: 0x04001D4D RID: 7501
		internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";

		// Token: 0x04001D4E RID: 7502
		internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

		// Token: 0x0200043B RID: 1083
		private static class DbProviderDictionarySectionHandler
		{
			// Token: 0x0600364D RID: 13901 RVA: 0x00149904 File Offset: 0x00148D04
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
							string name = xmlNode.Name;
							if (!(name == "add"))
							{
								if (!(name == "remove"))
								{
									if (!(name == "clear"))
									{
										throw ADP.ConfigUnrecognizedElement(xmlNode);
									}
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
								}
								else
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
								}
							}
							else
							{
								DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
							}
						}
					}
					config.AcceptChanges();
				}
				return config;
			}

			// Token: 0x0600364E RID: 13902 RVA: 0x001499DC File Offset: 0x00148DDC
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

			// Token: 0x0600364F RID: 13903 RVA: 0x00149A68 File Offset: 0x00148E68
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

			// Token: 0x06003650 RID: 13904 RVA: 0x00149AA8 File Offset: 0x00148EA8
			private static void HandleClear(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
