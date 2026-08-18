using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x0200013E RID: 318
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpGetAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x000168FE File Offset: 0x00014AFE
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpGetAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000245 RID: 581
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Get);
	}
}
