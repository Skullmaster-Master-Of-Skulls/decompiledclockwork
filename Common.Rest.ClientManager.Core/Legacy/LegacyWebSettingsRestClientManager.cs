using System;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Legacy
{
	// Token: 0x0200003E RID: 62
	public class LegacyWebSettingsRestClientManager : BearerTokenRestProxy<ILegacyWebSettingsClientManager>, ILegacyWebSettingsClientManager, IWebService
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000742B File Offset: 0x0000562B
		public LegacyWebSettingsRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007435 File Offset: 0x00005635
		public LegacyWebSettingsRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007440 File Offset: 0x00005640
		public string GetWebSettingValue(int webSetting, string instanceName)
		{
			return base.Get<string>(string.Format("legacywebsettings/websetting/{0}/instancename/{1}", webSetting, instanceName), true);
		}
	}
}
