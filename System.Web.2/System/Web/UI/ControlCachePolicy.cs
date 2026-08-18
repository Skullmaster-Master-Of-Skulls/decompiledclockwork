using System;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000262 RID: 610
	public sealed class ControlCachePolicy
	{
		// Token: 0x06001D15 RID: 7445 RVA: 0x000030B5 File Offset: 0x000012B5
		internal ControlCachePolicy()
		{
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0005EBC4 File Offset: 0x0005CDC4
		internal ControlCachePolicy(BasePartialCachingControl pcc)
		{
			this._pcc = pcc;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0005EBD3 File Offset: 0x0005CDD3
		internal static ControlCachePolicy GetCachePolicyStub()
		{
			return ControlCachePolicy._cachePolicyStub;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0005EBDA File Offset: 0x0005CDDA
		private void CheckValidCallingContext()
		{
			if (this._pcc == null)
			{
				throw new HttpException(SR.GetString("UC_not_cached"));
			}
			if (this._pcc.ControlState >= ControlState.PreRendered)
			{
				throw new HttpException(SR.GetString("UCCachePolicy_unavailable"));
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0005EC12 File Offset: 0x0005CE12
		public bool SupportsCaching
		{
			get
			{
				return this._pcc != null;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0005EC1D File Offset: 0x0005CE1D
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0005EC33 File Offset: 0x0005CE33
		public bool Cached
		{
			get
			{
				this.CheckValidCallingContext();
				return !this._pcc._cachingDisabled;
			}
			set
			{
				this.CheckValidCallingContext();
				this._pcc._cachingDisabled = !value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0005EC4A File Offset: 0x0005CE4A
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x0005EC5D File Offset: 0x0005CE5D
		public TimeSpan Duration
		{
			get
			{
				this.CheckValidCallingContext();
				return this._pcc.Duration;
			}
			set
			{
				this.CheckValidCallingContext();
				this._pcc.Duration = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0005EC71 File Offset: 0x0005CE71
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				this.CheckValidCallingContext();
				return this._pcc.VaryByParams;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0005EC84 File Offset: 0x0005CE84
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0005EC97 File Offset: 0x0005CE97
		public string VaryByControl
		{
			get
			{
				this.CheckValidCallingContext();
				return this._pcc.VaryByControl;
			}
			set
			{
				this.CheckValidCallingContext();
				this._pcc.VaryByControl = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0005ECAB File Offset: 0x0005CEAB
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0005ECBE File Offset: 0x0005CEBE
		public CacheDependency Dependency
		{
			get
			{
				this.CheckValidCallingContext();
				return this._pcc.Dependency;
			}
			set
			{
				this.CheckValidCallingContext();
				this._pcc.Dependency = value;
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0005ECD2 File Offset: 0x0005CED2
		public void SetVaryByCustom(string varyByCustom)
		{
			this.CheckValidCallingContext();
			this._pcc._varyByCustom = varyByCustom;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0005ECE6 File Offset: 0x0005CEE6
		public void SetSlidingExpiration(bool useSlidingExpiration)
		{
			this.CheckValidCallingContext();
			this._pcc._useSlidingExpiration = useSlidingExpiration;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0005ECFA File Offset: 0x0005CEFA
		public void SetExpires(DateTime expirationTime)
		{
			this.CheckValidCallingContext();
			this._pcc._utcExpirationTime = DateTimeUtil.ConvertToUniversalTime(expirationTime);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0005ED13 File Offset: 0x0005CF13
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0005ED39 File Offset: 0x0005CF39
		public string ProviderName
		{
			get
			{
				this.CheckValidCallingContext();
				if (this._pcc._provider == null)
				{
					return "AspNetInternalProvider";
				}
				return this._pcc._provider;
			}
			set
			{
				this.CheckValidCallingContext();
				if (value == "AspNetInternalProvider")
				{
					value = null;
				}
				OutputCache.ThrowIfProviderNotFound(value);
				this._pcc._provider = value;
			}
		}

		// Token: 0x0400193E RID: 6462
		private static ControlCachePolicy _cachePolicyStub = new ControlCachePolicy();

		// Token: 0x0400193F RID: 6463
		private BasePartialCachingControl _pcc;
	}
}
