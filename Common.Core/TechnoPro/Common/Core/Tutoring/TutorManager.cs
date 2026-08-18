using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.Tutoring;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Tutoring;
using TechnoPro.Common.DAO.Tutoring;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Tutoring
{
	// Token: 0x02000032 RID: 50
	public class TutorManager : ITutorManager, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000A75C File Offset: 0x0000895C
		public TutorManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TutorDAO(this.OpContext);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A77F File Offset: 0x0000897F
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000A787 File Offset: 0x00008987
		public OperationContext OpContext { get; set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A790 File Offset: 0x00008990
		private void UpdateTutorIsActiveCheckbox(int TutorPersonId, int activeCid, bool newIsActive)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			List<DynamicData> data = new List<DynamicData>
			{
				new DynamicData
				{
					Field = dynamicFieldManager.LoadFieldByControlId(activeCid),
					Value = newIsActive
				}
			};
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			dynamicDataManager.SaveData(new DynamicDataContext
			{
				PrimaryId = TutorPersonId
			}, data, eDynamicFormType.PerStudent);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A7FC File Offset: 0x000089FC
		private int GetTutorIsActiveCid()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "uTutorIsActiveCid";
			object obj = cacheStorageManager[key];
			int num = (obj != null && obj is int) ? ((int)obj) : 0;
			bool flag = num < 1;
			if (flag)
			{
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				num = webSettingManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
				bool flag2 = num > 0;
				if (flag2)
				{
					cacheStorageManager.Insert(key, num, TimeSpan.FromHours(3.0));
				}
			}
			return num;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A88C File Offset: 0x00008A8C
		public IList<Tutor> SearchForTutors(int LuCourseId, string SearchString, int MaxResultCount, out bool includingCourses)
		{
			includingCourses = true;
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
			LookupCourse lookupCourse = (LuCourseId > 0) ? lookupCourseManager.LoadCourse(LuCourseId) : null;
			bool flag = lookupCourse != null;
			IList<Tutor> list;
			if (flag)
			{
				string str = (lookupCourse.Subject == null) ? "" : (lookupCourse.Subject.SubjectDescription ?? "");
				string str2 = lookupCourse.Course ?? "";
				string courseSearchString = str + " " + str2;
				list = this.dao.SearchForTutors(courseSearchString, SearchString, settingValue);
				bool flag2 = list == null || list.Count == 0;
				if (flag2)
				{
					includingCourses = false;
					list = this.dao.SearchForTutors(string.Empty, SearchString, settingValue);
				}
			}
			else
			{
				list = this.dao.SearchForTutors(string.Empty, SearchString, settingValue);
			}
			bool flag3 = list == null;
			IList<Tutor> result;
			if (flag3)
			{
				result = new List<Tutor>();
			}
			else
			{
				IList<Tutor> list3;
				if (list.Count >= MaxResultCount)
				{
					IList<Tutor> list2 = list.Take(MaxResultCount).ToList<Tutor>();
					list3 = list2;
				}
				else
				{
					list3 = list;
				}
				result = list3;
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public Tutor LoadTutorByPersonId(int PersonId)
		{
			return this.dao.LoadTutorByPersonId(PersonId);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public AppointmentBookingRes TryToBookTutorAppointment(AppointmentBookingReq BookingRequest, bool BookAppointmentNow = true)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			AppointmentBookingFilterParameters appointmentBookingFilterParameters = new AppointmentBookingFilterParameters
			{
				MaxNumberOfAppointmentsInFuture = webSettingManager.GetSettingValue<int>(Setting.TUTORING_BookingRules_MaxNumberInFuture),
				MaxNumberOfAppointmentsPerWeek = webSettingManager.GetSettingValue<int>(Setting.TUTORING_BookingRules_MaxNumberPerWeek),
				MaxNumberOfAppointmentsPerDay = webSettingManager.GetSettingValue<int>(Setting.TUTORING_BookingRules_MaxNumberPerDay),
				MaxNumberOfNoShows = webSettingManager.GetSettingValue<int>(Setting.TUTORING_BookingRules_MaxNumberConsecutiveNoShowsEndingWithLastAppointment)
			};
			string settingValue;
			bool flag = !string.IsNullOrEmpty(settingValue = webSettingManager.GetSettingValue<string>(Setting.TUTORING_BookingRules_CutoffForSchedulingNewAppointments));
			if (flag)
			{
				appointmentBookingFilterParameters.CutoffTime = settingValue.CutoffTimeFromXml();
			}
			IList<IStudentAppointmentBookingRuleManager> allStudentRuleManagers = StudentAppointmentBookingRuleFactory.GetAllStudentRuleManagers(this.OpContext);
			AppointmentBookingRes appointmentBookingRes = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStudent, BookingRequest, appointmentBookingFilterParameters);
			bool flag2 = !appointmentBookingRes.PassedChecks;
			AppointmentBookingRes result;
			if (flag2)
			{
				result = appointmentBookingRes;
			}
			else
			{
				AppointmentBookingRes appointmentBookingRes2 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, BookingRequest, appointmentBookingFilterParameters);
				bool flag3 = !appointmentBookingRes2.PassedChecks;
				if (flag3)
				{
					result = appointmentBookingRes2;
				}
				else
				{
					AppointmentBookingRes appointmentBookingRes3 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, BookingRequest, appointmentBookingFilterParameters);
					bool flag4 = !appointmentBookingRes3.PassedChecks;
					if (flag4)
					{
						result = appointmentBookingRes3;
					}
					else
					{
						AppointmentBookingRes appointmentBookingRes4 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStaffToBookWith, BookingRequest, appointmentBookingFilterParameters);
						bool flag5 = !appointmentBookingRes4.PassedChecks;
						if (flag5)
						{
							result = appointmentBookingRes4;
						}
						else
						{
							bool flag6 = !BookAppointmentNow;
							if (flag6)
							{
								result = new AppointmentBookingRes
								{
									PassedChecks = true
								};
							}
							else
							{
								PersonBase personBase = new PersonBase
								{
									PersonId = BookingRequest.StudentPersonId
								};
								Appointment appointment = new Appointment
								{
									Attendees = new List<Attendee>
									{
										new Attendee
										{
											Person = personBase
										},
										new Attendee
										{
											Person = new PersonBase
											{
												PersonId = BookingRequest.StaffPersonId
											}
										}
									},
									StartDateTime = BookingRequest.StartDateTime,
									EndDateTime = BookingRequest.EndDateTime,
									AppType = new AppType
									{
										AppTypeId = BookingRequest.AppTypeId
									},
									SubTitle = BookingRequest.Subject,
									Location = BookingRequest.Location,
									Memo = BookingRequest.MemoRtf,
									WhoBooked = personBase
								};
								IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
								int num;
								if (!appointmentBookingFilterParameters.AllowDoubleBookingStaff || !appointmentBookingFilterParameters.AllowDoubleBookingStudent)
								{
									num = appointmentManager.CreateAppointmentEnsureUsersNotDoubleBooked(false, appointment, (from h in new int[]
									{
										appointmentBookingFilterParameters.AllowDoubleBookingStudent ? 0 : BookingRequest.StudentPersonId,
										appointmentBookingFilterParameters.AllowDoubleBookingStaff ? 0 : BookingRequest.StaffPersonId
									}
									where h > 0
									select h).ToArray<int>());
								}
								else
								{
									num = appointmentManager.CreateAppointment(false, appointment);
								}
								int num2 = num;
								bool flag7 = num2 > 0;
								if (flag7)
								{
									result = new AppointmentBookingRes
									{
										PassedChecks = true,
										AppointmentId = num2
									};
								}
								else
								{
									result = new AppointmentBookingRes
									{
										PassedChecks = false,
										PublicMessage = "Unable to book this appointment.  Please try again or contact us for assistance.",
										PrivateMessage = "Failed to book appointment"
									};
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		public void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId)
		{
			IConfidentialityFormSignedManager confidentialityFormSignedManager = new ConfidentialityFormSignedManager(this.OpContext);
			confidentialityFormSignedManager.RecordConfidentialityAgreementSignedByTutor(TutorPersonId, "TutorConfidentialityAgreementSigned", "Tutor signed confid. agreement");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public bool IsConfidentialityAgreementSigningRequiredForTutor(int TutorPersonId)
		{
			IConfidentialityFormSignedManager confidentialityFormSignedManager = new ConfidentialityFormSignedManager(this.OpContext);
			return confidentialityFormSignedManager.IsConfidentialityAgreementSigningRequired(TutorPersonId, Setting.TUTORING_TutorConfidentialityResignPolicy, "TutorConfidentialityAgreementSigned", "Tutor signed confid. agreement");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AD28 File Offset: 0x00008F28
		public eTutorStatus GetTutorStatus(int TutorPersonId)
		{
			bool flag = TutorPersonId < 1;
			eTutorStatus result;
			if (flag)
			{
				result = eTutorStatus.NotATutor;
			}
			else
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				IList<TechnoPro.Common.Public.Entities.People.Group> list = peopleManager.LoadUserGroupMemberships(TutorPersonId);
				TechnoPro.Common.Public.Entities.People.Group group;
				if (list == null)
				{
					group = null;
				}
				else
				{
					group = list.FirstOrDefault((TechnoPro.Common.Public.Entities.People.Group g) => g.GroupId == 5);
				}
				TechnoPro.Common.Public.Entities.People.Group group2 = group;
				bool flag2 = group2 == null;
				if (flag2)
				{
					result = eTutorStatus.NotATutor;
				}
				else
				{
					IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
					int settingValue = webSettingManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
					bool flag3 = settingValue < 1;
					if (flag3)
					{
						result = eTutorStatus.TutorActive;
					}
					else
					{
						IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
						DynamicDataContext context = new DynamicDataContext
						{
							PrimaryId = TutorPersonId
						};
						List<DynamicData> list2 = dynamicDataManager.LoadDataByFields(context, new List<int>
						{
							settingValue
						}, eDynamicFormType.PerStudent);
						bool flag4 = list2 == null || list2.Count < 1 || list2[0].Value == null || !(list2[0].Value is bool) || !(bool)list2[0].Value;
						if (flag4)
						{
							result = eTutorStatus.TutorNotActive;
						}
						else
						{
							result = (this.IsConfidentialityAgreementSigningRequiredForTutor(TutorPersonId) ? eTutorStatus.TutorActiveNeedsConfidentiality : eTutorStatus.TutorActive);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AE6C File Offset: 0x0000906C
		public IDictionary<int, eTutorStatus> GetTutorStatuses(int[] tutorPersonIds)
		{
			int[] array;
			if (tutorPersonIds == null)
			{
				array = null;
			}
			else
			{
				array = (from g in tutorPersonIds
				where g > 0
				select g).Distinct<int>().ToArray<int>();
			}
			int[] array2 = array ?? new int[0];
			bool flag = array2.Length < 1;
			IDictionary<int, eTutorStatus> result;
			if (flag)
			{
				result = new Dictionary<int, eTutorStatus>();
			}
			else
			{
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				int settingValue = webSettingManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
				bool flag2 = settingValue < 1;
				if (flag2)
				{
					result = array2.ToDictionary((int g) => g, (int g) => eTutorStatus.TutorNotActive);
				}
				else
				{
					IConfidentialityFormSignedManager confidentialityFormSignedManager = new ConfidentialityFormSignedManager(this.OpContext);
					DynamicField lastSignedConfidentialityAgreementField = confidentialityFormSignedManager.GetLastSignedConfidentialityAgreementField("TutorConfidentialityAgreementSigned", "Tutor signed confid. agreement");
					int tutorConfidentialityAgreementSignedCid = (lastSignedConfidentialityAgreementField != null) ? lastSignedConfidentialityAgreementField.ControlId : 0;
					IList<TutorInfo> list = this.dao.LoadTutorInfos(array2, settingValue, tutorConfidentialityAgreementSignedCid);
					Range<DateTime> confidentialityResignDateRange = confidentialityFormSignedManager.GetConfidentialityResignDateRange(Setting.TUTORING_TutorConfidentialityResignPolicy);
					Dictionary<int, eTutorStatus> dictionary = new Dictionary<int, eTutorStatus>();
					foreach (TutorInfo tutorInfo in list)
					{
						int tutorId = tutorInfo.TutorId;
						bool flag3 = dictionary.ContainsKey(tutorId);
						if (!flag3)
						{
							bool flag4 = tutorInfo.IsAuthorized == null || !tutorInfo.IsAuthorized.Value;
							if (flag4)
							{
								dictionary.Add(tutorId, eTutorStatus.TutorNotActive);
							}
							else
							{
								bool flag5 = confidentialityResignDateRange != null && (tutorInfo.ConfidentialitySignedDate == null || tutorInfo.ConfidentialitySignedDate.Value < confidentialityResignDateRange.Start);
								if (flag5)
								{
									dictionary.Add(tutorId, eTutorStatus.TutorActiveNeedsConfidentiality);
								}
								else
								{
									dictionary.Add(tutorId, eTutorStatus.TutorActive);
								}
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B094 File Offset: 0x00009294
		public int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber)
		{
			bool flag = string.IsNullOrEmpty(StudentNumber);
			if (flag)
			{
				throw new NullParameterException("Common.Core.Tutoring.TutorManager.RegisterTutor:Missing student number");
			}
			bool flag2 = string.IsNullOrEmpty(LastName);
			if (flag2)
			{
				throw new NullParameterException("Common.Core.Tutoring.TutorManager.RegisterTutor:Missing last name");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPersonByStudentNumber(StudentNumber);
			bool flag3 = personBase != null;
			if (flag3)
			{
				throw new NullParameterException("Common.Core.Tutoring.TutorManager.RegisterTutor:Already a student in ClockWork with this student number:snum=" + StudentNumber + ":pid=" + personBase.PersonId.ToString());
			}
			PersonBase user = new PersonBase
			{
				FirstName = (FirstName ?? ""),
				MiddleName = (MiddleName ?? ""),
				LastName = (LastName ?? ""),
				Student_no = StudentNumber,
				CoreGroup = eCoreGroup.Tutors,
				Groups = new List<TechnoPro.Common.Public.Entities.People.Group>
				{
					new TechnoPro.Common.Public.Entities.People.Group
					{
						GroupId = 5
					}
				},
				IsActivated = new bool?(true)
			};
			return peopleManager.CreateUser(user, new List<int>
			{
				5
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public void RegisterTutorByExistingPersonId(int PersonId)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			peopleManager.AddUserToGroups(PersonId, new List<int>
			{
				5
			});
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public IList<TutorWithActiveStatus> LoadAllTutors()
		{
			int tutorIsActiveCid = this.GetTutorIsActiveCid();
			List<TutorWithActiveStatus> list = this.dao.LoadAllTutors(tutorIsActiveCid).ToList<TutorWithActiveStatus>();
			list.Sort(delegate(TutorWithActiveStatus g1, TutorWithActiveStatus g2)
			{
				int num = (g1.LastName ?? "").CompareTo(g2.LastName ?? "");
				bool flag = num != 0;
				int result;
				if (flag)
				{
					result = num;
				}
				else
				{
					num = (g1.FirstName ?? "").CompareTo(g2.FirstName ?? "");
					bool flag2 = num != 0;
					if (flag2)
					{
						result = num;
					}
					else
					{
						result = (g1.MiddleName ?? "").CompareTo(g2.MiddleName ?? "");
					}
				}
				return result;
			});
			return list;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B22C File Offset: 0x0000942C
		public void ActivateTutor(int TutorPersonId)
		{
			int tutorIsActiveCid = this.GetTutorIsActiveCid();
			bool flag = tutorIsActiveCid < 1;
			if (flag)
			{
				CWLogger.Logger.Error("Common.Core.Tutoring.TutorManager.ActivateTutor.ActiveCidIsMissing");
			}
			else
			{
				this.UpdateTutorIsActiveCheckbox(TutorPersonId, tutorIsActiveCid, true);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B268 File Offset: 0x00009468
		public void DeActivateTutor(int TutorPersonId)
		{
			int tutorIsActiveCid = this.GetTutorIsActiveCid();
			bool flag = tutorIsActiveCid < 1;
			if (flag)
			{
				CWLogger.Logger.Error("Common.Core.Tutoring.TutorManager.DeActivateTutor.ActiveCidIsMissing");
			}
			else
			{
				this.UpdateTutorIsActiveCheckbox(TutorPersonId, tutorIsActiveCid, false);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public TutorAppointment LoadTutorAppointment(int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			return baseAppointmentManager.LoadBaseExtendedAppointmentById<TutorAppointment>(AppointmentId);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B2CC File Offset: 0x000094CC
		public TutorWithActiveStatus LoadTutorWithActiveStatusById(int TutorPersonId)
		{
			Tutor tutor = this.LoadTutorByPersonId(TutorPersonId);
			bool flag = tutor == null;
			TutorWithActiveStatus result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TutorWithActiveStatus tutorWithActiveStatus = tutor.ToTutorWithActiveStatus();
				int tutorIsActiveCid = this.GetTutorIsActiveCid();
				bool flag2 = tutorIsActiveCid < 1;
				if (flag2)
				{
					result = tutorWithActiveStatus;
				}
				else
				{
					tutorWithActiveStatus.Status = this.GetTutorStatus(TutorPersonId);
					result = tutorWithActiveStatus;
				}
			}
			return result;
		}

		// Token: 0x04000063 RID: 99
		private ITutorDAO dao;

		// Token: 0x04000065 RID: 101
		private const string TutorConfidentialityControlName = "TutorConfidentialityAgreementSigned";

		// Token: 0x04000066 RID: 102
		private const string TutorConfidentialityControlCaption = "Tutor signed confid. agreement";
	}
}
