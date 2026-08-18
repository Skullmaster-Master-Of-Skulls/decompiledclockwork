using System;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x02000897 RID: 2199
	[Serializable]
	internal class CachedVary
	{
		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x06006727 RID: 26407 RVA: 0x0016C2E0 File Offset: 0x0016A4E0
		internal Guid CachedVaryId
		{
			get
			{
				return this._cachedVaryId;
			}
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x0016C2E8 File Offset: 0x0016A4E8
		internal CachedVary(string[] contentEncodings, string[] headers, string[] parameters, bool varyByAllParams, string varyByCustom)
		{
			this._contentEncodings = contentEncodings;
			this._headers = headers;
			this._params = parameters;
			this._varyByAllParams = varyByAllParams;
			this._varyByCustom = varyByCustom;
			this._cachedVaryId = Guid.NewGuid();
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x0016C320 File Offset: 0x0016A520
		public override bool Equals(object obj)
		{
			CachedVary cachedVary = obj as CachedVary;
			return cachedVary != null && (this._varyByAllParams == cachedVary._varyByAllParams && this._varyByCustom == cachedVary._varyByCustom && StringUtil.StringArrayEquals(this._contentEncodings, cachedVary._contentEncodings) && StringUtil.StringArrayEquals(this._headers, cachedVary._headers)) && StringUtil.StringArrayEquals(this._params, cachedVary._params);
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x0016C394 File Offset: 0x0016A594
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddObject(this._varyByAllParams);
			hashCodeCombiner.AddObject(this._varyByCustom);
			hashCodeCombiner.AddArray(this._contentEncodings);
			hashCodeCombiner.AddArray(this._headers);
			hashCodeCombiner.AddArray(this._params);
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x04003547 RID: 13639
		private Guid _cachedVaryId;

		// Token: 0x04003548 RID: 13640
		internal readonly string[] _contentEncodings;

		// Token: 0x04003549 RID: 13641
		internal readonly string[] _headers;

		// Token: 0x0400354A RID: 13642
		internal readonly string[] _params;

		// Token: 0x0400354B RID: 13643
		internal readonly string _varyByCustom;

		// Token: 0x0400354C RID: 13644
		internal readonly bool _varyByAllParams;
	}
}
