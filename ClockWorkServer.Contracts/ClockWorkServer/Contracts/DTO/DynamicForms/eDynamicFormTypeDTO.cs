using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000697 RID: 1687
	[DataContract(Namespace = "http://tpro.ca")]
	public enum eDynamicFormTypeDTO
	{
		// Token: 0x04000C42 RID: 3138
		[EnumMember]
		[DynamicFormType("PS", false)]
		PerStudent,
		// Token: 0x04000C43 RID: 3139
		[EnumMember]
		[DynamicFormType("PA", true)]
		PerAppointment,
		// Token: 0x04000C44 RID: 3140
		[EnumMember]
		[DynamicFormType("AN", false)]
		Anonymous,
		// Token: 0x04000C45 RID: 3141
		[EnumMember]
		[DynamicFormType("AccommodationPS", true)]
		Accommodation,
		// Token: 0x04000C46 RID: 3142
		[EnumMember]
		[DynamicFormType("AccommodationPS", false)]
		AccommodationTemplateOnly,
		// Token: 0x04000C47 RID: 3143
		[EnumMember]
		[DynamicFormType("PA", true)]
		PerStaffAppointment = 20,
		// Token: 0x04000C48 RID: 3144
		[EnumMember]
		[DynamicFormType("PS", false)]
		PerStaff,
		// Token: 0x04000C49 RID: 3145
		[EnumMember]
		[DynamicFormType("PM", true)]
		PerDate = 25,
		// Token: 0x04000C4A RID: 3146
		[EnumMember]
		[DynamicFormType("InstructorPM", true)]
		PerInstructor = 30,
		// Token: 0x04000C4B RID: 3147
		[EnumMember]
		[DynamicFormType("PC", true)]
		PerCase = 51,
		// Token: 0x04000C4C RID: 3148
		[EnumMember]
		[DynamicFormType("WL", true)]
		PerWaitingList = 210,
		// Token: 0x04000C4D RID: 3149
		[EnumMember]
		[DynamicFormType("PI", false)]
		PerInventory = 220,
		// Token: 0x04000C4E RID: 3150
		[EnumMember]
		[DynamicFormType("Survey", true)]
		Survey = 250,
		// Token: 0x04000C4F RID: 3151
		[EnumMember]
		[DynamicFormType("OnlineForm", true)]
		OnlineForm = 255,
		// Token: 0x04000C50 RID: 3152
		[EnumMember]
		[DynamicFormType("PAF", false)]
		PerAltFormat = 260,
		// Token: 0x04000C51 RID: 3153
		[EnumMember]
		[DynamicFormType]
		UnknownLegacy = 999999
	}
}
