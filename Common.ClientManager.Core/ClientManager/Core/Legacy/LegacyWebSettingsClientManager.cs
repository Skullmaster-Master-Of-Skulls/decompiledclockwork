using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.WebSettings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x0200004C RID: 76
	public class LegacyWebSettingsClientManager : ILegacyWebSettingsClientManager, IWebService
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000C244 File Offset: 0x0000A444
		public string GetWebSettingValue(int webSetting, string instanceName)
		{
			GetWebSettingValueReq getWebSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetWebSettingValueReq>();
			getWebSettingValueReq.WebSetting = webSetting;
			getWebSettingValueReq.InstanceName = instanceName;
			return ClientServiceFactory.GetClientInstance<ILegacyWebSettings>().GetWebSettingValue(getWebSettingValueReq).SettingValue;
		}
	}
}
