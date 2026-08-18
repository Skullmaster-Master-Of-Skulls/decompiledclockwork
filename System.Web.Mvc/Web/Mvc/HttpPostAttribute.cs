using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000169 RID: 361
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPostAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0001A894 File Offset: 0x00018A94
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpPostAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000287 RID: 647
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Post);
	}
}
