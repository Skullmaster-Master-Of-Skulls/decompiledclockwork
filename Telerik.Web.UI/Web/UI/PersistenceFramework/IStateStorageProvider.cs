using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000C1B RID: 3099
	public interface IStateStorageProvider
	{
		// Token: 0x060075FB RID: 30203
		void SaveStateToStorage(string key, string serializedState);

		// Token: 0x060075FC RID: 30204
		string LoadStateFromStorage(string key);
	}
}
