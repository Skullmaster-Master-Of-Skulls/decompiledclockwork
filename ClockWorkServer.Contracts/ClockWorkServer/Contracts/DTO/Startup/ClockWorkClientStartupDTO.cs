using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000262 RID: 610
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkClientStartupDTO
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000069A7 File Offset: 0x00004BA7
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x000069AF File Offset: 0x00004BAF
		[DataMember]
		public DateTime? SessionChooserDefaultValue { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000069B8 File Offset: 0x00004BB8
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x000069C0 File Offset: 0x00004BC0
		[DataMember]
		public IList<PersonBaseDTO> Rooms { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000069C9 File Offset: 0x00004BC9
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x000069D1 File Offset: 0x00004BD1
		[DataMember]
		public byte[] DefaultBackGroundImage { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000069DA File Offset: 0x00004BDA
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x000069E2 File Offset: 0x00004BE2
		[DataMember]
		public IList<DynamicFormWithExtendedInfoDTO> Screens { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000069EB File Offset: 0x00004BEB
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x000069F3 File Offset: 0x00004BF3
		[DataMember]
		public IList<AcademicTermDTO> Sessions { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000069FC File Offset: 0x00004BFC
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00006A04 File Offset: 0x00004C04
		[DataMember]
		public UserPermissionIsAllowedSetDTO UserPermissionIsAllowedSet { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00006A0D File Offset: 0x00004C0D
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00006A15 File Offset: 0x00004C15
		[DataMember]
		public bool UseAlertTriggerSystem { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00006A1E File Offset: 0x00004C1E
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x00006A26 File Offset: 0x00004C26
		[DataMember]
		public bool AnyAlertTriggerDontAllowAppointmentBookingItems { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00006A2F File Offset: 0x00004C2F
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x00006A37 File Offset: 0x00004C37
		[DataMember]
		public byte[] ServerNonce { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00006A40 File Offset: 0x00004C40
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x00006A48 File Offset: 0x00004C48
		[DataMember]
		public int ServerCNonce { get; set; }
	}
}
