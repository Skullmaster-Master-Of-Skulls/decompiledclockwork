using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000078 RID: 120
	public interface ILegacyWebSettingsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600035A RID: 858
		string GetWebSettingValue(int webSetting, string instanceName);
	}
}
