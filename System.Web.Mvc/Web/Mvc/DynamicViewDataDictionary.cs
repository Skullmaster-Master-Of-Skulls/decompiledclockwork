using System;
using System.Collections.Generic;
using System.Dynamic;

namespace System.Web.Mvc
{
	// Token: 0x020000C1 RID: 193
	internal sealed class DynamicViewDataDictionary : DynamicObject
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000E386 File Offset: 0x0000C586
		public DynamicViewDataDictionary(Func<ViewDataDictionary> viewDataThunk)
		{
			this._viewDataThunk = viewDataThunk;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000E398 File Offset: 0x0000C598
		private ViewDataDictionary ViewData
		{
			get
			{
				return this._viewDataThunk();
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E3B2 File Offset: 0x0000C5B2
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this.ViewData.Keys;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E3BF File Offset: 0x0000C5BF
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = this.ViewData[binder.Name];
			return true;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E3D5 File Offset: 0x0000C5D5
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			this.ViewData[binder.Name] = value;
			return true;
		}

		// Token: 0x0400015F RID: 351
		private readonly Func<ViewDataDictionary> _viewDataThunk;
	}
}
