using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015B RID: 347
	internal class HttpActionDescriptorTracer : HttpActionDescriptor, IDecorator<HttpActionDescriptor>
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0001C570 File Offset: 0x0001A770
		public HttpActionDescriptorTracer(HttpControllerContext controllerContext, HttpActionDescriptor innerDescriptor, ITraceWriter traceWriter) : base(controllerContext.ControllerDescriptor)
		{
			this._innerDescriptor = innerDescriptor;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001C58C File Offset: 0x0001A78C
		public HttpActionDescriptor Inner
		{
			get
			{
				return this._innerDescriptor;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001C594 File Offset: 0x0001A794
		public override ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._innerDescriptor.Properties;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001C5A1 File Offset: 0x0001A7A1
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001C5AE File Offset: 0x0001A7AE
		public override HttpActionBinding ActionBinding
		{
			get
			{
				return this._innerDescriptor.ActionBinding;
			}
			set
			{
				this._innerDescriptor.ActionBinding = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		public override Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._innerDescriptor.SupportedHttpMethods;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001C5C9 File Offset: 0x0001A7C9
		public override string ActionName
		{
			get
			{
				return this._innerDescriptor.ActionName;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
		public override IActionResultConverter ResultConverter
		{
			get
			{
				return this._innerDescriptor.ResultConverter;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001C5E3 File Offset: 0x0001A7E3
		public override Type ReturnType
		{
			get
			{
				return this._innerDescriptor.ReturnType;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C690 File Offset: 0x0001A890
		public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(controllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerDescriptor.GetType().Name, "ExecuteAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceInvokingAction, new object[]
				{
					FormattingUtilities.ActionInvokeToString(this.ActionName, arguments)
				});
			}, () => this._innerDescriptor.ExecuteAsync(controllerContext, arguments, cancellationToken), delegate(TraceRecord tr, object value)
			{
				tr.Message = Error.Format(SRResources.TraceActionReturnValue, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture)
				});
			}, null);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C726 File Offset: 0x0001A926
		public override Collection<T> GetCustomAttributes<T>()
		{
			return this._innerDescriptor.GetCustomAttributes<T>();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C733 File Offset: 0x0001A933
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			return this._innerDescriptor.GetCustomAttributes<T>(inherit);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C744 File Offset: 0x0001A944
		public override Collection<IFilter> GetFilters()
		{
			List<IFilter> list = new List<IFilter>(this._innerDescriptor.GetFilters());
			List<IFilter> list2 = new List<IFilter>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (FilterTracer.IsFilterTracer(list[i]))
				{
					list2.Add(list[i]);
				}
				else
				{
					IEnumerable<IFilter> enumerable = FilterTracer.CreateFilterTracers(list[i], this._traceWriter);
					foreach (IFilter item in enumerable)
					{
						list2.Add(item);
					}
				}
			}
			return new Collection<IFilter>(list2);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		public override Collection<FilterInfo> GetFilterPipeline()
		{
			List<FilterInfo> list = new List<FilterInfo>(this._innerDescriptor.GetFilterPipeline());
			List<FilterInfo> list2 = new List<FilterInfo>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (FilterTracer.IsFilterTracer(list[i].Instance))
				{
					list2.Add(list[i]);
				}
				else
				{
					IEnumerable<FilterInfo> enumerable = FilterTracer.CreateFilterTracers(list[i], this._traceWriter);
					foreach (FilterInfo item in enumerable)
					{
						list2.Add(item);
					}
				}
			}
			return new Collection<FilterInfo>(list2);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public override Collection<HttpParameterDescriptor> GetParameters()
		{
			return this._innerDescriptor.GetParameters();
		}

		// Token: 0x04000291 RID: 657
		private const string ExecuteMethodName = "ExecuteAsync";

		// Token: 0x04000292 RID: 658
		private readonly HttpActionDescriptor _innerDescriptor;

		// Token: 0x04000293 RID: 659
		private readonly ITraceWriter _traceWriter;
	}
}
