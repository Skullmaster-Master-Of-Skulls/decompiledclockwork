using System;
using System.Collections.Specialized;
using System.Configuration.Provider;

namespace System.Web.SessionState
{
	// Token: 0x02000123 RID: 291
	public abstract class SessionStateStoreProviderBase : ProviderBase
	{
		// Token: 0x06001184 RID: 4484
		public abstract void Dispose();

		// Token: 0x06001185 RID: 4485
		public abstract bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback);

		// Token: 0x06001186 RID: 4486
		public abstract void InitializeRequest(HttpContext context);

		// Token: 0x06001187 RID: 4487
		public abstract SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions);

		// Token: 0x06001188 RID: 4488
		public abstract SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions);

		// Token: 0x06001189 RID: 4489
		public abstract void ReleaseItemExclusive(HttpContext context, string id, object lockId);

		// Token: 0x0600118A RID: 4490
		public abstract void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem);

		// Token: 0x0600118B RID: 4491
		public abstract void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item);

		// Token: 0x0600118C RID: 4492
		public abstract void ResetItemTimeout(HttpContext context, string id);

		// Token: 0x0600118D RID: 4493
		public abstract SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout);

		// Token: 0x0600118E RID: 4494
		public abstract void CreateUninitializedItem(HttpContext context, string id, int timeout);

		// Token: 0x0600118F RID: 4495
		public abstract void EndRequest(HttpContext context);

		// Token: 0x06001190 RID: 4496 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void Initialize(string name, NameValueCollection config, IPartitionResolver partitionResolver)
		{
		}
	}
}
