using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings;
using TechnoPro.Common.Core.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005D RID: 93
	public class LegacyWebSettingsServiceManager : ILegacyWebSettings, IService
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public GetWebSettingValueResp GetWebSettingValue(GetWebSettingValueReq Request)
		{
			ILegacyWebSettingsManager legacyWebSettingsManager = new LegacyWebSettingsManager(Request.GetOperationContext());
			string webSettingValue = legacyWebSettingsManager.GetWebSettingValue(Request.WebSetting, Request.InstanceName);
			return new GetWebSettingValueResp
			{
				SettingValue = webSettingValue
			};
		}
	}
}
