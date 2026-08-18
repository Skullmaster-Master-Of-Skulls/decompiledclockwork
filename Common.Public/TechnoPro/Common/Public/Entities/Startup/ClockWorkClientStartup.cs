using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Public.Entities.Startup
{
	// Token: 0x020001B1 RID: 433
	public class ClockWorkClientStartup
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00013F30 File Offset: 0x00012130
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00013F38 File Offset: 0x00012138
		public DateTime? SessionChooserDefaultValue { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00013F41 File Offset: 0x00012141
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00013F49 File Offset: 0x00012149
		public IList<PersonBase> Rooms { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00013F52 File Offset: 0x00012152
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00013F5A File Offset: 0x0001215A
		public byte[] DefaultBackGroundImage { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00013F63 File Offset: 0x00012163
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00013F6B File Offset: 0x0001216B
		public IList<DynamicFormWithExtendedInfo> Screens { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00013F74 File Offset: 0x00012174
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00013F7C File Offset: 0x0001217C
		public IList<AcademicTerm> Sessions { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00013F85 File Offset: 0x00012185
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00013F8D File Offset: 0x0001218D
		public UserPermissionIsAllowedSet UserPermissionIsAllowedSet { get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00013F96 File Offset: 0x00012196
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00013F9E File Offset: 0x0001219E
		public bool UseAlertTriggerSystem { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00013FA7 File Offset: 0x000121A7
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00013FAF File Offset: 0x000121AF
		public bool AnyAlertTriggerDontAllowAppointmentBookingItems { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00013FB8 File Offset: 0x000121B8
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00013FC0 File Offset: 0x000121C0
		public byte[] ServerNonce { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00013FC9 File Offset: 0x000121C9
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00013FD1 File Offset: 0x000121D1
		public int ServerCNonce { get; set; }
	}
}
