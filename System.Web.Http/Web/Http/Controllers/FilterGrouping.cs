using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200006D RID: 109
	internal class FilterGrouping
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00009E04 File Offset: 0x00008004
		public FilterGrouping(IEnumerable<FilterInfo> filters)
		{
			List<FilterInfo> list = filters.ToList<FilterInfo>();
			List<FilterInfo> overrideFilters = (from f in list
			where f.Instance is IOverrideFilter
			select f).ToList<FilterInfo>();
			FilterScope overrideFiltersBeforeScope = FilterGrouping.SelectLastOverrideScope<IActionFilter>(overrideFilters);
			FilterScope overrideFiltersBeforeScope2 = FilterGrouping.SelectLastOverrideScope<IAuthorizationFilter>(overrideFilters);
			FilterScope overrideFiltersBeforeScope3 = FilterGrouping.SelectLastOverrideScope<IAuthenticationFilter>(overrideFilters);
			FilterScope overrideFiltersBeforeScope4 = FilterGrouping.SelectLastOverrideScope<IExceptionFilter>(overrideFilters);
			this._actionFilters = FilterGrouping.SelectAvailable<IActionFilter>(list, overrideFiltersBeforeScope);
			this._authorizationFilters = FilterGrouping.SelectAvailable<IAuthorizationFilter>(list, overrideFiltersBeforeScope2);
			this._authenticationFilters = FilterGrouping.SelectAvailable<IAuthenticationFilter>(list, overrideFiltersBeforeScope3);
			this._exceptionFilters = FilterGrouping.SelectAvailable<IExceptionFilter>(list, overrideFiltersBeforeScope4);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009E9B File Offset: 0x0000809B
		public IActionFilter[] ActionFilters
		{
			get
			{
				return this._actionFilters;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009EA3 File Offset: 0x000080A3
		public IAuthorizationFilter[] AuthorizationFilters
		{
			get
			{
				return this._authorizationFilters;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009EAB File Offset: 0x000080AB
		public IAuthenticationFilter[] AuthenticationFilters
		{
			get
			{
				return this._authenticationFilters;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009EB3 File Offset: 0x000080B3
		public IExceptionFilter[] ExceptionFilters
		{
			get
			{
				return this._exceptionFilters;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009EF0 File Offset: 0x000080F0
		private static T[] SelectAvailable<T>(List<FilterInfo> filters, FilterScope overrideFiltersBeforeScope)
		{
			return (from f in filters
			where f.Scope >= overrideFiltersBeforeScope && f.Instance is T
			select (T)((object)f.Instance)).ToArray<T>();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009F54 File Offset: 0x00008154
		private static FilterScope SelectLastOverrideScope<T>(List<FilterInfo> overrideFilters)
		{
			FilterInfo filterInfo = (from f in overrideFilters
			where ((IOverrideFilter)f.Instance).FiltersToOverride == typeof(T)
			select f).LastOrDefault<FilterInfo>();
			if (filterInfo == null)
			{
				return FilterScope.Global;
			}
			return filterInfo.Scope;
		}

		// Token: 0x040000E4 RID: 228
		private IActionFilter[] _actionFilters;

		// Token: 0x040000E5 RID: 229
		private IAuthorizationFilter[] _authorizationFilters;

		// Token: 0x040000E6 RID: 230
		private IAuthenticationFilter[] _authenticationFilters;

		// Token: 0x040000E7 RID: 231
		private IExceptionFilter[] _exceptionFilters;
	}
}
