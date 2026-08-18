using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.Startup
{
	// Token: 0x020001B0 RID: 432
	public class CacheClusterFull : BusinessBase<int>
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00013E4C File Offset: 0x0001204C
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00013E64 File Offset: 0x00012064
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00013E6C File Offset: 0x0001206C
		public IList<OldUserSetting> UserSettings { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00013E75 File Offset: 0x00012075
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00013E7D File Offset: 0x0001207D
		public IList<UserPermission> UserPermissions { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00013E86 File Offset: 0x00012086
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00013E8E File Offset: 0x0001208E
		public DateTime OverrideDtpNowAdjusted { get; set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00013E97 File Offset: 0x00012097
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00013E9F File Offset: 0x0001209F
		public IList<DynamicFormWithExtendedInfo> ActiveDynamicForms { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00013EA8 File Offset: 0x000120A8
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00013EB0 File Offset: 0x000120B0
		public IList<AppTypeWithExtendedInfo> AppointmentTypesWithExtendedInfo { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00013EB9 File Offset: 0x000120B9
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00013EC1 File Offset: 0x000120C1
		public IList<AppTypeWithExtendedInfo> PointOfContactAppointmentTypesWithExtendedInfo { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00013ECA File Offset: 0x000120CA
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x00013ED2 File Offset: 0x000120D2
		public IList<Group> Groups { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00013EDB File Offset: 0x000120DB
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00013EE3 File Offset: 0x000120E3
		public IList<WorkshopDefinition> WorkshopDefinitions { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00013EEC File Offset: 0x000120EC
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00013EF4 File Offset: 0x000120F4
		public IList<AppShowTimeAsType> AppointmentShowTimeAsLookupList { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00013EFD File Offset: 0x000120FD
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00013F05 File Offset: 0x00012105
		public IList<AppointmentIcon> AppointmentIconLookupList { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00013F0E File Offset: 0x0001210E
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00013F16 File Offset: 0x00012116
		public IList<Session> SessionLookupList { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00013F1F File Offset: 0x0001211F
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00013F27 File Offset: 0x00012127
		public IList<int> DynamicScreenNonDataControlCodes { get; set; }
	}
}
