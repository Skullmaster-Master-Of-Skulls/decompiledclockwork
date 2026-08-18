using System;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200043A RID: 1082
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class OutputCacheParameters
	{
		// Token: 0x060033AB RID: 13227 RVA: 0x000E0ECF File Offset: 0x000DFECF
		internal bool IsParameterSet(OutputCacheParameter value)
		{
			return this._flags[(int)value];
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000E0EDD File Offset: 0x000DFEDD
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000E0EE5 File Offset: 0x000DFEE5
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

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000E0EFB File Offset: 0x000DFEFB
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x000E0F03 File Offset: 0x000DFF03
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

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000E0F19 File Offset: 0x000DFF19
		// (set) Token: 0x060033B1 RID: 13233 RVA: 0x000E0F21 File Offset: 0x000DFF21
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

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000E0F37 File Offset: 0x000DFF37
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x000E0F3F File Offset: 0x000DFF3F
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

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000E0F59 File Offset: 0x000DFF59
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000E0F61 File Offset: 0x000DFF61
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

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000E0F7B File Offset: 0x000DFF7B
		// (set) Token: 0x060033B7 RID: 13239 RVA: 0x000E0F83 File Offset: 0x000DFF83
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000E0F9D File Offset: 0x000DFF9D
		// (set) Token: 0x060033B9 RID: 13241 RVA: 0x000E0FA5 File Offset: 0x000DFFA5
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

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000E0FBF File Offset: 0x000DFFBF
		// (set) Token: 0x060033BB RID: 13243 RVA: 0x000E0FC7 File Offset: 0x000DFFC7
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000E0FDE File Offset: 0x000DFFDE
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x000E0FE6 File Offset: 0x000DFFE6
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

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000E0FFD File Offset: 0x000DFFFD
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000E1005 File Offset: 0x000E0005
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000E101C File Offset: 0x000E001C
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000E1024 File Offset: 0x000E0024
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

		// Token: 0x0400244F RID: 9295
		private SimpleBitVector32 _flags;

		// Token: 0x04002450 RID: 9296
		private bool _enabled = true;

		// Token: 0x04002451 RID: 9297
		private int _duration;

		// Token: 0x04002452 RID: 9298
		private OutputCacheLocation _location;

		// Token: 0x04002453 RID: 9299
		private string _varyByCustom;

		// Token: 0x04002454 RID: 9300
		private string _varyByParam;

		// Token: 0x04002455 RID: 9301
		private string _varyByContentEncoding;

		// Token: 0x04002456 RID: 9302
		private string _varyByHeader;

		// Token: 0x04002457 RID: 9303
		private bool _noStore;

		// Token: 0x04002458 RID: 9304
		private string _sqlDependency;

		// Token: 0x04002459 RID: 9305
		private string _varyByControl;

		// Token: 0x0400245A RID: 9306
		private string _cacheProfile;
	}
}
