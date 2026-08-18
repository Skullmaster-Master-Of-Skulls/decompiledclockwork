using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x02000174 RID: 372
	public interface IBodyModelValidator
	{
		// Token: 0x060009A7 RID: 2471
		bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix);
	}
}
