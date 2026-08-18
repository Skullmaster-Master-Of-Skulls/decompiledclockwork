using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using ClockWorkWebAPI.TestBooking;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Reports;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000068 RID: 104
	public class user_test_bookexams : Page
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		private Setting TESTBOOKING_WizardSetting_AdditionalInformationScreenNum
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001C214 File Offset: 0x0001A414
		private Setting TESTBOOKING_WizardSetting_MinDaysAheadToBook
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_MinDaysAheadToBook;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001C22C File Offset: 0x0001A42C
		private Setting TESTBOOKING_CutoffBookingDate
		{
			get
			{
				return Setting.EXAMBOOKING_CutoffBookingDate;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001C244 File Offset: 0x0001A444
		private Setting TESTBOOKING_TestBookingCancelUrl
		{
			get
			{
				return Setting.TESTBOOKING_TestBookingCancelUrl;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001C25C File Offset: 0x0001A45C
		private Setting TESTBOOKING_WizardSetting_WelcomeMsg
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_WelcomeMsg;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001C274 File Offset: 0x0001A474
		private Setting TESTBOOKING_SelectADateTimeMessageToStudents
		{
			get
			{
				return Setting.EXAMBOOKING_SelectADateTimeMessageToStudents;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001C28C File Offset: 0x0001A48C
		private Setting TESTBOOKING_WizardSetting_ConfirmBookingMsg
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmBookingMsg;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		private Setting TESTBOOKING_WizardSetting_ConfirmationPage_IntroText
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IntroText;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001C2BC File Offset: 0x0001A4BC
		private Setting TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IAgreeText;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		private Setting TESTBOOKING_StudentAllowedToSelectOwnDateTime
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectOwnDateTime;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousDateTimes
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousDateTimes;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001C304 File Offset: 0x0001A504
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001C31C File Offset: 0x0001A51C
		private Setting TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar
		{
			get
			{
				return Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001C334 File Offset: 0x0001A534
		private Setting TESTBOOKING_BookTestsAsTentative
		{
			get
			{
				return Setting.EXAMBOOKING_BookTestsAsTentative;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001C34C File Offset: 0x0001A54C
		private Setting TESTBOOKING_AppointmentTypeToUseForBooking
		{
			get
			{
				return Setting.EXAMBOOKING_AppointmentTypeToUseForBooking;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001C364 File Offset: 0x0001A564
		private Setting TESTBOOKING_Email_StudentBookingConfirmation
		{
			get
			{
				return Setting.EXAMBOOKING_Email_StudentBookingConfirmation;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001C37C File Offset: 0x0001A57C
		private Setting TESTBOOKING_TestBookingCoordinatorEmail
		{
			get
			{
				return Setting.EXAMBOOKING_TestBookingCoordinatorEmail;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001C394 File Offset: 0x0001A594
		private Setting TESTBOOKING_Email_StudentBookingConfirmationForInstructor
		{
			get
			{
				return Setting.EXAMBOOKING_Email_StudentBookingConfirmationForInstructor;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		private Setting TESTBOOKING_DepartmentContactInformation
		{
			get
			{
				return Setting.EXAMBOOKING_DepartmentContactInformation;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001C3C4 File Offset: 0x0001A5C4
		private Setting TESTBOOKING_OverrideRoomPidForAvailability
		{
			get
			{
				return Setting.EXAMBOOKING_OverrideRoomPidForAvailability;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		private Setting TESTBOOKING_Rooms
		{
			get
			{
				return Setting.EXAMBOOKING_Rooms;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		private Setting TESTBOOKING_Assets
		{
			get
			{
				return Setting.EXAMBOOKING_Assets;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001C40C File Offset: 0x0001A60C
		private Setting TESTBOOKING_SpecialAccommodations
		{
			get
			{
				return Setting.EXAMBOOKING_SpecialAccommodations;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001C424 File Offset: 0x0001A624
		private Setting TESTBOOKING_Rules
		{
			get
			{
				return Setting.EXAMBOOKING_Rules;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001C43C File Offset: 0x0001A63C
		private Setting TESTBOOKING_dontAskStudentToConfirmInstructorInformation
		{
			get
			{
				return Setting.EXAMBOOKING_dontAskStudentToConfirmInstructorInformation;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0001C454 File Offset: 0x0001A654
		private Setting TESTBOOKING_code_FindPotentialBookingsStart
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsStart;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0001C46C File Offset: 0x0001A66C
		private Setting TESTBOOKING_code_FindPotentialBookingsMid
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsMid;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001C484 File Offset: 0x0001A684
		private Setting TESTBOOKING_code_FindPotentialBookingsEnd
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsEnd;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001C49C File Offset: 0x0001A69C
		private Setting TESTBOOKING_code_FindPotentialBookingsMisc
		{
			get
			{
				return Setting.EXAMBOOKING_code_FindPotentialBookingsMisc;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private Setting TESTBOOKING_SelectCourseInstructionMessage
		{
			get
			{
				return Setting.EXAMBOOKING_SelectCourseInstructionMessage;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001C4CC File Offset: 0x0001A6CC
		private Setting TESTBOOKING_NonNegotiableAccommodationCids
		{
			get
			{
				return Setting.EXAMBOOKING_NonNegotiableAccommodationCids;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		private Setting TESTBOOKING_RestrictCoursesToCampus
		{
			get
			{
				return Setting.EXAMBOOKING_RestrictCoursesToCampus;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		private Setting TESTBOOKING_WizardSetting_AccommodationsDefaultChecked
		{
			get
			{
				return Setting.EXAMBOOKING_WizardSetting_AccommodationsDefaultChecked;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001C514 File Offset: 0x0001A714
		private Setting TESTBOOKING_AskStudentForInstructorPhone
		{
			get
			{
				return Setting.EXAMBOOKING_AskStudentForInstructorPhone;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0001C52C File Offset: 0x0001A72C
		private Setting TESTBOOKING_ChooseAccommodationsInstructions
		{
			get
			{
				return Setting.EXAMBOOKING_ChooseAccommodationsInstructions;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001C544 File Offset: 0x0001A744
		private Setting TESTBOOKING_ChooseAccommodationsNote
		{
			get
			{
				return Setting.EXAMBOOKING_ChooseAccommodationsNote;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001C55C File Offset: 0x0001A75C
		private Setting TESTBOOKING_AllowStudentToSelectFromApprovedDateTimes
		{
			get
			{
				return Setting.EXAMBOOKING_AllowStudentToSelectFromApprovedDateTimes;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001C574 File Offset: 0x0001A774
		private Setting TESTBOOKING_IgnoreStudentSchedule
		{
			get
			{
				return Setting.EXAMBOOKING_IgnoreStudentSchedule;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001C58C File Offset: 0x0001A78C
		private Setting TESTBOOKING_IgnoreStudentTwoTestsSameCourseSameDay
		{
			get
			{
				return Setting.EXAMBOOKING_IgnoreStudentTwoTestsSameCourseSameDay;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		private Setting TESTBOOKING_MaxDuration
		{
			get
			{
				return Setting.EXAMBOOKING_MaxDuration;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		private Setting TESTBOOKING_MaxDurationUseTimetable
		{
			get
			{
				return Setting.EXAMBOOKING_MaxDurationUseTimetable;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		private Setting GENERAL_ErrorMessage_NotAClockWorkStudent
		{
			get
			{
				return Setting.GENERAL_ErrorMessage_NotAClockWorkStudent;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		private Setting TESTBOOKING_ErrorMessage_ModuleInactive
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_ModuleInactive;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001C604 File Offset: 0x0001A804
		private Setting TESTBOOKING_ErrorMessage_NoCourses
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_NoCourses;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001C61C File Offset: 0x0001A81C
		private Setting TESTBOOKING_ErrorMessage_AccommodationsExpired
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_AccommodationsExpired;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001C634 File Offset: 0x0001A834
		private Setting TESTBOOKING_ErrorMessage_MissingPerStudentData
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_MissingPerStudentData;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001C64C File Offset: 0x0001A84C
		private Setting TESTBOOKING_ErrorMessage_Pilot
		{
			get
			{
				return Setting.EXAMBOOKING_ErrorMessage_Pilot;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001C664 File Offset: 0x0001A864
		private Setting TESTBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids
		{
			get
			{
				return Setting.EXAMBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001C67C File Offset: 0x0001A87C
		private Setting EXAMBOOKING_FinalExamRequest_Enabled
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_Enabled;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001C694 File Offset: 0x0001A894
		private Setting EXAMBOOKING_AllowStudentsToBookMultipleExams
		{
			get
			{
				return Setting.EXAMBOOKING_AllowStudentsToBookMultipleExams;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		private Setting EXAMBOOKING_FinalExamRequest_FinalsStartDate
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_FinalsStartDate;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		private Setting EXAMBOOKING_FinalExamRequest_FinalsEndDate
		{
			get
			{
				return Setting.EXAMBOOKING_FinalExamRequest_FinalsEndDate;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		private Setting EXAMBOOKING_ReportForLookingUpExamInfo
		{
			get
			{
				return Setting.EXAMBOOKING_ReportForLookingUpExamInfo;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		private Setting TESTBOOKING_StudentsAllowedToBookTests
		{
			get
			{
				return Setting.EXAMBOOKING_StudentsAllowedToBookExams;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001C70C File Offset: 0x0001A90C
		private Setting TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom
		{
			get
			{
				return Setting.EXAMBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001C724 File Offset: 0x0001A924
		private Setting TESTBOOKING_SpecialAccommodationsEmailTemplate
		{
			get
			{
				return Setting.EXAMBOOKING_SpecialAccommodationsEmailTemplate;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001C73C File Offset: 0x0001A93C
		private Setting TESTBOOKING_FilterCourseListByTimeOfDay
		{
			get
			{
				return Setting.EXAMBOOKING_FilterCourseListByTimeOfDay;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001C754 File Offset: 0x0001A954
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001C76C File Offset: 0x0001A96C
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001C784 File Offset: 0x0001A984
		private Setting TESTBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline
		{
			get
			{
				return Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001C79C File Offset: 0x0001A99C
		public RadGrid RadGrid1
		{
			get
			{
				Control control = this.step_chooseAccommodations.ContentTemplateContainer.FindControl("RadGrid1");
				return (RadGrid)control;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		private Label lbl_chooseAccommodationsMessage
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodationsMessage");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private Panel p_courseInstruction
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_courseInstruction");
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001C824 File Offset: 0x0001AA24
		private Label lbl_courseInstruction
		{
			get
			{
				return (Label)this.step_selectCourse.ContentTemplateContainer.FindControl("lbl_courseInstruction");
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001C850 File Offset: 0x0001AA50
		private Panel p_available
		{
			get
			{
				return (Panel)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("p_available");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001C87C File Offset: 0x0001AA7C
		private Panel p_welcome
		{
			get
			{
				return (Panel)this.step_welcome.ContentTemplateContainer.FindControl("p_welcome");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		private Label lbl_emsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_emsg");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		private Label lbl_chooseAccommodations
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodations");
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001C900 File Offset: 0x0001AB00
		private Panel p_emsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_emsg");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001C92C File Offset: 0x0001AB2C
		private HiddenField hidden_lastSelectedLucids
		{
			get
			{
				return (HiddenField)this.step_selectCourse.ContentTemplateContainer.FindControl("hidden_lastSelectedLucids");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001C958 File Offset: 0x0001AB58
		private RadListBox lb_accommodations
		{
			get
			{
				return (RadListBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lb_accommodations");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001C984 File Offset: 0x0001AB84
		private CheckBoxList chk_accommodations
		{
			get
			{
				return (CheckBoxList)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("chk_accommodations");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
		private Label lbl_additionalRequirementsValue
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirementsValue");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		private CheckBox chk_iagree
		{
			get
			{
				return (CheckBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("chk_iagree");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001CA08 File Offset: 0x0001AC08
		private Label lbl_additionalRequirements
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirements");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001CA34 File Offset: 0x0001AC34
		private Panel p_confirmationIntroMsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_confirmationIntroMsg");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001CA60 File Offset: 0x0001AC60
		private Label lbl_finishMessage
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_finishMessage");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		private Label lbl_confirmationIntroMsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmationIntroMsg");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		private Label lbl_yourTestDateTimeVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeVal");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		private Label lbl_confirmAndCompleteTitle
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmAndCompleteTitle");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001CB10 File Offset: 0x0001AD10
		private DropDownList cmb_course
		{
			get
			{
				return (DropDownList)this.step_selectCourse.ContentTemplateContainer.FindControl("cmb_course");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		private RadGrid grid_courses
		{
			get
			{
				return (RadGrid)this.step_selectCourse.ContentTemplateContainer.FindControl("grid_courses");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001CB68 File Offset: 0x0001AD68
		private RadGrid grid_teststobook
		{
			get
			{
				return (RadGrid)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("grid_teststobook");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001CB94 File Offset: 0x0001AD94
		private Panel p_courseInfo
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_courseInfo");
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_StudentsAllowedToBookTests);
			bool flag = !settingValue;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_ModuleInactive, this.Page);
			}
			else
			{
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(this.EXAMBOOKING_AllowStudentsToBookMultipleExams);
				bool flag2 = !settingValue2;
				if (flag2)
				{
					base.Response.Redirect("bookexam.aspx", true);
				}
				int num = this.LookupStudentPid();
				bool flag3 = num < 1;
				if (flag3)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
				else
				{
					IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
					bool flag4 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(num, true);
					bool flag5 = flag4;
					if (flag5)
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.TESTBOOKING_ErrorMessage_AccommodationsExpired, this.Page);
					}
					else
					{
						bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSqlEnabled);
						bool flag6 = settingValue3;
						if (flag6)
						{
							string settingValue4 = new WebSettingsClientManager().GetSettingValue<string>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql);
							bool flag7 = !string.IsNullOrEmpty(settingValue4);
							if (flag7)
							{
								DataTable dataTable = new DataTable();
								try
								{
									DbParameter[] parameters = new DbParameter[]
									{
										clockWork.GetParameter("@pid", DbType.Int32, num)
									};
									dataTable = clockWork.ExecuteQuery(settingValue4, parameters);
									bool flag8 = dataTable.Rows.Count > 0;
									if (flag8)
									{
										string value = dataTable.Rows[0][0].ToString().Trim();
										bool flag9 = !string.IsNullOrEmpty(value);
										if (flag9)
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
						bool flag10 = !this.Page.IsPostBack;
						if (flag10)
						{
							bool flag11 = base.Master != null && base.Master is IClockWorkMasterPage;
							if (flag11)
							{
								((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookExam);
							}
							string onClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
							((Button)this.step_welcome.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
							((Button)this.step_selectCourse.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
							((Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
							((Button)this.step_chooseAccommodations.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
							int settingValue5 = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_MinDaysAheadToBook);
							Button button = this.Wizard1.FindControl("StartNavigationTemplateContainerID").FindControl("CancelButton") as Button;
							bool flag12 = button != null;
							if (flag12)
							{
								button.OnClientClick = "return confirm('Are you sure you want to cancel?')";
							}
							this.Wizard1.CancelDestinationPageUrl = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_TestBookingCancelUrl);
							string text = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_WelcomeMsg);
							DateTime settingValue6 = new WebSettingsClientManager().GetSettingValue<DateTime>(this.EXAMBOOKING_FinalExamRequest_FinalsStartDate);
							DateTime settingValue7 = new WebSettingsClientManager().GetSettingValue<DateTime>(this.EXAMBOOKING_FinalExamRequest_FinalsEndDate);
							text = text.Replace("#~startdate~#", settingValue6.ToString("MMMM d"));
							text = text.Replace("#~enddate~#", settingValue7.ToString("MMMM d"));
							((Label)this.step_welcome.ContentTemplateContainer.FindControl("lbl_welcome")).Text = text;
							string settingValue8 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmationPage_IntroText);
							string settingValue9 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText);
							bool flag13 = settingValue9.Length > 0;
							if (flag13)
							{
								this.chk_iagree.Text = settingValue9;
							}
							this.lbl_chooseAccommodationsMessage.Text = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_ChooseAccommodationsInstructions);
							this.grid_courses.Rebind();
							string settingValue10 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_SelectCourseInstructionMessage);
							bool flag14 = !string.IsNullOrEmpty(settingValue10);
							if (flag14)
							{
								this.p_courseInstruction.Visible = true;
								this.lbl_courseInstruction.Text = settingValue10;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001D030 File Offset: 0x0001B230
		private IList<user_test_bookexams.CourseWithExamInfoWrapper> LoadCoursesWithInfosForGrid()
		{
			user_test_bookexams.<>c__DisplayClass169_0 CS$<>8__locals1 = new user_test_bookexams.<>c__DisplayClass169_0();
			int personId = this.LookupStudentPid();
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionView currentSession = sessionClientManager.GetCurrentSession();
			DateTime startDate = currentSession.StartDate;
			DateTime endDate = currentSession.EndDate;
			ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
			IList<CourseRegistrationDTO> list = courseRegistrationClientManager.LoadStudentsCourses(startDate, endDate, personId, false);
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_RestrictCoursesToCampus);
			user_test_bookexams.<>c__DisplayClass169_0 CS$<>8__locals2 = CS$<>8__locals1;
			string[] onlyAllowTheseCampuses;
			if (!string.IsNullOrEmpty(settingValue))
			{
				onlyAllowTheseCampuses = (from g in settingValue.Split(new char[]
				{
					','
				})
				select g.Trim()).ToArray<string>();
			}
			else
			{
				onlyAllowTheseCampuses = new string[0];
			}
			CS$<>8__locals2.onlyAllowTheseCampuses = onlyAllowTheseCampuses;
			bool flag = CS$<>8__locals1.onlyAllowTheseCampuses.Length != 0;
			if (flag)
			{
				list = (from g in list
				where CS$<>8__locals1.onlyAllowTheseCampuses.Any(delegate(string h)
				{
					LookupCourseDTO course = g.Course;
					return h.Equals((((course != null) ? course.Campus : null) ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
				})
				select g).ToList<CourseRegistrationDTO>();
			}
			TestBookingClientManager testBookingClientManager = new TestBookingClientManager();
			List<TestDTO> existingExams = (from g in testBookingClientManager.LoadTestsByStudent(personId, startDate, endDate, true)
			where g.ClassTestInfo != null && g.ClassTestInfo.ExamType == eClassTestType.FinalExam
			select g).ToList<TestDTO>();
			int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(this.EXAMBOOKING_ReportForLookingUpExamInfo);
			bool flag2 = settingValue2 > 0;
			if (flag2)
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(personId);
				bool flag3 = personBaseDTO != null && !string.IsNullOrEmpty(personBaseDTO.Student_no);
				if (flag3)
				{
					string value = personBaseDTO.Student_no ?? "";
					IReportClientManager reportClientManager = new ReportClientManager();
					RunReportResultDTO runReportResultDTO = reportClientManager.ExecuteReport(settingValue2, eReportExecutedFromLocation.Web, new List<ReportParameterDTO>
					{
						new ReportParameterDTO
						{
							Name = "studentno",
							Value = value
						}
					}.ToArray());
					bool flag4 = runReportResultDTO == null || runReportResultDTO.ReportStatus == null || runReportResultDTO.ReportStatus.LastStatusStep != eRunStatusStepDTO.CompletedSuccessfully;
					if (flag4)
					{
						CWLogger.Logger.Error("bookexams.aspx:LoadLookupExams:pid={0}:reportStatus={1}:err={2}", personId.ToString(), (runReportResultDTO == null) ? "Null1" : ((runReportResultDTO.ReportStatus == null) ? "Null2" : runReportResultDTO.ReportStatus.LastStatusStep.ToString()), (runReportResultDTO == null || runReportResultDTO.ReportStatus == null) ? "NULL" : (runReportResultDTO.ReportStatus.ErrorMessage ?? "-"));
					}
					DataTable t = (runReportResultDTO == null || runReportResultDTO.PrimaryData == null) ? null : runReportResultDTO.PrimaryData.Table;
					List<user_test_bookexams.LookupExamWrapper> testsFromTable = user_test_bookexams.GetTestsFromTable(t);
					return this.GetCoursesWithExamInfos(list, existingExams, testsFromTable);
				}
				CWLogger.Logger.Error("user:test:bookexams.aspx.cs:LoadExamsForGrid:Can't load student or missing student number:pid={0}", personId.ToString());
			}
			else
			{
				CWLogger.Logger.Error("user:test:bookexams.aspx.cs:LoadExamsForGrid:MissingReportId");
			}
			return new List<user_test_bookexams.CourseWithExamInfoWrapper>();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001D300 File Offset: 0x0001B500
		protected void grid_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			IList<user_test_bookexams.CourseWithExamInfoWrapper> dataSource = this.LoadCoursesWithInfosForGrid();
			this.grid_courses.DataSource = dataSource;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001D324 File Offset: 0x0001B524
		private static List<user_test_bookexams.LookupExamWrapper> GetTestsFromTable(DataTable t0)
		{
			List<user_test_bookexams.LookupExamWrapper> list = new List<user_test_bookexams.LookupExamWrapper>();
			bool flag = t0 == null;
			if (flag)
			{
				CWLogger.Logger.Error("user:test:bookexams.aspx.cs:GetTestsFromTable:TableIsNull");
			}
			else
			{
				bool flag2 = t0.Columns.Contains("startdatetime") && t0.Columns.Contains("enddatetime") && t0.Columns.Contains("lucourseid");
				bool flag3 = !flag2;
				if (!flag3)
				{
					bool flag4 = t0.Columns["startdatetime"].DataType == typeof(DateTime);
					bool flag5 = t0.Columns.Contains("location");
					foreach (object obj in t0.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag6 = dataRow["startdatetime"] != DBNull.Value && dataRow["enddatetime"] != DBNull.Value && dataRow["lucourseid"] != DBNull.Value;
						if (flag6)
						{
							int luCourseId = (int)dataRow["lucourseid"];
							bool flag7 = flag4;
							DateTime dateTime;
							DateTime dateTime2;
							if (flag7)
							{
								dateTime = (DateTime)dataRow["startdatetime"];
								dateTime2 = (DateTime)dataRow["enddatetime"];
							}
							else
							{
								bool flag8 = !DateTime.TryParse(dataRow["startdatetime"].ToString(), out dateTime);
								if (flag8)
								{
									dateTime = DateTime.MinValue;
								}
								bool flag9 = !DateTime.TryParse(dataRow["enddatetime"].ToString(), out dateTime2);
								if (flag9)
								{
									dateTime2 = DateTime.MinValue;
								}
							}
							bool flag10 = dateTime != DateTime.MinValue && dateTime2 != DateTime.MinValue;
							if (flag10)
							{
								user_test_bookexams.LookupExamWrapper lookupExamWrapper = new user_test_bookexams.LookupExamWrapper();
								lookupExamWrapper.ClassStartDateTime = dateTime;
								lookupExamWrapper.ClassEndDateTime = dateTime2;
								lookupExamWrapper.LuCourseId = luCourseId;
								bool flag11 = flag5;
								if (flag11)
								{
									lookupExamWrapper.Location = dataRow["location"].ToString().Trim();
								}
								list.Add(lookupExamWrapper);
							}
						}
					}
					return list;
				}
				CWLogger.Logger.Error("user:test:bookexams.aspx.cs:GetTestsFromTable:MissingRequiredColumns");
			}
			return new List<user_test_bookexams.LookupExamWrapper>();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		private IList<user_test_bookexams.CourseWithExamInfoWrapper> GetCoursesWithExamInfos(IList<CourseRegistrationDTO> studentsCourses, IList<TestDTO> existingExams, IList<user_test_bookexams.LookupExamWrapper> lookupExams)
		{
			List<user_test_bookexams.CourseWithExamInfoWrapper> list = new List<user_test_bookexams.CourseWithExamInfoWrapper>();
			bool flag = studentsCourses == null;
			IList<user_test_bookexams.CourseWithExamInfoWrapper> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				foreach (CourseRegistrationDTO courseRegistrationDTO in studentsCourses)
				{
					int lucid = (courseRegistrationDTO.Course == null) ? 0 : courseRegistrationDTO.Course.LuCourseId;
					bool alreadyBooked = existingExams.FirstOrDefault(delegate(TestDTO g)
					{
						LookupCourseBaseDTO course2 = g.GetCourse();
						return course2 != null && course2.LuCourseId == lucid;
					}) != null;
					bool flag2 = false;
					bool flag3 = lucid > 0;
					if (flag3)
					{
						LookupCourseDTO course = courseRegistrationDTO.Course;
						user_test_bookexams.CourseWithExamInfoWrapper courseWithExamInfoWrapper = new user_test_bookexams.CourseWithExamInfoWrapper
						{
							AlreadyBooked = alreadyBooked,
							LuCourseId = lucid,
							CourseDescription = course.GetCourseDescription(),
							CourseTerm = ((course == null) ? "" : (course.Term ?? "")),
							CourseDuration = ((course == null) ? "" : (course.Duration ?? ""))
						};
						user_test_bookexams.LookupExamWrapper lookupExamWrapper = lookupExams.FirstOrDefault((user_test_bookexams.LookupExamWrapper g) => g.LuCourseId == lucid);
						bool flag4 = lookupExamWrapper != null;
						if (flag4)
						{
							courseWithExamInfoWrapper.StartDate = lookupExamWrapper.ClassStartDateTime;
							courseWithExamInfoWrapper.EndDate = lookupExamWrapper.ClassEndDateTime;
							courseWithExamInfoWrapper.Duration = lookupExamWrapper.ClassDurationMinutes;
							courseWithExamInfoWrapper.Location = lookupExamWrapper.Location;
							list.Add(courseWithExamInfoWrapper);
							flag2 = true;
						}
						bool flag5 = !flag2 && courseWithExamInfoWrapper != null;
						if (flag5)
						{
							list.Add(courseWithExamInfoWrapper);
						}
					}
				}
				DateTime now = DateTime.Now;
				result = (from g in list
				where g.StartDate > now
				select g).ToList<user_test_bookexams.CourseWithExamInfoWrapper>();
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
		protected void grid_courses_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["coursedesc"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
			bool flag3 = e.Item.ItemType != GridItemType.AlternatingItem && e.Item.ItemType != GridItemType.Item;
			if (!flag3)
			{
				GridDataItem gridDataItem2 = (GridDataItem)e.Item;
				bool flag4 = e.Item.DataItem == null || !(e.Item.DataItem is DataRowView);
				if (!flag4)
				{
					DataRow row = ((DataRowView)e.Item.DataItem).Row;
					Label label = (Label)gridDataItem2.FindControl("lbl_courseSelected");
					bool flag5 = row["startdate"] == DBNull.Value;
					if (flag5)
					{
						gridDataItem2["FinalDate"].ColumnSpan = 3;
						gridDataItem2["col_startTime"].Visible = false;
						gridDataItem2["col_endTime"].Visible = false;
						Label label2 = (Label)gridDataItem2.FindControl("lbl_finalDate");
						label2.Text = "No exam dates are currently available for this course.";
						CheckBox checkBox = (CheckBox)gridDataItem2.FindControl("chk_courseSelected");
						bool flag6 = checkBox != null && label != null;
						if (flag6)
						{
							label.Visible = true;
							checkBox.Visible = false;
							checkBox.Checked = false;
						}
						else
						{
							gridDataItem2.Enabled = false;
						}
					}
					else
					{
						bool flag7 = row["alreadybooked"] != DBNull.Value && Convert.ToBoolean(row["alreadybooked"]);
						if (flag7)
						{
							gridDataItem2["FinalDate"].ColumnSpan = 3;
							gridDataItem2["col_startTime"].Visible = false;
							gridDataItem2["col_endTime"].Visible = false;
							Label label3 = (Label)gridDataItem2.FindControl("lbl_finalDate");
							label3.Text = "Your final exam is already booked for this course.";
							CheckBox checkBox2 = (CheckBox)gridDataItem2.FindControl("chk_courseSelected");
							bool flag8 = checkBox2 != null && label != null;
							if (flag8)
							{
								label.Visible = true;
								checkBox2.Visible = false;
								checkBox2.Checked = false;
							}
							else
							{
								gridDataItem2.Enabled = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001DA30 File Offset: 0x0001BC30
		protected void RadGrid1_NeedDataSource(object sender, EventArgs e)
		{
			int pid = this.LookupStudentPid();
			List<int> list;
			List<ClockWorkWebAPI.Course> selectedCourses = this.GetSelectedCourses(pid, out list);
			ClockWorkWebAPI.AccommodationCollection accommodationCollection = ClockWorkController.Accommodation.LoadAccommodations(pid, list, "");
			accommodationCollection.SortListByCaptionWithValue();
			List<ClockWorkWebAPI.Accommodation> list2 = new List<ClockWorkWebAPI.Accommodation>();
			foreach (object obj in accommodationCollection)
			{
				ClockWorkWebAPI.Accommodation accommodation = (ClockWorkWebAPI.Accommodation)obj;
				bool flag = accommodation.Lucid > 0;
				if (flag)
				{
					list2.Add(accommodation);
				}
				else
				{
					foreach (int lucid in list)
					{
						list2.Add(new ClockWorkWebAPI.Accommodation(accommodation)
						{
							Lucid = lucid
						});
					}
				}
			}
			accommodationCollection.Clear();
			list2.Sort(delegate(ClockWorkWebAPI.Accommodation a1, ClockWorkWebAPI.Accommodation a2)
			{
				string text = string.Format("{0}:{1}", a1.Lucid.ToString(), a1.ControlCaption ?? "");
				string strB = string.Format("{0}:{1}", a2.Lucid.ToString(), a2.ControlCaption ?? "");
				return text.CompareTo(strB);
			});
			foreach (ClockWorkWebAPI.Accommodation accommodation2 in list2)
			{
				accommodationCollection.Add(accommodation2);
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("coursedescription");
			dataTable.Columns.Add("accommodations");
			dataTable.Columns.Add("instructor");
			dataTable.Columns.Add("instructoremail");
			dataTable.Columns.Add("lucid");
			dataTable.Columns.Add("dateoftest");
			dataTable.Columns.Add("testduration");
			int j;
			for (int i = 0; i < accommodationCollection.Count; i = j)
			{
				ClockWorkWebAPI.Accommodation acc = accommodationCollection[i];
				j = i;
				StringBuilder stringBuilder = new StringBuilder();
				while (j < accommodationCollection.Count)
				{
					bool flag2 = accommodationCollection[j].Lucid != acc.Lucid;
					if (flag2)
					{
						break;
					}
					bool flag3 = stringBuilder.Length > 0;
					if (flag3)
					{
						stringBuilder.Append("`");
					}
					stringBuilder.Append(accommodationCollection[j].CaptionWithValue);
					stringBuilder.Append(".");
					stringBuilder.Append(accommodationCollection[j].ControlId.ToString());
					j++;
				}
				DataRow dataRow = dataTable.NewRow();
				ClockWorkWebAPI.Course course = selectedCourses.Find((ClockWorkWebAPI.Course g) => g.LuCourseId == acc.Lucid);
				bool flag4 = course != null;
				if (flag4)
				{
					dataRow["coursedescription"] = course.Description;
					dataRow["lucid"] = acc.Lucid.ToString();
					bool flag5 = course.Tag != null;
					if (flag5)
					{
						dataRow["dateoftest"] = ((DateTime)((object[])course.Tag)[0]).ToString("yyyy-MM-dd h:mm tt");
						dataRow["testduration"] = ((int)((object[])course.Tag)[1]).ToString();
					}
					bool flag6 = course.Instructor != null;
					if (flag6)
					{
						dataRow["instructor"] = course.Instructor.Name;
						dataRow["instructoremail"] = course.Instructor.Email;
					}
				}
				else
				{
					dataRow["coursedescription"] = "??";
				}
				dataRow["accommodations"] = stringBuilder.ToString();
				dataTable.Rows.Add(dataRow);
			}
			this.RadGrid1.DataSource = dataTable;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001DE64 File Offset: 0x0001C064
		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = !(e.Item is GridDataItem);
			if (!flag)
			{
				GridDataItem gridDataItem = (GridDataItem)e.Item;
				Control control = gridDataItem.FindControl("chks_accommodations");
				bool flag2 = control == null;
				if (!flag2)
				{
					CheckBoxList checkBoxList = (CheckBoxList)control;
					HiddenField hiddenField = (HiddenField)gridDataItem.FindControl("acclist");
					string value = hiddenField.Value;
					string[] array = value.Split(new char[]
					{
						'`'
					});
					foreach (string text in array)
					{
						int num = text.LastIndexOf('.');
						string text2 = (num >= 0) ? text.Substring(0, num) : text;
						ListItem item = new ListItem(text2, text)
						{
							Selected = true
						};
						checkBoxList.Items.Add(item);
					}
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001DF4C File Offset: 0x0001C14C
		protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				(e.Item as GridDataItem)["ExpandColumn"].Visible = false;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001DF88 File Offset: 0x0001C188
		protected void RadGrid1_PreRender(object sender, EventArgs e)
		{
			bool flag = this.RadGrid1 == null;
			if (!flag)
			{
				(this.RadGrid1.MasterTableView.GetColumn("ExpandColumn") as GridExpandColumn).Visible = false;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			DateTime dateTime;
			DateTime dateTime2;
			ClockWorkWebAPI.Core.GetTermStartEndDates(out dateTime, out dateTime2);
			int pid = this.LookupStudentPid();
			List<int> lucids;
			List<ClockWorkWebAPI.Course> selectedCourses = this.GetSelectedCourses(pid, out lucids);
			List<int> lastSelectedLucids = this.GetLastSelectedLucids();
			bool flag = lucids.FindAll((int g) => lastSelectedLucids.Contains(g)).Count != lucids.Count || lastSelectedLucids.FindAll((int h) => lucids.Contains(h)).Count != lastSelectedLucids.Count;
			if (flag)
			{
				this.CourseChanged(lucids, pid);
			}
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			string text = (activeStep == null) ? "" : activeStep.Title;
			bool flag2 = !string.IsNullOrEmpty(text);
			if (flag2)
			{
				this.Page.Title = "Book Final Exams - " + text;
			}
			bool flag3 = this.Wizard1.ActiveStep == this.step_welcome;
			if (!flag3)
			{
				bool flag4 = this.Wizard1.ActiveStep == this.step_selectCourse;
				if (flag4)
				{
					ClockWorkWebCore.SetFocus(this.cmb_course);
				}
				else
				{
					bool flag5 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
					if (flag5)
					{
						bool flag6 = selectedCourses.Count < 1;
						if (flag6)
						{
							this.ShowEMessage("You must select at least one course in order to continue.");
							this.Wizard1.ActiveStepIndex = 1;
						}
						else
						{
							bool flag7 = this.RadGrid1.Items.Count < 1;
							if (flag7)
							{
								this.RadGrid1.Rebind();
							}
						}
					}
					else
					{
						bool flag8 = this.Wizard1.ActiveStep == this.step_confirmAndComplete;
						if (flag8)
						{
							bool flag9 = selectedCourses.Count < 1;
							if (flag9)
							{
								this.Wizard1.ActiveStepIndex = 1;
								this.ShowEMessage("You must select at least one course in order to continue.");
							}
							else
							{
								List<ClockWorkWebAPI.TestBooking.SpecialAccommodation> specialAccommodations = Caching.LoadSpecialAccommodations(this.Page, this.TESTBOOKING_SpecialAccommodations);
								List<ClockWorkWebAPI.TestBooking.Test> list = new List<ClockWorkWebAPI.TestBooking.Test>();
								foreach (ClockWorkWebAPI.Course course in selectedCourses)
								{
									ClockWorkWebAPI.TestBooking.Test classTest = new ClockWorkWebAPI.TestBooking.Test
									{
										Lucid = course.LuCourseId,
										CourseDescription = course.Description,
										StartDate = course.StartDate,
										EndDate = course.EndDate
									};
									List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse = this.GetAccommodationsToUse(pid, course.LuCourseId);
									List<PrivateNote> list2;
									StringBuilder stringBuilder;
									List<int> list3;
									ClockWorkWebAPI.TestBooking.Test test = Booker.ApplySpecialAccommodationRules(false, pid, course.LuCourseId, specialAccommodations, classTest, accommodationsToUse, out list2, out stringBuilder, out list3);
									test.CourseDescription = course.Description;
									test.Location = course.SubjectEmail;
									test.Lucid = course.LuCourseId;
									list.Add(test);
								}
								this.grid_teststobook.DataSource = list;
								this.grid_teststobook.DataBind();
								this.p_emsg.Visible = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect(new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_TestBookingCancelUrl), true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001E31C File Offset: 0x0001C51C
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			int num = this.LookupStudentPid();
			List<ClockWorkWebAPI.TestBooking.Test> testsToBook = this.GetTestsToBook();
			List<int> luCourseIds;
			List<ClockWorkWebAPI.Course> selectedCourses = this.GetSelectedCourses(num, out luCourseIds);
			try
			{
				int settingValue = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_AppointmentTypeToUseForBooking);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<table border='1'><tr><td><b>Date</b></td><td><b>Start</b></td><td><b>End</b></td><td><b>Course</b></td><td><b>Term</b></td><td><b>CRN</b></td><td><b>Exam Accommodations</b></td></tr>");
				List<int> list = new List<int>();
				using (List<ClockWorkWebAPI.TestBooking.Test>.Enumerator enumerator = testsToBook.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClockWorkWebAPI.TestBooking.Test test = enumerator.Current;
						bool flag = list.Contains(test.Lucid);
						if (!flag)
						{
							list.Add(test.Lucid);
							ClockWorkWebAPI.Course course = selectedCourses.Find((ClockWorkWebAPI.Course c) => c.LuCourseId == test.Lucid);
							List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse = this.GetAccommodationsToUse(num, test.Lucid);
							List<PrivateNote> privateNotes = new List<PrivateNote>();
							int breakTime = test.BreakTime;
							FindPotentialBookingsInfo findPotentialBookingsInfo = new FindPotentialBookingsInfo();
							findPotentialBookingsInfo.RestrictByCampus = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom);
							findPotentialBookingsInfo.IgnoreStudentsSchedule = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_IgnoreStudentSchedule);
							ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason eCreateAppointmentFailedReason;
							Exception ex;
							ClockWorkController.Appointment.CreateExam(num, -1, false, test.StartDate, test.EndDate, course.StartDate, course.EndDate, settingValue, false, test.Lucid, accommodationsToUse, out eCreateAppointmentFailedReason, out ex, breakTime, privateNotes, findPotentialBookingsInfo);
							bool flag2 = eCreateAppointmentFailedReason > ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.None;
							if (flag2)
							{
								throw new Exception(string.Format("Booking failed for pid={0}, lucid={1}. Error message={2}", num.ToString(), test.Lucid.ToString(), eCreateAppointmentFailedReason.ToString()));
							}
							stringBuilder.Append("<tr>");
							stringBuilder.AppendFormat("<td>{0}</td>", test.StartDate.ToString("dd-MMM-yy"));
							stringBuilder.AppendFormat("<td>{0}</td>", test.StartDate.ToString("h:mm tt"));
							stringBuilder.AppendFormat("<td>{0}</td>", test.EndDate.ToString("h:mm tt"));
							stringBuilder.AppendFormat("<td>{0}</td>", test.CourseDescription);
							stringBuilder.AppendFormat("<td>{0}</td>", course.Term);
							stringBuilder.AppendFormat("<td>{0}</td>", course.Duration);
							string text = string.Join(", ", accommodationsToUse.ConvertAll<string>((ClockWorkWebAPI.TestBooking.Accommodation acc) => acc.Title).ToArray());
							stringBuilder.AppendFormat("<td>{0}</td>", text);
							stringBuilder.Append("</tr>");
							CWLogger.Logger.Info("bookexams.aspx:bookedexam:pid={0}:lucid={1}:sd={2}:ed={3}:accs={4}", new object[]
							{
								num.ToString(),
								course.LuCourseId.ToString(),
								test.StartDate.ToString("yyyy-MM-dd h:mm tt"),
								test.EndDate.ToString("h:mm tt"),
								text
							});
						}
					}
				}
				stringBuilder.Append("</table>");
				StringDictionary stringDictionary = new StringDictionary();
				stringDictionary.Add("appointmentsummaryhtml", stringBuilder.ToString());
				ClockWorkWebAPI.Person studentInfo = ClockWorkWebAPI.Person.GetStudentInfo(num, this.Page);
				stringDictionary.Add("email", studentInfo.Email);
				stringDictionary.Add("firstname", studentInfo.FirstName);
				stringDictionary.Add("lastname", studentInfo.LastName);
				stringDictionary.Add("student_no", studentInfo.StudentNumber);
				stringDictionary.Add("name", studentInfo.Name);
				IMailMergeCodes mailMergeCodes = new MailMergeCodes();
				stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.TestsExams));
				stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.TestsExams));
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
				{
					PersonId = num,
					LuCourseIds = luCourseIds
				};
				string value = this.hidden_bookingemailbody.Value;
				bool flag3 = !string.IsNullOrEmpty(value);
				if (flag3)
				{
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(this.TESTBOOKING_TestBookingCoordinatorEmail);
					stringDictionary.Add("coordinatoremail", settingValue2);
					stringDictionary.Add("list", value);
					emailClientManager.SendEmail(this.TESTBOOKING_SpecialAccommodationsEmailTemplate, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "BookExams0");
				}
				emailClientManager.SendEmail(this.TESTBOOKING_Email_StudentBookingConfirmation, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "BookExams");
				string key = "studentapps" + num.ToString();
				bool flag4 = base.Cache[key] != null;
				if (flag4)
				{
					base.Cache.Remove(key);
				}
				base.Response.Redirect("ThankyouExam.aspx", false);
			}
			catch (Exception exception)
			{
				CWLogger.Logger.ErrorException("Failed to book final", exception);
				this.ShowEMessage("There was an unexpected error. The administrator has been notified.  Please contact us to book your final exams.");
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001E888 File Offset: 0x0001CA88
		private List<ClockWorkWebAPI.TestBooking.Test> GetTestsToBook()
		{
			List<ClockWorkWebAPI.TestBooking.Test> list = new List<ClockWorkWebAPI.TestBooking.Test>();
			foreach (object obj in this.grid_teststobook.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj;
				string text = gridDataItem["c_coursedesc"].Text;
				string text2 = gridDataItem["c_testdate"].Text;
				string text3 = gridDataItem["c_testduration"].Text;
				string text4 = gridDataItem["col_location"].Text;
				string text5 = gridDataItem["c_lucourseid"].Text;
				int lucid;
				DateTime startDate;
				int num;
				bool flag = !int.TryParse(text5, out lucid) || !DateTime.TryParse(text2, out startDate) || !int.TryParse(text3, out num);
				if (!flag)
				{
					ClockWorkWebAPI.TestBooking.Test item = new ClockWorkWebAPI.TestBooking.Test
					{
						StartDate = startDate,
						EndDate = startDate.AddMinutes((double)num),
						Location = text4,
						CourseDescription = text,
						Lucid = lucid
					};
					list.Add(item);
				}
			}
			list.Sort((ClockWorkWebAPI.TestBooking.Test g1, ClockWorkWebAPI.TestBooking.Test g2) => g1.StartDate.CompareTo(g2.StartDate));
			return list;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		private List<ClockWorkWebAPI.TestBooking.Accommodation> GetAccommodationsToUse(int pid, int lucid)
		{
			List<ClockWorkWebAPI.TestBooking.Accommodation> list = new List<ClockWorkWebAPI.TestBooking.Accommodation>();
			foreach (object obj in this.RadGrid1.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj;
				string text = gridDataItem["col_lucid"].Text;
				int num;
				bool flag = !int.TryParse(text, out num) || num != lucid;
				if (!flag)
				{
					CheckBoxList checkBoxList = (CheckBoxList)gridDataItem.FindControl("chks_accommodations");
					bool flag2 = checkBoxList == null;
					if (!flag2)
					{
						foreach (object obj2 in checkBoxList.Items)
						{
							ListItem listItem = (ListItem)obj2;
							bool flag3 = !listItem.Selected;
							if (!flag3)
							{
								string value = listItem.Value;
								int num2 = value.LastIndexOf(".");
								bool flag4 = num2 <= 0;
								if (!flag4)
								{
									string text2 = value.Substring(0, num2);
									string s = value.Substring(num2 + 1);
									int cid;
									bool flag5 = !int.TryParse(s, out cid);
									if (!flag5)
									{
										ClockWorkWebAPI.TestBooking.Accommodation item = new ClockWorkWebAPI.TestBooking.Accommodation(cid, text2, text2);
										list.Add(item);
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		private List<ClockWorkWebAPI.Course> GetSelectedCourses(int pid, out List<int> lucids)
		{
			lucids = new List<int>();
			List<ClockWorkWebAPI.Course> list = new List<ClockWorkWebAPI.Course>();
			foreach (object obj in this.grid_courses.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj;
				CheckBox checkBox = (CheckBox)gridDataItem.FindControl("chk_courseSelected");
				bool flag = checkBox == null || !checkBox.Checked;
				if (!flag)
				{
					string text = gridDataItem["alreadybooked"].Text;
					bool flag3;
					bool flag2 = !bool.TryParse(text, out flag3);
					if (flag2)
					{
						flag3 = false;
					}
					bool flag4 = flag3;
					if (!flag4)
					{
						TableCell tableCell = gridDataItem["lucourseid"];
						bool flag5 = tableCell == null;
						if (!flag5)
						{
							string text2 = tableCell.Text;
							int num;
							bool flag6 = string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num);
							if (!flag6)
							{
								ClockWorkWebAPI.Course course = new ClockWorkWebAPI.Course
								{
									LuCourseId = num
								};
								TableCell tableCell2 = gridDataItem["coursedesc"];
								bool flag7 = tableCell2 != null;
								if (flag7)
								{
									course.Description = tableCell2.Text;
								}
								course.Term = gridDataItem["col_courseterm"].Text;
								course.Duration = gridDataItem["col_courseduration"].Text;
								TableCell tableCell3 = gridDataItem["testdate"];
								TableCell tableCell4 = gridDataItem["testduration"];
								TableCell tableCell5 = gridDataItem["col_location"];
								DateTime dateTime;
								int num2;
								bool flag8 = tableCell3 == null || !DateTime.TryParse(tableCell3.Text, out dateTime) || tableCell4 == null || !int.TryParse(tableCell4.Text, out num2);
								if (!flag8)
								{
									course.Tag = new object[]
									{
										dateTime,
										num2
									};
									course.StartDate = dateTime;
									course.EndDate = dateTime.AddMinutes((double)num2);
									course.SubjectEmail = tableCell5.Text.ToString();
									list.Add(course);
									lucids.Add(num);
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private void ShowEMessage(string emsg)
		{
			this.p_emsg.Visible = true;
			this.lbl_emsg.Text = emsg;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001EE10 File Offset: 0x0001D010
		private List<int> GetLastSelectedLucids()
		{
			string value = this.hidden_lastSelectedLucids.Value;
			return ClockWorkWebAPI.ClockWorkAPIReplacement.Utility.IntListFromString(value);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001EE34 File Offset: 0x0001D034
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001EE58 File Offset: 0x0001D058
		private void CourseChanged(List<int> lucids, int pid)
		{
			int settingValue = new WebSettingsClientManager().GetSettingValue<int>(this.TESTBOOKING_WizardSetting_MinDaysAheadToBook);
			bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_StudentAllowedToSelectPreviousDateTimes);
			bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions);
			bool settingValue4 = new WebSettingsClientManager().GetSettingValue<bool>(this.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar);
			bool flag = settingValue2 || settingValue3 || settingValue4;
			bool flag2 = flag;
			if (flag2)
			{
			}
		}

		// Token: 0x04000203 RID: 515
		protected ScriptManager bbb;

		// Token: 0x04000204 RID: 516
		protected ValidationSummary ValidationSummary4;

		// Token: 0x04000205 RID: 517
		protected Wizard Wizard1;

		// Token: 0x04000206 RID: 518
		protected TemplatedWizardStep step_welcome;

		// Token: 0x04000207 RID: 519
		protected TemplatedWizardStep step_selectCourse;

		// Token: 0x04000208 RID: 520
		protected TemplatedWizardStep step_chooseAccommodations;

		// Token: 0x04000209 RID: 521
		protected TemplatedWizardStep step_confirmAndComplete;

		// Token: 0x0400020A RID: 522
		protected HiddenField hidden_bookingemailbody;

		// Token: 0x020001D9 RID: 473
		internal class CourseWithExamInfoWrapper
		{
			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0004E550 File Offset: 0x0004C750
			// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0004E558 File Offset: 0x0004C758
			public int LuCourseId { get; set; }

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0004E561 File Offset: 0x0004C761
			// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0004E569 File Offset: 0x0004C769
			public string CourseDescription { get; set; }

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0004E572 File Offset: 0x0004C772
			// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0004E57A File Offset: 0x0004C77A
			public DateTime StartDate { get; set; }

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0004E583 File Offset: 0x0004C783
			// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0004E58B File Offset: 0x0004C78B
			public DateTime EndDate { get; set; }

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0004E594 File Offset: 0x0004C794
			// (set) Token: 0x06000D04 RID: 3332 RVA: 0x0004E59C File Offset: 0x0004C79C
			public int Duration { get; set; }

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0004E5A5 File Offset: 0x0004C7A5
			// (set) Token: 0x06000D06 RID: 3334 RVA: 0x0004E5AD File Offset: 0x0004C7AD
			public string Location { get; set; }

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0004E5B6 File Offset: 0x0004C7B6
			// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0004E5BE File Offset: 0x0004C7BE
			public bool AlreadyBooked { get; set; }

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0004E5C7 File Offset: 0x0004C7C7
			// (set) Token: 0x06000D0A RID: 3338 RVA: 0x0004E5CF File Offset: 0x0004C7CF
			public string CourseTerm { get; set; }

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0004E5D8 File Offset: 0x0004C7D8
			// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
			public string CourseDuration { get; set; }
		}

		// Token: 0x020001DA RID: 474
		internal class LookupExamWrapper
		{
			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0004E5E9 File Offset: 0x0004C7E9
			// (set) Token: 0x06000D0F RID: 3343 RVA: 0x0004E5F1 File Offset: 0x0004C7F1
			public DateTime ClassStartDateTime { get; set; }

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0004E5FA File Offset: 0x0004C7FA
			// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0004E602 File Offset: 0x0004C802
			public DateTime ClassEndDateTime { get; set; }

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0004E60B File Offset: 0x0004C80B
			// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0004E613 File Offset: 0x0004C813
			public int LuCourseId { get; set; }

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0004E61C File Offset: 0x0004C81C
			// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0004E624 File Offset: 0x0004C824
			public string Location { get; set; }

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0004E630 File Offset: 0x0004C830
			public int ClassDurationMinutes
			{
				get
				{
					return Convert.ToInt32((this.ClassEndDateTime - this.ClassStartDateTime).TotalMinutes);
				}
			}
		}
	}
}
