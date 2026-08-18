using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000087 RID: 135
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpHeadAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BBFB File Offset: 0x00009DFB
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return HttpHeadAttribute._innerAttribute.IsValidForRequest(controllerContext, methodInfo);
		}

		// Token: 0x04000111 RID: 273
		private static readonly AcceptVerbsAttribute _innerAttribute = new AcceptVerbsAttribute(HttpVerbs.Head);
	}
}
