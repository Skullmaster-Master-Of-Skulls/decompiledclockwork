using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http.Dispatcher;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000022 RID: 34
	internal sealed class WebHostAssembliesResolver : IAssembliesResolver
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00004D15 File Offset: 0x00002F15
		ICollection<Assembly> IAssembliesResolver.GetAssemblies()
		{
			return BuildManager.GetReferencedAssemblies().OfType<Assembly>().ToList<Assembly>();
		}
	}
}
