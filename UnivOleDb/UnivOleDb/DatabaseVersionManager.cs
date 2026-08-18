using System;
using System.Collections;

namespace UnivOleDb
{
	// Token: 0x0200000B RID: 11
	public class DatabaseVersionManager
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00005359 File Offset: 0x00004359
		public DatabaseVersionManager(UnivDataAdapter da)
		{
			this.da = da.Clone();
			this.availableFeatures = new ArrayList();
			this.unavailableFeatures = new ArrayList();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005388 File Offset: 0x00004388
		public bool DoesCurrentDatabaseSupportFeature(DatabaseVersionManager.ClockWorkFeature clockWorkFeature)
		{
			return DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(this.da, this.availableFeatures, this.unavailableFeatures, clockWorkFeature);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000053B4 File Offset: 0x000043B4
		public static bool DoesCurrentDatabaseSupportFeature(UnivDataAdapter da, DatabaseVersionManager.ClockWorkFeature clockWorkFeature)
		{
			bool flag = da.availableFeatures == null;
			if (flag)
			{
				da.availableFeatures = new ArrayList();
				da.unavailableFeatures = new ArrayList();
			}
			return DatabaseVersionManager.DoesCurrentDatabaseSupportFeature(da, da.availableFeatures, da.unavailableFeatures, clockWorkFeature);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005400 File Offset: 0x00004400
		public static bool DoesCurrentDatabaseSupportFeature(UnivDataAdapter da, ArrayList availableFeatures, ArrayList unavailableFeatures, DatabaseVersionManager.ClockWorkFeature clockWorkFeature)
		{
			return true;
		}

		// Token: 0x0400002E RID: 46
		private UnivDataAdapter da;

		// Token: 0x0400002F RID: 47
		private ArrayList availableFeatures;

		// Token: 0x04000030 RID: 48
		private ArrayList unavailableFeatures;

		// Token: 0x02000028 RID: 40
		public enum ClockWorkFeature
		{
			// Token: 0x04000074 RID: 116
			NotetakingExtendedDataFields_Oct_06,
			// Token: 0x04000075 RID: 117
			DynamicScreenControlExtendedDescriptionFields_Mar_07,
			// Token: 0x04000076 RID: 118
			StudentFeeOwingManagement,
			// Token: 0x04000077 RID: 119
			ScreensExtended,
			// Token: 0x04000078 RID: 120
			WorkshopsExtended,
			// Token: 0x04000079 RID: 121
			LookupListsExtended,
			// Token: 0x0400007A RID: 122
			AppointmentTypesExtended,
			// Token: 0x0400007B RID: 123
			GroupsExtended,
			// Token: 0x0400007C RID: 124
			EasyRecurringAppointments,
			// Token: 0x0400007D RID: 125
			NotetakingExtendedDataFields2_July_07,
			// Token: 0x0400007E RID: 126
			AppointmentModificationsTrackingEnhancement,
			// Token: 0x0400007F RID: 127
			CancelledReasonAndNoShowFees,
			// Token: 0x04000080 RID: 128
			StudentReferrals,
			// Token: 0x04000081 RID: 129
			MiscUpgrades_Nov_2007,
			// Token: 0x04000082 RID: 130
			NewAccommdoationsOnlineStuffForCSD_Dec_2007,
			// Token: 0x04000083 RID: 131
			NewPerStudentDataScreenRememberSchoolYearSnapshots,
			// Token: 0x04000084 RID: 132
			ChildrenInLookupLists,
			// Token: 0x04000085 RID: 133
			DynamicImageData,
			// Token: 0x04000086 RID: 134
			AppointmentSubjectLocation,
			// Token: 0x04000087 RID: 135
			NewAccommodations_Dec2008,
			// Token: 0x04000088 RID: 136
			FormattedReports,
			// Token: 0x04000089 RID: 137
			ForcePasswordChange,
			// Token: 0x0400008A RID: 138
			OutlookRecurringAppointmentSync,
			// Token: 0x0400008B RID: 139
			CourseEmails,
			// Token: 0x0400008C RID: 140
			NewAccommodations_July2009,
			// Token: 0x0400008D RID: 141
			AccommodationsApproval_July2009,
			// Token: 0x0400008E RID: 142
			NewAppointmentWorkshopsInfo_July2009,
			// Token: 0x0400008F RID: 143
			AvailabilityScheduleRooms,
			// Token: 0x04000090 RID: 144
			CourseTimetables,
			// Token: 0x04000091 RID: 145
			Is_DontImportCoursesForStudentsWithCid_TurnedOn_NOTE_notfeaturejustsetting,
			// Token: 0x04000092 RID: 146
			ExtendedServiceProviderFundingInfo_Nov_2009,
			// Token: 0x04000093 RID: 147
			ExtendedServiceProviderFundingInfo_Nov_2009_parts,
			// Token: 0x04000094 RID: 148
			InfoPc,
			// Token: 0x04000095 RID: 149
			ServiceProvidersUpdates_Dec_2009,
			// Token: 0x04000096 RID: 150
			AppointmentCases,
			// Token: 0x04000097 RID: 151
			AccommodationsApprovalNotes,
			// Token: 0x04000098 RID: 152
			EmailOut,
			// Token: 0x04000099 RID: 153
			CourseAlternateContact,
			// Token: 0x0400009A RID: 154
			RecordLoaIssuedHistory,
			// Token: 0x0400009B RID: 155
			ReportExecutionLog,
			// Token: 0x0400009C RID: 156
			LookupGroupVisible
		}
	}
}
