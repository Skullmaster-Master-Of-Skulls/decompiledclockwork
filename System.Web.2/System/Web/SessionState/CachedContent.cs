using System;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x0200013A RID: 314
	internal sealed class CachedContent
	{
		// Token: 0x060012BC RID: 4796 RVA: 0x0003587A File Offset: 0x00033A7A
		internal CachedContent(byte[] content, IntPtr stateItem, bool locked, DateTime utcLockDate, TimeSpan slidingExpiration, int lockCookie, int extraFlags)
		{
			this._content = content;
			this._stateItem = stateItem;
			this._locked = locked;
			this._utcLockDate = utcLockDate;
			this._slidingExpiration = slidingExpiration;
			this._lockCookie = lockCookie;
			this._extraFlags = extraFlags;
		}

		// Token: 0x040014A1 RID: 5281
		internal byte[] _content;

		// Token: 0x040014A2 RID: 5282
		internal IntPtr _stateItem;

		// Token: 0x040014A3 RID: 5283
		internal bool _locked;

		// Token: 0x040014A4 RID: 5284
		internal DateTime _utcLockDate;

		// Token: 0x040014A5 RID: 5285
		internal TimeSpan _slidingExpiration;

		// Token: 0x040014A6 RID: 5286
		internal int _lockCookie;

		// Token: 0x040014A7 RID: 5287
		internal int _extraFlags;

		// Token: 0x040014A8 RID: 5288
		internal ReadWriteSpinLock _spinLock;
	}
}
