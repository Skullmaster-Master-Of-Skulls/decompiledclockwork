using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x020007F3 RID: 2035
	internal class ApplicationFileCodeDomTreeGenerator : BaseCodeDomTreeGenerator
	{
		// Token: 0x060060FA RID: 24826 RVA: 0x0014E7CA File Offset: 0x0014C9CA
		internal ApplicationFileCodeDomTreeGenerator(ApplicationFileParser appParser) : base(appParser)
		{
			this._appParser = appParser;
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x060060FB RID: 24827 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool IsGlobalAsaxGenerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400326D RID: 12909
		protected ApplicationFileParser _appParser;
	}
}
