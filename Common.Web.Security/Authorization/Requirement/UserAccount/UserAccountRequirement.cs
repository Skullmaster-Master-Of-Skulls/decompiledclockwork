using System;
using Microsoft.AspNetCore.Authorization;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement.UserAccount
{
	// Token: 0x0200001D RID: 29
	public class UserAccountRequirement : IAuthorizationRequirement
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000039E1 File Offset: 0x00001BE1
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000039E9 File Offset: 0x00001BE9
		public bool HasManageUserRoomPermission { get; set; }
	}
}
