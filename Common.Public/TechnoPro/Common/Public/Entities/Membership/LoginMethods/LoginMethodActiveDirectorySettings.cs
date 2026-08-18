using System;

namespace TechnoPro.Common.Public.Entities.Membership.LoginMethods
{
	// Token: 0x020002AD RID: 685
	public class LoginMethodActiveDirectorySettings
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0001A258 File Offset: 0x00018458
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0001A260 File Offset: 0x00018460
		public string Domain { get; set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0001A269 File Offset: 0x00018469
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x0001A271 File Offset: 0x00018471
		public bool DontAllowFallbackToClockWorkUsernamePasswordCheck { get; set; }
	}
}
