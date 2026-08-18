using System;
using System.Collections;
using System.Web.Management;

namespace System.Web.Caching
{
	// Token: 0x02000880 RID: 2176
	internal sealed class CacheEntry : CacheKey
	{
		// Token: 0x06006665 RID: 26213 RVA: 0x00168E7C File Offset: 0x0016707C
		internal CacheEntry(string key, object value, CacheDependency dependency, CacheItemRemovedCallback onRemovedHandler, DateTime utcAbsoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, bool isPublic, CacheInternal cache) : base(key, isPublic)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (slidingExpiration < TimeSpan.Zero || CacheEntry.OneYear < slidingExpiration)
			{
				throw new ArgumentOutOfRangeException("slidingExpiration");
			}
			if (utcAbsoluteExpiration != Cache.NoAbsoluteExpiration && slidingExpiration != Cache.NoSlidingExpiration)
			{
				throw new ArgumentException(SR.GetString("Invalid_expiration_combination"));
			}
			if (priority < CacheItemPriority.Low || CacheItemPriority.NotRemovable < priority)
			{
				throw new ArgumentOutOfRangeException("priority");
			}
			this._value = value;
			this._dependency = dependency;
			this._onRemovedTargets = onRemovedHandler;
			this._utcCreated = DateTime.UtcNow;
			this._slidingExpiration = slidingExpiration;
			if (this._slidingExpiration > TimeSpan.Zero)
			{
				this._utcExpires = this._utcCreated + this._slidingExpiration;
			}
			else
			{
				this._utcExpires = utcAbsoluteExpiration;
			}
			this._expiresEntryRef = ExpiresEntryRef.INVALID;
			this._expiresBucket = byte.MaxValue;
			this._usageEntryRef = UsageEntryRef.INVALID;
			if (priority == CacheItemPriority.NotRemovable)
			{
				this._usageBucket = byte.MaxValue;
			}
			else
			{
				this._usageBucket = (byte)(priority - CacheItemPriority.Low);
			}
			this._cache = cache;
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x00168FAA File Offset: 0x001671AA
		internal object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06006667 RID: 26215 RVA: 0x00168FB2 File Offset: 0x001671B2
		internal DateTime UtcCreated
		{
			get
			{
				return this._utcCreated;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x00168FBA File Offset: 0x001671BA
		// (set) Token: 0x06006669 RID: 26217 RVA: 0x00168FC6 File Offset: 0x001671C6
		internal CacheEntry.EntryState State
		{
			get
			{
				return (CacheEntry.EntryState)(this._bits & 31);
			}
			set
			{
				this._bits = (byte)(((int)this._bits & -32) | (int)value);
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x00168FDA File Offset: 0x001671DA
		// (set) Token: 0x0600666B RID: 26219 RVA: 0x00168FE2 File Offset: 0x001671E2
		internal DateTime UtcExpires
		{
			get
			{
				return this._utcExpires;
			}
			set
			{
				this._utcExpires = value;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x0600666C RID: 26220 RVA: 0x00168FEB File Offset: 0x001671EB
		internal TimeSpan SlidingExpiration
		{
			get
			{
				return this._slidingExpiration;
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x00168FF3 File Offset: 0x001671F3
		// (set) Token: 0x0600666E RID: 26222 RVA: 0x00168FFB File Offset: 0x001671FB
		internal byte ExpiresBucket
		{
			get
			{
				return this._expiresBucket;
			}
			set
			{
				this._expiresBucket = value;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x0600666F RID: 26223 RVA: 0x00169004 File Offset: 0x00167204
		// (set) Token: 0x06006670 RID: 26224 RVA: 0x0016900C File Offset: 0x0016720C
		internal ExpiresEntryRef ExpiresEntryRef
		{
			get
			{
				return this._expiresEntryRef;
			}
			set
			{
				this._expiresEntryRef = value;
			}
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x00169015 File Offset: 0x00167215
		internal bool HasExpiration()
		{
			return this._utcExpires < DateTime.MaxValue;
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x00169027 File Offset: 0x00167227
		internal bool InExpires()
		{
			return !this._expiresEntryRef.IsInvalid;
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x00169037 File Offset: 0x00167237
		internal byte UsageBucket
		{
			get
			{
				return this._usageBucket;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x0016903F File Offset: 0x0016723F
		// (set) Token: 0x06006675 RID: 26229 RVA: 0x00169047 File Offset: 0x00167247
		internal UsageEntryRef UsageEntryRef
		{
			get
			{
				return this._usageEntryRef;
			}
			set
			{
				this._usageEntryRef = value;
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06006676 RID: 26230 RVA: 0x00169050 File Offset: 0x00167250
		// (set) Token: 0x06006677 RID: 26231 RVA: 0x00169058 File Offset: 0x00167258
		internal DateTime UtcLastUsageUpdate
		{
			get
			{
				return this._utcLastUpdate;
			}
			set
			{
				this._utcLastUpdate = value;
			}
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x00169061 File Offset: 0x00167261
		internal bool HasUsage()
		{
			return this._usageBucket != byte.MaxValue;
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x00169073 File Offset: 0x00167273
		internal bool InUsage()
		{
			return !this._usageEntryRef.IsInvalid;
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x00169083 File Offset: 0x00167283
		internal CacheDependency Dependency
		{
			get
			{
				return this._dependency;
			}
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0016908C File Offset: 0x0016728C
		internal void MonitorDependencyChanges()
		{
			CacheDependency dependency = this._dependency;
			if (dependency != null && this.State == CacheEntry.EntryState.AddedToCache)
			{
				if (!dependency.TakeOwnership())
				{
					throw new InvalidOperationException(SR.GetString("Cache_dependency_used_more_that_once"));
				}
				dependency.SetCacheDependencyChanged(delegate(object sender, EventArgs args)
				{
					this.DependencyChanged(sender, args);
				});
			}
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x001690D6 File Offset: 0x001672D6
		private void DependencyChanged(object sender, EventArgs e)
		{
			if (this.State == CacheEntry.EntryState.AddedToCache)
			{
				this._cache.Remove(this, CacheItemRemovedReason.DependencyChanged);
			}
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x001690F0 File Offset: 0x001672F0
		private void CallCacheItemRemovedCallback(CacheItemRemovedCallback callback, CacheItemRemovedReason reason)
		{
			if (base.IsPublic)
			{
				try
				{
					if (HttpContext.Current == null)
					{
						using (new ApplicationImpersonationContext())
						{
							callback(this._key, this._value, reason);
							goto IL_47;
						}
					}
					callback(this._key, this._value, reason);
					IL_47:
					return;
				}
				catch (Exception ex)
				{
					HttpApplicationFactory.RaiseError(ex);
					try
					{
						WebBaseEvent.RaiseRuntimeError(ex, this);
					}
					catch
					{
					}
					return;
				}
			}
			try
			{
				using (new ApplicationImpersonationContext())
				{
					callback(this._key, this._value, reason);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x001691C8 File Offset: 0x001673C8
		internal void Close(CacheItemRemovedReason reason)
		{
			this.State = CacheEntry.EntryState.Closed;
			object obj = null;
			object[] array = null;
			lock (this)
			{
				if (this._onRemovedTargets != null)
				{
					obj = this._onRemovedTargets;
					if (obj is Hashtable)
					{
						ICollection keys = ((Hashtable)obj).Keys;
						array = new object[keys.Count];
						keys.CopyTo(array, 0);
					}
				}
			}
			if (obj != null)
			{
				if (array != null)
				{
					foreach (object obj2 in array)
					{
						if (obj2 is CacheDependency)
						{
							((CacheDependency)obj2).ItemRemoved();
						}
						else
						{
							this.CallCacheItemRemovedCallback((CacheItemRemovedCallback)obj2, reason);
						}
					}
				}
				else if (obj is CacheItemRemovedCallback)
				{
					this.CallCacheItemRemovedCallback((CacheItemRemovedCallback)obj, reason);
				}
				else
				{
					((CacheDependency)obj).ItemRemoved();
				}
			}
			if (this._dependency != null)
			{
				this._dependency.DisposeInternal();
			}
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x001692C4 File Offset: 0x001674C4
		internal void AddDependent(CacheDependency dependency)
		{
			lock (this)
			{
				if (this._onRemovedTargets == null)
				{
					this._onRemovedTargets = dependency;
				}
				else if (this._onRemovedTargets is Hashtable)
				{
					Hashtable hashtable = (Hashtable)this._onRemovedTargets;
					hashtable[dependency] = dependency;
				}
				else
				{
					Hashtable hashtable2 = new Hashtable(2);
					hashtable2[this._onRemovedTargets] = this._onRemovedTargets;
					hashtable2[dependency] = dependency;
					this._onRemovedTargets = hashtable2;
				}
			}
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x00169358 File Offset: 0x00167558
		internal void RemoveDependent(CacheDependency dependency)
		{
			lock (this)
			{
				if (this._onRemovedTargets != null)
				{
					if (this._onRemovedTargets == dependency)
					{
						this._onRemovedTargets = null;
					}
					else if (this._onRemovedTargets is Hashtable)
					{
						Hashtable hashtable = (Hashtable)this._onRemovedTargets;
						hashtable.Remove(dependency);
						if (hashtable.Count == 0)
						{
							this._onRemovedTargets = null;
						}
					}
				}
			}
		}

		// Token: 0x040034BF RID: 13503
		private const CacheItemPriority CacheItemPriorityMin = CacheItemPriority.Low;

		// Token: 0x040034C0 RID: 13504
		private const CacheItemPriority CacheItemPriorityMax = CacheItemPriority.NotRemovable;

		// Token: 0x040034C1 RID: 13505
		private static readonly TimeSpan OneYear = new TimeSpan(365, 0, 0, 0);

		// Token: 0x040034C2 RID: 13506
		private const byte EntryStateMask = 31;

		// Token: 0x040034C3 RID: 13507
		private object _value;

		// Token: 0x040034C4 RID: 13508
		private DateTime _utcCreated;

		// Token: 0x040034C5 RID: 13509
		private DateTime _utcExpires;

		// Token: 0x040034C6 RID: 13510
		private TimeSpan _slidingExpiration;

		// Token: 0x040034C7 RID: 13511
		private byte _expiresBucket;

		// Token: 0x040034C8 RID: 13512
		private ExpiresEntryRef _expiresEntryRef;

		// Token: 0x040034C9 RID: 13513
		private byte _usageBucket;

		// Token: 0x040034CA RID: 13514
		private UsageEntryRef _usageEntryRef;

		// Token: 0x040034CB RID: 13515
		private DateTime _utcLastUpdate;

		// Token: 0x040034CC RID: 13516
		private CacheInternal _cache;

		// Token: 0x040034CD RID: 13517
		private CacheDependency _dependency;

		// Token: 0x040034CE RID: 13518
		private object _onRemovedTargets;

		// Token: 0x02000A76 RID: 2678
		internal enum EntryState : byte
		{
			// Token: 0x04003BB6 RID: 15286
			NotInCache,
			// Token: 0x04003BB7 RID: 15287
			AddingToCache,
			// Token: 0x04003BB8 RID: 15288
			AddedToCache,
			// Token: 0x04003BB9 RID: 15289
			RemovingFromCache = 4,
			// Token: 0x04003BBA RID: 15290
			RemovedFromCache = 8,
			// Token: 0x04003BBB RID: 15291
			Closed = 16
		}
	}
}
