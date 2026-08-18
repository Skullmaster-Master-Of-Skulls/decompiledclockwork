using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.Description
{
	// Token: 0x020000C1 RID: 193
	public interface IDocumentationProvider
	{
		// Token: 0x06000475 RID: 1141
		string GetDocumentation(HttpControllerDescriptor controllerDescriptor);

		// Token: 0x06000476 RID: 1142
		string GetDocumentation(HttpActionDescriptor actionDescriptor);

		// Token: 0x06000477 RID: 1143
		string GetDocumentation(HttpParameterDescriptor parameterDescriptor);

		// Token: 0x06000478 RID: 1144
		string GetResponseDocumentation(HttpActionDescriptor actionDescriptor);
	}
}
