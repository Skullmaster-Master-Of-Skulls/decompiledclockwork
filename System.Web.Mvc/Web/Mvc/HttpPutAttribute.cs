using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPutAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00016921 File Offset: 0x00014B21
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpPutAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000246 RID: 582
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Put);
	}
}
