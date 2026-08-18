using System;
using System.Threading.Tasks;

namespace Google.Apis.Util.Store
{
	// Token: 0x02000010 RID: 16
	public interface IDataStore
	{
		// Token: 0x0600003B RID: 59
		Task StoreAsync<T>(string key, T value);

		// Token: 0x0600003C RID: 60
		Task DeleteAsync<T>(string key);

		// Token: 0x0600003D RID: 61
		Task<T> GetAsync<T>(string key);

		// Token: 0x0600003E RID: 62
		Task ClearAsync();
	}
}
