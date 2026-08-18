using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002ED RID: 749
	[Serializable]
	internal class ControlCachedVary
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x00071B0A File Offset: 0x0006FD0A
		internal Guid CachedVaryId
		{
			get
			{
				return this._cachedVaryId;
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00071B12 File Offset: 0x0006FD12
		internal ControlCachedVary(string[] varyByParams, string[] varyByControls, string varyByCustom)
		{
			this._varyByParams = varyByParams;
			this._varyByControls = varyByControls;
			this._varyByCustom = varyByCustom;
			this._cachedVaryId = Guid.NewGuid();
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00071B3C File Offset: 0x0006FD3C
		public override bool Equals(object obj)
		{
			if (!(obj is ControlCachedVary))
			{
				return false;
			}
			ControlCachedVary controlCachedVary = (ControlCachedVary)obj;
			return this._varyByCustom == controlCachedVary._varyByCustom && StringUtil.StringArrayEquals(this._varyByParams, controlCachedVary._varyByParams) && StringUtil.StringArrayEquals(this._varyByControls, controlCachedVary._varyByControls);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00071B94 File Offset: 0x0006FD94
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddInt(StringUtil.GetNonRandomizedHashCode(this._varyByCustom, false));
			hashCodeCombiner.AddArray(this._varyByParams);
			hashCodeCombiner.AddArray(this._varyByControls);
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x04001C74 RID: 7284
		private Guid _cachedVaryId;

		// Token: 0x04001C75 RID: 7285
		internal readonly string[] _varyByParams;

		// Token: 0x04001C76 RID: 7286
		internal readonly string _varyByCustom;

		// Token: 0x04001C77 RID: 7287
		internal readonly string[] _varyByControls;
	}
}
