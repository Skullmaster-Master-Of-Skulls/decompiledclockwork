using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015A RID: 346
	internal class HttpActionBindingTracer : HttpActionBinding, IDecorator<HttpActionBinding>
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		public HttpActionBindingTracer(HttpActionBinding innerBinding, ITraceWriter traceWriter)
		{
			this._innerBinding = innerBinding;
			this._traceWriter = traceWriter;
			if (this._innerBinding.ParameterBindings != null)
			{
				base.ParameterBindings = this._innerBinding.ParameterBindings;
			}
			if (this._innerBinding.ActionDescriptor != null)
			{
				base.ActionDescriptor = this._innerBinding.ActionDescriptor;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001C439 File Offset: 0x0001A639
		public HttpActionBinding Inner
		{
			get
			{
				return this._innerBinding;
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		public override Task ExecuteBindingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(actionContext.ControllerContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this._innerBinding.GetType().Name, "ExecuteBindingAsync", null, () => this._innerBinding.ExecuteBindingAsync(actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				if (!actionContext.ModelState.IsValid)
				{
					tr.Message = Error.Format(SRResources.TraceModelStateInvalidMessage, new object[]
					{
						FormattingUtilities.ModelStateToString(actionContext.ModelState)
					});
					return;
				}
				if (actionContext.ActionDescriptor.GetParameters().Count > 0)
				{
					tr.Message = Error.Format(SRResources.TraceValidModelState, new object[]
					{
						FormattingUtilities.ActionArgumentsToString(actionContext.ActionArguments)
					});
				}
			}, null);
		}

		// Token: 0x0400028E RID: 654
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";

		// Token: 0x0400028F RID: 655
		private readonly HttpActionBinding _innerBinding;

		// Token: 0x04000290 RID: 656
		private readonly ITraceWriter _traceWriter;
	}
}
