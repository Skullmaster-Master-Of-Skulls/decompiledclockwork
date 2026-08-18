using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000088 RID: 136
	public class ReferenceTableSettingServiceManager : IReferenceTableSetting, IService
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x000176D4 File Offset: 0x000158D4
		public GetValuesFromColumnResp GetValuesFromColumn(GetValuesFromColumnReq request)
		{
			IReferenceTableSettingManager referenceTableSettingManager = new ReferenceTableSettingManager(request.GetOperationContext());
			bool flag = !string.IsNullOrEmpty(request.OverrideSql);
			GetValuesFromColumnResp result;
			if (flag)
			{
				GetValuesFromColumnResp getValuesFromColumnResp;
				if (!request.OverrideSortByDisplayName)
				{
					(getValuesFromColumnResp = new GetValuesFromColumnResp()).Values = referenceTableSettingManager.GetValues(request.TableName, request.IdColumnName, request.ColumnName, request.IsValueEncrypted, request.OverrideSql);
				}
				else
				{
					(getValuesFromColumnResp = new GetValuesFromColumnResp()).Values = referenceTableSettingManager.GetValues(request.TableName, request.IdColumnName, request.ColumnName, request.IsValueEncrypted, request.OverrideSql, request.OverrideSortByDisplayName);
				}
				result = getValuesFromColumnResp;
			}
			else
			{
				result = new GetValuesFromColumnResp
				{
					Values = referenceTableSettingManager.GetValues(request.TableName, request.IdColumnName, request.ColumnName, request.IsValueEncrypted)
				};
			}
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000177A4 File Offset: 0x000159A4
		public GetValuesFromColumnsResp GetValuesFromColumns(GetValuesFromColumnsReq request)
		{
			IReferenceTableSettingManager referenceTableSettingManager = new ReferenceTableSettingManager(request.GetOperationContext());
			GetValuesFromColumnsResp result;
			if (!string.IsNullOrEmpty(request.OverrideSql))
			{
				(result = new GetValuesFromColumnsResp()).Values = referenceTableSettingManager.GetValues(request.TableName, request.IdColumnName, request.ColumnNames, request.IsValueEncrypted, request.OverrideSql);
			}
			else
			{
				(result = new GetValuesFromColumnsResp()).Values = referenceTableSettingManager.GetValues(request.TableName, request.IdColumnName, request.ColumnNames, request.IsValueEncrypted);
			}
			return result;
		}
	}
}
