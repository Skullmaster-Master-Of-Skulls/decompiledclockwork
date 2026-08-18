using System;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200017B RID: 379
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ActionNameAttribute : ActionNameSelectorAttribute
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0001BF74 File Offset: 0x0001A174
		public ActionNameAttribute(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
			}
			this.Name = name;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001BF9B File Offset: 0x0001A19B
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0001BFA3 File Offset: 0x0001A1A3
		public string Name { get; private set; }

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001BFAC File Offset: 0x0001A1AC
		public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
		{
			return string.Equals(actionName, this.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
