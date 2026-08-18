using System;
using System.Reflection;

namespace Telerik.Web.UI.Common
{
	// Token: 0x020001D4 RID: 468
	public static class Version
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0003E589 File Offset: 0x0003C789
		public static string GetVersion()
		{
			return Version.version;
		}

		// Token: 0x040004D2 RID: 1234
		private static readonly string version = Assembly.GetAssembly(typeof(RadWebControl)).GetName().Version.ToString();
	}
}
