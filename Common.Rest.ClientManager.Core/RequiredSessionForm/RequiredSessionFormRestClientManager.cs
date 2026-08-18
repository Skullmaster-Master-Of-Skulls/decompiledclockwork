using System;
using TechnoPro.Common.ClientManager.ICore.RequiredSessionForm;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.RequiredSessionForm
{
	// Token: 0x02000020 RID: 32
	public class RequiredSessionFormRestClientManager : BearerTokenRestProxy<IRequiredSessionFormClientManager>, IRequiredSessionFormClientManager, IWebService
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000046AD File Offset: 0x000028AD
		public RequiredSessionFormRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000046B7 File Offset: 0x000028B7
		public RequiredSessionFormRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000046C2 File Offset: 0x000028C2
		public RequiredSessionFormItem[] GetRequiredSessionFormInfo()
		{
			string settingValue = ObjectFactory.Resolve<IWebSettingsClientManager>().GetSettingValue<string>(Setting.REQUIREDSESSIONFORM_RequiredFormInfos);
			if (settingValue == null)
			{
				return null;
			}
			return settingValue.RequiredSessionsFormItemFromXml();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000046DE File Offset: 0x000028DE
		public int LoadInfoPmIdForCurrentSession(int StudentPersonId, int ScreenNum)
		{
			return base.Get<int>(string.Format("requiredsessionform/infopmidforcurrentsession/studentpersonid/{0}/screennum/{1}", StudentPersonId, ScreenNum), true);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000046FD File Offset: 0x000028FD
		public int LoadInfoPmIdForSession(int StudentPersonId, int ScreenNum, DateTime DateInSession)
		{
			return base.Get<int>(string.Format("requiredsessionform/infopmidforsession/studentpersonid/{0}/screennum/{1}/dateinsession/{2}", StudentPersonId, ScreenNum, DateInSession), true);
		}
	}
}
