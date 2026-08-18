using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.DAO.UserSettingsPermissions
{
	// Token: 0x02000017 RID: 23
	public interface IMiscCodeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000035 RID: 53
		string LoadMiscCodeValue(eMiscCode miscCode);

		// Token: 0x06000036 RID: 54
		Task<string> LoadMiscCodeValueAsync(eMiscCode miscCode);

		// Token: 0x06000037 RID: 55
		void SaveMiscCodeValue(eMiscCode miscCode, string newValue);

		// Token: 0x06000038 RID: 56
		Task SaveMiscCodeValueAsync(eMiscCode miscCode, string newValue);

		// Token: 0x06000039 RID: 57
		void DeleteMiscCodeValue(eMiscCode miscCode);
	}
}
