using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x020001E6 RID: 486
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class NonActionAttribute : ActionMethodSelectorAttribute
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00026B29 File Offset: 0x00024D29
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return false;
		}
	}
}
