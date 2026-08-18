using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000470 RID: 1136
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eValueFormatTypeDTO
	{
		// Token: 0x04000849 RID: 2121
		[EnumMember]
		DefaultToStringFormat,
		// Token: 0x0400084A RID: 2122
		[EnumMember]
		CustomFormat,
		// Token: 0x0400084B RID: 2123
		[EnumMember]
		DateSmall,
		// Token: 0x0400084C RID: 2124
		[EnumMember]
		DateLarge,
		// Token: 0x0400084D RID: 2125
		[EnumMember]
		TimeAmPm,
		// Token: 0x0400084E RID: 2126
		[EnumMember]
		TimeMilitary,
		// Token: 0x0400084F RID: 2127
		[EnumMember]
		BooleanYesNo,
		// Token: 0x04000850 RID: 2128
		[EnumMember]
		BooleanTrueFalse,
		// Token: 0x04000851 RID: 2129
		[EnumMember]
		BulletedList,
		// Token: 0x04000852 RID: 2130
		[EnumMember]
		CommaSeparatedList,
		// Token: 0x04000853 RID: 2131
		[EnumMember]
		NumberedList
	}
}
