using System;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020003C1 RID: 961
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ControlCachePolicy
	{
		// Token: 0x06002F12 RID: 12050 RVA: 0x000D2465 File Offset: 0x000D1465
		internal ControlCachePolicy()
		{
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000D246D File Offset: 0x000D146D
		internal ControlCachePolicy(BasePartialCachingControl pcc)
		{
			this._pcc = pcc;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000D247C File Offset: 0x000D147C
		internal static ControlCachePolicy GetCachePolicyStub()
		{
			return ControlCachePolicy._cachePolicyStub;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000D2483 File Offset: 0x000D1483
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

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000D24BB File Offset: 0x000D14BB
		public bool SupportsCaching
		{
			get
			{
				return this._pcc != null;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x000D24C9 File Offset: 0x000D14C9
		// (set) Token: 0x06002F18 RID: 12056 RVA: 0x000D24DF File Offset: 0x000D14DF
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

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000D24F6 File Offset: 0x000D14F6
		// (set) Token: 0x06002F1A RID: 12058 RVA: 0x000D2509 File Offset: 0x000D1509
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

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000D251D File Offset: 0x000D151D
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				this.CheckValidCallingContext();
				return this._pcc.VaryByParams;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000D2530 File Offset: 0x000D1530
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x000D2543 File Offset: 0x000D1543
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

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000D2557 File Offset: 0x000D1557
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x000D256A File Offset: 0x000D156A
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

		// Token: 0x06002F20 RID: 12064 RVA: 0x000D257E File Offset: 0x000D157E
		public void SetVaryByCustom(string varyByCustom)
		{
			this.CheckValidCallingContext();
			this._pcc._varyByCustom = varyByCustom;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000D2592 File Offset: 0x000D1592
		public void SetSlidingExpiration(bool useSlidingExpiration)
		{
			this.CheckValidCallingContext();
			this._pcc._useSlidingExpiration = useSlidingExpiration;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000D25A6 File Offset: 0x000D15A6
		public void SetExpires(DateTime expirationTime)
		{
			this.CheckValidCallingContext();
			this._pcc._utcExpirationTime = DateTimeUtil.ConvertToUniversalTime(expirationTime);
		}

		// Token: 0x040021C2 RID: 8642
		private static ControlCachePolicy _cachePolicyStub = new ControlCachePolicy();

		// Token: 0x040021C3 RID: 8643
		private BasePartialCachingControl _pcc;
	}
}
