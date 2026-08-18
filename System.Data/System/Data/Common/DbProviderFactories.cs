using System;
using System.Configuration;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x0200013F RID: 319
	public static class DbProviderFactories
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x00241598 File Offset: 0x00240998
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			DataSet configTable = DbProviderFactories.GetConfigTable();
			DataTable dataTable = (configTable != null) ? configTable.Tables["DbProviderFactories"] : null;
			if (dataTable != null)
			{
				DataRow dataRow = dataTable.Rows.Find(providerInvariantName);
				if (dataRow != null)
				{
					return DbProviderFactories.GetFactory(dataRow);
				}
			}
			throw ADP.ConfigProviderNotFound();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x002415F8 File Offset: 0x002409F8
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			ADP.CheckArgumentNull(providerRow, "providerRow");
			DataColumn dataColumn = providerRow.Table.Columns["AssemblyQualifiedName"];
			if (dataColumn != null)
			{
				string text = providerRow[dataColumn] as string;
				if (!ADP.IsEmpty(text))
				{
					Type type = Type.GetType(text);
					if (type != null)
					{
						FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if (field != null && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object value = field.GetValue(null);
							if (value != null)
							{
								return (DbProviderFactory)value;
							}
						}
						throw ADP.ConfigProviderInvalid();
					}
					throw ADP.ConfigProviderNotInstalled();
				}
			}
			throw ADP.ConfigProviderMissing();
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00241698 File Offset: 0x00240A98
		public static DataTable GetFactoryClasses()
		{
			DataSet configTable = DbProviderFactories.GetConfigTable();
			DataTable dataTable = (configTable != null) ? configTable.Tables["DbProviderFactories"] : null;
			if (dataTable != null)
			{
				dataTable = dataTable.Copy();
			}
			else
			{
				dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			}
			return dataTable;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x002416D8 File Offset: 0x00240AD8
		private static DataSet GetConfigTable()
		{
			DbProviderFactories.Initialize();
			return DbProviderFactories._configTable;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x002416F8 File Offset: 0x00240AF8
		private static void Initialize()
		{
			if (ConnectionState.Open != DbProviderFactories._initState)
			{
				lock (DbProviderFactories._lockobj)
				{
					switch (DbProviderFactories._initState)
					{
					case ConnectionState.Closed:
						DbProviderFactories._initState = ConnectionState.Connecting;
						try
						{
							DbProviderFactories._configTable = (PrivilegedConfigurationManager.GetSection("system.data") as DataSet);
						}
						finally
						{
							DbProviderFactories._initState = ConnectionState.Open;
						}
						break;
					}
				}
			}
		}

		// Token: 0x04000C60 RID: 3168
		private const string AssemblyQualifiedName = "AssemblyQualifiedName";

		// Token: 0x04000C61 RID: 3169
		private const string Instance = "Instance";

		// Token: 0x04000C62 RID: 3170
		private const string InvariantName = "InvariantName";

		// Token: 0x04000C63 RID: 3171
		private static ConnectionState _initState;

		// Token: 0x04000C64 RID: 3172
		private static DataSet _configTable;

		// Token: 0x04000C65 RID: 3173
		private static object _lockobj = new object();
	}
}
