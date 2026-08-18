using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000D4 RID: 212
	public class HttpRequestParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x00010DFD File Offset: 0x0000EFFD
		public HttpRequestParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
		{
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010E08 File Offset: 0x0000F008
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			string parameterName = base.Descriptor.ParameterName;
			HttpRequestMessage request = actionContext.ControllerContext.Request;
			actionContext.ActionArguments.Add(parameterName, request);
			return TaskHelpers.Completed();
		}
	}
}
