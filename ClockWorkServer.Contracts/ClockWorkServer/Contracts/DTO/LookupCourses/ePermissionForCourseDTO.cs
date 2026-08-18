using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200079F RID: 1951
	[Flags]
	[DataContract(Namespace = "http://tpro.ca")]
	public enum ePermissionForCourseDTO
	{
		// Token: 0x04000EC8 RID: 3784
		[EnumMember]
		NoPermission = -1,
		// Token: 0x04000EC9 RID: 3785
		[EnumMember]
		PassiveAcceptAll = 0,
		// Token: 0x04000ECA RID: 3786
		[EnumMember]
		ReceiveEmails = 1,
		// Token: 0x04000ECB RID: 3787
		[EnumMember]
		AccessTestInfoOnline = 2,
		// Token: 0x04000ECC RID: 3788
		[EnumMember]
		AccessAccommodationLettersOnline = 4,
		// Token: 0x04000ECD RID: 3789
		[EnumMember]
		ReceiveEmailsAndAccessTestInfoOnline = 3,
		// Token: 0x04000ECE RID: 3790
		[EnumMember]
		ReceiveEmailsAndAccessTestInfoAndAccommodationLettersOnline = 7,
		// Token: 0x04000ECF RID: 3791
		[EnumMember]
		ReceiveEmailsAndAccessAccommodationLettersOnline = 5,
		// Token: 0x04000ED0 RID: 3792
		[EnumMember]
		AccessTestInfoAndAccommodationLettersOnline = 6
	}
}
