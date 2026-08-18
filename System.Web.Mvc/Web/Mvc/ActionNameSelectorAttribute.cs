using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000174 RID: 372
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public abstract class ActionNameSelectorAttribute : Attribute
	{
		// Token: 0x060009AE RID: 2478
		public abstract bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo);
	}
}
