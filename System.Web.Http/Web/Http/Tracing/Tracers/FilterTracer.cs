using System;
using System.Collections.Generic;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x020000A5 RID: 165
	internal class FilterTracer : IFilter, IDecorator<IFilter>
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		public FilterTracer(IFilter innerFilter, ITraceWriter traceWriter)
		{
			this.InnerFilter = innerFilter;
			this.TraceWriter = traceWriter;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000C0C2 File Offset: 0x0000A2C2
		public IFilter Inner
		{
			get
			{
				return this.InnerFilter;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
		public IFilter InnerFilter { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public bool AllowMultiple
		{
			get
			{
				return this.InnerFilter.AllowMultiple;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		public static IEnumerable<IFilter> CreateFilterTracers(IFilter filter, ITraceWriter traceWriter)
		{
			List<IFilter> list = new List<IFilter>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			ActionFilterAttribute actionFilterAttribute = filter as ActionFilterAttribute;
			if (actionFilterAttribute != null)
			{
				list.Add(new ActionFilterAttributeTracer(actionFilterAttribute, traceWriter));
				flag = true;
			}
			AuthorizationFilterAttribute authorizationFilterAttribute = filter as AuthorizationFilterAttribute;
			if (authorizationFilterAttribute != null)
			{
				list.Add(new AuthorizationFilterAttributeTracer(authorizationFilterAttribute, traceWriter));
				flag2 = true;
			}
			ExceptionFilterAttribute exceptionFilterAttribute = filter as ExceptionFilterAttribute;
			if (exceptionFilterAttribute != null)
			{
				list.Add(new ExceptionFilterAttributeTracer(exceptionFilterAttribute, traceWriter));
				flag3 = true;
			}
			IActionFilter actionFilter = filter as IActionFilter;
			if (actionFilter != null && !flag)
			{
				list.Add(new ActionFilterTracer(actionFilter, traceWriter));
			}
			IAuthorizationFilter authorizationFilter = filter as IAuthorizationFilter;
			if (authorizationFilter != null && !flag2)
			{
				list.Add(new AuthorizationFilterTracer(authorizationFilter, traceWriter));
			}
			IAuthenticationFilter authenticationFilter = filter as IAuthenticationFilter;
			if (authenticationFilter != null)
			{
				list.Add(new AuthenticationFilterTracer(authenticationFilter, traceWriter));
			}
			IExceptionFilter exceptionFilter = filter as IExceptionFilter;
			if (exceptionFilter != null && !flag3)
			{
				list.Add(new ExceptionFilterTracer(exceptionFilter, traceWriter));
			}
			IOverrideFilter overrideFilter = filter as IOverrideFilter;
			if (overrideFilter != null)
			{
				list.Add(new OverrideFilterTracer(overrideFilter, traceWriter));
			}
			if (list.Count == 0)
			{
				list.Add(new FilterTracer(filter, traceWriter));
			}
			return list;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000C20C File Offset: 0x0000A40C
		public static IEnumerable<FilterInfo> CreateFilterTracers(FilterInfo filter, ITraceWriter traceWriter)
		{
			IFilter instance = filter.Instance;
			IEnumerable<IFilter> enumerable = FilterTracer.CreateFilterTracers(instance, traceWriter);
			List<FilterInfo> list = new List<FilterInfo>();
			foreach (IFilter instance2 in enumerable)
			{
				list.Add(new FilterInfo(instance2, filter.Scope));
			}
			return list;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C27C File Offset: 0x0000A47C
		public static bool IsFilterTracer(IFilter filter)
		{
			return filter is FilterTracer || filter is ActionFilterAttributeTracer || filter is AuthorizationFilterAttributeTracer || filter is ExceptionFilterAttributeTracer;
		}
	}
}
