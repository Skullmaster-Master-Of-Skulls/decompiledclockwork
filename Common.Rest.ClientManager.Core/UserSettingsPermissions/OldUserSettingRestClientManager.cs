using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.UserSettingsPermissions
{
	// Token: 0x02000005 RID: 5
	public class OldUserSettingRestClientManager : BearerTokenRestProxy<IOldUserSettingClientManager>, IOldUserSettingClientManager, IWebService
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000026A3 File Offset: 0x000008A3
		public OldUserSettingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026AD File Offset: 0x000008AD
		public OldUserSettingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026B8 File Offset: 0x000008B8
		public OldUserSettingDTO GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode)
		{
			return base.Get<OldUserSettingDTO>(string.Format("oldusersetting/userpersonalsettingvalue/personid/{0}/settingcode/{1}", PersonId, SettingCode), true);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026D8 File Offset: 0x000008D8
		public void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal)
		{
			SetUserPersonalSettingValueReq setUserPersonalSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetUserPersonalSettingValueReq>();
			setUserPersonalSettingValueReq.PersonId = PersonId;
			setUserPersonalSettingValueReq.SettingCode = SettingCode;
			setUserPersonalSettingValueReq.IntVal = IntVal;
			setUserPersonalSettingValueReq.StringVal = StringVal;
			base.Post<SetUserPersonalSettingValueReq>(setUserPersonalSettingValueReq, "oldusersetting/personalsettingvalue");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002719 File Offset: 0x00000919
		public string GetSettingValue_String(eSettingCode SettingCode)
		{
			return base.Get<string>(string.Format("oldusersetting/settingvaluestring/settingcode/{0}", SettingCode), true);
		}
	}
}
