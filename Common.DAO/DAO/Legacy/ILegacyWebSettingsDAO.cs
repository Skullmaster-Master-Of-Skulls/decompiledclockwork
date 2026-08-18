using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x02000063 RID: 99
	public interface ILegacyWebSettingsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000241 RID: 577
		string GetWebSettingValue(int webSetting, string instanceName);
	}
}
