using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Entity
{
	// Token: 0x02000002 RID: 2
	public class CourseWithStudentAccommodationRequestView : WrapperBase<CourseRegistrationWithAccommodationRequestDTO>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CourseWithStudentAccommodationRequestView()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public CourseWithStudentAccommodationRequestView(CourseRegistrationWithAccommodationRequestDTO item, DateTime? dateCourseMustStartBefore) : base(item)
		{
			this.DisableRequestingBecauseCourseStartDateIsAfterAccExpiry = (dateCourseMustStartBefore != null && item.CourseRegistrationWithAccommodations.CourseReg.Course.StartDate >= dateCourseMustStartBefore.Value);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020A1 File Offset: 0x000002A1
		public bool DisableRequestingBecauseCourseStartDateIsAfterAccExpiry { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020AA File Offset: 0x000002AA
		public CourseRegistrationWithAccommodationsDTO CourseRegistrationWithAccommodations
		{
			get
			{
				CourseRegistrationWithAccommodationRequestDTO item = base.Item;
				return (item != null) ? item.CourseRegistrationWithAccommodations : null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BE File Offset: 0x000002BE
		public StudentCourseAccommodationRequestDTO AccommodationRequest
		{
			get
			{
				CourseRegistrationWithAccommodationRequestDTO item = base.Item;
				return (item != null) ? item.AccommodationRequest : null;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D2 File Offset: 0x000002D2
		public eStudentCourseAccommodationRequestStatusDTO eStatus
		{
			get
			{
				StudentCourseAccommodationRequestDTO accommodationRequest = this.AccommodationRequest;
				return (accommodationRequest != null) ? accommodationRequest.Status : eStudentCourseAccommodationRequestStatusDTO.Unknown;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public string Status
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				DateTime? courseEndDate = this.CourseEndDate;
				bool flag = courseEndDate != null && courseEndDate.Value < DateTime.Now.Date;
				bool flag2 = flag;
				string text;
				string settingValue;
				if (flag2)
				{
					text = "Course has ended";
					settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_CourseHasEnded);
				}
				else
				{
					bool flag3 = this.AccommodationRequest == null;
					if (flag3)
					{
						bool disableRequestingBecauseCourseStartDateIsAfterAccExpiry = this.DisableRequestingBecauseCourseStartDateIsAfterAccExpiry;
						if (disableRequestingBecauseCourseStartDateIsAfterAccExpiry)
						{
							text = "Accommodations are expired";
							settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_AccommodationsAreExpired);
						}
						else
						{
							text = "Waiting for student to request";
							settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_PleaseClickRequest);
						}
					}
					else
					{
						eStudentCourseAccommodationRequestStatusDTO status = this.AccommodationRequest.Status;
						eStudentCourseAccommodationRequestStatusDTO eStudentCourseAccommodationRequestStatusDTO = status;
						switch (eStudentCourseAccommodationRequestStatusDTO)
						{
						case eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStaff:
							break;
						case eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStudent:
							text = "Pending";
							settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_YourAdvisorHasUpdatedYourAccommodationsPleaseClickRequest);
							goto IL_184;
						case eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStaff | eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStudent:
							goto IL_170;
						case eStudentCourseAccommodationRequestStatusDTO.Denied:
							text = "Denied";
							settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_PleaseContactYourAdvisorForAdditionalInfo);
							goto IL_184;
						default:
							if (eStudentCourseAccommodationRequestStatusDTO == eStudentCourseAccommodationRequestStatusDTO.Approved)
							{
								bool flag4 = this.CourseRegistrationWithAccommodations.CourseReg.DateLetterReturned != null;
								bool flag5 = !flag4;
								if (flag5)
								{
									text = "Sent";
									settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_AccommodationLetterHasBeenSentToYourInstructorAndIsAwaitingConfirmation);
								}
								else
								{
									text = "Confirmed";
									settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_AccommodationLetterHasBeenConfirmedByYourInstructor);
								}
								goto IL_184;
							}
							if (eStudentCourseAccommodationRequestStatusDTO != eStudentCourseAccommodationRequestStatusDTO.InstructorInfoMissing)
							{
								goto IL_170;
							}
							break;
						}
						text = "Pending";
						settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_YourAdvisorWillReview);
						goto IL_184;
						IL_170:
						text = "Unknown";
						settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Status_UnknownPleaseContactUs);
						IL_184:;
					}
				}
				return string.Concat(new string[]
				{
					"<b>",
					text,
					"</b><br /><span style='font-size: .75em;'>",
					settingValue,
					"</span>"
				});
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000022AC File Offset: 0x000004AC
		public string Accommodations
		{
			get
			{
				CourseRegistrationWithAccommodationsDTO courseRegistrationWithAccommodations = this.CourseRegistrationWithAccommodations;
				string[] value = courseRegistrationWithAccommodations.CourseOrTemplateAccommodations.ToList<AccommodationDataDTO>().ConvertAll<string>(delegate(AccommodationDataDTO f)
				{
					string str2 = "<li>";
					object value2 = f.Data.Value;
					return str2 + (((value2 != null) ? value2.ToString() : null) ?? "NULL") + "</li>";
				}).ToArray();
				string str = string.Join("", value);
				return "<ul>" + str + "</ul>";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002318 File Offset: 0x00000518
		private DateTime? CourseEndDate
		{
			get
			{
				CourseRegistrationWithAccommodationsDTO courseRegistrationWithAccommodations = this.CourseRegistrationWithAccommodations;
				DateTime? result;
				if (courseRegistrationWithAccommodations == null)
				{
					result = null;
				}
				else
				{
					CourseRegistrationDTO courseReg = courseRegistrationWithAccommodations.CourseReg;
					if (courseReg == null)
					{
						result = null;
					}
					else
					{
						LookupCourseDTO course = courseReg.Course;
						result = ((course != null) ? new DateTime?(course.EndDate) : null);
					}
				}
				return result;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000236C File Offset: 0x0000056C
		public int LuCourseId
		{
			get
			{
				CourseRegistrationWithAccommodationsDTO courseRegistrationWithAccommodations = this.CourseRegistrationWithAccommodations;
				int? num;
				if (courseRegistrationWithAccommodations == null)
				{
					num = null;
				}
				else
				{
					CourseRegistrationDTO courseReg = courseRegistrationWithAccommodations.CourseReg;
					if (courseReg == null)
					{
						num = null;
					}
					else
					{
						LookupCourseDTO course = courseReg.Course;
						num = ((course != null) ? new int?(course.LuCourseId) : null);
					}
				}
				int? num2 = num;
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023C8 File Offset: 0x000005C8
		public string CourseDescription
		{
			get
			{
				CourseRegistrationWithAccommodationsDTO courseRegistrationWithAccommodations = this.CourseRegistrationWithAccommodations;
				object obj;
				if (courseRegistrationWithAccommodations == null)
				{
					obj = null;
				}
				else
				{
					CourseRegistrationDTO courseReg = courseRegistrationWithAccommodations.CourseReg;
					obj = ((courseReg != null) ? courseReg.Course : null);
				}
				bool flag = obj == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					LookupCourseDTO course = courseRegistrationWithAccommodations.CourseReg.Course;
					result = string.Concat(new string[]
					{
						course.Subject.SubjectDescription,
						" ",
						course.Course,
						"<br /><b style='font-size: .85em'>Section ",
						course.Section,
						" ",
						course.TimeOfDay,
						"</b>"
					});
				}
				return result;
			}
		}
	}
}
