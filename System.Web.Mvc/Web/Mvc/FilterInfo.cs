using System;
using System.Collections.Generic;
using System.Web.Mvc.Filters;

namespace System.Web.Mvc
{
	// Token: 0x020001D1 RID: 465
	public class FilterInfo
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00024877 File Offset: 0x00022A77
		public FilterInfo()
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000248B8 File Offset: 0x00022AB8
		public FilterInfo(IEnumerable<Filter> filters)
		{
			FilterInfo.OverrideFilterInfo info = FilterInfo.ProcessOverrideFilters(filters);
			this.SplitFilters(info);
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00024910 File Offset: 0x00022B10
		public IList<IActionFilter> ActionFilters
		{
			get
			{
				return this._actionFilters;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00024918 File Offset: 0x00022B18
		public IList<IAuthenticationFilter> AuthenticationFilters
		{
			get
			{
				return this._authenticationFilters;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00024920 File Offset: 0x00022B20
		public IList<IAuthorizationFilter> AuthorizationFilters
		{
			get
			{
				return this._authorizationFilters;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00024928 File Offset: 0x00022B28
		public IList<IExceptionFilter> ExceptionFilters
		{
			get
			{
				return this._exceptionFilters;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00024930 File Offset: 0x00022B30
		public IList<IResultFilter> ResultFilters
		{
			get
			{
				return this._resultFilters;
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00024938 File Offset: 0x00022B38
		private static FilterInfo.OverrideFilterInfo ProcessOverrideFilters(IEnumerable<Filter> filters)
		{
			FilterInfo.OverrideFilterInfo result = new FilterInfo.OverrideFilterInfo
			{
				ActionOverrideScope = FilterScope.First,
				AuthenticationOverrideScope = FilterScope.First,
				AuthorizationOverrideScope = FilterScope.First,
				ExceptionOverrideScope = FilterScope.First,
				ResultOverrideScope = FilterScope.First,
				Filters = new List<Filter>()
			};
			foreach (Filter filter in filters)
			{
				if (filter != null)
				{
					IOverrideFilter overrideFilter = filter.Instance as IOverrideFilter;
					if (overrideFilter != null)
					{
						if (overrideFilter.FiltersToOverride == typeof(IActionFilter) && filter.Scope >= result.ActionOverrideScope)
						{
							result.ActionOverrideScope = filter.Scope;
						}
						else if (overrideFilter.FiltersToOverride == typeof(IAuthenticationFilter) && filter.Scope >= result.AuthenticationOverrideScope)
						{
							result.AuthenticationOverrideScope = filter.Scope;
						}
						else if (overrideFilter.FiltersToOverride == typeof(IAuthorizationFilter) && filter.Scope >= result.AuthorizationOverrideScope)
						{
							result.AuthorizationOverrideScope = filter.Scope;
						}
						else if (overrideFilter.FiltersToOverride == typeof(IExceptionFilter) && filter.Scope >= result.ExceptionOverrideScope)
						{
							result.ExceptionOverrideScope = filter.Scope;
						}
						else if (overrideFilter.FiltersToOverride == typeof(IResultFilter) && filter.Scope >= result.ResultOverrideScope)
						{
							result.ResultOverrideScope = filter.Scope;
						}
					}
					result.Filters.Add(filter);
				}
			}
			return result;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00024B04 File Offset: 0x00022D04
		private void SplitFilters(FilterInfo.OverrideFilterInfo info)
		{
			foreach (Filter filter in info.Filters)
			{
				IActionFilter actionFilter = filter.Instance as IActionFilter;
				if (actionFilter != null && filter.Scope >= info.ActionOverrideScope)
				{
					this._actionFilters.Add(actionFilter);
				}
				IAuthenticationFilter authenticationFilter = filter.Instance as IAuthenticationFilter;
				if (authenticationFilter != null && filter.Scope >= info.AuthenticationOverrideScope)
				{
					this._authenticationFilters.Add(authenticationFilter);
				}
				IAuthorizationFilter authorizationFilter = filter.Instance as IAuthorizationFilter;
				if (authorizationFilter != null && filter.Scope >= info.AuthorizationOverrideScope)
				{
					this._authorizationFilters.Add(authorizationFilter);
				}
				IExceptionFilter exceptionFilter = filter.Instance as IExceptionFilter;
				if (exceptionFilter != null && filter.Scope >= info.ExceptionOverrideScope)
				{
					this._exceptionFilters.Add(exceptionFilter);
				}
				IResultFilter resultFilter = filter.Instance as IResultFilter;
				if (resultFilter != null && filter.Scope >= info.ResultOverrideScope)
				{
					this._resultFilters.Add(resultFilter);
				}
			}
		}

		// Token: 0x04000397 RID: 919
		private readonly List<IActionFilter> _actionFilters = new List<IActionFilter>();

		// Token: 0x04000398 RID: 920
		private readonly List<IAuthenticationFilter> _authenticationFilters = new List<IAuthenticationFilter>();

		// Token: 0x04000399 RID: 921
		private readonly List<IAuthorizationFilter> _authorizationFilters = new List<IAuthorizationFilter>();

		// Token: 0x0400039A RID: 922
		private readonly List<IExceptionFilter> _exceptionFilters = new List<IExceptionFilter>();

		// Token: 0x0400039B RID: 923
		private readonly List<IResultFilter> _resultFilters = new List<IResultFilter>();

		// Token: 0x020001D2 RID: 466
		private struct OverrideFilterInfo
		{
			// Token: 0x0400039C RID: 924
			public FilterScope ActionOverrideScope;

			// Token: 0x0400039D RID: 925
			public FilterScope AuthenticationOverrideScope;

			// Token: 0x0400039E RID: 926
			public FilterScope AuthorizationOverrideScope;

			// Token: 0x0400039F RID: 927
			public FilterScope ExceptionOverrideScope;

			// Token: 0x040003A0 RID: 928
			public FilterScope ResultOverrideScope;

			// Token: 0x040003A1 RID: 929
			public List<Filter> Filters;
		}
	}
}
