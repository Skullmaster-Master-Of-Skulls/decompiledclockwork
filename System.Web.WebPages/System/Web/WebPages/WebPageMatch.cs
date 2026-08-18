using System;

namespace System.Web.WebPages
{
	// Token: 0x02000093 RID: 147
	internal sealed class WebPageMatch
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000E85C File Offset: 0x0000CA5C
		public WebPageMatch(string matchedPath, string pathInfo)
		{
			this.MatchedPath = matchedPath;
			this.PathInfo = pathInfo;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000E872 File Offset: 0x0000CA72
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000E87A File Offset: 0x0000CA7A
		public string MatchedPath { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000E883 File Offset: 0x0000CA83
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000E88B File Offset: 0x0000CA8B
		public string PathInfo { get; private set; }
	}
}
