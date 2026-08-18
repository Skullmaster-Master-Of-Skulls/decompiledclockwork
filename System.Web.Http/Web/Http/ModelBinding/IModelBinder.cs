using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000FD RID: 253
	public interface IModelBinder
	{
		// Token: 0x06000626 RID: 1574
		bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext);
	}
}
