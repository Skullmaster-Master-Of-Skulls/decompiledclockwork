using System;

namespace TechnoPro.Common.UI.Web.Entity.WebLogin
{
	// Token: 0x02000022 RID: 34
	public class WebUserInfoView
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000287A File Offset: 0x00000A7A
		public string UserName { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000288B File Offset: 0x00000A8B
		public string DisplayName { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000289C File Offset: 0x00000A9C
		public string Email { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000028A5 File Offset: 0x00000AA5
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000028AD File Offset: 0x00000AAD
		public eWebUserGroupMembershipView Memberships { get; set; }
	}
}
