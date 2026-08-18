using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000089 RID: 137
	public class WebSettingsServiceManager : IWebSettings, IService
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0001782C File Offset: 0x00015A2C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017840 File Offset: 0x00015A40
		public GetInstanceNameResp GetInstanceNames(GetInstanceNameReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			return new GetInstanceNameResp
			{
				InstanceNames = webSettingManager.GetInstanceNames()
			};
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017880 File Offset: 0x00015A80
		public GetSettingsByGroupResp GetSettings(GetSettingsByGroupReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			return new GetSettingsByGroupResp
			{
				Settings = webSettingManager.GetSettings(request.SettingGroup).ToDTO()
			};
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000178CC File Offset: 0x00015ACC
		public GetSettingResp GetSetting(GetSettingReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			return new GetSettingResp
			{
				Setting = webSettingManager.GetSetting(request.Setting).ToDTO()
			};
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017918 File Offset: 0x00015B18
		public GetSettingFromStringResp GetSettingFromString(GetSettingFromStringReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			return new GetSettingFromStringResp
			{
				Setting = webSettingManager.GetSetting(request.Setting, request.StringValue).ToDTO()
			};
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017968 File Offset: 0x00015B68
		public void SaveSetting(SaveSettingReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			webSettingManager.Save(request.Setting.ToDomainObject());
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000179A4 File Offset: 0x00015BA4
		public void ClearSettingsCache(ClearSettingsCacheByGroupReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			webSettingManager.RemoveSettings(request.Group);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000179DC File Offset: 0x00015BDC
		public void ClearSettingsCache(ClearSettingsCacheReq request)
		{
			SettingsOperationContext operationContext = request.GetOperationContext<SettingsOperationContext>();
			operationContext.InstanceName = request.InstanceName;
			IWebSettingManager webSettingManager = new WebSettingManager(operationContext);
			webSettingManager.ClearCache();
		}
	}
}
