using System;

namespace TechnoPro.Common.UI.Web.Entity.WebLogin
{
	// Token: 0x02000020 RID: 32
	[Flags]
	[Serializable]
	public enum eWebUserGroupMembershipView
	{
		// Token: 0x04000093 RID: 147
		unknown = 0,
		// Token: 0x04000094 RID: 148
		student = 1,
		// Token: 0x04000095 RID: 149
		staff = 2,
		// Token: 0x04000096 RID: 150
		faculty = 4,
		// Token: 0x04000097 RID: 151
		instructors = 8,
		// Token: 0x04000098 RID: 152
		notetakers = 16,
		// Token: 0x04000099 RID: 153
		tutors = 32,
		// Token: 0x0400009A RID: 154
		admin = 64,
		// Token: 0x0400009B RID: 155
		externalstudent = 128,
		// Token: 0x0400009C RID: 156
		altcontact = 256
	}
}
