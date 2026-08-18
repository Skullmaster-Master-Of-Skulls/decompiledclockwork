using System;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000006 RID: 6
	[Flags]
	[Serializable]
	public enum GroupMembership
	{
		// Token: 0x04000007 RID: 7
		unknown = 0,
		// Token: 0x04000008 RID: 8
		student = 1,
		// Token: 0x04000009 RID: 9
		staff = 2,
		// Token: 0x0400000A RID: 10
		faculty = 4,
		// Token: 0x0400000B RID: 11
		instructors = 8,
		// Token: 0x0400000C RID: 12
		notetakers = 16,
		// Token: 0x0400000D RID: 13
		tutors = 32,
		// Token: 0x0400000E RID: 14
		admin = 64,
		// Token: 0x0400000F RID: 15
		externalstudent = 128,
		// Token: 0x04000010 RID: 16
		altcontact = 256
	}
}
