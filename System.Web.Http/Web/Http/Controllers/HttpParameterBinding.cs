using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Metadata;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000C5 RID: 197
	public abstract class HttpParameterBinding
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000E688 File Offset: 0x0000C888
		protected HttpParameterBinding(HttpParameterDescriptor descriptor)
		{
			if (descriptor == null)
			{
				throw Error.ArgumentNull("descriptor");
			}
			this._descriptor = descriptor;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E6A5 File Offset: 0x0000C8A5
		public virtual bool WillReadBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public bool IsValid
		{
			get
			{
				return this.ErrorMessage == null;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000E6B3 File Offset: 0x0000C8B3
		public virtual string ErrorMessage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000E6B6 File Offset: 0x0000C8B6
		public HttpParameterDescriptor Descriptor
		{
			get
			{
				return this._descriptor;
			}
		}

		// Token: 0x06000489 RID: 1161
		public abstract Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken);

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		protected object GetValue(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			object result;
			actionContext.ActionArguments.TryGetValue(this.Descriptor.ParameterName, out result);
			return result;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E6F5 File Offset: 0x0000C8F5
		protected void SetValue(HttpActionContext actionContext, object value)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			actionContext.ActionArguments[this.Descriptor.ParameterName] = value;
		}

		// Token: 0x04000158 RID: 344
		private readonly HttpParameterDescriptor _descriptor;
	}
}
