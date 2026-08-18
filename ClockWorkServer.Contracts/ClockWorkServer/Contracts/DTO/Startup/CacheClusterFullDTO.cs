using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000261 RID: 609
	[DataContract(Namespace = "http://tpro.ca")]
	public class CacheClusterFullDTO
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x000068CA File Offset: 0x00004ACA
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x000068D2 File Offset: 0x00004AD2
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x000068DB File Offset: 0x00004ADB
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x000068E3 File Offset: 0x00004AE3
		[DataMember]
		public IList<OldUserSettingDTO> UserSettings { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x000068EC File Offset: 0x00004AEC
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x000068F4 File Offset: 0x00004AF4
		[DataMember]
		public IList<UserPermissionDTO> UserPermissions { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x000068FD File Offset: 0x00004AFD
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00006905 File Offset: 0x00004B05
		[DataMember]
		public DateTime OverrideDtpNowAdjusted { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0000690E File Offset: 0x00004B0E
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00006916 File Offset: 0x00004B16
		[DataMember]
		public IList<DynamicFormWithExtendedInfoDTO> ActiveDynamicForms { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0000691F File Offset: 0x00004B1F
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00006927 File Offset: 0x00004B27
		[DataMember]
		public IList<AppTypeWithExtendedInfoDTO> AppointmentTypesWithExtendedInfo { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00006930 File Offset: 0x00004B30
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00006938 File Offset: 0x00004B38
		[DataMember]
		public IList<AppTypeWithExtendedInfoDTO> PointOfContactAppointmentTypesWithExtendedInfo { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00006941 File Offset: 0x00004B41
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x00006949 File Offset: 0x00004B49
		[DataMember]
		public IList<GroupDTO> Groups { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00006952 File Offset: 0x00004B52
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0000695A File Offset: 0x00004B5A
		[DataMember]
		public IList<WorkshopDefinitionDTO> WorkshopDefinitions { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00006963 File Offset: 0x00004B63
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0000696B File Offset: 0x00004B6B
		[DataMember]
		public IList<AppShowTimeAsTypeDTO> AppointmentShowTimeAsLookupList { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00006974 File Offset: 0x00004B74
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x0000697C File Offset: 0x00004B7C
		[DataMember]
		public IList<AppointmentIconDTO> AppointmentIconLookupList { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00006985 File Offset: 0x00004B85
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0000698D File Offset: 0x00004B8D
		[DataMember]
		public IList<SessionDTO> SessionLookupList { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00006996 File Offset: 0x00004B96
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x0000699E File Offset: 0x00004B9E
		[DataMember]
		public IList<int> DynamicScreenNonDataControlCodes { get; set; }
	}
}
