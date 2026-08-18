using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000268 RID: 616
	internal interface IScriptResourceMapping
	{
		// Token: 0x06001D49 RID: 7497
		IScriptResourceDefinition GetDefinition(string resourceName);

		// Token: 0x06001D4A RID: 7498
		IScriptResourceDefinition GetDefinition(string resourceName, Assembly resourceAssembly);
	}
}
