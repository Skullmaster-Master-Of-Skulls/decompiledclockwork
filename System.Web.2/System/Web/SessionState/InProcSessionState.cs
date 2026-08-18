using System;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x0200011D RID: 285
	internal sealed class InProcSessionState
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00030FCC File Offset: 0x0002F1CC
		internal InProcSessionState(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool locked, DateTime utcLockDate, int lockCookie, int flags)
		{
			this.Copy(sessionItems, staticObjects, timeout, locked, utcLockDate, lockCookie, flags);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00030FE5 File Offset: 0x0002F1E5
		internal void Copy(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool locked, DateTime utcLockDate, int lockCookie, int flags)
		{
			this._sessionItems = sessionItems;
			this._staticObjects = staticObjects;
			this._timeout = timeout;
			this._locked = locked;
			this._utcLockDate = utcLockDate;
			this._lockCookie = lockCookie;
			this._flags = flags;
		}

		// Token: 0x040013DD RID: 5085
		internal ISessionStateItemCollection _sessionItems;

		// Token: 0x040013DE RID: 5086
		internal HttpStaticObjectsCollection _staticObjects;

		// Token: 0x040013DF RID: 5087
		internal int _timeout;

		// Token: 0x040013E0 RID: 5088
		internal bool _locked;

		// Token: 0x040013E1 RID: 5089
		internal DateTime _utcLockDate;

		// Token: 0x040013E2 RID: 5090
		internal int _lockCookie;

		// Token: 0x040013E3 RID: 5091
		internal ReadWriteSpinLock _spinLock;

		// Token: 0x040013E4 RID: 5092
		internal int _flags;
	}
}
