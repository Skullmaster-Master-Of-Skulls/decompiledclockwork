using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Services;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016D RID: 365
	internal class HttpParameterBindingTracer : HttpParameterBinding, IValueProviderParameterBinding, IDecorator<HttpParameterBinding>
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x0001EA5F File Offset: 0x0001CC5F
		public HttpParameterBindingTracer(HttpParameterBinding innerBinding, ITraceWriter traceWriter) : base(innerBinding.Descriptor)
		{
			this.InnerBinding = innerBinding;
			this.TraceWriter = traceWriter;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001EA7B File Offset: 0x0001CC7B
		public HttpParameterBinding Inner
		{
			get
			{
				return this.InnerBinding;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001EA83 File Offset: 0x0001CC83
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001EA8B File Offset: 0x0001CC8B
		private protected HttpParameterBinding InnerBinding { protected get; private set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001EA94 File Offset: 0x0001CC94
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		private protected ITraceWriter TraceWriter { protected get; private set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
		public override string ErrorMessage
		{
			get
			{
				return this.InnerBinding.ErrorMessage;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001EAB2 File Offset: 0x0001CCB2
		public override bool WillReadBody
		{
			get
			{
				return this.InnerBinding.WillReadBody;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		public IEnumerable<ValueProviderFactory> ValueProviderFactories
		{
			get
			{
				IValueProviderParameterBinding valueProviderParameterBinding = this.InnerBinding as IValueProviderParameterBinding;
				if (valueProviderParameterBinding == null)
				{
					return Enumerable.Empty<ValueProviderFactory>();
				}
				return valueProviderParameterBinding.ValueProviderFactories;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001EC28 File Offset: 0x0001CE28
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this.InnerBinding.GetType().Name, "ExecuteBindingAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceBeginParameterBind, new object[]
				{
					this.InnerBinding.Descriptor.ParameterName
				});
			}, () => this.InnerBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				string parameterName = this.InnerBinding.Descriptor.ParameterName;
				if (!actionContext.ModelState.IsValid && actionContext.ModelState.ContainsKey(parameterName))
				{
					tr.Message = Error.Format(SRResources.TraceModelStateInvalidMessage, new object[]
					{
						FormattingUtilities.ModelStateToString(actionContext.ModelState)
					});
					return;
				}
				tr.Message = (actionContext.ActionArguments.ContainsKey(parameterName) ? Error.Format(SRResources.TraceEndParameterBind, new object[]
				{
					parameterName,
					FormattingUtilities.ValueToString(actionContext.ActionArguments[parameterName], CultureInfo.CurrentCulture)
				}) : Error.Format(SRResources.TraceEndParameterBindNoBind, new object[]
				{
					parameterName
				}));
			}, null);
		}

		// Token: 0x040002C4 RID: 708
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";
	}
}
