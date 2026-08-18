using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200015F RID: 351
	public class OldUserSettingReusableClientProxy : WCFTokenBasedReusableClientProxy<IOldUserSetting>, IOldUserSetting, IService
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x00021726 File Offset: 0x0001F926
		public OldUserSettingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00021731 File Offset: 0x0001F931
		public OldUserSettingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00021740 File Offset: 0x0001F940
		public void UpdateGroupSettings(UpdateGroupSettingsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateGroupSettings(Request);
			});
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00021778 File Offset: 0x0001F978
		public void UpdateUserSettings(UpdateUserSettingsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateUserSettings(Request);
			});
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000217B0 File Offset: 0x0001F9B0
		public LoadAllUserSettingsResp LoadAllUserSettings(LoadAllUserSettingsReq Request)
		{
			return this.WrapServiceMethod<LoadAllUserSettingsResp>(() => this.Proxy.LoadAllUserSettings(Request));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000217E8 File Offset: 0x0001F9E8
		public void SaveSettings(SaveSettingsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveSettings(Request);
			});
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00021820 File Offset: 0x0001FA20
		public LoadEveryoneSettingsResp LoadEveryoneSettings(LoadEveryoneSettingsReq Request)
		{
			return this.WrapServiceMethod<LoadEveryoneSettingsResp>(() => this.Proxy.LoadEveryoneSettings(Request));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00021858 File Offset: 0x0001FA58
		public LoadGroupSettingsResp LoadGroupSettings(LoadGroupSettingsReq Request)
		{
			return this.WrapServiceMethod<LoadGroupSettingsResp>(() => this.Proxy.LoadGroupSettings(Request));
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00021890 File Offset: 0x0001FA90
		public LoadPersonSettingsResp LoadPersonSettings(LoadPersonSettingsReq Request)
		{
			return this.WrapServiceMethod<LoadPersonSettingsResp>(() => this.Proxy.LoadPersonSettings(Request));
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000218C8 File Offset: 0x0001FAC8
		public void ClearCacheForUser(ClearCacheForUserReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearCacheForUser(Request);
			});
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00021900 File Offset: 0x0001FB00
		public GetUserPersonalSettingValueResp GetUserPersonalSettingValue(GetUserPersonalSettingValueReq Request)
		{
			return this.WrapServiceMethod<GetUserPersonalSettingValueResp>(() => this.Proxy.GetUserPersonalSettingValue(Request));
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00021938 File Offset: 0x0001FB38
		public void SetUserPersonalSettingValue(SetUserPersonalSettingValueReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetUserPersonalSettingValue(Request);
			});
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00021970 File Offset: 0x0001FB70
		public LoadUserSettingReportForUserSetResp LoadUserSettingReportForUserSet(LoadUserSettingReportForUserSetReq Request)
		{
			return this.WrapServiceMethod<LoadUserSettingReportForUserSetResp>(() => this.Proxy.LoadUserSettingReportForUserSet(Request));
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000219A8 File Offset: 0x0001FBA8
		public GetSettingValueStringResp GetSettingValueString(GetSettingValueStringReq Request)
		{
			return this.WrapServiceMethod<GetSettingValueStringResp>(() => this.Proxy.GetSettingValueString(Request));
		}
	}
}
