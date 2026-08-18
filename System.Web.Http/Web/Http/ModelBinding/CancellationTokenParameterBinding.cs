using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000C7 RID: 199
	public class CancellationTokenParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0000EADE File Offset: 0x0000CCDE
		public CancellationTokenParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			string parameterName = base.Descriptor.ParameterName;
			actionContext.ActionArguments.Add(parameterName, cancellationToken);
			return TaskHelpers.Completed();
		}
	}
}
