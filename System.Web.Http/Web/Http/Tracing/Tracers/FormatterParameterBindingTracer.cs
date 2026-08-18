using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200016B RID: 363
	internal class FormatterParameterBindingTracer : FormatterParameterBinding, IDecorator<FormatterParameterBinding>
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0001E730 File Offset: 0x0001C930
		public FormatterParameterBindingTracer(FormatterParameterBinding innerBinding, ITraceWriter traceWriter) : base(innerBinding.Descriptor, innerBinding.Formatters, innerBinding.BodyModelValidator)
		{
			this._innerBinding = innerBinding;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001E758 File Offset: 0x0001C958
		public FormatterParameterBinding Inner
		{
			get
			{
				return this._innerBinding;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001E760 File Offset: 0x0001C960
		public override string ErrorMessage
		{
			get
			{
				return this._innerBinding.ErrorMessage;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001E76D File Offset: 0x0001C96D
		public override bool WillReadBody
		{
			get
			{
				return this._innerBinding.WillReadBody;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001E77A File Offset: 0x0001C97A
		public override Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return this._innerBinding.ReadContentAsync(request, type, this.CreateFormatterTracers(request, formatters), formatterLogger);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001E793 File Offset: 0x0001C993
		public override Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerBinding.ReadContentAsync(request, type, formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001E894 File Offset: 0x0001CA94
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this._innerBinding.GetType().Name, "ExecuteBindingAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceBeginParameterBind, new object[]
				{
					this._innerBinding.Descriptor.ParameterName
				});
			}, () => this._innerBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				string parameterName = this._innerBinding.Descriptor.ParameterName;
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

		// Token: 0x06000933 RID: 2355 RVA: 0x0001E91C File Offset: 0x0001CB1C
		private IEnumerable<MediaTypeFormatter> CreateFormatterTracers(HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			List<MediaTypeFormatter> list = new List<MediaTypeFormatter>();
			foreach (MediaTypeFormatter formatter in formatters)
			{
				list.Add(MediaTypeFormatterTracer.CreateTracer(formatter, this._traceWriter, request));
			}
			return list;
		}

		// Token: 0x040002BF RID: 703
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";

		// Token: 0x040002C0 RID: 704
		private readonly FormatterParameterBinding _innerBinding;

		// Token: 0x040002C1 RID: 705
		private readonly ITraceWriter _traceWriter;
	}
}
