using System;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x0200007C RID: 124
	[Flags]
	[Serializable]
	public enum GroupMembership
	{
		// Token: 0x04000341 RID: 833
		unknown = 0,
		// Token: 0x04000342 RID: 834
		student = 1,
		// Token: 0x04000343 RID: 835
		staff = 2,
		// Token: 0x04000344 RID: 836
		faculty = 4,
		// Token: 0x04000345 RID: 837
		instructors = 8,
		// Token: 0x04000346 RID: 838
		notetakers = 16,
		// Token: 0x04000347 RID: 839
		tutors = 32,
		// Token: 0x04000348 RID: 840
		admin = 64,
		// Token: 0x04000349 RID: 841
		externalstudent = 128,
		// Token: 0x0400034A RID: 842
		altcontact = 256
	}
}
