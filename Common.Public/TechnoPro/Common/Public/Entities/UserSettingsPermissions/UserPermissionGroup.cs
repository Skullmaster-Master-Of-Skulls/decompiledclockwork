using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public enum UserPermissionGroup
	{
		// Token: 0x04000342 RID: 834
		[UserPermissionGroup("Users and Students", IconIndex = 36, Description = "")]
		USERSANDSTUDENTS = 1,
		// Token: 0x04000343 RID: 835
		[UserPermissionGroup("Disability", IconIndex = 45, Description = "")]
		DISABILITY,
		// Token: 0x04000344 RID: 836
		[UserPermissionGroup("Appointments", IconIndex = 46, Description = "")]
		APPOINTMENTS,
		// Token: 0x04000345 RID: 837
		[UserPermissionGroup("Events and Workshops", IconIndex = 5, Description = "")]
		EVENTSANDWORKSHOPS,
		// Token: 0x04000346 RID: 838
		[UserPermissionGroup("Miscellaneous", IconIndex = 30, Description = "")]
		MISC,
		// Token: 0x04000347 RID: 839
		[UserPermissionGroup("ClockWork Admin", IconIndex = 37, Description = "")]
		CLOCKWORKADMIN,
		// Token: 0x04000348 RID: 840
		[UserPermissionGroup("Forms", IconIndex = 31, Description = "", IsScreenViewModifyCreatePermissions = true)]
		FORMS
	}
}
