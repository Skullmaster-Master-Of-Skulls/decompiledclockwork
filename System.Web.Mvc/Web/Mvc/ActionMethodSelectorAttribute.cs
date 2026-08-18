using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000086 RID: 134
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public abstract class ActionMethodSelectorAttribute : Attribute
	{
		// Token: 0x060003EF RID: 1007
		public abstract bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo);
	}
}
