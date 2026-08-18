using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.UserSettingsPermissions
{
	// Token: 0x02000009 RID: 9
	public class OldUserSettingClientManager : IOldUserSettingClientManager, IWebService
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003180 File Offset: 0x00001380
		public OldUserSettingDTO GetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode)
		{
			GetUserPersonalSettingValueReq getUserPersonalSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetUserPersonalSettingValueReq>();
			getUserPersonalSettingValueReq.PersonId = PersonId;
			getUserPersonalSettingValueReq.SettingCode = SettingCode;
			return ClientServiceFactory.GetClientInstance<IOldUserSetting>().GetUserPersonalSettingValue(getUserPersonalSettingValueReq).SettingValue;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000031C0 File Offset: 0x000013C0
		public void SetUserPersonalSettingValue(int PersonId, eSettingCode SettingCode, int IntVal, string StringVal)
		{
			SetUserPersonalSettingValueReq setUserPersonalSettingValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetUserPersonalSettingValueReq>();
			setUserPersonalSettingValueReq.PersonId = PersonId;
			setUserPersonalSettingValueReq.SettingCode = SettingCode;
			setUserPersonalSettingValueReq.IntVal = IntVal;
			setUserPersonalSettingValueReq.StringVal = StringVal;
			ClientServiceFactory.GetClientInstance<IOldUserSetting>().SetUserPersonalSettingValue(setUserPersonalSettingValueReq);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003208 File Offset: 0x00001408
		public string GetSettingValue_String(eSettingCode SettingCode)
		{
			GetSettingValueStringReq getSettingValueStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSettingValueStringReq>();
			getSettingValueStringReq.SettingCode = SettingCode;
			return ClientServiceFactory.GetClientInstance<IOldUserSetting>().GetSettingValueString(getSettingValueStringReq).SettingValue;
		}
	}
}
