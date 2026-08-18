using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

namespace System.Web.Caching
{
	// Token: 0x02000895 RID: 2197
	internal class SRefMultiple
	{
		// Token: 0x0600671E RID: 26398 RVA: 0x0016C1A5 File Offset: 0x0016A3A5
		internal void AddSRefTarget(object o)
		{
			this._srefs.Add(new SRef(o));
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x0600671F RID: 26399 RVA: 0x0016C1B8 File Offset: 0x0016A3B8
		internal long ApproximateSize
		{
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				return this._srefs.Sum((SRef s) => s.ApproximateSize);
			}
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x0016C1E4 File Offset: 0x0016A3E4
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void Dispose()
		{
			foreach (SRef sref in this._srefs)
			{
				sref.Dispose();
			}
		}

		// Token: 0x04003543 RID: 13635
		private List<SRef> _srefs = new List<SRef>();
	}
}
