using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Metadata;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000CC RID: 204
	public class HttpActionBinding
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x0000F7B7 File Offset: 0x0000D9B7
		public HttpActionBinding()
		{
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000F7BF File Offset: 0x0000D9BF
		public HttpActionBinding(HttpActionDescriptor actionDescriptor, HttpParameterBinding[] bindings)
		{
			this.ActionDescriptor = actionDescriptor;
			this.ParameterBindings = bindings;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000F7DD File Offset: 0x0000D9DD
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public HttpParameterBinding[] ParameterBindings
		{
			get
			{
				return this._parameterBindings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._parameterBindings = value;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000F814 File Offset: 0x0000DA14
		public virtual Task ExecuteBindingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (this._parameterBindings.Length == 0)
			{
				return TaskHelpers.Completed();
			}
			for (int i = 0; i < this.ParameterBindings.Length; i++)
			{
				HttpParameterBinding httpParameterBinding = this.ParameterBindings[i];
				if (!httpParameterBinding.IsValid)
				{
					throw new InvalidOperationException(httpParameterBinding.ErrorMessage);
				}
			}
			if (this._metadataProvider == null)
			{
				HttpConfiguration configuration = actionContext.ControllerContext.Configuration;
				this._metadataProvider = configuration.Services.GetModelMetadataProvider();
			}
			return this.ExecuteBindingAsyncCore(actionContext, cancellationToken);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private async Task ExecuteBindingAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			for (int index = 0; index < this.ParameterBindings.Length; index++)
			{
				HttpParameterBinding parameterBinder = this.ParameterBindings[index];
				await parameterBinder.ExecuteBindingAsync(this._metadataProvider, actionContext, cancellationToken);
			}
		}

		// Token: 0x04000165 RID: 357
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x04000166 RID: 358
		private HttpParameterBinding[] _parameterBindings;

		// Token: 0x04000167 RID: 359
		private ModelMetadataProvider _metadataProvider;
	}
}
