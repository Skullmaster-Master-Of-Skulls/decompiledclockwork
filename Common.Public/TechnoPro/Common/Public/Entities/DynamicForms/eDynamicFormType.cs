using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200035F RID: 863
	[Serializable]
	public enum eDynamicFormType
	{
		// Token: 0x0400157F RID: 5503
		[DynamicFormType("PS", false, eDynamicDataContextColumnName.PersonId)]
		PerStudent,
		// Token: 0x04001580 RID: 5504
		[DynamicFormType("PA", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerAppointment,
		// Token: 0x04001581 RID: 5505
		[DynamicFormType("AN", false, eDynamicDataContextColumnName.PersonId)]
		Anonymous,
		// Token: 0x04001582 RID: 5506
		[DynamicFormType("AccommodationPS", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.CourseId)]
		Accommodation,
		// Token: 0x04001583 RID: 5507
		[DynamicFormType("AccommodationPS", false, eDynamicDataContextColumnName.PersonId)]
		AccommodationTemplateOnly,
		// Token: 0x04001584 RID: 5508
		[DynamicFormType("PA", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerStaffAppointment = 20,
		// Token: 0x04001585 RID: 5509
		[DynamicFormType("PS", false, eDynamicDataContextColumnName.PersonId)]
		PerStaff,
		// Token: 0x04001586 RID: 5510
		[DynamicFormType("PM", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerDate = 25,
		// Token: 0x04001587 RID: 5511
		[DynamicFormType("InstructorPM", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerInstructor = 30,
		// Token: 0x04001588 RID: 5512
		[DynamicFormType("PC", false, eDynamicDataContextColumnName.PersonId)]
		PerCase = 51,
		// Token: 0x04001589 RID: 5513
		[DynamicFormType("WL", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerWaitingList = 210,
		// Token: 0x0400158A RID: 5514
		[DynamicFormType("PI", false, eDynamicDataContextColumnName.PersonId)]
		PerInventory = 220,
		// Token: 0x0400158B RID: 5515
		[DynamicFormType("Survey", true, eDynamicDataContextColumnName.PersonId)]
		Survey = 250,
		// Token: 0x0400158C RID: 5516
		[DynamicFormType("OnlineForm", true, eDynamicDataContextColumnName.PersonId)]
		OnlineForm = 255,
		// Token: 0x0400158D RID: 5517
		[DynamicFormType("PAF", false, eDynamicDataContextColumnName.PersonId)]
		PerAltFormat = 260,
		// Token: 0x0400158E RID: 5518
		[DynamicFormType("PJA", true, eDynamicDataContextColumnName.PersonId, eDynamicDataContextColumnName.AppointmentId)]
		PerJustAppointment = 270,
		// Token: 0x0400158F RID: 5519
		[DynamicFormType("Intake", false, eDynamicDataContextColumnName.PersonId)]
		PerIntake = 230,
		// Token: 0x04001590 RID: 5520
		[DynamicFormType]
		UnknownLegacy = 999999
	}
}
