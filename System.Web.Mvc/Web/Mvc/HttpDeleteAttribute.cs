using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpDeleteAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x00016944 File Offset: 0x00014B44
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpDeleteAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000247 RID: 583
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Delete);
	}
}
