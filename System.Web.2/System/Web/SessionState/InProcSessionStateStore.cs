using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Web.Caching;

namespace System.Web.SessionState
{
	// Token: 0x0200011C RID: 284
	internal sealed class InProcSessionStateStore : SessionStateStoreProviderBase
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x000309FC File Offset: 0x0002EBFC
		public void OnCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			PerfCounters.DecrementCounter(AppPerfCounter.SESSIONS_ACTIVE);
			InProcSessionState inProcSessionState = (InProcSessionState)value;
			if ((inProcSessionState._flags & 2) != 0 || (inProcSessionState._flags & 1) != 0)
			{
				return;
			}
			if (reason != CacheItemRemovedReason.Removed)
			{
				if (reason == CacheItemRemovedReason.Expired)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_TIMED_OUT);
				}
			}
			else
			{
				PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_ABANDONED);
			}
			if (this._expireCallback != null)
			{
				string id = key.Substring(InProcSessionStateStore.CACHEKEYPREFIXLENGTH);
				this._expireCallback(id, SessionStateUtility.CreateLegitStoreData(null, inProcSessionState._sessionItems, inProcSessionState._staticObjects, inProcSessionState._timeout));
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00030A7C File Offset: 0x0002EC7C
		private string CreateSessionStateCacheKey(string id)
		{
			return "j" + id;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00030A89 File Offset: 0x0002EC89
		public override void Initialize(string name, NameValueCollection config)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = "InProc Session State Provider";
			}
			base.Initialize(name, config);
			this._callback = new CacheItemRemovedCallback(this.OnCacheItemRemoved);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00030AB4 File Offset: 0x0002ECB4
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			this._expireCallback = expireCallback;
			return true;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00006164 File Offset: 0x00004364
		public override void Dispose()
		{
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00006164 File Offset: 0x00004364
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00030AC0 File Offset: 0x0002ECC0
		private SessionStateStoreData DoGet(HttpContext context, string id, bool exclusive, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			string key = this.CreateSessionStateCacheKey(id);
			locked = false;
			lockId = null;
			lockAge = TimeSpan.Zero;
			actionFlags = SessionStateActions.None;
			SessionIDManager.CheckIdLength(id, true);
			InProcSessionState inProcSessionState = (InProcSessionState)HttpRuntime.Cache.InternalCache.Get(key);
			if (inProcSessionState == null)
			{
				return null;
			}
			int flags = inProcSessionState._flags;
			if ((flags & 1) != 0 && flags == Interlocked.CompareExchange(ref inProcSessionState._flags, flags & -2, flags))
			{
				actionFlags = SessionStateActions.InitializeItem;
			}
			bool flag;
			if (exclusive)
			{
				flag = true;
				if (!inProcSessionState._locked)
				{
					inProcSessionState._spinLock.AcquireWriterLock();
					try
					{
						if (!inProcSessionState._locked)
						{
							flag = false;
							inProcSessionState._locked = true;
							inProcSessionState._utcLockDate = DateTime.UtcNow;
							inProcSessionState._lockCookie++;
						}
						lockId = inProcSessionState._lockCookie;
						goto IL_103;
					}
					finally
					{
						inProcSessionState._spinLock.ReleaseWriterLock();
					}
				}
				lockId = inProcSessionState._lockCookie;
			}
			else
			{
				inProcSessionState._spinLock.AcquireReaderLock();
				try
				{
					flag = inProcSessionState._locked;
					lockId = inProcSessionState._lockCookie;
				}
				finally
				{
					inProcSessionState._spinLock.ReleaseReaderLock();
				}
			}
			IL_103:
			if (flag)
			{
				locked = true;
				lockAge = DateTime.UtcNow - inProcSessionState._utcLockDate;
				return null;
			}
			return SessionStateUtility.CreateLegitStoreData(context, inProcSessionState._sessionItems, inProcSessionState._staticObjects, inProcSessionState._timeout);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00030C28 File Offset: 0x0002EE28
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.DoGet(context, id, false, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00030C3A File Offset: 0x0002EE3A
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
		{
			return this.DoGet(context, id, true, out locked, out lockAge, out lockId, out actionFlags);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			string key = this.CreateSessionStateCacheKey(id);
			int num = (int)lockId;
			SessionIDManager.CheckIdLength(id, true);
			InProcSessionState inProcSessionState = (InProcSessionState)HttpRuntime.Cache.InternalCache.Get(key);
			if (inProcSessionState == null)
			{
				return;
			}
			if (inProcSessionState._locked)
			{
				inProcSessionState._spinLock.AcquireWriterLock();
				try
				{
					if (inProcSessionState._locked && num == inProcSessionState._lockCookie)
					{
						inProcSessionState._locked = false;
					}
				}
				finally
				{
					inProcSessionState._spinLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00030CD4 File Offset: 0x0002EED4
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			string key = this.CreateSessionStateCacheKey(id);
			bool flag = true;
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			int lockCookie = InProcSessionStateStore.NewLockCookie;
			ISessionStateItemCollection sessionItems = null;
			HttpStaticObjectsCollection staticObjects = null;
			SessionIDManager.CheckIdLength(id, true);
			if (item.Items.Count > 0)
			{
				sessionItems = item.Items;
			}
			if (!item.StaticObjects.NeverAccessed)
			{
				staticObjects = item.StaticObjects;
			}
			if (!newItem)
			{
				InProcSessionState inProcSessionState = (InProcSessionState)internalCache.Get(key);
				int num = (int)lockId;
				if (inProcSessionState == null)
				{
					return;
				}
				inProcSessionState._spinLock.AcquireWriterLock();
				try
				{
					if (!inProcSessionState._locked || inProcSessionState._lockCookie != num)
					{
						return;
					}
					if (inProcSessionState._timeout == item.Timeout)
					{
						inProcSessionState.Copy(sessionItems, staticObjects, item.Timeout, false, DateTime.MinValue, num, inProcSessionState._flags);
						flag = false;
					}
					else
					{
						inProcSessionState._flags |= 2;
						lockCookie = num;
						inProcSessionState._lockCookie = 0;
					}
				}
				finally
				{
					inProcSessionState._spinLock.ReleaseWriterLock();
				}
			}
			if (flag)
			{
				InProcSessionState inProcSessionState2 = new InProcSessionState(sessionItems, staticObjects, item.Timeout, false, DateTime.MinValue, lockCookie, 0);
				try
				{
				}
				finally
				{
					internalCache.Insert(key, inProcSessionState2, new CacheInsertOptions
					{
						SlidingExpiration = new TimeSpan(0, inProcSessionState2._timeout, 0),
						Priority = CacheItemPriority.NotRemovable,
						OnRemovedCallback = this._callback
					});
					PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_TOTAL);
					PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_ACTIVE);
				}
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00030E58 File Offset: 0x0002F058
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			string key = this.CreateSessionStateCacheKey(id);
			SessionIDManager.CheckIdLength(id, true);
			InProcSessionState item = new InProcSessionState(null, null, timeout, false, DateTime.MinValue, InProcSessionStateStore.NewLockCookie, 1);
			try
			{
			}
			finally
			{
				if (HttpRuntime.Cache.InternalCache.Add(key, item, new CacheInsertOptions
				{
					SlidingExpiration = new TimeSpan(0, timeout, 0),
					Priority = CacheItemPriority.NotRemovable,
					OnRemovedCallback = this._callback
				}) == null)
				{
					PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_TOTAL);
					PerfCounters.IncrementCounter(AppPerfCounter.SESSIONS_ACTIVE);
				}
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00030EE8 File Offset: 0x0002F0E8
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			string key = this.CreateSessionStateCacheKey(id);
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			int num = (int)lockId;
			SessionIDManager.CheckIdLength(id, true);
			InProcSessionState inProcSessionState = (InProcSessionState)internalCache.Get(key);
			if (inProcSessionState == null)
			{
				return;
			}
			inProcSessionState._spinLock.AcquireWriterLock();
			try
			{
				if (!inProcSessionState._locked || inProcSessionState._lockCookie != num)
				{
					return;
				}
				inProcSessionState._lockCookie = 0;
			}
			finally
			{
				inProcSessionState._spinLock.ReleaseWriterLock();
			}
			internalCache.Remove(key);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00030F74 File Offset: 0x0002F174
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			string key = this.CreateSessionStateCacheKey(id);
			SessionIDManager.CheckIdLength(id, true);
			HttpRuntime.Cache.InternalCache.Get(key);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return SessionStateUtility.CreateLegitStoreData(context, null, null, timeout);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00006164 File Offset: 0x00004364
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void TraceSessionStats()
		{
		}

		// Token: 0x040013D9 RID: 5081
		internal static readonly int CACHEKEYPREFIXLENGTH = "j".Length;

		// Token: 0x040013DA RID: 5082
		internal static readonly int NewLockCookie = 1;

		// Token: 0x040013DB RID: 5083
		private CacheItemRemovedCallback _callback;

		// Token: 0x040013DC RID: 5084
		private SessionStateItemExpireCallback _expireCallback;
	}
}
