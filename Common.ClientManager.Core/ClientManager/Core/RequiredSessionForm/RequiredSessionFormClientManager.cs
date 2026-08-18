using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.RequiredSessionForm;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.RequiredSessionForm
{
	// Token: 0x02000026 RID: 38
	public class RequiredSessionFormClientManager : IRequiredSessionFormClientManager, IWebService
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000064E4 File Offset: 0x000046E4
		public RequiredSessionFormItem[] GetRequiredSessionFormInfo()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.REQUIREDSESSIONFORM_RequiredFormInfos);
			return (settingValue != null) ? settingValue.RequiredSessionsFormItemFromXml() : null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006514 File Offset: 0x00004714
		public int LoadInfoPmIdForCurrentSession(int StudentPersonId, int ScreenNum)
		{
			LoadInfoPmIdForCurrentSessionReq loadInfoPmIdForCurrentSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInfoPmIdForCurrentSessionReq>();
			loadInfoPmIdForCurrentSessionReq.StudentPersonId = StudentPersonId;
			loadInfoPmIdForCurrentSessionReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IRequiredSessionForm>().LoadInfoPmIdForCurrentSession(loadInfoPmIdForCurrentSessionReq).InfoPmId;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006554 File Offset: 0x00004754
		public int LoadInfoPmIdForSession(int StudentPersonId, int ScreenNum, DateTime DateInSession)
		{
			LoadInfoPmIdForSessionReq loadInfoPmIdForSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInfoPmIdForSessionReq>();
			loadInfoPmIdForSessionReq.StudentPersonId = StudentPersonId;
			loadInfoPmIdForSessionReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IRequiredSessionForm>().LoadInfoPmIdForSession(loadInfoPmIdForSessionReq).InfoPmId;
		}
	}
}
