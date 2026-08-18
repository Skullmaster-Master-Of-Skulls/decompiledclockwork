using System;
using System.Runtime.InteropServices;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000DD RID: 221
	[ComVisible(false)]
	internal sealed class NativeMethods
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x000030B5 File Offset: 0x000012B5
		private NativeMethods()
		{
		}

		// Token: 0x06000E2B RID: 3627
		[DllImport("Fusion.dll", CharSet = CharSet.Auto)]
		internal static extern int CreateAssemblyCache(out IAssemblyCache ppAsmCache, uint dwReserved);
	}
}
