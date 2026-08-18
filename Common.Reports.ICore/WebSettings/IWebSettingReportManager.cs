using System;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;
using TechnoPro.Common.Reports.Public.Entities.WebSettings;

namespace TechnoPro.Common.Reports.ICore.WebSettings
{
	// Token: 0x02000002 RID: 2
	public interface IWebSettingReportManager : IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x06000001 RID: 1
		string GetCustomWebSettingValue(eWebCustomSetting settingCode);
	}
}
