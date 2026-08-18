using System;
using System.Web;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000081 RID: 129
	internal sealed class VirtualPathUtilityWrapper : IVirtualPathUtility
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000CA09 File Offset: 0x0000AC09
		public string Combine(string basePath, string relativePath)
		{
			return VirtualPathUtility.Combine(basePath, relativePath);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000CA12 File Offset: 0x0000AC12
		public string ToAbsolute(string virtualPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath);
		}
	}
}
