using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C7C RID: 3196
	public static class AlternateContactAdapter
	{
		// Token: 0x06004294 RID: 17044 RVA: 0x00020844 File Offset: 0x0001EA44
		public static bool IsAllowed(this AlternateContactDTO AlternateContact, ePermissionForCourseDTO permissionLevel)
		{
			bool flag = AlternateContact == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = permissionLevel == ePermissionForCourseDTO.NoPermission;
				result = (!flag2 && (AlternateContact.PermissionLevel & (int)permissionLevel) > 0);
			}
			return result;
		}
	}
}
