using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.Web.Entity.WebLogin
{
	// Token: 0x02000021 RID: 33
	public class LoginPageUrlRule
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002850 File Offset: 0x00000A50
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002858 File Offset: 0x00000A58
		public IDictionary<eWebPageTargetAudience, string> LoginUrls { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002869 File Offset: 0x00000A69
		public IDictionary<eWebPageTargetAudience, string> LogoutUrls { get; set; }
	}
}
