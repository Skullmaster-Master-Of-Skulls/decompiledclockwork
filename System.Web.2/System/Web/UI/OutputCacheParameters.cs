using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002D1 RID: 721
	public sealed class OutputCacheParameters
	{
		// Token: 0x06002063 RID: 8291 RVA: 0x000680C3 File Offset: 0x000662C3
		internal bool IsParameterSet(OutputCacheParameter value)
		{
			return this._flags[(int)value];
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x000680D1 File Offset: 0x000662D1
		// (set) Token: 0x06002065 RID: 8293 RVA: 0x000680D9 File Offset: 0x000662D9
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._flags[4] = true;
				this._enabled = value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x000680EF File Offset: 0x000662EF
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x000680F7 File Offset: 0x000662F7
		public int Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._flags[2] = true;
				this._duration = value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0006810D File Offset: 0x0006630D
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x00068115 File Offset: 0x00066315
		public OutputCacheLocation Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._flags[8] = true;
				this._location = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0006812B File Offset: 0x0006632B
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x00068133 File Offset: 0x00066333
		public string VaryByCustom
		{
			get
			{
				return this._varyByCustom;
			}
			set
			{
				this._flags[128] = true;
				this._varyByCustom = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0006814D File Offset: 0x0006634D
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x00068155 File Offset: 0x00066355
		public string VaryByParam
		{
			get
			{
				return this._varyByParam;
			}
			set
			{
				this._flags[512] = true;
				this._varyByParam = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x0006816F File Offset: 0x0006636F
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00068177 File Offset: 0x00066377
		public string VaryByContentEncoding
		{
			get
			{
				return this._varyByContentEncoding;
			}
			set
			{
				this._flags[1024] = true;
				this._varyByContentEncoding = value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00068191 File Offset: 0x00066391
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x00068199 File Offset: 0x00066399
		public string VaryByHeader
		{
			get
			{
				return this._varyByHeader;
			}
			set
			{
				this._flags[256] = true;
				this._varyByHeader = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000681B3 File Offset: 0x000663B3
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x000681BB File Offset: 0x000663BB
		public bool NoStore
		{
			get
			{
				return this._noStore;
			}
			set
			{
				this._flags[16] = true;
				this._noStore = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x000681D2 File Offset: 0x000663D2
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x000681DA File Offset: 0x000663DA
		public string SqlDependency
		{
			get
			{
				return this._sqlDependency;
			}
			set
			{
				this._flags[32] = true;
				this._sqlDependency = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x000681F1 File Offset: 0x000663F1
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x000681F9 File Offset: 0x000663F9
		public string VaryByControl
		{
			get
			{
				return this._varyByControl;
			}
			set
			{
				this._flags[64] = true;
				this._varyByControl = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00068210 File Offset: 0x00066410
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x00068218 File Offset: 0x00066418
		public string CacheProfile
		{
			get
			{
				return this._cacheProfile;
			}
			set
			{
				this._flags[1] = true;
				this._cacheProfile = value;
			}
		}

		// Token: 0x04001B26 RID: 6950
		private SimpleBitVector32 _flags;

		// Token: 0x04001B27 RID: 6951
		private bool _enabled = true;

		// Token: 0x04001B28 RID: 6952
		private int _duration;

		// Token: 0x04001B29 RID: 6953
		private OutputCacheLocation _location;

		// Token: 0x04001B2A RID: 6954
		private string _varyByCustom;

		// Token: 0x04001B2B RID: 6955
		private string _varyByParam;

		// Token: 0x04001B2C RID: 6956
		private string _varyByContentEncoding;

		// Token: 0x04001B2D RID: 6957
		private string _varyByHeader;

		// Token: 0x04001B2E RID: 6958
		private bool _noStore;

		// Token: 0x04001B2F RID: 6959
		private string _sqlDependency;

		// Token: 0x04001B30 RID: 6960
		private string _varyByControl;

		// Token: 0x04001B31 RID: 6961
		private string _cacheProfile;
	}
}
