using System;
using System.ComponentModel;
using System.Web.Caching;

namespace System.Web.UI
{
	// Token: 0x02000276 RID: 630
	internal class DataSourceCache : IStateManager
	{
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00060C6C File Offset: 0x0005EE6C
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00060C95 File Offset: 0x0005EE95
		public virtual int Duration
		{
			get
			{
				object obj = this.ViewState["Duration"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("DataSourceCache_InvalidDuration"));
				}
				this.ViewState["Duration"] = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00060CC8 File Offset: 0x0005EEC8
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x00060CF1 File Offset: 0x0005EEF1
		public virtual bool Enabled
		{
			get
			{
				object obj = this.ViewState["Enabled"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x00060D0C File Offset: 0x0005EF0C
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x00060D35 File Offset: 0x0005EF35
		public virtual DataSourceCacheExpiry ExpirationPolicy
		{
			get
			{
				object obj = this.ViewState["ExpirationPolicy"];
				if (obj != null)
				{
					return (DataSourceCacheExpiry)obj;
				}
				return DataSourceCacheExpiry.Absolute;
			}
			set
			{
				if (value < DataSourceCacheExpiry.Absolute || value > DataSourceCacheExpiry.Sliding)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("DataSourceCache_InvalidExpiryPolicy"));
				}
				this.ViewState["ExpirationPolicy"] = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00060D68 File Offset: 0x0005EF68
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x00060D95 File Offset: 0x0005EF95
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[WebSysDescription("DataSourceCache_KeyDependency")]
		public virtual string KeyDependency
		{
			get
			{
				object obj = this.ViewState["KeyDependency"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["KeyDependency"] = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00060DA8 File Offset: 0x0005EFA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._tracking)
					{
						this._viewState.TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00060DD6 File Offset: 0x0005EFD6
		public void Invalidate(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (!this.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("DataSourceCache_CacheMustBeEnabled"));
			}
			HttpRuntime.Cache.InternalCache.Remove(key);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00060E14 File Offset: 0x0005F014
		public object LoadDataFromCache(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (!this.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("DataSourceCache_CacheMustBeEnabled"));
			}
			return HttpRuntime.Cache.InternalCache.Get(key);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00060E51 File Offset: 0x0005F051
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00060E62 File Offset: 0x0005F062
		public void SaveDataToCache(string key, object data)
		{
			this.SaveDataToCache(key, data, null);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00060E6D File Offset: 0x0005F06D
		public void SaveDataToCache(string key, object data, CacheDependency dependency)
		{
			this.SaveDataToCacheInternal(key, data, dependency);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00060E78 File Offset: 0x0005F078
		protected virtual void SaveDataToCacheInternal(string key, object data, CacheDependency dependency)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (!this.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("DataSourceCache_CacheMustBeEnabled"));
			}
			DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;
			TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
			DataSourceCacheExpiry expirationPolicy = this.ExpirationPolicy;
			if (expirationPolicy != DataSourceCacheExpiry.Absolute)
			{
				if (expirationPolicy == DataSourceCacheExpiry.Sliding)
				{
					slidingExpiration = TimeSpan.FromSeconds((double)this.Duration);
				}
			}
			else
			{
				absoluteExpiration = DateTime.UtcNow.AddSeconds((double)((this.Duration == 0) ? int.MaxValue : this.Duration));
			}
			AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
			if (this.KeyDependency.Length > 0)
			{
				string[] cachekeys = new string[]
				{
					this.KeyDependency
				};
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					new CacheDependency(null, cachekeys)
				});
			}
			if (dependency != null)
			{
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					dependency
				});
			}
			HttpRuntime.Cache.InternalCache.Insert(key, data, new CacheInsertOptions
			{
				Dependencies = aggregateCacheDependency,
				AbsoluteExpiration = absoluteExpiration,
				SlidingExpiration = slidingExpiration
			});
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00060F7D File Offset: 0x0005F17D
		protected virtual object SaveViewState()
		{
			if (this._viewState == null)
			{
				return null;
			}
			return ((IStateManager)this._viewState).SaveViewState();
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00060F94 File Offset: 0x0005F194
		protected void TrackViewState()
		{
			this._tracking = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00060FB0 File Offset: 0x0005F1B0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00060FB8 File Offset: 0x0005F1B8
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00060FC1 File Offset: 0x0005F1C1
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00060FC9 File Offset: 0x0005F1C9
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04001970 RID: 6512
		public const int Infinite = 0;

		// Token: 0x04001971 RID: 6513
		private bool _tracking;

		// Token: 0x04001972 RID: 6514
		private StateBag _viewState;
	}
}
