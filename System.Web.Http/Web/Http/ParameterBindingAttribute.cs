using System;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000D9 RID: 217
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public abstract class ParameterBindingAttribute : Attribute
	{
		// Token: 0x06000545 RID: 1349
		public abstract HttpParameterBinding GetBinding(HttpParameterDescriptor parameter);
	}
}
