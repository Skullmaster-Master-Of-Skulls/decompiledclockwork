using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.ICore.UserSettingsPermissions
{
	// Token: 0x02000014 RID: 20
	public interface IMiscCodeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000077 RID: 119
		string LoadMiscCodeValue(eMiscCode miscCode);

		// Token: 0x06000078 RID: 120
		Task<string> LoadMiscCodeValueAsync(eMiscCode miscCode);

		// Token: 0x06000079 RID: 121
		void SaveMiscCodeValue(eMiscCode miscCode, string newValue);

		// Token: 0x0600007A RID: 122
		Task SaveMiscCodeValueAsync(eMiscCode miscCode, string newValue);
	}
}
