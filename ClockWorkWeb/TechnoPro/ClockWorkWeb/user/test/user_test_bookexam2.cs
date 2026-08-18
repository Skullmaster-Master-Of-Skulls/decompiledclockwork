using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.TestBooking;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000067 RID: 103
	public class user_test_bookexam2 : Page
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00019504 File Offset: 0x00017704
		private Setting TESTBOOKING_WizardSetting_AdditionalInformationScreenNum
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0001951C File Offset: 0x0001771C
		private Setting TESTBOOKING_WizardSetting_MinDaysAheadToBook
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_MinDaysAheadToBook;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00019534 File Offset: 0x00017734
		private Setting TESTBOOKING_CutoffBookingDate
		{
			get
			{
				return Setting.EXAMBOOKING_CutoffBookingDate;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001954C File Offset: 0x0001774C
		private Setting TESTBOOKING_TestBookingCancelUrl
		{
			get
			{
				return Setting.TESTBOOKING_TestBookingCancelUrl;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00019564 File Offset: 0x00017764
		private Setting TESTBOOKING_WizardSetting_WelcomeMsg
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_WelcomeMsg;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001957C File Offset: 0x0001777C
		private Setting TESTBOOKING_SelectADateTimeMessageToStudents
		{
			get
			{
				return Setting.EXAMBOOKING_SelectADateTimeMessageToStudents;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00019594 File Offset: 0x00017794
		private Setting TESTBOOKING_WizardSetting_HideContinuingEducationDropList
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_HideContinuingEducationDropList;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000195AC File Offset: 0x000177AC
		private Setting TESTBOOKING_WizardSetting_ConfirmBookingFinishButtonText
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmBookingFinishButtonText;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000195C4 File Offset: 0x000177C4
		private Setting TESTBOOKING_WizardSetting_ConfirmBookingMsg
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmBookingMsg;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000195DC File Offset: 0x000177DC
		private Setting TESTBOOKING_WizardSetting_ConfirmationPage_IntroText
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IntroText;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000195F4 File Offset: 0x000177F4
		private Setting TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IAgreeText;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0001960C File Offset: 0x0001780C
		private Setting TESTBOOKING_StudentAllowedToSelectOwnDateTime
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectOwnDateTime;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00019624 File Offset: 0x00017824
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousDateTimes
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousDateTimes;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001963C File Offset: 0x0001783C
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00019654 File Offset: 0x00017854
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001966C File Offset: 0x0001786C
		private Setting TESTBOOKING_BookTestsAsTentative
		{
			get
			{
				return Setting.EXAMBOOKING_BookTestsAsTentative;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00019684 File Offset: 0x00017884
		private Setting TESTBOOKING_AppointmentTypeToUseForBooking
		{
			get
			{
				return Setting.EXAMBOOKING_AppointmentTypeToUseForBooking;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001969C File Offset: 0x0001789C
		private Setting TESTBOOKING_Email_StudentBookingConfirmation
		{
			get
			{
				return Setting.EXAMBOOKING_Email_StudentBookingConfirmation;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000196B4 File Offset: 0x000178B4
		private Setting TESTBOOKING_TestBookingCoordinatorEmail
		{
			get
			{
				return Setting.EXAMBOOKING_TestBookingCoordinatorEmail;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000196CC File Offset: 0x000178CC
		private Setting TESTBOOKING_Email_StudentBookingConfirmationForInstructor
		{
			get
			{
				return Setting.EXAMBOOKING_Email_StudentBookingConfirmationForInstructor;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000196E4 File Offset: 0x000178E4
		private Setting TESTBOOKING_DepartmentContactInformation
		{
			get
			{
				return Setting.EXAMBOOKING_DepartmentContactInformation;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000196FC File Offset: 0x000178FC
		private Setting TESTBOOKING_OverrideRoomPidForAvailability
		{
			get
			{
				return Setting.EXAMBOOKING_OverrideRoomPidForAvailability;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00019714 File Offset: 0x00017914
		private Setting TESTBOOKING_Rooms
		{
			get
			{
				return Setting.EXAMBOOKING_Rooms;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001972C File Offset: 0x0001792C
		private Setting TESTBOOKING_Assets
		{
			get
			{
				return Setting.EXAMBOOKING_Assets;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00019744 File Offset: 0x00017944
		private Setting TESTBOOKING_SpecialAccommodations
		{
			get
			{
				return Setting.EXAMBOOKING_SpecialAccommodations;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001975C File Offset: 0x0001795C
		private Setting TESTBOOKING_Rules
		{
			get
			{
				return Setting.EXAMBOOKING_Rules;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00019774 File Offset: 0x00017974
		private Setting TESTBOOKING_dontAskStudentToConfirmInstructorInformation
		{
			get
			{
				return Setting.EXAMBOOKING_dontAskStudentToConfirmInstructorInformation;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001978C File Offset: 0x0001798C
		private Setting TESTBOOKING_code_FindPotentialBookingsStart
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsStart;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000197A4 File Offset: 0x000179A4
		private Setting TESTBOOKING_code_FindPotentialBookingsMid
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsMid;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000197BC File Offset: 0x000179BC
		private Setting TESTBOOKING_code_FindPotentialBookingsEnd
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsEnd;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000197D4 File Offset: 0x000179D4
		private Setting TESTBOOKING_code_FindPotentialBookingsMisc
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsMisc;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000197EC File Offset: 0x000179EC
		private Setting TESTBOOKING_NonNegotiableAccommodationCids
		{
			get
			{
				return Setting.EXAMBOOKING_NonNegotiableAccommodationCids;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00019804 File Offset: 0x00017A04
		private Setting TESTBOOKING_RestrictCoursesToCampus
		{
			get
			{
				return Setting.EXAMBOOKING_RestrictCoursesToCampus;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001981C File Offset: 0x00017A1C
		private Setting TESTBOOKING_WizardSetting_AccommodationsDefaultChecked
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_AccommodationsDefaultChecked;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00019834 File Offset: 0x00017A34
		private Setting TESTBOOKING_AskStudentForInstructorPhone
		{
			get
			{
				return Setting.EXAMBOOKING_AskStudentForInstructorPhone;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001984C File Offset: 0x00017A4C
		private Setting TESTBOOKING_ChooseAccommodationsInstructions
		{
			get
			{
				return Setting.EXAMBOOKING_ChooseAccommodationsInstructions;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00019864 File Offset: 0x00017A64
		private Setting TESTBOOKING_ChooseAccommodationsNote
		{
			get
			{
				return Setting.EXAMBOOKING_ChooseAccommodationsNote;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001987C File Offset: 0x00017A7C
		private Setting TESTBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation
		{
			get
			{
				return Setting.EXAMBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00019894 File Offset: 0x00017A94
		private Setting TESTBOOKING_AllowStudentToSelectFromApprovedDateTimes
		{
			get
			{
				return Setting.EXAMBOOKING_AllowStudentToSelectFromApprovedDateTimes;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000198AC File Offset: 0x00017AAC
		private Setting TESTBOOKING_AskStudentForCourseAlternateContactInfo
		{
			get
			{
				return Setting.EXAMBOOKING_AskStudentForCourseAlternateContactInfo;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000198C4 File Offset: 0x00017AC4
		private Setting TESTBOOKING_AvailableTestDateTimesImportantNote
		{
			get
			{
				return Setting.EXAMBOOKING_AvailableTestDateTimesImportantNote;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000198DC File Offset: 0x00017ADC
		private Setting TESTBOOKING_NoRoomFoundMessage
		{
			get
			{
				return Setting.EXAMBOOKING_NoRoomFoundMessage;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000198F4 File Offset: 0x00017AF4
		private Setting TESTBOOKING_RoomFoundMessage
		{
			get
			{
				return Setting.EXAMBOOKING_RoomFoundMessage;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001990C File Offset: 0x00017B0C
		private Setting TESTBOOKING_IgnoreStudentSchedule
		{
			get
			{
				return Setting.EXAMBOOKING_IgnoreStudentSchedule;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00019924 File Offset: 0x00017B24
		private Setting TESTBOOKING_IgnoreStudentTwoTestsSameCourseSameDay
		{
			get
			{
				return Setting.EXAMBOOKING_IgnoreStudentTwoTestsSameCourseSameDay;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001993C File Offset: 0x00017B3C
		private Setting TESTBOOKING_MaxDuration
		{
			get
			{
				return Setting.EXAMBOOKING_MaxDuration;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00019954 File Offset: 0x00017B54
		private Setting TESTBOOKING_MaxDurationUseTimetable
		{
			get
			{
				return Setting.EXAMBOOKING_MaxDurationUseTimetable;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001996C File Offset: 0x00017B6C
		private Setting GENERAL_ErrorMessage_NotAClockWorkStudent
		{
			get
			{
				return Setting.GENERAL_ErrorMessage_NotAClockWorkStudent;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00019984 File Offset: 0x00017B84
		private Setting TESTBOOKING_ErrorMessage_ModuleInactive
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_ModuleInactive;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001999C File Offset: 0x00017B9C
		private Setting TESTBOOKING_ErrorMessage_NoCourses
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_NoCourses;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000199B4 File Offset: 0x00017BB4
		private Setting TESTBOOKING_ErrorMessage_AccommodationsExpired
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_AccommodationsExpired;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000199CC File Offset: 0x00017BCC
		private Setting TESTBOOKING_ErrorMessage_MissingPerStudentData
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_MissingPerStudentData;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000199E4 File Offset: 0x00017BE4
		private Setting TESTBOOKING_ErrorMessage_Pilot
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_Pilot;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000199FC File Offset: 0x00017BFC
		private Setting TESTBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids
		{
			get
			{
				return Setting.EXAMBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00019A14 File Offset: 0x00017C14
		private Setting TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom
		{
			get
			{
				return Setting.EXAMBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00019A2C File Offset: 0x00017C2C
		private Setting TESTBOOKING_SpecialAccommodationsEmailTemplate
		{
			get
			{
				return Setting.EXAMBOOKING_SpecialAccommodationsEmailTemplate;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00019A44 File Offset: 0x00017C44
		private Setting TESTBOOKING_FilterCourseListByTimeOfDay
		{
			get
			{
				return Setting.EXAMBOOKING_FilterCourseListByTimeOfDay;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00019A5C File Offset: 0x00017C5C
		private Setting EXAMBOOKING_FinalExamRequest_Enabled
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_Enabled;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00019A74 File Offset: 0x00017C74
		private Setting EXAMBOOKING_AllowStudentsToBookMultipleExams
		{
			get
			{
				return Setting.EXAMBOOKING_AllowStudentsToBookMultipleExams;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00019A8C File Offset: 0x00017C8C
		private Setting EXAMBOOKING_FinalExamRequest_FinalsStartDate
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_FinalsStartDate;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00019AA4 File Offset: 0x00017CA4
		private Setting EXAMBOOKING_FinalExamRequest_FinalsEndDate
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_FinalsEndDate;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00019ABC File Offset: 0x00017CBC
		private Setting EXAMBOOKING_ReportForLookingUpExamInfo
		{
			get
			{
				return Setting.EXAMBOOKING_ReportForLookingUpExamInfo;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00019AD4 File Offset: 0x00017CD4
		private Setting TESTBOOKING_StudentsAllowedToBookTests
		{
			get
			{
				return Setting.EXAMBOOKING_StudentsAllowedToBookExams;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00019AEC File Offset: 0x00017CEC
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00019B04 File Offset: 0x00017D04
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00019B1C File Offset: 0x00017D1C
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00019B34 File Offset: 0x00017D34
		private Setting TESTBOOKING_CustomWizardStepRewording_Enabled
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_Enabled;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00019B4C File Offset: 0x00017D4C
		private Setting TESTBOOKING_CustomWizardStepRewording_StepWelcome
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepWelcome;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00019B64 File Offset: 0x00017D64
		private Setting TESTBOOKING_CustomWizardStepRewording_StepSelectCourse
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepSelectCourse;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00019B7C File Offset: 0x00017D7C
		private Setting TESTBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00019B94 File Offset: 0x00017D94
		private Setting TESTBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00019BAC File Offset: 0x00017DAC
		private Setting TESTBOOKING_CustomWizardStepRewording_StepAdditionalInfo
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepAdditionalInfo;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00019BC4 File Offset: 0x00017DC4
		private Setting TESTBOOKING_CustomWizardStepRewording_StepChooseAccommodations
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepChooseAccommodations;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00019BDC File Offset: 0x00017DDC
		private Setting TESTBOOKING_CustomWizardStepRewording_StepSelectScheduledTime
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepSelectScheduledTime;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00019BF4 File Offset: 0x00017DF4
		private Setting TESTBOOKING_CustomWizardStepRewording_StepConfirmAndComplete
		{
			get
			{
				return Setting.EXAMBOOKING_CustomWizardStepRewording_StepConfirmAndComplete;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00019C0C File Offset: 0x00017E0C
		private Label GetWizardStepLabel(TemplatedWizardStep step)
		{
			bool flag = step.ID == this.step_welcome.ID;
			string text;
			if (flag)
			{
				text = "lbl_welcome";
			}
			else
			{
				bool flag2 = step.ID == this.step_selectCourse.ID;
				if (flag2)
				{
					text = "lblTitle";
				}
				else
				{
					bool flag3 = step.ID == this.step_confirmProfInfo.ID;
					if (flag3)
					{
						text = "Label1";
					}
					else
					{
						bool flag4 = step.ID == this.step_chooseAccommodations.ID;
						if (flag4)
						{
							text = "lbl_chooseAccommodations";
						}
						else
						{
							bool flag5 = step.ID == this.step_confirmAndComplete.ID;
							if (flag5)
							{
								text = "lbl_confirmAndCompleteTitle";
							}
							else
							{
								bool flag6 = step.ID == this.step_additionalInfo.ID;
								if (flag6)
								{
									text = "lbl_title_additionalRequirements";
								}
								else
								{
									text = null;
								}
							}
						}
					}
				}
			}
			bool flag7 = !string.IsNullOrEmpty(text);
			Label result;
			if (flag7)
			{
				result = (Label)step.ContentTemplateContainer.FindControl(text);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00019D20 File Offset: 0x00017F20
		private void ChangeWizardStepTitle(TemplatedWizardStep step, string newTitle)
		{
			bool flag = string.IsNullOrEmpty(newTitle) || step == null;
			if (!flag)
			{
				step.Title = newTitle;
				Label wizardStepLabel = this.GetWizardStepLabel(step);
				bool flag2 = wizardStepLabel != null;
				if (flag2)
				{
					wizardStepLabel.Text = newTitle;
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00019D64 File Offset: 0x00017F64
		private void Page_Init(object sender, EventArgs e)
		{
			int settingValue = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
			bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = settingValue2;
			if (flag)
			{
				this.step_confirmProfInfo.Title = " ";
				this.step_chooseAccommodations.Title = string.Format("{0}{1}", "3", this.step_chooseAccommodations.Title.Substring(1));
				this.lbl_chooseAccommodations.Text = this.step_chooseAccommodations.Title;
			}
			bool flag2 = settingValue <= 0;
			if (!flag2)
			{
				int num = settingValue2 ? 4 : 5;
				string arg = num.ToString();
				WizardStep wizardStep = new WizardStep
				{
					Title = string.Format("{0}. Additional requirements", arg),
					ID = "step_additionalRequirements"
				};
				Label child = new Label
				{
					Text = string.Format("{0}. Additional requirements", arg),
					CssClass = "PageTitle"
				};
				wizardStep.Controls.Add(child);
				child = new Label
				{
					Text = "Please fill in the appropriate information below.",
					CssClass = "Intro4"
				};
				wizardStep.Controls.Add(child);
				Panel panel = new Panel
				{
					ID = "p_data",
					CssClass = "DynamicForm"
				};
				wizardStep.Controls.Add(panel);
				this.Wizard1.WizardSteps.Insert(num, wizardStep);
				this.step_confirmAndComplete.Title = string.Format("{0}{1}", (num + 2).ToString(), this.step_confirmAndComplete.Title.Substring(1));
				this.lbl_confirmAndCompleteTitle.Text = this.step_confirmAndComplete.Title;
				this.AddWizardControls(settingValue, panel);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00019F30 File Offset: 0x00018130
		private void AddWizardControls(int screenNum, Panel p_data)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper();
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, p_data, null, false, false, "");
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00019F5C File Offset: 0x0001815C
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00019F80 File Offset: 0x00018180
		private Panel p_checkAll
		{
			get
			{
				return (Panel)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("p_checkAllCheckNone");
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00019FAC File Offset: 0x000181AC
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_StudentsAllowedToBookTests);
			bool flag = !settingValue;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_ModuleInactive, this.Page);
			}
			else
			{
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(this.EXAMBOOKING_FinalExamRequest_Enabled);
				bool flag2 = !settingValue2;
				if (flag2)
				{
					base.Response.Redirect("bookexam.aspx", true);
				}
				else
				{
					DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
					bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(this.EXAMBOOKING_AllowStudentsToBookMultipleExams);
					bool flag3 = settingValue3;
					if (flag3)
					{
						base.Response.Redirect("bookexams.aspx", true);
					}
					else
					{
						int num = this.LookupStudentPid();
						bool flag4 = num < 1;
						if (flag4)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
						}
						else
						{
							IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
							bool flag5 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(num, true);
							bool flag6 = flag5;
							if (flag6)
							{
								NavigatorClientManager.CurrentInstance.NotAllowed(Setting.TESTBOOKING_ErrorMessage_AccommodationsExpired, this.Page);
							}
							else
							{
								bool settingValue4 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSqlEnabled);
								bool flag7 = settingValue4;
								if (flag7)
								{
									string settingValue5 = new WebSettingsClientManager().GetSettingValue<string>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql);
									bool flag8 = !string.IsNullOrEmpty(settingValue5);
									if (flag8)
									{
										DataTable dataTable = new DataTable();
										try
										{
											DbParameter[] parameters = new DbParameter[]
											{
												clockWork.GetParameter("@pid", DbType.Int32, num)
											};
											dataTable = clockWork.ExecuteQuery(settingValue5, parameters);
											bool flag9 = dataTable.Rows.Count > 0;
											if (flag9)
											{
												string value = dataTable.Rows[0][0].ToString().Trim();
												bool flag10 = !string.IsNullOrEmpty(value);
												if (flag10)
												{
													CacheStorageManager.Current.Insert("web_exam_custom_check_emsg_" + num.ToString(), value);
													NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql, this.Page);
													return;
												}
											}
										}
										catch
										{
										}
									}
								}
								bool flag11 = !this.Page.IsPostBack;
								if (flag11)
								{
									bool flag12 = base.Master != null && base.Master is IClockWorkMasterPage;
									if (flag12)
									{
										((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookExam);
									}
									bool settingValue6 = SettingManager.CurrentInstance.GetSettingValue<bool>(Setting.EXAMBOOKING_HideCheckAllCheckNone);
									bool flag13 = settingValue6;
									if (flag13)
									{
										this.p_checkAll.Visible = false;
									}
									bool settingValue7 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_CustomWizardStepRewording_Enabled);
									bool flag14 = settingValue7;
									if (flag14)
									{
										this.ChangeWizardStepTitle(this.step_welcome, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepWelcome));
										this.ChangeWizardStepTitle(this.step_selectCourse, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepSelectCourse));
										this.ChangeWizardStepTitle(this.step_confirmProfInfo, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo));
										this.ChangeWizardStepTitle(this.step_additionalInfo, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepAdditionalInfo));
										this.ChangeWizardStepTitle(this.step_chooseAccommodations, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepChooseAccommodations));
										this.ChangeWizardStepTitle(this.step_confirmAndComplete, new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_CustomWizardStepRewording_StepConfirmAndComplete));
									}
									string onClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
									((Button)this.step_welcome.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_selectCourse.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_confirmProfInfo.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_chooseAccommodations.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									int settingValue8 = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_MinDaysAheadToBook);
									this.cutoffNumDays.Value = settingValue8.ToString();
									Button button = this.Wizard1.FindControl("StartNavigationTemplateContainerID").FindControl("CancelButton") as Button;
									bool flag15 = button != null;
									if (flag15)
									{
										button.OnClientClick = "return confirm('Are you sure you want to cancel?')";
									}
									this.Wizard1.CancelDestinationPageUrl = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_TestBookingCancelUrl);
									string text = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_WelcomeMsg);
									DateTime settingValue9 = new WebSettingsClientManager().GetSettingValue<DateTime>(this.EXAMBOOKING_FinalExamRequest_FinalsStartDate);
									DateTime settingValue10 = new WebSettingsClientManager().GetSettingValue<DateTime>(this.EXAMBOOKING_FinalExamRequest_FinalsEndDate);
									text = text.Replace("#~startdate~#", settingValue9.ToString("MMMM d"));
									text = text.Replace("#~enddate~#", settingValue10.ToString("MMMM d"));
									((Label)this.step_welcome.ContentTemplateContainer.FindControl("lbl_welcome")).Text = text;
									string settingValue11 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmBookingFinishButtonText);
									bool flag16 = settingValue11.Length > 0;
									if (flag16)
									{
										this.Wizard1.FinishCompleteButtonText = settingValue11;
									}
									string settingValue12 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmBookingMsg);
									bool flag17 = settingValue12.Length > 0;
									if (flag17)
									{
										this.lbl_finishMessage.Text = settingValue12;
									}
									string settingValue13 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmationPage_IntroText);
									bool flag18 = settingValue13.Length > 0;
									if (flag18)
									{
										this.lbl_confirmationIntroMsg.Text = settingValue13;
										this.p_confirmationIntroMsg.Visible = true;
									}
									string settingValue14 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText);
									bool flag19 = settingValue14.Length > 0;
									if (flag19)
									{
										this.chk_iagree.Text = settingValue14;
									}
									DateTime dateTime;
									DateTime dateTime2;
									ClockWorkWebAPI.Core.GetTermStartEndDates(out dateTime, out dateTime2);
									string settingValue15 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_RestrictCoursesToCampus);
									DataTable dataTable2 = ClockWorkController.Course.LoadStudentsCoursesOverlappingNow_Table(num, settingValue15);
									bool flag20 = dataTable2.Rows.Count < 1;
									if (flag20)
									{
										NavigatorClientManager.CurrentInstance.NotAllowed(Setting.TESTBOOKING_ErrorMessage_NoCourses, this.Page);
									}
									else
									{
										string text2 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_FilterCourseListByTimeOfDay).ToLower();
										string[] array = text2.Split(new char[]
										{
											','
										}, StringSplitOptions.RemoveEmptyEntries);
										bool flag21 = !string.IsNullOrEmpty(text2);
										for (int i = 0; i < dataTable2.Rows.Count; i++)
										{
											DataRow dataRow = dataTable2.Rows[i];
											bool flag22 = flag21;
											bool flag23;
											if (flag22)
											{
												string value2 = dataRow["timeofday"].ToString().ToLower().Trim();
												flag23 = (Array.IndexOf<string>(array, value2) < 0);
											}
											else
											{
												flag23 = true;
											}
											bool flag24 = flag23;
											if (flag24)
											{
												string text3 = ClockWorkWebAPI.Course.CourseToString(dataRow);
												DateTime? dateTime3 = (dataRow["startdate"] == DBNull.Value) ? null : new DateTime?((DateTime)dataRow["startdate"]);
												DateTime? dateTime4 = (dataRow["enddate"] == DBNull.Value) ? null : new DateTime?((DateTime)dataRow["enddate"]);
												string value3 = string.Format("{0},{1},{2}", ((int)dataRow["lucourseid"]).ToString(), (dateTime3 != null) ? dateTime3.Value.ToString("yyyy-MM-dd") : "", (dateTime4 != null) ? dateTime4.Value.ToString("yyyy-MM-dd") : "");
												ListItem item = new ListItem(text3, value3);
												this.cmb_course.Items.Add(item);
											}
										}
										int settingValue16 = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
										bool flag25 = settingValue16 < 1;
										if (flag25)
										{
											this.lbl_additionalRequirements.Visible = false;
											this.lbl_additionalRequirementsValue.Visible = false;
										}
										bool settingValue17 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_AskStudentForInstructorPhone);
										bool flag26 = settingValue17;
										if (flag26)
										{
											this.row_instructorPhone.Visible = true;
										}
										bool settingValue18 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_AskStudentForCourseAlternateContactInfo);
										bool flag27 = !settingValue18;
										if (flag27)
										{
											this.row_altContact.Visible = false;
										}
										string settingValue19 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_ChooseAccommodationsInstructions);
										string settingValue20 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_ChooseAccommodationsNote);
										this.lbl_chooseAccommodationsInstructions.Text = settingValue19;
										this.lbl_accommodationsNote.Text = settingValue20;
										bool settingValue21 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_AllowStudentToSelectFromApprovedDateTimes);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001A894 File Offset: 0x00018A94
		private Label lbl_accommodations
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_accommodations");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		private Label lbl_chooseAccommodationsInstructions
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodationsInstructions");
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001A8EC File Offset: 0x00018AEC
		private Label lbl_accommodationsNote
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_accommodationsNote");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0001A918 File Offset: 0x00018B18
		private TextBox txt_instructorPhone
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorPhone");
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001A944 File Offset: 0x00018B44
		private TableRow row_instructorPhone
		{
			get
			{
				return (TableRow)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("row_instructorPhone");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001A970 File Offset: 0x00018B70
		private Panel p_available
		{
			get
			{
				return (Panel)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("p_available");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001A99C File Offset: 0x00018B9C
		private Panel p_instructorInfo
		{
			get
			{
				return (Panel)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("p_instructorInfo");
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		private Panel p_welcome
		{
			get
			{
				return (Panel)this.step_welcome.ContentTemplateContainer.FindControl("p_welcome");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		private Label lbl_emsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_emsg");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0001AA20 File Offset: 0x00018C20
		private Label lbl_chooseAccommodations
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodations");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001AA4C File Offset: 0x00018C4C
		private Panel p_emsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_emsg");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001AA78 File Offset: 0x00018C78
		private TextBox txt_altProfEmail
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_altProfEmail");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		private Label lbl_courseDescription
		{
			get
			{
				return (Label)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("lbl_courseDescription");
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		private HiddenField lastSelectedLucid
		{
			get
			{
				return (HiddenField)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("lastSelectedLucid");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001AAFC File Offset: 0x00018CFC
		private RadListBox lb_accommodations
		{
			get
			{
				return (RadListBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lb_accommodations");
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001AB28 File Offset: 0x00018D28
		private TextBox txt_instructorEmail
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorEmail");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001AB54 File Offset: 0x00018D54
		private Label lbl_instructorVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_instructorVal");
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0001AB80 File Offset: 0x00018D80
		private Label lbl_courseVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_courseVal");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001ABAC File Offset: 0x00018DAC
		private Label lbl_classDateTimeVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_classDateTimeVal");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		private Label lbl_yourTestDateTimeVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeVal");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001AC04 File Offset: 0x00018E04
		private Label lbl_yourTestDateTime
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTime");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001AC30 File Offset: 0x00018E30
		private Label lbl_yourTestDateTimeGap
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeGap");
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001AC5C File Offset: 0x00018E5C
		private Label lbl_yourTestDateTimeGap0
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeGap0");
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001AC88 File Offset: 0x00018E88
		private CheckBoxList chk_accommodations
		{
			get
			{
				return (CheckBoxList)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("chk_accommodations");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		private TextBox txt_instructorName
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorName");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		private Label lbl_additionalRequirementsValue
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirementsValue");
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001AD0C File Offset: 0x00018F0C
		private CheckBox chk_iagree
		{
			get
			{
				return (CheckBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("chk_iagree");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001AD38 File Offset: 0x00018F38
		private Label lbl_additionalRequirements
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirements");
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001AD64 File Offset: 0x00018F64
		private Panel p_confirmationIntroMsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_confirmationIntroMsg");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001AD90 File Offset: 0x00018F90
		private Label lbl_finishMessage
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_finishMessage");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001ADBC File Offset: 0x00018FBC
		private Label lbl_confirmationIntroMsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmationIntroMsg");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		private TableRow row_altContact
		{
			get
			{
				return (TableRow)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("row_altContact");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001AE14 File Offset: 0x00019014
		private HiddenField cutoffNumDays
		{
			get
			{
				return (HiddenField)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("cutoffNumDays");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001AE40 File Offset: 0x00019040
		private Label lbl_confirmAndCompleteTitle
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmAndCompleteTitle");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001AE6C File Offset: 0x0001906C
		private DropDownList cmb_course
		{
			get
			{
				return (DropDownList)this.step_selectCourse.ContentTemplateContainer.FindControl("cmb_course");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001AE98 File Offset: 0x00019098
		private Panel p_courseInfo
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_courseInfo");
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001AEC4 File Offset: 0x000190C4
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = settingValue && this.Wizard1.ActiveStep == this.step_confirmProfInfo;
			if (flag)
			{
				Wizard wizard = this.Wizard1;
				int activeStepIndex = wizard.ActiveStepIndex;
				wizard.ActiveStepIndex = activeStepIndex + 1;
			}
			DateTime value;
			DateTime value2;
			ClockWorkWebAPI.Core.GetTermStartEndDates(out value, out value2);
			int pid = this.LookupStudentPid();
			DateTime? dateTime;
			DateTime? dateTime2;
			int selectedLucid = this.GetSelectedLucid(out dateTime, out dateTime2);
			bool flag2 = dateTime2 != null && dateTime2.Value > value2;
			if (flag2)
			{
				value2 = dateTime2.Value;
			}
			bool flag3 = dateTime != null && dateTime.Value < value;
			if (flag3)
			{
				value = dateTime.Value;
			}
			int num = (selectedLucid > 0) ? this.GetLastSelectedLucid() : 0;
			bool flag4 = selectedLucid > 0 && num != selectedLucid;
			if (flag4)
			{
				this.CourseChanged(selectedLucid, value, value2, pid);
			}
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			string text = (activeStep == null) ? "" : activeStep.Title;
			bool flag5 = !string.IsNullOrEmpty(text);
			if (flag5)
			{
				this.Page.Title = "Schedule a Final Exam - " + text;
			}
			bool flag6 = this.Wizard1.ActiveStep == this.step_welcome;
			if (!flag6)
			{
				bool flag7 = this.Wizard1.ActiveStep == this.step_selectCourse;
				if (flag7)
				{
					ClockWorkWebCore.SetFocus(this.cmb_course);
				}
			}
			bool flag8 = this.Wizard1.ActiveStep == this.step_confirmProfInfo;
			if (flag8)
			{
				ClockWorkWebCore.SetFocus(this.txt_instructorName);
			}
			else
			{
				bool flag9 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
				if (flag9)
				{
					bool flag10 = selectedLucid > 0 && num != selectedLucid;
					if (flag10)
					{
						this.CourseChanged(selectedLucid, value, value2, pid);
					}
					ClockWorkWebCore.SetFocus(this.chk_accommodations);
				}
				else
				{
					bool flag11 = this.Wizard1.ActiveStep == this.step_confirmAndComplete;
					if (flag11)
					{
						this.p_emsg.Visible = false;
						bool flag12 = this.cmb_course.SelectedIndex < 0;
						if (flag12)
						{
							this.Wizard1.ActiveStepIndex = 1;
							return;
						}
						this.lbl_courseVal.Text = this.cmb_course.SelectedItem.Text;
						this.lbl_instructorVal.Text = this.txt_instructorName.Text + " . " + this.txt_instructorEmail.Text;
						bool flag13 = !new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation);
						bool flag14 = !flag13;
						if (flag14)
						{
							this.lbl_accommodations.Text = "You opted out of the following accommodation(s):";
						}
						this.lb_accommodations.Items.Clear();
						foreach (object obj in this.chk_accommodations.Items)
						{
							ListItem listItem = (ListItem)obj;
							bool flag15 = listItem.Selected == flag13;
							if (flag15)
							{
								RadListBoxItem item = new RadListBoxItem(listItem.Text, listItem.Value);
								this.lb_accommodations.Items.Add(item);
							}
						}
						bool visible = this.lbl_additionalRequirementsValue.Visible;
						if (visible)
						{
							int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
							Panel pdata = this.GetPData();
							DynamicScreenLayout.AddSummaryToLabel(this.lbl_additionalRequirementsValue, pdata, settingValue2, pid, base.Cache, new DynamicControlLayoutHelper(), "", true);
						}
					}
				}
			}
			this.SetAppropriateFocus();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001B288 File Offset: 0x00019488
		private T GetControl<T>(TemplatedWizardStep wizardStepPanel, string controlName) where T : Control
		{
			return (T)((object)wizardStepPanel.ContentTemplateContainer.FindControl(controlName));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001B2AC File Offset: 0x000194AC
		private void SetAppropriateFocus()
		{
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			bool flag = activeStep == this.step_welcome;
			if (flag)
			{
				user_test_bookexam2.SetFocus2(this.GetControl<Label>(this.step_welcome, "lbl_welcome"));
			}
			else
			{
				bool flag2 = activeStep == this.step_selectCourse;
				if (flag2)
				{
					user_test_bookexam2.SetFocus2(this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course"));
				}
				else
				{
					bool flag3 = activeStep == this.step_confirmProfInfo;
					if (flag3)
					{
						user_test_bookexam2.SetFocus2(this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName"));
					}
					else
					{
						bool flag4 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
						if (flag4)
						{
							user_test_bookexam2.SetFocus2(this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations"));
						}
						else
						{
							bool flag5 = this.Wizard1.ActiveStep == this.step_additionalInfo;
							if (flag5)
							{
								Panel panel = this.step_additionalInfo.FindControl("p_data") as Panel;
								Control control = (panel == null || panel.Controls.Count < 1) ? null : panel.Controls[0];
								while (control != null && control is Panel && control.Controls.Count > 0)
								{
									control = control.Controls[0];
								}
								bool flag6 = control != null;
								if (flag6)
								{
									user_test_bookexam2.SetFocus2(control);
								}
							}
							else
							{
								bool flag7 = activeStep == this.step_confirmAndComplete;
								if (flag7)
								{
									user_test_bookexam2.SetFocus2ForSummaryStep(this.GetControl<CheckBox>(this.step_confirmAndComplete, "chk_iagree"));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001B444 File Offset: 0x00019644
		private static void SetFocus2ForSummaryStep(Control control)
		{
			string activeJavascript = "try { FocusTextBox('" + control.ClientID + "'); } catch ( ex0 ) { } \r\n" + "try { MakeSummaryAlertPop(); } catch (ex) { } \r\n";
			user_test_bookexam2.SetFocus2(control, activeJavascript);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001B478 File Offset: 0x00019678
		private static void SetFocus2(Control control)
		{
			bool flag = control == null;
			if (!flag)
			{
				bool flag2 = control is CheckBoxList;
				string activeJavascript;
				if (flag2)
				{
					activeJavascript = "SelectAccommodationsCheckBoxList();\r\n";
				}
				else
				{
					bool flag3 = control is RadioButtonList;
					if (flag3)
					{
						activeJavascript = (control.ID.Equals("rbtns_existingClassDateTimes") ? "SelectPotentialTimesRadioButtonList2();\r\n" : "SelectPotentialTimesRadioButtonList();\r\n");
					}
					else
					{
						activeJavascript = "FocusTextBox('" + control.ClientID + "');\r\n";
					}
				}
				user_test_bookexam2.SetFocus2(control, activeJavascript);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001B4F4 File Offset: 0x000196F4
		private static void SetFocus2(Control control, string activeJavascript)
		{
			bool flag = control == null;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\r\n<script language='JavaScript'>\r\n");
				stringBuilder.Append("<!--\r\n");
				stringBuilder.Append("function SetFocus()\r\n");
				stringBuilder.Append("{\r\n");
				stringBuilder.Append("try {");
				stringBuilder.Append(activeJavascript);
				stringBuilder.Append("window.location='#MainContent';\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("catch ( e ) {\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("window.onload = SetFocus;\r\n");
				stringBuilder.Append("// -->\r\n");
				stringBuilder.Append("</script>");
				control.Page.ClientScript.RegisterClientScriptBlock(control.Page.GetType(), "SetFocus", stringBuilder.ToString());
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001B5E0 File Offset: 0x000197E0
		private List<ClockWorkWebAPI.TestBooking.Accommodation> GetSelectedAccommodations()
		{
			List<ClockWorkWebAPI.TestBooking.Accommodation> list = new List<ClockWorkWebAPI.TestBooking.Accommodation>();
			foreach (object obj in this.chk_accommodations.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool selected = listItem.Selected;
				if (selected)
				{
					string value = listItem.Value;
					string lookupText = "";
					int num = value.IndexOf('`');
					bool flag = num > 0;
					string s;
					if (flag)
					{
						s = value.Substring(0, num);
						bool flag2 = num < value.Length - 1;
						if (flag2)
						{
							lookupText = value.Substring(num + 1);
						}
					}
					else
					{
						s = value;
					}
					ClockWorkWebAPI.TestBooking.Accommodation item = new ClockWorkWebAPI.TestBooking.Accommodation(int.Parse(s), listItem.Text, lookupText);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001B6E0 File Offset: 0x000198E0
		private static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001B76C File Offset: 0x0001996C
		private void CourseChanged(int newLucid, DateTime sdate, DateTime edate, int pid)
		{
			this.lastSelectedLucid.Value = newLucid.ToString();
			DataTable dataTable = ClockWorkController.Course.LoadStudentsCourse(pid, newLucid, sdate, edate);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				this.txt_instructorName.Text = dataRow["instructor"].ToString();
				this.txt_instructorEmail.Text = dataRow["instructoremail"].ToString();
				bool visible = this.txt_instructorPhone.Visible;
				if (visible)
				{
					this.txt_instructorPhone.Text = dataRow["instructorphone"].ToString();
				}
				this.lbl_courseDescription.Text = ClockWorkWebAPI.Course.CourseToString(dataRow);
			}
			else
			{
				this.txt_instructorEmail.Text = "";
				this.txt_instructorName.Text = "";
				this.lbl_courseDescription.Text = "unknown";
			}
			ClockWorkWebAPI.AccommodationCollection accommodationCollection = ClockWorkController.Accommodation.LoadAccommodations(pid, newLucid, "");
			accommodationCollection.SortListByCaptionWithValue();
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_NonNegotiableAccommodationCids);
			List<int> list = user_test_bookexam2.IntListFromString(settingValue);
			bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_WizardSetting_AccommodationsDefaultChecked);
			this.chk_accommodations.Items.Clear();
			foreach (object obj in accommodationCollection)
			{
				ClockWorkWebAPI.Accommodation accommodation = (ClockWorkWebAPI.Accommodation)obj;
				string value = accommodation.ControlId.ToString() + "`" + accommodation.ControlCaption;
				string captionWithValue = accommodation.CaptionWithValue;
				ListItem listItem = new ListItem(captionWithValue, value);
				this.chk_accommodations.Items.Add(listItem);
				bool flag2 = settingValue2;
				if (flag2)
				{
					listItem.Selected = true;
				}
				bool flag3 = list.Contains(accommodation.ControlId);
				if (flag3)
				{
					listItem.Selected = true;
					listItem.Enabled = false;
				}
			}
			bool flag4 = this.chk_accommodations.Items.Count < 1;
			if (flag4)
			{
				this.chk_accommodations.Items.Add("");
				this.chk_accommodations.Enabled = false;
			}
			int settingValue3 = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_MinDaysAheadToBook);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		public string accommodationsclientid()
		{
			return this.chk_accommodations.ClientID;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001BA08 File Offset: 0x00019C08
		private int GetRule_CutoffNumDays()
		{
			string value = this.cutoffNumDays.Value;
			bool flag = value.Length > 0;
			if (flag)
			{
				try
				{
					return int.Parse(value);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001BA58 File Offset: 0x00019C58
		private int GetLastSelectedLucid()
		{
			string value = this.lastSelectedLucid.Value;
			bool flag = value.Length > 0;
			if (flag)
			{
				try
				{
					return int.Parse(value);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		private int GetSelectedLucid()
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			return this.GetSelectedLucid(out dateTime, out dateTime2);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		private int GetSelectedLucid(out DateTime? sd, out DateTime? ed)
		{
			bool flag = this.cmb_course.SelectedItem != null;
			if (flag)
			{
				string value = this.cmb_course.SelectedItem.Value;
				bool flag2 = value.Length > 0;
				if (flag2)
				{
					int num = value.IndexOf(",");
					string s = (num > 0) ? value.Substring(0, num) : value;
					int num2;
					bool flag3 = !int.TryParse(s, out num2);
					if (flag3)
					{
						num2 = 0;
					}
					bool flag4 = num2 > 0;
					if (flag4)
					{
						bool flag5 = num > 0;
						if (flag5)
						{
							string text = value.Substring(num + 1);
							int num3 = text.IndexOf(",");
							bool flag6 = num3 > 0;
							if (flag6)
							{
								string s2 = text.Substring(0, num3);
								string s3 = text.Substring(num3 + 1);
								DateTime value2;
								DateTime value3;
								bool flag7 = DateTime.TryParse(s2, out value2) && DateTime.TryParse(s3, out value3);
								if (flag7)
								{
									sd = new DateTime?(value2);
									ed = new DateTime?(value3);
									return num2;
								}
							}
						}
						sd = null;
						ed = null;
						return num2;
					}
				}
			}
			sd = null;
			ed = null;
			return 0;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001BBFF File Offset: 0x00019DFF
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect(new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_TestBookingCancelUrl), true);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001BC20 File Offset: 0x00019E20
		private Panel GetPData()
		{
			for (int i = 0; i < this.Wizard1.WizardSteps.Count; i++)
			{
				bool flag = this.Wizard1.WizardSteps[i] is WizardStep;
				if (flag)
				{
					WizardStep wizardStep = (WizardStep)this.Wizard1.WizardSteps[i];
					bool flag2 = wizardStep.ID.Equals("step_additionalRequirements");
					if (flag2)
					{
						foreach (object obj in wizardStep.Controls)
						{
							Control control = (Control)obj;
							bool flag3 = control is Panel;
							if (flag3)
							{
								return (Panel)control;
							}
						}
					}
				}
				else
				{
					bool flag4 = this.Wizard1.WizardSteps[i] is TemplatedWizardStep;
					if (flag4)
					{
						TemplatedWizardStep templatedWizardStep = (TemplatedWizardStep)this.Wizard1.WizardSteps[i];
						bool flag5 = templatedWizardStep.ID.Equals("step_additionalRequirements");
						if (flag5)
						{
							foreach (object obj2 in templatedWizardStep.Controls)
							{
								Control control2 = (Control)obj2;
								bool flag6 = control2 is Panel;
								if (flag6)
								{
									return (Panel)control2;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int num = this.LookupStudentPid();
			int selectedLucid = this.GetSelectedLucid();
			List<ClockWorkWebAPI.TestBooking.Accommodation> selectedAccommodations = this.GetSelectedAccommodations();
			string query = "INSERT INTO ExamRequest (personid,lucourseid,instructorfirstname,instructoremail) \r\n    VALUES (@pid,@lucid,@iname,@iemail);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS ExamRequestId";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, num),
				clockWork.GetParameter("@lucid", DbType.Int32, selectedLucid),
				clockWork.GetParameter("@iname", DbType.String, this.txt_instructorName.Text),
				clockWork.GetParameter("@iemail", DbType.String, this.txt_instructorEmail.Text)
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			bool flag = dataTable.Rows.Count <= 0;
			if (!flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				int num2 = (dataRow[0] == DBNull.Value) ? 0 : ((int)dataRow[0]);
				bool flag2 = num2 > 0;
				if (flag2)
				{
					foreach (ClockWorkWebAPI.TestBooking.Accommodation accommodation in selectedAccommodations)
					{
						query = "INSERT INTO ExamRequestAccommodations (ExamRequestId,controlid) VALUES (@id,@cid)";
						parameters = new DbParameter[]
						{
							clockWork.GetParameter("@id", DbType.Int32, num2),
							clockWork.GetParameter("@cid", DbType.Int32, accommodation.Controlid)
						};
						clockWork.ExecuteNonQuery(query, parameters);
					}
					StringDictionary stringDictionary = new StringDictionary();
					ClockWorkWebAPI.Person studentInfo = ClockWorkWebAPI.Person.GetStudentInfo(num, this.Page);
					string value = (this.cmb_course.SelectedItem == null) ? "" : this.cmb_course.SelectedItem.Text;
					ClockWorkController.Instructor instructor = new ClockWorkController.Instructor(selectedLucid);
					string value2 = this.txt_instructorName.Text.Trim();
					string value3 = this.txt_instructorEmail.Text.Trim();
					string value4 = this.txt_instructorPhone.Visible ? this.txt_instructorPhone.Text.Trim() : "";
					stringDictionary.Add("email", studentInfo.Email);
					stringDictionary.Add("firstname", studentInfo.FirstName);
					stringDictionary.Add("lastname", studentInfo.LastName);
					stringDictionary.Add("student_no", studentInfo.StudentNumber);
					stringDictionary.Add("name", studentInfo.Name);
					stringDictionary.Add("accommodations", ClockWorkWebAPI.TestBooking.Accommodation.GetAccommodationsString(selectedAccommodations));
					stringDictionary.Add("course", value);
					stringDictionary.Add("personid", num.ToString());
					stringDictionary.Add("instructorname", instructor.InstructorName);
					stringDictionary.Add("instructoremail", instructor.InstructorEmail);
					stringDictionary.Add("instructorphone", instructor.InstructorPhone);
					stringDictionary.Add("newinstructorname", value2);
					stringDictionary.Add("newinstructoremail", value3);
					stringDictionary.Add("newinstructorphone", value4);
					IMailMergeCodes mailMergeCodes = new MailMergeCodes();
					stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.TestsExams));
					stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.TestsExams));
					IEmailClientManager emailClientManager = new EmailClientManager();
					MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
					{
						PersonId = num,
						LuCourseId = selectedLucid
					};
					emailClientManager.SendEmail(this.TESTBOOKING_Email_StudentBookingConfirmation, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "BookExam2");
					this.cmb_course.SelectedIndex = -1;
					string key = "studentapps" + num.ToString();
					bool flag3 = base.Cache[key] != null;
					if (flag3)
					{
						base.Cache.Remove(key);
					}
					base.Response.Redirect("ThankyouExam2.aspx", true);
				}
				else
				{
					CWLogger.Logger.Error(string.Format("BookExam2:Finish:id=0:pid={0}:lucid={1}", num.ToString(), selectedLucid.ToString()));
				}
			}
		}

		// Token: 0x040001F9 RID: 505
		private TemplatedWizardStep step_additionalInfo = null;

		// Token: 0x040001FA RID: 506
		protected ScriptManager bbb;

		// Token: 0x040001FB RID: 507
		protected ValidationSummary ValidationSummary4;

		// Token: 0x040001FC RID: 508
		protected Wizard Wizard1;

		// Token: 0x040001FD RID: 509
		protected TemplatedWizardStep step_welcome;

		// Token: 0x040001FE RID: 510
		protected TemplatedWizardStep step_selectCourse;

		// Token: 0x040001FF RID: 511
		protected TemplatedWizardStep step_confirmProfInfo;

		// Token: 0x04000200 RID: 512
		protected TemplatedWizardStep step_chooseAccommodations;

		// Token: 0x04000201 RID: 513
		protected TemplatedWizardStep step_confirmAndComplete;

		// Token: 0x04000202 RID: 514
		protected HiddenField hidden_bookingemailbody;
	}
}
