using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000C8 RID: 200
	public class ErrorParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public ErrorParameterBinding(HttpParameterDescriptor descriptor, string message) : base(descriptor)
		{
			if (message == null)
			{
				throw Error.ArgumentNull(message);
			}
			this._message = message;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000EB32 File Offset: 0x0000CD32
		public override string ErrorMessage
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000EB3A File Offset: 0x0000CD3A
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return TaskHelpers.FromError(new InvalidOperationException());
		}

		// Token: 0x0400015D RID: 349
		private readonly string _message;
	}
}
