using System;

namespace System.Web.Mvc
{
	// Token: 0x0200010D RID: 269
	public sealed class UrlParameter
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0001398A File Offset: 0x00011B8A
		private UrlParameter()
		{
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00013992 File Offset: 0x00011B92
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x04000205 RID: 517
		public static readonly UrlParameter Optional = new UrlParameter();
	}
}
