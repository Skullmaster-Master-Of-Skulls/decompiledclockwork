using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B4 RID: 180
	[DataContract(Namespace = "http://tpro.ca")]
	public enum MessageCode
	{
		// Token: 0x04000046 RID: 70
		[EnumMember]
		ERROR_MESSAGE,
		// Token: 0x04000047 RID: 71
		[EnumMember]
		SERVER_INFO_MESSAGE,
		// Token: 0x04000048 RID: 72
		[EnumMember]
		SYSTEM_MESSAGE,
		// Token: 0x04000049 RID: 73
		[EnumMember]
		STUDENT_WAITING,
		// Token: 0x0400004A RID: 74
		[EnumMember]
		DOUBLE_LOGIN,
		// Token: 0x0400004B RID: 75
		[EnumMember]
		APPOINTMENTS_REMINDER,
		// Token: 0x0400004C RID: 76
		[EnumMember]
		REGULAR_MESSAGE = 100
	}
}
