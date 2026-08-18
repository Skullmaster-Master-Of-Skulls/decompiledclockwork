using System;

namespace System.Diagnostics
{
	// Token: 0x0200049B RID: 1179
	internal class FilterElement : TypedElement
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000C6358 File Offset: 0x000C4558
		public FilterElement() : base(typeof(TraceFilter))
		{
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000C636C File Offset: 0x000C456C
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000C6392 File Offset: 0x000C4592
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
