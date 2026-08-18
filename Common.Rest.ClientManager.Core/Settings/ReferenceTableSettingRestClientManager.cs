using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Settings
{
	// Token: 0x02000015 RID: 21
	public class ReferenceTableSettingRestClientManager : BearerTokenRestProxy<IReferenceTableSettingClientManager>, IReferenceTableSettingClientManager, IWebService
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003917 File Offset: 0x00001B17
		public ReferenceTableSettingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003921 File Offset: 0x00001B21
		public ReferenceTableSettingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000392C File Offset: 0x00001B2C
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql)
		{
			GetValuesFromColumnsReq getValuesFromColumnsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnsReq>();
			getValuesFromColumnsReq.TableName = tableName;
			getValuesFromColumnsReq.OverrideSql = overrideSql;
			getValuesFromColumnsReq.IdColumnName = idColumnName;
			getValuesFromColumnsReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnsReq.ColumnNames = columnNames;
			return base.Post<GetValuesFromColumnsReq, GetValuesFromColumnsResp>(getValuesFromColumnsReq, "referencetablesetting/valuesfromcolumns").Values;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000397C File Offset: 0x00001B7C
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted)
		{
			GetValuesFromColumnsReq getValuesFromColumnsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnsReq>();
			getValuesFromColumnsReq.TableName = tableName;
			getValuesFromColumnsReq.IdColumnName = idColumnName;
			getValuesFromColumnsReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnsReq.ColumnNames = columnNames;
			return base.Post<GetValuesFromColumnsReq, GetValuesFromColumnsResp>(getValuesFromColumnsReq, "referencetablesetting/valuesfromcolumns").Values;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000039C4 File Offset: 0x00001BC4
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.OverrideSql = overrideSql;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			getValuesFromColumnReq.OverrideSortByDisplayName = overrideSortByDisplayName;
			return base.Post<GetValuesFromColumnReq, GetValuesFromColumnResp>(getValuesFromColumnReq, "referencetablesetting/valuesfromcolumn").Values;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A1C File Offset: 0x00001C1C
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			return base.Post<GetValuesFromColumnReq, GetValuesFromColumnResp>(getValuesFromColumnReq, "referencetablesetting/valuesfromcolumn").Values;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003A64 File Offset: 0x00001C64
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql)
		{
			GetValuesFromColumnReq getValuesFromColumnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetValuesFromColumnReq>();
			getValuesFromColumnReq.TableName = tableName;
			getValuesFromColumnReq.OverrideSql = overrideSql;
			getValuesFromColumnReq.IdColumnName = idColumnName;
			getValuesFromColumnReq.IsValueEncrypted = isValueEncrypted;
			getValuesFromColumnReq.ColumnName = columnName;
			return base.Post<GetValuesFromColumnReq, GetValuesFromColumnResp>(getValuesFromColumnReq, "referencetablesetting/valuesfromcolumn").Values;
		}
	}
}
