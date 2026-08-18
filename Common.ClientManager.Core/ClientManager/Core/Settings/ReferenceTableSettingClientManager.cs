using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Settings
{
	// Token: 0x0200001B RID: 27
	public class ReferenceTableSettingClientManager : IReferenceTableSettingClientManager, IWebService
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005148 File Offset: 0x00003348
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql)
		{
			GetValuesFromColumnsReq getValuesFromColumnsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnsReq>();
			getValuesFromColumnsReq.TableName = tableName;
			getValuesFromColumnsReq.OverrideSql = overrideSql;
			getValuesFromColumnsReq.IdColumnName = idColumnName;
			getValuesFromColumnsReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnsReq.ColumnNames = columnNames;
			return ClientServiceFactory.GetClientInstance<IReferenceTableSetting>().GetValuesFromColumns(getValuesFromColumnsReq).Values;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000051A0 File Offset: 0x000033A0
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted)
		{
			GetValuesFromColumnsReq getValuesFromColumnsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnsReq>();
			getValuesFromColumnsReq.TableName = tableName;
			getValuesFromColumnsReq.IdColumnName = idColumnName;
			getValuesFromColumnsReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnsReq.ColumnNames = columnNames;
			return ClientServiceFactory.GetClientInstance<IReferenceTableSetting>().GetValuesFromColumns(getValuesFromColumnsReq).Values;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000051F0 File Offset: 0x000033F0
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.OverrideSql = overrideSql;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			getValuesFromColumnReq.OverrideSortByDisplayName = overrideSortByDisplayName;
			return ClientServiceFactory.GetClientInstance<IReferenceTableSetting>().GetValuesFromColumn(getValuesFromColumnReq).Values;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005250 File Offset: 0x00003450
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			return ClientServiceFactory.GetClientInstance<IReferenceTableSetting>().GetValuesFromColumn(getValuesFromColumnReq).Values;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000052A0 File Offset: 0x000034A0
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.OverrideSql = overrideSql;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			return ClientServiceFactory.GetClientInstance<IReferenceTableSetting>().GetValuesFromColumn(getValuesFromColumnReq).Values;
		}
	}
}
