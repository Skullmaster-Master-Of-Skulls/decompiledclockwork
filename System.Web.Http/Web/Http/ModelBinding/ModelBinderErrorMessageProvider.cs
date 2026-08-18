using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200014B RID: 331
	// (Invoke) Token: 0x06000834 RID: 2100
	public delegate string ModelBinderErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue);
}
