using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x020006AB RID: 1707
	internal class BrowserTree : OrderedDictionary
	{
		// Token: 0x060052C9 RID: 21193 RVA: 0x00123A08 File Offset: 0x00121C08
		internal BrowserTree() : base(StringComparer.OrdinalIgnoreCase)
		{
		}
	}
}
