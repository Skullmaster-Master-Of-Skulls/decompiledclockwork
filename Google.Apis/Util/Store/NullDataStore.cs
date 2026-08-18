using System;
using System.Threading.Tasks;

namespace Google.Apis.Util.Store
{
	// Token: 0x0200000F RID: 15
	public class NullDataStore : IDataStore
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002E6C File Offset: 0x0000106C
		private static Task<T> CompletedTask<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetResult(default(T));
			return taskCompletionSource.Task;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002E92 File Offset: 0x00001092
		public Task ClearAsync()
		{
			return NullDataStore.s_completedTask;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E92 File Offset: 0x00001092
		public Task DeleteAsync<T>(string key)
		{
			return NullDataStore.s_completedTask;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E99 File Offset: 0x00001099
		public Task<T> GetAsync<T>(string key)
		{
			return NullDataStore.CompletedTask<T>();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E92 File Offset: 0x00001092
		public Task StoreAsync<T>(string key, T value)
		{
			return NullDataStore.s_completedTask;
		}

		// Token: 0x0400003D RID: 61
		private static readonly Task s_completedTask = NullDataStore.CompletedTask<int>();
	}
}
