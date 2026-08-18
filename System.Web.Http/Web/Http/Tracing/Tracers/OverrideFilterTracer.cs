using System;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x020000AB RID: 171
	internal class OverrideFilterTracer : FilterTracer, IOverrideFilter, IFilter, IDecorator<IOverrideFilter>
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0000C978 File Offset: 0x0000AB78
		public OverrideFilterTracer(IOverrideFilter innerFilter, ITraceWriter traceWriter) : base(innerFilter, traceWriter)
		{
			this._innerFilter = innerFilter;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000C989 File Offset: 0x0000AB89
		public new IOverrideFilter Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000C991 File Offset: 0x0000AB91
		public Type FiltersToOverride
		{
			get
			{
				return this._innerFilter.FiltersToOverride;
			}
		}

		// Token: 0x0400012B RID: 299
		private readonly IOverrideFilter _innerFilter;
	}
}
