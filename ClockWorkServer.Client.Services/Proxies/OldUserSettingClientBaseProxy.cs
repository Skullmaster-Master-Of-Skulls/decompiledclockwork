using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000160 RID: 352
	internal class OldUserSettingClientBaseProxy : ClientBase<IOldUserSetting>, IOldUserSetting, IService
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x000219E0 File Offset: 0x0001FBE0
		public OldUserSettingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000219EB File Offset: 0x0001FBEB
		public OldUserSettingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000219F7 File Offset: 0x0001FBF7
		public void UpdateGroupSettings(UpdateGroupSettingsReq Request)
		{
			base.Channel.UpdateGroupSettings(Request);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00021A07 File Offset: 0x0001FC07
		public void UpdateUserSettings(UpdateUserSettingsReq Request)
		{
			base.Channel.UpdateUserSettings(Request);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00021A18 File Offset: 0x0001FC18
		public LoadAllUserSettingsResp LoadAllUserSettings(LoadAllUserSettingsReq Request)
		{
			return base.Channel.LoadAllUserSettings(Request);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00021A36 File Offset: 0x0001FC36
		public void SaveSettings(SaveSettingsReq Request)
		{
			base.Channel.SaveSettings(Request);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00021A48 File Offset: 0x0001FC48
		public LoadEveryoneSettingsResp LoadEveryoneSettings(LoadEveryoneSettingsReq Request)
		{
			return base.Channel.LoadEveryoneSettings(Request);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00021A68 File Offset: 0x0001FC68
		public LoadGroupSettingsResp LoadGroupSettings(LoadGroupSettingsReq Request)
		{
			return base.Channel.LoadGroupSettings(Request);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00021A88 File Offset: 0x0001FC88
		public LoadPersonSettingsResp LoadPersonSettings(LoadPersonSettingsReq Request)
		{
			return base.Channel.LoadPersonSettings(Request);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00021AA6 File Offset: 0x0001FCA6
		public void ClearCacheForUser(ClearCacheForUserReq Request)
		{
			base.Channel.ClearCacheForUser(Request);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		public GetUserPersonalSettingValueResp GetUserPersonalSettingValue(GetUserPersonalSettingValueReq Request)
		{
			return base.Channel.GetUserPersonalSettingValue(Request);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00021AD6 File Offset: 0x0001FCD6
		public void SetUserPersonalSettingValue(SetUserPersonalSettingValueReq Request)
		{
			base.Channel.SetUserPersonalSettingValue(Request);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		public LoadUserSettingReportForUserSetResp LoadUserSettingReportForUserSet(LoadUserSettingReportForUserSetReq Request)
		{
			return base.Channel.LoadUserSettingReportForUserSet(Request);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00021B08 File Offset: 0x0001FD08
		public GetSettingValueStringResp GetSettingValueString(GetSettingValueStringReq Request)
		{
			return base.Channel.GetSettingValueString(Request);
		}
	}
}
