using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Databases;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x0200004A RID: 74
	public class ReferenceTableSettingDAO : IReferenceTableSettingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00011734 File Offset: 0x0000F934
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0001173C File Offset: 0x0000F93C
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x00011745 File Offset: 0x0000F945
		public ReferenceTableSettingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00011776 File Offset: 0x0000F976
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0001177E File Offset: 0x0000F97E
		public OperationContext OpContext { get; set; }

		// Token: 0x060001F5 RID: 501 RVA: 0x00011788 File Offset: 0x0000F988
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql)
		{
			IList<KeyValuePair<int, string[]>> result;
			try
			{
				List<KeyValuePair<int, string[]>> list = new List<KeyValuePair<int, string[]>>();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(columnNames[0]);
				for (int i = 1; i < columnNames.Length; i++)
				{
					stringBuilder.Append(string.Format(", {0}", columnNames[i]));
				}
				string query = (!string.IsNullOrEmpty(overrideSql)) ? overrideSql : string.Format(QueryStorageSettings.QS_ALL_VALUES, idColumnName, stringBuilder.ToString(), tableName);
				DataTable dataTable = this.DatabaseManager.ExecuteQuery(query);
				bool flag = dataTable != null && dataTable.Rows.Count > 0;
				if (flag)
				{
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int key = (int)dataRow[0];
						List<string> list2 = new List<string>();
						for (int j = 0; j < columnNames.Length; j++)
						{
							list2.Add(isValueEncrypted[j] ? this.DatabaseManager.Encryption.Decrypt((byte[])dataRow[j + 1]) : ((string)dataRow[j + 1]));
						}
						list.Add(new KeyValuePair<int, string[]>(key, list2.ToArray()));
					}
				}
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00011938 File Offset: 0x0000FB38
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName)
		{
			IList<KeyValuePair<int, string>> result;
			try
			{
				List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
				string query = (!string.IsNullOrEmpty(overrideSql)) ? overrideSql : string.Format(QueryStorageSettings.QS_ALL_VALUES, idColumnName, columnName, tableName);
				DataTable dataTable = this.DatabaseManager.ExecuteQuery(query);
				bool flag = dataTable != null && dataTable.Rows.Count > 0;
				if (flag)
				{
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						int key = (int)dataRow[0];
						string value = isValueEncrypted ? this.DatabaseManager.Encryption.Decrypt((byte[])dataRow[1]) : ((string)dataRow[1]);
						list.Add(new KeyValuePair<int, string>(key, value));
					}
					if (overrideSortByDisplayName)
					{
						list.Sort((KeyValuePair<int, string> p1, KeyValuePair<int, string> p2) => p1.Value.CompareTo(p2.Value));
					}
				}
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
