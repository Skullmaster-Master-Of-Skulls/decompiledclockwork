using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace System.Web.Handlers
{
	// Token: 0x020000DF RID: 223
	internal interface IScriptResourceHandler
	{
		// Token: 0x06000C86 RID: 3206
		string GetScriptResourceUrl(Assembly assembly, string resourceName, CultureInfo culture, bool zip);

		// Token: 0x06000C87 RID: 3207
		string GetScriptResourceUrl(List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>> assemblyResourceLists, bool zip);

		// Token: 0x06000C88 RID: 3208
		string GetEmptyPageUrl(string title);
	}
}
