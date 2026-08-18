using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Settings
{
	// Token: 0x0200002D RID: 45
	public interface IMiscTableSettingsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000BF RID: 191
		string LoadMiscSettingValue(int code);

		// Token: 0x060000C0 RID: 192
		void SaveMiscSettingValue(int code, string value);
	}
}
