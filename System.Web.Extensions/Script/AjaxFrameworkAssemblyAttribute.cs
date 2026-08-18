using System;
using System.Reflection;

namespace System.Web.Script
{
	// Token: 0x020000EA RID: 234
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class AjaxFrameworkAssemblyAttribute : Attribute
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002B1E4 File Offset: 0x000293E4
		protected internal virtual Assembly GetDefaultAjaxFrameworkAssembly(Assembly currentAssembly)
		{
			return currentAssembly;
		}
	}
}
