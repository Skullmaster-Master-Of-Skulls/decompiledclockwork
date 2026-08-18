using System;
using System.Web.ApplicationServices;
using System.Web.Script.Services;
using System.Web.Services;

namespace System.Web.Security
{
	// Token: 0x020000DB RID: 219
	[ScriptService]
	internal sealed class RoleService
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x00029B62 File Offset: 0x00027D62
		[WebMethod]
		public string[] GetRolesForCurrentUser()
		{
			ApplicationServiceHelper.EnsureRoleServiceEnabled();
			return Roles.GetRolesForUser();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00029B6E File Offset: 0x00027D6E
		[WebMethod]
		public bool IsCurrentUserInRole(string role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			ApplicationServiceHelper.EnsureRoleServiceEnabled();
			return Roles.IsUserInRole(role);
		}
	}
}
