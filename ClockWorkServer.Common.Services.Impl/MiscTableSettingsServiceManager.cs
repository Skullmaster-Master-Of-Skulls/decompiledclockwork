using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.Core.Mappers.Authentication;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Login;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006D RID: 109
	public class MiscTableSettingsServiceManager : IMiscTableSettings, IService
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x000131EC File Offset: 0x000113EC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013200 File Offset: 0x00011400
		public LoadMiscSettingValueResp LoadMiscSettingValue(LoadMiscSettingValueReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			string value = miscTableSettingsManager.LoadMiscSettingValue(Request.Code);
			return new LoadMiscSettingValueResp
			{
				Value = value
			};
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013238 File Offset: 0x00011438
		public void SaveMiscSettingValue(SaveMiscSettingValueReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			miscTableSettingsManager.SaveMiscSettingValue(Request.Code, Request.Value);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013268 File Offset: 0x00011468
		public LoadLdapConnectionInfoResp LoadLdapConnectionInfo(LoadLdapConnectionInfoReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			LdapConnectionInfo ldapConnectionInfo = miscTableSettingsManager.LoadLdapConnectionInfo();
			return new LoadLdapConnectionInfoResp
			{
				Info = ldapConnectionInfo.ToDTO()
			};
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000132A0 File Offset: 0x000114A0
		public void SaveLdapConnectionInfo(SaveLdapConnectionInfoReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			miscTableSettingsManager.SaveLdapConnectionInfo(Request.Info.ToDomainObject());
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000132CC File Offset: 0x000114CC
		public GetLoginMethodResp GetLoginMethod(GetLoginMethodReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			eLoginMethod loginMethod = miscTableSettingsManager.GetLoginMethod();
			return new GetLoginMethodResp
			{
				Method = (eLoginMethodDTO)loginMethod
			};
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013300 File Offset: 0x00011500
		public void SetLoginMethod(SetLoginMethodReq Request)
		{
			IMiscTableSettingsManager miscTableSettingsManager = new MiscTableSettingsManager(Request.GetOperationContext());
			miscTableSettingsManager.SetLoginMethod((eLoginMethod)Request.Method);
		}
	}
}
