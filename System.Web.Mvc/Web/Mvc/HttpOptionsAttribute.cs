using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000088 RID: 136
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpOptionsAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000BC1F File Offset: 0x00009E1F
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpOptionsAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000112 RID: 274
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Options);
	}
}
