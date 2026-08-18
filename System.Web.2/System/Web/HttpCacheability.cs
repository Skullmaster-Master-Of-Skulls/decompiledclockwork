using System;

namespace System.Web
{
	// Token: 0x02000082 RID: 130
	public enum HttpCacheability
	{
		// Token: 0x040002AB RID: 683
		NoCache = 1,
		// Token: 0x040002AC RID: 684
		Private,
		// Token: 0x040002AD RID: 685
		Server,
		// Token: 0x040002AE RID: 686
		ServerAndNoCache = 3,
		// Token: 0x040002AF RID: 687
		Public,
		// Token: 0x040002B0 RID: 688
		ServerAndPrivate
	}
}
