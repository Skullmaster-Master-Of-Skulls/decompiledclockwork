using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000089 RID: 137
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPatchAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000BC43 File Offset: 0x00009E43
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpPatchAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000113 RID: 275
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Patch);
	}
}
