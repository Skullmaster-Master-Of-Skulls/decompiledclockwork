using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authorization
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	public enum eAuthorizationContextItemType
	{
		// Token: 0x04001A75 RID: 6773
		Unknown,
		// Token: 0x04001A76 RID: 6774
		Staff,
		// Token: 0x04001A77 RID: 6775
		Student,
		// Token: 0x04001A78 RID: 6776
		Notetaking = 4,
		// Token: 0x04001A79 RID: 6777
		Instructor = 8,
		// Token: 0x04001A7A RID: 6778
		AlternateContact = 16,
		// Token: 0x04001A7B RID: 6779
		Tutors = 32
	}
}
