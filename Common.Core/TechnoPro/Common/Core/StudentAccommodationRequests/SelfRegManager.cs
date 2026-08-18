using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.StudentAccommodationRequests;
using TechnoPro.Common.DAO.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegEmail;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.Core.StudentAccommodationRequests
{
	// Token: 0x0200003D RID: 61
	public class SelfRegManager : ISelfRegManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000CD55 File Offset: 0x0000AF55
		public SelfRegManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000CD67 File Offset: 0x0000AF67
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000270 RID: 624 RVA: 0x0000CD78 File Offset: 0x0000AF78
		private static IDictionary<int, LookupInstructor> LoadInstructors(OperationContext opContext, IList<int> lucids)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(opContext);
			IList<LookupCourse> list = lookupCourseManager.LoadCoursesByIds(lucids);
			Dictionary<int, LookupInstructor> dictionary = new Dictionary<int, LookupInstructor>();
			foreach (LookupCourse lookupCourse in list)
			{
				int num = (lookupCourse != null) ? lookupCourse.LuCourseId : 0;
				bool flag = num < 1;
				if (!flag)
				{
					LookupInstructor lookupInstructor = (lookupCourse != null) ? lookupCourse.GetPrimaryInstructor() : null;
					bool flag2 = !string.IsNullOrEmpty((lookupInstructor != null) ? lookupInstructor.Email : null) && !dictionary.ContainsKey(num);
					if (flag2)
					{
						dictionary.Add(num, lookupInstructor);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CE3C File Offset: 0x0000B03C
		private static SelfRegManager.AccommodationsInfo ProcessAccommodations(IWebSettingManager sm, OperationContext opContext, int pid, List<SelfRegCheckedAccommodation> checkedAccommodations, IList<AccommodationData> hidingAccommodations)
		{
			List<StudentCourseAccommodationModificationRequestItem> list = new List<StudentCourseAccommodationModificationRequestItem>();
			bool hasAtLeastOneSpecialAccommodation = false;
			string text = (sm.GetSettingValue<string>(Setting.SELFREGC_SpecialAccommodationControlIds) ?? "").Trim();
			List<int> list2 = (from n in (from g in text.Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h).Select(delegate(string m)
			{
				int num;
				return int.TryParse(m, out num) ? num : 0;
			})
			where n > 0
			select n).Distinct<int>().ToList<int>();
			List<string> list3 = new List<string>();
			List<int> cidsToSkip = new List<int>();
			bool flag = hidingAccommodations != null && hidingAccommodations.Count > 0;
			if (flag)
			{
				bool settingValue = sm.GetSettingValue<bool>(Setting.SELFREGC_HiddenControlIds_AutoApproveHiddenAccommodations);
				bool flag2 = settingValue;
				if (flag2)
				{
					checkedAccommodations.AddRange(from g in hidingAccommodations
					select new SelfRegCheckedAccommodation
					{
						ControlId = g.Data.Field.ControlId,
						Text = SelfRegManager.GetDisplayString(g),
						IsChecked = true
					});
				}
				else
				{
					cidsToSkip = (from g in hidingAccommodations
					select g.Data.Field.ControlId).ToList<int>();
				}
			}
			foreach (SelfRegCheckedAccommodation selfRegCheckedAccommodation in checkedAccommodations)
			{
				int controlId = selfRegCheckedAccommodation.ControlId;
				bool flag3 = !selfRegCheckedAccommodation.IsChecked;
				if (flag3)
				{
					list.Add(new StudentCourseAccommodationModificationRequestItem
					{
						DateEntered = DateTime.Now,
						ModificationType = eStudentCourseAccommodationModificationType.Remove,
						Note1 = "",
						Note2 = "",
						Status = eStudentCourseAccommodationRequestStatus.Approved,
						RequestedAccommodationData = new DynamicData
						{
							Field = new DynamicField
							{
								ControlId = controlId
							}
						},
						WhoEntered = new PersonBase
						{
							PersonId = pid
						}
					});
				}
				else
				{
					bool flag4 = list2.Contains(controlId);
					if (flag4)
					{
						hasAtLeastOneSpecialAccommodation = true;
						list3.Add("* " + selfRegCheckedAccommodation.Text);
					}
					else
					{
						list3.Add(selfRegCheckedAccommodation.Text);
					}
				}
			}
			return new SelfRegManager.AccommodationsInfo
			{
				AccommodationModificationRequests = list,
				CidsToSkip = cidsToSkip,
				HasAtLeastOneSpecialAccommodation = hasAtLeastOneSpecialAccommodation,
				SelectedAccommodations = list3
			};
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public AllowedStudentCourseRegistrationsForCustomEmailLogic GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(string studentPersonIdHash, string plainTextStudentPersonIdHash)
		{
			return this.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(this.GetStudentPersonIdFromHash(studentPersonIdHash, plainTextStudentPersonIdHash));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D114 File Offset: 0x0000B314
		private static string GenerateNonce()
		{
			byte[] array = new byte[16];
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(array);
			return array.ToMd5Hash();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D144 File Offset: 0x0000B344
		private static string GetStudentPersonIdHash(int pid, out string hashPlainText)
		{
			string text = SelfRegManager.GenerateNonce();
			string text2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
			string text3 = string.Concat(new string[]
			{
				pid.ToString(),
				",",
				text2,
				",",
				text
			});
			hashPlainText = text3;
			string password = text3 + "," + "u(<A;l@qfdqp{@x$";
			IHashingProvider hashingProvider = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault);
			return hashingProvider.CreateHash(password, null);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		private int GetStudentPersonIdFromHash(string pidHash, string plainTextHash)
		{
			string password = (plainTextHash ?? "") + "," + "u(<A;l@qfdqp{@x$";
			IHashingProvider hashingProvider = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault);
			bool flag = !hashingProvider.ValidatePassword(password, pidHash, null);
			int result;
			if (flag)
			{
				this.LogFailedStudentPersonIdHashValidate(pidHash, plainTextHash, "Failed Validation");
				result = 0;
			}
			else
			{
				List<string> list = (from g in plainTextHash.Split(new char[]
				{
					','
				})
				select g.Trim()).ToList<string>();
				bool flag2 = list.Count < 2;
				if (flag2)
				{
					this.LogFailedStudentPersonIdHashValidate(pidHash, plainTextHash, "Failed plainTextHash subitem count");
					result = 0;
				}
				else
				{
					string s = list[0];
					string s2 = list[1];
					DateTime d;
					bool flag3 = !DateTime.TryParse(s2, out d) || (DateTime.Now - d).TotalDays > 120.0;
					if (flag3)
					{
						this.LogFailedStudentPersonIdHashValidate(pidHash, plainTextHash, "Failed date check");
						result = 0;
					}
					else
					{
						int num;
						int.TryParse(s, out num);
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		private void LogFailedStudentPersonIdHashValidate(string pidHash, string plainTextHash, string msg)
		{
			CWLogger.Logger.Warn("SelfRegManager:FailedStudentPersonIdHashValidate:msg={0}:pidHash={1}:plainText={2}", msg ?? "", pidHash ?? "", plainTextHash ?? "");
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D31C File Offset: 0x0000B51C
		public SelfRegEmailLogicRule FindLogicRuleThatApplies(int studentPersonId, int luCourseId)
		{
			int whoAmI = this.OpContext.WhoAmI;
			bool flag = whoAmI < 1;
			SelfRegEmailLogicRule result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				string settingValue = webSettingManager.GetSettingValue<string>(Setting.SELFREGC_LogicEmailsRules);
				List<SelfRegEmailLogicRule> list = (from g in settingValue.XmlToSelfRegEmailLogicRules() ?? new SelfRegEmailLogicRule[0]
				where !g.IsDisabled
				select g).ToList<SelfRegEmailLogicRule>();
				bool flag2 = list.Count < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
					IList<int> whoAmIGids = peopleGroupManager.GetGroupIdsByPersonId(whoAmI);
					list = (from g in list
					where g.AuthorizedGroupId > 0 && whoAmIGids.Contains(g.AuthorizedGroupId)
					select g).ToList<SelfRegEmailLogicRule>();
					bool flag3 = list.Count < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
						CourseRegistration courseRegistration = courseRegistrationManager.LoadCourseRegistrationsByStudentAndCourse(studentPersonId, luCourseId);
						bool flag4 = courseRegistration == null || courseRegistration.RegistrationStatus == eRegistrationStatus.Dropped;
						if (flag4)
						{
							result = null;
						}
						else
						{
							bool flag5 = courseRegistration.Course.EndDate < DateTime.Now;
							if (flag5)
							{
								result = null;
							}
							else
							{
								SelfRegManager.LogicEmailsTemporaryDataCache tempCache = new SelfRegManager.LogicEmailsTemporaryDataCache();
								IList<SelfRegEmailLogicRule> source = SelfRegManager.FindRulesThatMatch(this.OpContext, list.ToArray(), tempCache, studentPersonId, luCourseId, null);
								result = source.FirstOrDefault<SelfRegEmailLogicRule>();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D490 File Offset: 0x0000B690
		public AllowedStudentCourseRegistrationsForCustomEmailLogic GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(int studentPersonId)
		{
			int whoAmI = this.OpContext.WhoAmI;
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			AllowedStudentCourseRegistrationsForCustomEmailLogic allowedStudentCourseRegistrationsForCustomEmailLogic = new AllowedStudentCourseRegistrationsForCustomEmailLogic
			{
				AuthorizedUserPersonId = whoAmI,
				CourseRegistrations = new List<CourseRegistration>(),
				Student = peopleManager.LoadPerson(studentPersonId)
			};
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			string settingValue = webSettingManager.GetSettingValue<string>(Setting.SELFREGC_LogicEmailsRules);
			List<SelfRegEmailLogicRule> emailLogicRules = (from g in settingValue.XmlToSelfRegEmailLogicRules() ?? new SelfRegEmailLogicRule[0]
			where !g.IsDisabled
			select g).ToList<SelfRegEmailLogicRule>();
			bool flag = emailLogicRules.Count < 1;
			AllowedStudentCourseRegistrationsForCustomEmailLogic result;
			if (flag)
			{
				result = allowedStudentCourseRegistrationsForCustomEmailLogic;
			}
			else
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				IList<int> whoAmIGids = peopleGroupManager.GetGroupIdsByPersonId(whoAmI);
				emailLogicRules = (from g in emailLogicRules
				where g.AuthorizedGroupId > 0 && whoAmIGids.Contains(g.AuthorizedGroupId)
				select g).ToList<SelfRegEmailLogicRule>();
				bool flag2 = emailLogicRules.Count < 1;
				if (flag2)
				{
					result = allowedStudentCourseRegistrationsForCustomEmailLogic;
				}
				else
				{
					IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(this.OpContext);
					List<CourseRegistration> list;
					if (studentPersonId <= 0)
					{
						list = new List<CourseRegistration>();
					}
					else
					{
						list = (from h in studentAccommodationRequestManager.LoadCourseRegistrationsWithRequestByStudentAndDate(studentPersonId, DateTime.Now, DateTime.Now, false).Where(delegate(CourseRegistrationWithAccommodationRequest g)
						{
							StudentCourseAccommodationRequest accommodationRequest = g.AccommodationRequest;
							return accommodationRequest != null && accommodationRequest.Status == eStudentCourseAccommodationRequestStatus.Approved;
						})
						select h.CourseRegistrationWithAccommodations.CourseReg).ToList<CourseRegistration>();
					}
					List<CourseRegistration> list2 = list;
					bool flag3 = list2.Count < 1;
					if (flag3)
					{
						result = allowedStudentCourseRegistrationsForCustomEmailLogic;
					}
					else
					{
						SelfRegManager.LogicEmailsTemporaryDataCache logicRulesTempDataCache = new SelfRegManager.LogicEmailsTemporaryDataCache();
						List<CourseRegistration> courseRegistrations = (from course in list2
						let lucid = course.Course.LuCourseId
						let matchingRules = SelfRegManager.FindRulesThatMatch(this.OpContext, emailLogicRules.ToArray(), logicRulesTempDataCache, studentPersonId, lucid, null)
						where matchingRules.Count > 0
						select course).ToList<CourseRegistration>();
						allowedStudentCourseRegistrationsForCustomEmailLogic.CourseRegistrations = courseRegistrations;
						result = allowedStudentCourseRegistrationsForCustomEmailLogic;
					}
				}
			}
			return result;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D70C File Offset: 0x0000B90C
		public IList<SelfRegSendLetterToPreviouslyMissingInstructorRes> SendLetterAndMarkRequestApprovedForPreviouslyMissingInstructorRequests()
		{
			IStudentAccommodationRequestManager studentAccommodationRequestManager = new StudentAccommodationRequestManager(this.OpContext);
			IList<StudentCourseAccommodationRequest> list = studentAccommodationRequestManager.LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(eStudentCourseAccommodationRequestStatus.InstructorInfoMissing);
			bool flag = list == null || list.Any<StudentCourseAccommodationRequest>();
			IList<SelfRegSendLetterToPreviouslyMissingInstructorRes> result;
			if (flag)
			{
				result = new List<SelfRegSendLetterToPreviouslyMissingInstructorRes>();
			}
			else
			{
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				string text = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_WebBaseUrl, false).Trim();
				bool flag2 = text.EndsWith("/");
				if (flag2)
				{
					text = text.Substring(0, text.Length - 1);
				}
				ISelfRegDAO selfRegDAO = new SelfRegDAO(this.OpContext);
				List<SelfRegSendLetterToPreviouslyMissingInstructorRes> list2 = new List<SelfRegSendLetterToPreviouslyMissingInstructorRes>();
				foreach (StudentCourseAccommodationRequest studentCourseAccommodationRequest in list)
				{
					try
					{
						int num;
						if (studentCourseAccommodationRequest.LuCourseId <= 0)
						{
							LookupCourseBaseWithPrimaryInstructor courseBase = studentCourseAccommodationRequest.CourseBase;
							num = ((courseBase != null) ? courseBase.LuCourseId : 0);
						}
						else
						{
							num = studentCourseAccommodationRequest.LuCourseId;
						}
						int num2 = num;
						Pair<string, string> personIdAndLuCourseIdAsLongtermUrlStrings = selfRegDAO.GetPersonIdAndLuCourseIdAsLongtermUrlStrings(studentCourseAccommodationRequest.Student.PersonId, num2);
						IWebSettingManager sm = webSettingManager;
						OperationContext opContext = this.OpContext;
						PersonBase student = studentCourseAccommodationRequest.Student;
						int luCourseId = num2;
						LookupCourseBaseWithPrimaryInstructor courseBase2 = studentCourseAccommodationRequest.CourseBase;
						TryToSendInstructorEmailResult tryToSendInstructorEmailResult = SelfRegManager.TryToSendInstructorEmail(sm, opContext, student, luCourseId, (courseBase2 != null) ? courseBase2.PrimaryInstructor : null, eStudentCourseAccommodationRequestStatus.Approved, studentCourseAccommodationRequest.CourseBase.GetCourseDescription(), text, personIdAndLuCourseIdAsLongtermUrlStrings.Item1, personIdAndLuCourseIdAsLongtermUrlStrings.Item2, null);
						bool flag3 = tryToSendInstructorEmailResult.Status == eTryToSendInstructorEmailStatus.Success;
						if (flag3)
						{
							studentAccommodationRequestManager.UpdateRequestStatus(studentCourseAccommodationRequest.StudentCourseAccommodationRequestId, eStudentCourseAccommodationRequestStatus.Approved);
						}
						list2.Add(new SelfRegSendLetterToPreviouslyMissingInstructorRes
						{
							Request = studentCourseAccommodationRequest,
							EmailResult = tryToSendInstructorEmailResult
						});
					}
					catch (Exception ex)
					{
						list2.Add(new SelfRegSendLetterToPreviouslyMissingInstructorRes
						{
							Request = studentCourseAccommodationRequest,
							EmailResult = new TryToSendInstructorEmailResult
							{
								Status = eTryToSendInstructorEmailStatus.FailedUnspecified,
								ErrorMessage = "Failed: " + ex.ToString()
							}
						});
					}
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000D944 File Offset: 0x0000BB44
		public void ProcessSelfRegRequest(int pid, eSelfRegCoursesAccommodationsStatus accChange, IList<SelfRegCourseInfo> selectedLucids, List<SelfRegCheckedAccommodation> checkedAccommodations, IList<AccommodationData> hidingAccommodations, string noteFromStudent, string baseUrl, string pidEncodedForUrl, string ipAddressForLogging)
		{
			SelfRegManager.<>c__DisplayClass20_0 CS$<>8__locals1 = new SelfRegManager.<>c__DisplayClass20_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.ipAddressForLogging = ipAddressForLogging;
			CS$<>8__locals1.sm = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			SelfRegManager.<>c__DisplayClass20_0 CS$<>8__locals2 = CS$<>8__locals1;
			PersonBase student;
			if ((student = peopleManager.LoadPerson(pid)) == null)
			{
				(student = new PersonBase()).PersonId = pid;
			}
			CS$<>8__locals2.student = student;
			IDictionary<int, LookupInstructor> profs = SelfRegManager.LoadInstructors(this.OpContext, (from g in selectedLucids
			select g.LuCourseId).ToList<int>());
			CS$<>8__locals1.accommodationInfo = SelfRegManager.ProcessAccommodations(CS$<>8__locals1.sm, this.OpContext, pid, checkedAccommodations, hidingAccommodations);
			string settingValue = CS$<>8__locals1.sm.GetSettingValue<string>(Setting.SELFREGC_LogicEmailsRules);
			SelfRegEmailLogicRule[] array = (from g in settingValue.XmlToSelfRegEmailLogicRules() ?? new SelfRegEmailLogicRule[0]
			where !g.IsDisabled
			select g).ToArray<SelfRegEmailLogicRule>();
			bool settingValue2 = CS$<>8__locals1.sm.GetSettingValue<bool>(Setting.SELFREGC_NeverApprove);
			ISelfRegDAO dao = new SelfRegDAO(this.OpContext);
			IStudentAccommodationRequestManager studentAccommodationRequestMan = new StudentAccommodationRequestManager(this.OpContext);
			CS$<>8__locals1.jobs = new List<SelfRegManager.SelfRegSingleCourseJob>();
			foreach (SelfRegCourseInfo selfRegCourseInfo in selectedLucids)
			{
				SelfRegManager.ProcessSingleSelfRegRequestCourseParameters processSingleSelfRegRequestCourseParameters = new SelfRegManager.ProcessSingleSelfRegRequestCourseParameters
				{
					Dao = dao,
					StudentAccommodationRequestMan = studentAccommodationRequestMan,
					WebSettingMan = CS$<>8__locals1.sm,
					OpContext = this.OpContext,
					Student = CS$<>8__locals1.student,
					LuCourseId = selfRegCourseInfo.LuCourseId,
					CourseDescription = selfRegCourseInfo.CourseDescription,
					AccChange = accChange,
					NoteFromStudent = noteFromStudent,
					NeverApprove = settingValue2,
					Profs = profs,
					HasAtLeastOneSpecialAccommodation = CS$<>8__locals1.accommodationInfo.HasAtLeastOneSpecialAccommodation,
					AccommodationModificationRequests = CS$<>8__locals1.accommodationInfo.AccommodationModificationRequests,
					CidsToSkip = CS$<>8__locals1.accommodationInfo.CidsToSkip,
					BaseUrl = baseUrl,
					PidEncodedForUrl = pidEncodedForUrl,
					LucidEncodedForUrl = selfRegCourseInfo.EncodedLucidForUrl,
					IpAddressForLogging = CS$<>8__locals1.ipAddressForLogging,
					EmailLogicRules = array
				};
				StudentCourseAccommodationRequest request = new StudentCourseAccommodationRequest
				{
					LuCourseId = processSingleSelfRegRequestCourseParameters.LuCourseId,
					WhoEntered = new PersonBase
					{
						PersonId = pid
					},
					AccommodationModificationRequests = new List<StudentCourseAccommodationModificationRequestItem>(),
					DateEntered = DateTime.Now,
					DateRequested = new DateTime?(DateTime.Now),
					Student = new PersonBase
					{
						PersonId = pid
					},
					Note1 = processSingleSelfRegRequestCourseParameters.NoteFromStudent,
					Note2 = ""
				};
				SelfRegManager.SelfRegSingleCourseJob item = new SelfRegManager.SelfRegSingleCourseJob
				{
					Request = request,
					RequestParameters = processSingleSelfRegRequestCourseParameters
				};
				CS$<>8__locals1.jobs.Add(item);
			}
			DateTime? dateTime = null;
			bool settingValue3 = CS$<>8__locals1.sm.GetSettingValue<bool>(Setting.SELFREGC_DontAllowStudentsToCompleteSelfRegForCoursesStartingAfterAccommodationsExpiryDate);
			bool flag = settingValue3;
			if (flag)
			{
				IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
				dateTime = accommodationsManager.GetStudentAccommodationsExpiryDate(pid);
				ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
				IList<LookupCourseBase> source = lookupCourseManager.LoadCourseBasesByIds((from g in CS$<>8__locals1.jobs
				select g.Request.LuCourseId).ToArray<int>());
				using (List<SelfRegManager.SelfRegSingleCourseJob>.Enumerator enumerator2 = CS$<>8__locals1.jobs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SelfRegManager.SelfRegSingleCourseJob job = enumerator2.Current;
						LookupCourseBase lookupCourseBase = source.FirstOrDefault((LookupCourseBase g) => g.LuCourseId == job.Request.LuCourseId);
						bool flag2 = lookupCourseBase == null;
						if (!flag2)
						{
							job.Request.CourseBase = new LookupCourseBaseWithPrimaryInstructor(lookupCourseBase);
						}
					}
				}
			}
			SelfRegManager.LogicEmailsTemporaryDataCache tempCache = new SelfRegManager.LogicEmailsTemporaryDataCache();
			foreach (SelfRegManager.SelfRegSingleCourseJob selfRegSingleCourseJob in CS$<>8__locals1.jobs)
			{
				SelfRegManager.ProcessSingleSelfRegRequestCourseParameters requestParameters = selfRegSingleCourseJob.RequestParameters;
				bool flag3 = dateTime != null && (selfRegSingleCourseJob.Request.CourseBase == null || selfRegSingleCourseJob.Request.CourseBase.StartDate >= dateTime.Value);
				if (flag3)
				{
					selfRegSingleCourseJob.Action = SelfRegManager.eSelfRegJobAction.Abort;
					selfRegSingleCourseJob.DoSelfReg = false;
				}
				else if (accChange != eSelfRegCoursesAccommodationsStatus.MyAccommodationsAreCorrectTheWayTheyAre)
				{
					if (accChange - eSelfRegCoursesAccommodationsStatus.INeedAdditionalAccommodations > 1)
					{
						selfRegSingleCourseJob.Action = SelfRegManager.eSelfRegJobAction.Abort;
						selfRegSingleCourseJob.DoSelfReg = false;
					}
					else
					{
						selfRegSingleCourseJob.DoSelfReg = true;
						selfRegSingleCourseJob.Action = SelfRegManager.eSelfRegJobAction.MarkBackToStaff;
					}
				}
				else
				{
					selfRegSingleCourseJob.DoSelfReg = true;
					bool neverApprove = requestParameters.NeverApprove;
					if (neverApprove)
					{
						selfRegSingleCourseJob.Action = SelfRegManager.eSelfRegJobAction.MarkBackToStaff;
					}
					else
					{
						List<AccommodationData> accData = (from h in checkedAccommodations
						where h.IsChecked
						select h into g
						select new AccommodationData
						{
							Data = new DynamicData
							{
								Field = new DynamicField
								{
									ControlId = g.ControlId
								},
								Value = g.Text
							}
						}).ToList<AccommodationData>();
						selfRegSingleCourseJob.MatchingEmailLogicRules = SelfRegManager.FindRulesThatMatch(this.OpContext, array, tempCache, CS$<>8__locals1.student.PersonId, selfRegSingleCourseJob.RequestParameters.LuCourseId, accData);
						bool flag4 = selfRegSingleCourseJob.MatchingEmailLogicRules.Any((SelfRegEmailLogicRule g) => g.CancelProfEmail);
						bool flag5 = flag4;
						if (flag5)
						{
							selfRegSingleCourseJob.Action = SelfRegManager.eSelfRegJobAction.Approve;
						}
						else
						{
							selfRegSingleCourseJob.Prof = (requestParameters.Profs.ContainsKey(requestParameters.LuCourseId) ? requestParameters.Profs[requestParameters.LuCourseId] : null);
							LookupInstructor prof = selfRegSingleCourseJob.Prof;
							string text = (((prof != null) ? prof.Email : null) ?? "").Trim();
							selfRegSingleCourseJob.Action = ((text.Length > 0) ? SelfRegManager.eSelfRegJobAction.Approve : SelfRegManager.eSelfRegJobAction.MarkMissingProf);
							bool flag6 = selfRegSingleCourseJob.Action == SelfRegManager.eSelfRegJobAction.Approve;
							if (flag6)
							{
								selfRegSingleCourseJob.SendProfEmail = true;
							}
						}
					}
				}
			}
			CS$<>8__locals1.atLeastOneApprovedWithSpecialAccommodations = CS$<>8__locals1.jobs.Any((SelfRegManager.SelfRegSingleCourseJob g) => g.DoSelfReg && g.RequestParameters.HasAtLeastOneSpecialAccommodation);
			SelfRegManager.<>c__DisplayClass20_0 CS$<>8__locals4 = CS$<>8__locals1;
			bool atLeastOneNotApprovedOrHasNote;
			if ((noteFromStudent ?? "").Trim().Length <= 0)
			{
				atLeastOneNotApprovedOrHasNote = CS$<>8__locals1.jobs.Any((SelfRegManager.SelfRegSingleCourseJob g) => g.Action != SelfRegManager.eSelfRegJobAction.Approve);
			}
			else
			{
				atLeastOneNotApprovedOrHasNote = true;
			}
			CS$<>8__locals4.atLeastOneNotApprovedOrHasNote = atLeastOneNotApprovedOrHasNote;
			CS$<>8__locals1.requestEmailInfos = new List<SelfRegManager.RequestEmailInfo>();
			foreach (SelfRegManager.SelfRegSingleCourseJob selfRegSingleCourseJob2 in CS$<>8__locals1.jobs)
			{
				bool flag7 = !selfRegSingleCourseJob2.DoSelfReg;
				if (!flag7)
				{
					StudentCourseAccommodationRequest request2 = selfRegSingleCourseJob2.Request;
					SelfRegManager.ProcessSingleSelfRegRequestCourseParameters requestParameters2 = selfRegSingleCourseJob2.RequestParameters;
					switch (selfRegSingleCourseJob2.Action)
					{
					case SelfRegManager.eSelfRegJobAction.Approve:
						request2.Status = eStudentCourseAccommodationRequestStatus.Approved;
						break;
					case SelfRegManager.eSelfRegJobAction.MarkMissingProf:
						request2.Status = eStudentCourseAccommodationRequestStatus.InstructorInfoMissing;
						break;
					case SelfRegManager.eSelfRegJobAction.MarkBackToStaff:
						request2.Status = eStudentCourseAccommodationRequestStatus.PendingWaitingForStaff;
						break;
					default:
						CWLogger.Logger.Warn("SelfRegManager:ProcessSelfRegRequest:Don't understand job.action:pid={0}:job.Action={1}", pid.ToString(), selfRegSingleCourseJob2.Action.ToString());
						break;
					}
					request2.StudentCourseAccommodationRequestId = requestParameters2.StudentAccommodationRequestMan.AddRequest(pid, request2);
					requestParameters2.Dao.CopyAccommodationsToCourse(pid, requestParameters2.LuCourseId, requestParameters2.AccommodationModificationRequests, requestParameters2.CidsToSkip);
					bool flag8 = selfRegSingleCourseJob2.DoSelfReg && selfRegSingleCourseJob2.Action == SelfRegManager.eSelfRegJobAction.Approve && selfRegSingleCourseJob2.SendProfEmail;
					if (flag8)
					{
						SelfRegManager.TryToSendInstructorEmail(requestParameters2.WebSettingMan, requestParameters2.OpContext, requestParameters2.Student, requestParameters2.LuCourseId, selfRegSingleCourseJob2.Prof, request2.Status, requestParameters2.CourseDescription, requestParameters2.BaseUrl, requestParameters2.PidEncodedForUrl, requestParameters2.LucidEncodedForUrl, requestParameters2.IpAddressForLogging);
					}
					SelfRegManager.RequestEmailInfo item2 = new SelfRegManager.RequestEmailInfo
					{
						CourseDescription = requestParameters2.CourseDescription,
						Prof = selfRegSingleCourseJob2.Prof,
						Lucid = requestParameters2.LuCourseId,
						Status = request2.Status.ToString()
					};
					CS$<>8__locals1.requestEmailInfos.Add(item2);
				}
			}
			Task.Run(delegate()
			{
				CS$<>8__locals1.<>4__this.ExecutePostJobListExecuteActions(CS$<>8__locals1.accommodationInfo, CS$<>8__locals1.sm, CS$<>8__locals1.student, CS$<>8__locals1.requestEmailInfos, CS$<>8__locals1.ipAddressForLogging, CS$<>8__locals1.atLeastOneNotApprovedOrHasNote, CS$<>8__locals1.atLeastOneApprovedWithSpecialAccommodations, CS$<>8__locals1.jobs);
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E27C File Offset: 0x0000C47C
		private void ExecutePostJobListExecuteActions(SelfRegManager.AccommodationsInfo accommodationInfo, IWebSettingManager sm, PersonBase student, List<SelfRegManager.RequestEmailInfo> requestEmailInfos, string ipAddressForLogging, bool atLeastOneNotApprovedOrHasNote, bool atLeastOneApprovedWithSpecialAccommodations, List<SelfRegManager.SelfRegSingleCourseJob> jobs)
		{
			List<string> selectedAccommodations = accommodationInfo.SelectedAccommodations;
			Dictionary<string, string> argsForStaffStudentEmail = SelfRegManager.GetArgsForStaffStudentEmail(sm, this.OpContext, student, requestEmailInfos, ipAddressForLogging);
			selectedAccommodations.Sort((string s1, string s2) => s1.CompareTo(s2));
			argsForStaffStudentEmail.Add("selectedaccommodations", string.Join("<br />", selectedAccommodations.ToArray()));
			Dictionary<string, string> args = SelfRegManager.SendStaffEmail(this.OpContext, student, requestEmailInfos, argsForStaffStudentEmail);
			if (atLeastOneNotApprovedOrHasNote)
			{
				bool settingValue = sm.GetSettingValue<bool>(Setting.SELFREGC_SendEmailToAssignedAdvisor);
				bool flag = settingValue;
				if (flag)
				{
					SelfRegManager.SendAssignedAdvisorEmail(this.OpContext, sm, args, student);
				}
			}
			if (atLeastOneApprovedWithSpecialAccommodations)
			{
				SelfRegManager.SendAssignedAdvisorEmailAboutSpecialAccommodations(this.OpContext, sm, args, student);
			}
			SelfRegManager.SendOneStudentEmail(sm, this.OpContext, student, argsForStaffStudentEmail, (from g in requestEmailInfos
			select g.Lucid into h
			where h > 0
			select h).Distinct<int>().ToArray<int>());
			this.SendEmailLogicRulesEmails(jobs, student.PersonId, argsForStaffStudentEmail);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		private static void SendOneStudentEmail(IWebSettingManager sm, OperationContext opContext, PersonBase student, Dictionary<string, string> args, int[] lucids)
		{
			int num = (student != null) ? student.PersonId : 0;
			bool flag = num < 1;
			if (flag)
			{
				CWLogger.Logger.Error("SelfRegManager:SendOneStudentEmail:CantCreatePocBecausePid<1");
			}
			else
			{
				TPMailMessage tpmailMessage;
				Exception ex = SelfRegManager.SendEmail(opContext, Setting.SELFREGC_Email_StudentConfirmation, args, (student != null) ? student.PersonId : 0, lucids, out tpmailMessage);
				bool flag2 = ex != null;
				if (flag2)
				{
					CWLogger.Logger.Error("SelfRegManager:SendStudentEmail:SendEmail:Context={0}:Error={1}", "AccommodationRequestGroup", ex.ToString());
				}
				bool settingValue = sm.GetSettingValue<bool>(Setting.SELFREGC_CreatePocsForSubmittedRequests);
				bool flag3 = settingValue;
				if (flag3)
				{
					IPointOfContactManager pointOfContactManager = new PointOfContactManager(opContext);
					try
					{
						bool flag4 = tpmailMessage == null;
						if (flag4)
						{
							string plainTextMessage = string.Join("\r\n", args.Select(delegate(KeyValuePair<string, string> kvp)
							{
								KeyValuePair<string, string> keyValuePair = kvp;
								string key = keyValuePair.Key;
								string str = "=";
								keyValuePair = kvp;
								return key + str + (keyValuePair.Value ?? "");
							}).ToArray<string>());
							pointOfContactManager.CreatePointOfContactFromMessage(ePointOfContactContext.AutomaticSystemCreated, num, plainTextMessage);
						}
						else
						{
							pointOfContactManager.SaveEmailAsPointOfContact(false, student.PersonId, 0, tpmailMessage, ePointOfContactContext.AutomaticSystemCreated);
						}
					}
					catch (Exception ex2)
					{
						CWLogger.Logger.Warn("SelfRegManager:SendOneStudentEmail:FailedToCreatePOC:err={0}", ex2.ToString());
					}
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		private void SendEmailLogicRulesEmails(IList<SelfRegManager.SelfRegSingleCourseJob> jobs, int studentPersonId, Dictionary<string, string> args)
		{
			List<SelfRegEmailLogicRule> list = new List<SelfRegEmailLogicRule>();
			foreach (SelfRegManager.SelfRegSingleCourseJob selfRegSingleCourseJob in jobs)
			{
				bool flag = selfRegSingleCourseJob.MatchingEmailLogicRules == null || selfRegSingleCourseJob.MatchingEmailLogicRules.Count < 1;
				if (!flag)
				{
					foreach (SelfRegEmailLogicRule selfRegEmailLogicRule in selfRegSingleCourseJob.MatchingEmailLogicRules)
					{
						string title = (selfRegEmailLogicRule.Title ?? "").Trim();
						bool flag2 = title.Length < 1;
						if (flag2)
						{
							title = Guid.NewGuid().ToString();
						}
						bool flag3 = list.Any((SelfRegEmailLogicRule g) => (g.Title ?? "").Trim() == title);
						if (!flag3)
						{
							list.Add(selfRegEmailLogicRule);
						}
					}
				}
			}
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string text = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_WebBaseUrl, false).Trim();
			bool flag4 = text.EndsWith("/");
			if (flag4)
			{
				text = text.Substring(0, text.Length - 1);
			}
			foreach (SelfRegEmailLogicRule selfRegEmailLogicRule2 in list)
			{
				int emailTemplateId = selfRegEmailLogicRule2.EmailTemplateId;
				bool flag5 = emailTemplateId < 1;
				if (flag5)
				{
					CWLogger.Logger.Info("Can't send logic email because template id is not set: title={0}", selfRegEmailLogicRule2.Title);
				}
				else
				{
					string s;
					string studentPersonIdHash = SelfRegManager.GetStudentPersonIdHash(studentPersonId, out s);
					string newValue = string.Concat(new string[]
					{
						text,
						"/user/misc/StudentLetters.aspx?hashstr=",
						SelfRegManager.EncodeStringForUrl(studentPersonIdHash),
						"&plainstr=",
						SelfRegManager.EncodeStringForUrl(s)
					});
					SelfRegManager.AddOrUpdateDictionaryItem(ref args, "url", newValue);
					SelfRegManager.AddOrUpdateDictionaryItem(ref args, "ruletitle", selfRegEmailLogicRule2.Title);
					SelfRegManager.AddOrUpdateDictionaryItem(ref args, "notificationemails", string.Join(",", selfRegEmailLogicRule2.NotificationEmails.ToArray<string>()));
					SelfRegManager.SendEmail(this.OpContext, emailTemplateId, args, studentPersonId, 0);
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E76C File Offset: 0x0000C96C
		private static string EncodeStringForUrl(string s)
		{
			return WebUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(s)));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E794 File Offset: 0x0000C994
		private static void AddOrUpdateDictionaryItem(ref Dictionary<string, string> args, string name, string newValue)
		{
			bool flag = args.ContainsKey(name);
			if (flag)
			{
				args[name] = newValue;
			}
			else
			{
				args.Add(name, newValue);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		private static TryToSendInstructorEmailResult TryToSendInstructorEmail(IWebSettingManager sm, OperationContext opContext, PersonBase student, int luCourseId, LookupInstructor prof, eStudentCourseAccommodationRequestStatus requestStatus, string courseDescription, string baseUrl, string pidEncodedForUrl, string lucidEncodedForUrl, string ipAddressForLogging)
		{
			bool flag = !string.IsNullOrEmpty((prof != null) ? prof.Email : null);
			bool flag2 = !flag;
			TryToSendInstructorEmailResult result;
			if (flag2)
			{
				result = new TryToSendInstructorEmailResult
				{
					Status = eTryToSendInstructorEmailStatus.FailedMissingProfEmail
				};
			}
			else
			{
				bool flag3 = requestStatus != eStudentCourseAccommodationRequestStatus.Approved;
				if (flag3)
				{
					result = new TryToSendInstructorEmailResult
					{
						Status = eTryToSendInstructorEmailStatus.FailedStatusNotApproved
					};
				}
				else
				{
					int settingValue = sm.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
					bool flag4 = settingValue > 0;
					if (flag4)
					{
						IDynamicDataManager dynamicDataManager = new DynamicDataManager(opContext);
						List<DynamicData> list = dynamicDataManager.LoadDataByFields(new DynamicDataContext
						{
							PrimaryId = ((student != null) ? student.PersonId : 0)
						}, new List<int>
						{
							settingValue
						}, eDynamicFormType.AccommodationTemplateOnly);
						bool flag5 = list == null || list.Count < 1;
						bool flag6 = !flag5;
						if (flag6)
						{
							return new TryToSendInstructorEmailResult
							{
								Status = eTryToSendInstructorEmailStatus.FailedProfNotAllowedToSeeStudentAccommodationLetter
							};
						}
					}
					result = SelfRegManager.SendInstructorEmail(sm, opContext, courseDescription, student, prof, (student != null) ? student.PersonId : 0, luCourseId, baseUrl, pidEncodedForUrl, lucidEncodedForUrl, ipAddressForLogging);
				}
			}
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E8CC File Offset: 0x0000CACC
		private static string GetDisplayString(AccommodationData accommodationData)
		{
			object obj;
			if (accommodationData == null)
			{
				obj = null;
			}
			else
			{
				DynamicData data = accommodationData.Data;
				obj = ((data != null) ? data.Field : null);
			}
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string description = SelfRegManager.GetDescription(accommodationData.Data.Field);
				bool flag2 = accommodationData.Data.Value == null;
				string text;
				if (flag2)
				{
					text = string.Empty;
				}
				else
				{
					Type type = accommodationData.Data.Value.GetType();
					bool flag3 = type == typeof(string) || type == typeof(int) || type == typeof(double);
					if (flag3)
					{
						text = accommodationData.Data.Value.ToString();
					}
					else
					{
						bool flag4 = type == typeof(DateTime);
						if (flag4)
						{
							text = ((DateTime)accommodationData.Data.Value).ToString("yyyy-MM-dd");
						}
						else
						{
							text = string.Empty;
						}
					}
				}
				result = string.Format("{0}{1}{2}", description, (text.Length > 0) ? ": " : "", text);
			}
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EA04 File Offset: 0x0000CC04
		private static string GetDescription(DynamicField DynamicField)
		{
			int num = DynamicField.ControlCaption.IndexOf("~~");
			return (num > 0) ? DynamicField.ControlCaption.Substring(0, num) : DynamicField.ControlCaption;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private static TryToSendInstructorEmailResult SendInstructorEmail(IWebSettingManager wm, OperationContext opContext, string courseDescription, PersonBase student, LookupInstructor prof, int pid, int lucid, string baseUrl, string pidEncodedForUrl, string lucidEncodedForUrl, string ipAddressForLogging)
		{
			TryToSendInstructorEmailResult result;
			try
			{
				bool flag = prof == null || pid < 1 || lucid < 1;
				if (flag)
				{
					result = new TryToSendInstructorEmailResult
					{
						Status = eTryToSendInstructorEmailStatus.FailedUnspecified,
						ErrorMessage = string.Format("prof={0}:pid={1}:lucid={2}", ((prof != null) ? prof.ToString() : null) ?? string.Empty, pid, lucid)
					};
				}
				else
				{
					string text = baseUrl + ("/user/instructor/iletter.aspx?pid=" + pidEncodedForUrl + "&lucid=" + lucidEncodedForUrl);
					string settingValue = wm.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_SelfRegistration);
					bool flag2 = string.IsNullOrWhiteSpace(settingValue);
					if (flag2)
					{
						settingValue = wm.GetSettingValue<string>(Setting.GENERAL_FromEmailAddress);
					}
					string settingValue2 = wm.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_SelfRegistration);
					Dictionary<string, string> args = new Dictionary<string, string>
					{
						{
							"from",
							settingValue
						},
						{
							"signature",
							settingValue2
						},
						{
							"profletterurl",
							text
						},
						{
							"url",
							string.Format("<a href='{0}'>{0}</a>", text)
						},
						{
							"instructorname",
							prof.Name ?? ""
						},
						{
							"instructoremail",
							prof.Email ?? ""
						},
						{
							"coursedescription",
							courseDescription
						},
						{
							"coursedescriptionplain",
							Regex.Replace(courseDescription, "<[^>]*>", string.Empty)
						},
						{
							"firstname",
							((student != null) ? student.FirstName : null) ?? string.Empty
						},
						{
							"lastname",
							((student != null) ? student.LastName : null) ?? string.Empty
						},
						{
							"student_no",
							((student != null) ? student.Student_no : null) ?? string.Empty
						},
						{
							"name",
							((student != null) ? student.GetName() : null) ?? ""
						},
						{
							"ip",
							ipAddressForLogging ?? ""
						}
					};
					bool flag3 = SelfRegManager.SendEmail(opContext, Setting.SELFREGC_Email_InstructorNotification, args, pid, lucid);
					bool flag4 = !flag3;
					if (flag4)
					{
						result = new TryToSendInstructorEmailResult
						{
							Status = eTryToSendInstructorEmailStatus.FailedUnspecified,
							ErrorMessage = "Unspecified send email error; could be email template is marked inactive or email failed to send."
						};
					}
					else
					{
						result = new TryToSendInstructorEmailResult
						{
							Status = eTryToSendInstructorEmailStatus.Success
						};
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SelfRegManager:SendInstructorEmail:SendEmail:Context={0}:Error={1}", "AccommodationRequestGroup", ex.ToString());
				result = new TryToSendInstructorEmailResult
				{
					Status = eTryToSendInstructorEmailStatus.FailedUnspecified,
					ErrorMessage = ex.Message
				};
			}
			return result;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private static bool SendEmail(OperationContext opContext, Setting setting, Dictionary<string, string> args, int pid, int lucid)
		{
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = pid,
					LuCourseId = lucid
				},
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = args
				}
			};
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
			TPMailMessage tpmailMessage = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, setting);
			bool flag = !tpmailMessage.IsActive;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IEmailManager emailManager = new EmailManager(opContext);
				try
				{
					IList<TPMailMessage> list = emailManager.SendEmail(new TPMailMessage[]
					{
						tpmailMessage
					});
					result = (list != null && list.Count > 0 && list[0].WasSent);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("SelfRegManager:SendEmail:OpContext:Setting:Args:pid:lucid:ex=" + ex.ToString());
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		private static Exception SendEmail(OperationContext opContext, Setting setting, Dictionary<string, string> args, int pid, int[] lucids, out TPMailMessage emailMessage)
		{
			emailMessage = null;
			Exception result;
			try
			{
				MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
				{
					Context = new MailMergeContext
					{
						PersonId = pid,
						LuCourseIds = ((lucids != null) ? lucids.ToList<int>() : null)
					},
					CustomDictionary = new MailMergeCustomDictionary
					{
						Args = args
					}
				};
				IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
				emailMessage = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, setting);
				bool flag = emailMessage == null;
				if (flag)
				{
					throw new Exception("SelfRegManager:SendEmail:MailMerge failed.");
				}
				bool flag2 = !emailMessage.IsActive;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IEmailManager emailManager = new EmailManager(opContext);
					emailManager.SendEmail(new TPMailMessage[]
					{
						emailMessage
					});
					result = null;
				}
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EE90 File Offset: 0x0000D090
		private static void SendEmail(OperationContext opContext, int templateId, Dictionary<string, string> args, int pid, int lucid)
		{
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = pid,
					LuCourseId = lucid
				},
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = args
				}
			};
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
			TPMailMessage tpmailMessage = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, templateId);
			bool flag = !tpmailMessage.IsActive;
			if (!flag)
			{
				IEmailManager emailManager = new EmailManager(opContext);
				emailManager.SendEmail(new TPMailMessage[]
				{
					tpmailMessage
				});
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EF10 File Offset: 0x0000D110
		private static Dictionary<string, string> SendStaffEmail(OperationContext opContext, PersonBase student, List<SelfRegManager.RequestEmailInfo> requestEmailInfos, Dictionary<string, string> args)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
			IMailMergingEmailManager mailMergingEmailManager2 = mailMergingEmailManager;
			MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary = new MailMergeContextWithCustomDictionary();
			MailMergeContext mailMergeContext = new MailMergeContext();
			mailMergeContext.PersonId = student.PersonId;
			mailMergeContext.LuCourseIds = (from g in requestEmailInfos
			select g.Lucid into h
			where h > 0
			select h).Distinct<int>().ToList<int>();
			mailMergeContextWithCustomDictionary.Context = mailMergeContext;
			mailMergeContextWithCustomDictionary.CustomDictionary = new MailMergeCustomDictionary
			{
				Args = args
			};
			TPMailMessage tpmailMessage = mailMergingEmailManager2.MailMerge(mailMergeContextWithCustomDictionary, Setting.SELFREGC_Email_StaffNotification);
			bool flag = !tpmailMessage.IsActive;
			Dictionary<string, string> result;
			if (flag)
			{
				result = args;
			}
			else
			{
				List<TPMailMessage> list = new List<TPMailMessage>
				{
					tpmailMessage
				};
				IEmailManager emailManager = new EmailManager(opContext);
				emailManager.SendEmail(list.ToArray());
				result = args;
			}
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		private static Dictionary<string, string> GetArgsForStaffStudentEmail(IWebSettingManager wm, OperationContext opContext, PersonBase student, IList<SelfRegManager.RequestEmailInfo> requestEmailInfos, string ipAddressForLogging)
		{
			string settingValue = wm.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_SelfRegistration);
			bool flag = string.IsNullOrWhiteSpace(settingValue);
			if (flag)
			{
				settingValue = wm.GetSettingValue<string>(Setting.GENERAL_FromEmailAddress);
			}
			string settingValue2 = wm.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_SelfRegistration);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("from", settingValue);
			dictionary.Add("signature", settingValue2);
			dictionary.Add("coursedescriptions", string.Join(", ", (from f in requestEmailInfos
			select f.CourseDescription ?? "").ToArray<string>()));
			dictionary.Add("coursedescriptionsplain", string.Join(", ", (from f in requestEmailInfos
			select Regex.Replace(f.CourseDescription, "<[^>]*>", string.Empty) ?? "").ToArray<string>()));
			dictionary.Add("ip", ipAddressForLogging ?? "");
			Dictionary<string, string> dictionary2 = dictionary;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul>");
			foreach (SelfRegManager.RequestEmailInfo requestEmailInfo in requestEmailInfos)
			{
				stringBuilder.AppendFormat("<li><b>{0}</b>: {1} (Professor {2} {3})</li>", new object[]
				{
					requestEmailInfo.Status ?? "",
					requestEmailInfo.CourseDescription ?? "",
					(requestEmailInfo.Prof == null) ? "" : (requestEmailInfo.Prof.Name ?? ""),
					(requestEmailInfo.Prof == null) ? "" : (requestEmailInfo.Prof.Email ?? "")
				});
			}
			stringBuilder.Append("</ul>");
			dictionary2.Add("coursedescriptionswithstatus", stringBuilder.ToString());
			dictionary2.Add("student_no", (student == null) ? "" : (student.Student_no ?? ""));
			dictionary2.Add("firstname", (student == null) ? "" : (student.FirstName ?? ""));
			dictionary2.Add("lastname", (student == null) ? "" : (student.LastName ?? ""));
			dictionary2.Add("name", (student == null) ? "" : student.GetName());
			bool flag2 = student != null && student.PersonId > 0;
			string value;
			if (flag2)
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(opContext);
				DynamicData dynamicData = dynamicDataManager.LoadEmail(student.PersonId);
				value = (((dynamicData != null) ? dynamicData.Value.ToString().Trim() : null) ?? "");
			}
			else
			{
				value = "";
			}
			dictionary2.Add("email", value);
			dictionary2.Add("studentemail", value);
			return dictionary2;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		private static void SendAssignedAdvisorEmail(OperationContext opContext, IWebSettingManager sm, Dictionary<string, string> args, PersonBase student)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(opContext);
			StudentCommonInfo studentCommonInfo = studentCommonInfoManager.LoadStudentCommonInfo(student.PersonId);
			bool flag = string.IsNullOrEmpty(studentCommonInfo.AssignedCounsellorEmail);
			if (flag)
			{
				CWLogger.Logger.Info("SelfRegManager:SendStudentEmail:Trying to send email to assigned advisor but no email present for advisor:pid={0}", student.PersonId.ToString());
				studentCommonInfo.AssignedCounsellorEmail = sm.GetSettingValue<string>(Setting.GENERAL_AdminEmail);
				bool flag2 = string.IsNullOrEmpty(studentCommonInfo.AssignedCounsellorEmail);
				if (flag2)
				{
					CWLogger.Logger.Info("SelfRegManager:SendStudentEmail:Trying to send email to department but no email present for general:admin:pid={0}", student.PersonId.ToString());
					return;
				}
			}
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
			TPMailMessage tpmailMessage = mailMergingEmailManager.MailMerge(new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = student.PersonId
				},
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = args
				}
			}, Setting.SELFREGC_Email_StaffNotification);
			bool flag3 = !tpmailMessage.IsActive;
			if (!flag3)
			{
				tpmailMessage.To = new List<TPMailAddress>
				{
					new TPMailAddress
					{
						EmailAddress = studentCommonInfo.AssignedCounsellorEmail
					}
				};
				tpmailMessage.Subject = "Advisor Notice: " + tpmailMessage.Subject;
				List<TPMailMessage> list = new List<TPMailMessage>
				{
					tpmailMessage
				};
				IEmailManager emailManager = new EmailManager(opContext);
				emailManager.SendEmail(list.ToArray());
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F44C File Offset: 0x0000D64C
		private static void SendAssignedAdvisorEmailAboutSpecialAccommodations(OperationContext opContext, IWebSettingManager sm, Dictionary<string, string> args, PersonBase student)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(opContext);
			StudentCommonInfo studentCommonInfo = studentCommonInfoManager.LoadStudentCommonInfo(student.PersonId);
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(opContext);
			TPMailMessage tpmailMessage = mailMergingEmailManager.MailMerge(new MailMergeContextWithCustomDictionary
			{
				Context = new MailMergeContext
				{
					PersonId = student.PersonId
				},
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = args
				}
			}, Setting.SELFREGC_Email_SpecialAccommodationStaffNotification);
			bool flag = !string.IsNullOrWhiteSpace(studentCommonInfo.AssignedCounsellorEmail);
			if (flag)
			{
				tpmailMessage.To = new List<TPMailAddress>
				{
					new TPMailAddress
					{
						EmailAddress = studentCommonInfo.AssignedCounsellorEmail
					}
				};
			}
			List<TPMailAddress> to = tpmailMessage.To;
			string value;
			if (to == null)
			{
				value = null;
			}
			else
			{
				TPMailAddress tpmailAddress = to.FirstOrDefault<TPMailAddress>();
				value = ((tpmailAddress != null) ? tpmailAddress.EmailAddress : null);
			}
			bool flag2 = string.IsNullOrWhiteSpace(value);
			if (flag2)
			{
				List<TPMailAddress> list = tpmailMessage.Cc;
				TPMailAddress tpmailAddress2;
				if (list == null)
				{
					tpmailAddress2 = null;
				}
				else
				{
					tpmailAddress2 = list.FirstOrDefault((TPMailAddress g) => !string.IsNullOrWhiteSpace(g.EmailAddress));
				}
				TPMailAddress tpmailAddress3 = tpmailAddress2;
				bool flag3 = tpmailAddress3 == null;
				if (flag3)
				{
					list = tpmailMessage.Bcc;
					TPMailAddress tpmailAddress4;
					if (list == null)
					{
						tpmailAddress4 = null;
					}
					else
					{
						tpmailAddress4 = list.FirstOrDefault((TPMailAddress g) => !string.IsNullOrWhiteSpace(g.EmailAddress));
					}
					tpmailAddress3 = tpmailAddress4;
				}
				bool flag4 = tpmailAddress3 != null;
				if (flag4)
				{
					list.Remove(tpmailAddress3);
					tpmailMessage.To = new List<TPMailAddress>
					{
						tpmailAddress3.Clone()
					};
				}
				List<TPMailAddress> to2 = tpmailMessage.To;
				string value2;
				if (to2 == null)
				{
					value2 = null;
				}
				else
				{
					TPMailAddress tpmailAddress5 = to2.FirstOrDefault<TPMailAddress>();
					value2 = ((tpmailAddress5 != null) ? tpmailAddress5.EmailAddress : null);
				}
				bool flag5 = string.IsNullOrWhiteSpace(value2);
				if (flag5)
				{
					string settingValue = sm.GetSettingValue<string>(Setting.GENERAL_AdminEmail);
					bool flag6 = !string.IsNullOrWhiteSpace(settingValue);
					if (flag6)
					{
						tpmailMessage.To = new List<TPMailAddress>
						{
							new TPMailAddress
							{
								EmailAddress = settingValue
							}
						};
					}
					List<TPMailAddress> to3 = tpmailMessage.To;
					string value3;
					if (to3 == null)
					{
						value3 = null;
					}
					else
					{
						TPMailAddress tpmailAddress6 = to3.FirstOrDefault<TPMailAddress>();
						value3 = ((tpmailAddress6 != null) ? tpmailAddress6.EmailAddress : null);
					}
					bool flag7 = string.IsNullOrWhiteSpace(value3);
					if (flag7)
					{
						CWLogger.Logger.Info("SelfRegManager:SendAssignedAdvisorEmailAboutSpecialAccommodations:Trying to send email to department but no email present for general:admin:pid={0} and no email present in mail merged email template", student.PersonId.ToString());
						return;
					}
				}
			}
			tpmailMessage.Subject = "Advisor Notice: " + tpmailMessage.Subject;
			List<TPMailMessage> list2 = new List<TPMailMessage>
			{
				tpmailMessage
			};
			IEmailManager emailManager = new EmailManager(opContext);
			emailManager.SendEmail(list2.ToArray());
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		private static IList<SelfRegEmailLogicRule> FindRulesThatMatch(OperationContext opContext, SelfRegEmailLogicRule[] allRules, SelfRegManager.LogicEmailsTemporaryDataCache tempCache, int studentPersonId, int luCourseId, IList<AccommodationData> accData = null)
		{
			bool flag = allRules == null || allRules.Length < 1;
			IList<SelfRegEmailLogicRule> result;
			if (flag)
			{
				result = new List<SelfRegEmailLogicRule>();
			}
			else
			{
				List<SelfRegEmailLogicRule> list = new List<SelfRegEmailLogicRule>();
				IEnumerable<SelfRegEmailLogicRule> enumerable = from g in allRules
				where g.LogicType == eSelfRegEmailLogicType.PerStudentData
				select g;
				List<SelfRegDataFieldMatchingRule> list2 = new List<SelfRegDataFieldMatchingRule>();
				foreach (SelfRegEmailLogicRule selfRegEmailLogicRule in enumerable)
				{
					bool flag2 = selfRegEmailLogicRule.DataMatchingRules == null;
					if (!flag2)
					{
						list2.AddRange(from matchRule in selfRegEmailLogicRule.DataMatchingRules
						where matchRule.ControlId > 0
						select matchRule);
					}
				}
				foreach (SelfRegEmailLogicRule selfRegEmailLogicRule2 in allRules)
				{
					eSelfRegEmailLogicType logicType = selfRegEmailLogicRule2.LogicType;
					eSelfRegEmailLogicType eSelfRegEmailLogicType = logicType;
					if (eSelfRegEmailLogicType != eSelfRegEmailLogicType.PerStudentData)
					{
						if (eSelfRegEmailLogicType == eSelfRegEmailLogicType.AccommodationData)
						{
							bool flag3 = accData == null;
							if (flag3)
							{
								IAccommodationsManager accommodationsManager = tempCache.GetAccommodationsManager(opContext);
								accData = accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(studentPersonId, luCourseId);
							}
							using (IEnumerator<SelfRegDataFieldMatchingRule> enumerator2 = selfRegEmailLogicRule2.DataMatchingRules.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									SelfRegDataFieldMatchingRule matchingRule = enumerator2.Current;
									AccommodationData accommodationData = accData.FirstOrDefault((AccommodationData g) => g.Data.Field.ControlId == matchingRule.ControlId);
									bool flag4 = accommodationData == null;
									if (!flag4)
									{
										bool flag5 = !SelfRegManager.DoesRuleMatch(matchingRule, accommodationData.Data.Value.ToString());
										if (!flag5)
										{
											list.Add(selfRegEmailLogicRule2);
											break;
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag6 = tempCache.PerStudentData == null;
						if (flag6)
						{
							DynamicDataContext context = new DynamicDataContext
							{
								PrimaryId = studentPersonId
							};
							IDynamicDataManager dynamicDataManager = tempCache.GetDynamicDataManager(opContext);
							tempCache.PerStudentData = dynamicDataManager.LoadDataByFields(context, (from g in list2
							select g.ControlId).Distinct<int>().ToList<int>(), eDynamicFormType.PerStudent);
						}
						List<DynamicData> source = tempCache.PerStudentData ?? new List<DynamicData>();
						using (IEnumerator<SelfRegDataFieldMatchingRule> enumerator3 = selfRegEmailLogicRule2.DataMatchingRules.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SelfRegDataFieldMatchingRule matchingRule = enumerator3.Current;
								DynamicData dynamicData = source.FirstOrDefault((DynamicData g) => g.Field.ControlId == matchingRule.ControlId);
								bool flag7 = dynamicData == null;
								if (!flag7)
								{
									bool flag8 = !SelfRegManager.DoesRuleMatch(matchingRule, dynamicData.Value.ToString());
									if (!flag8)
									{
										list.Add(selfRegEmailLogicRule2);
										break;
									}
								}
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		private static bool DoesRuleMatch(SelfRegDataFieldMatchingRule rule, string val)
		{
			bool flag = string.IsNullOrEmpty(rule.MatchingString);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = !rule.MatchingString.Contains(',');
				if (flag2)
				{
					result = rule.MatchingString.Equals(val, StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					result = rule.MatchingString.Split(new char[]
					{
						','
					}).Any((string g) => g.Length > 0 && g.Equals(val, StringComparison.OrdinalIgnoreCase));
				}
			}
			return result;
		}

		// Token: 0x0400007B RID: 123
		private const string StudentPersonIdHashSecretKey = "u(<A;l@qfdqp{@x$";

		// Token: 0x020001CF RID: 463
		internal class RequestEmailInfo
		{
			// Token: 0x17000233 RID: 563
			// (get) Token: 0x06001162 RID: 4450 RVA: 0x0007E08C File Offset: 0x0007C28C
			// (set) Token: 0x06001163 RID: 4451 RVA: 0x0007E094 File Offset: 0x0007C294
			public string CourseDescription { get; set; }

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06001164 RID: 4452 RVA: 0x0007E09D File Offset: 0x0007C29D
			// (set) Token: 0x06001165 RID: 4453 RVA: 0x0007E0A5 File Offset: 0x0007C2A5
			public string Status { get; set; }

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06001166 RID: 4454 RVA: 0x0007E0AE File Offset: 0x0007C2AE
			// (set) Token: 0x06001167 RID: 4455 RVA: 0x0007E0B6 File Offset: 0x0007C2B6
			public int Lucid { get; set; }

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06001168 RID: 4456 RVA: 0x0007E0BF File Offset: 0x0007C2BF
			// (set) Token: 0x06001169 RID: 4457 RVA: 0x0007E0C7 File Offset: 0x0007C2C7
			public LookupInstructor Prof { get; set; }
		}

		// Token: 0x020001D0 RID: 464
		internal class AccommodationsInfo
		{
			// Token: 0x17000237 RID: 567
			// (get) Token: 0x0600116B RID: 4459 RVA: 0x0007E0D0 File Offset: 0x0007C2D0
			// (set) Token: 0x0600116C RID: 4460 RVA: 0x0007E0D8 File Offset: 0x0007C2D8
			public List<StudentCourseAccommodationModificationRequestItem> AccommodationModificationRequests { get; set; }

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x0600116D RID: 4461 RVA: 0x0007E0E1 File Offset: 0x0007C2E1
			// (set) Token: 0x0600116E RID: 4462 RVA: 0x0007E0E9 File Offset: 0x0007C2E9
			public bool HasAtLeastOneSpecialAccommodation { get; set; }

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x0600116F RID: 4463 RVA: 0x0007E0F2 File Offset: 0x0007C2F2
			// (set) Token: 0x06001170 RID: 4464 RVA: 0x0007E0FA File Offset: 0x0007C2FA
			public List<int> CidsToSkip { get; set; }

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06001171 RID: 4465 RVA: 0x0007E103 File Offset: 0x0007C303
			// (set) Token: 0x06001172 RID: 4466 RVA: 0x0007E10B File Offset: 0x0007C30B
			public List<string> SelectedAccommodations { get; set; }
		}

		// Token: 0x020001D1 RID: 465
		internal class SelfRegSingleCourseJob
		{
			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06001174 RID: 4468 RVA: 0x0007E114 File Offset: 0x0007C314
			// (set) Token: 0x06001175 RID: 4469 RVA: 0x0007E11C File Offset: 0x0007C31C
			public SelfRegManager.ProcessSingleSelfRegRequestCourseParameters RequestParameters { get; set; }

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06001176 RID: 4470 RVA: 0x0007E125 File Offset: 0x0007C325
			// (set) Token: 0x06001177 RID: 4471 RVA: 0x0007E12D File Offset: 0x0007C32D
			public StudentCourseAccommodationRequest Request { get; set; }

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06001178 RID: 4472 RVA: 0x0007E136 File Offset: 0x0007C336
			// (set) Token: 0x06001179 RID: 4473 RVA: 0x0007E13E File Offset: 0x0007C33E
			public SelfRegManager.eSelfRegJobAction Action { get; set; }

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x0600117A RID: 4474 RVA: 0x0007E147 File Offset: 0x0007C347
			// (set) Token: 0x0600117B RID: 4475 RVA: 0x0007E14F File Offset: 0x0007C34F
			public bool DoSelfReg { get; set; }

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x0600117C RID: 4476 RVA: 0x0007E158 File Offset: 0x0007C358
			// (set) Token: 0x0600117D RID: 4477 RVA: 0x0007E160 File Offset: 0x0007C360
			public LookupInstructor Prof { get; set; }

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x0600117E RID: 4478 RVA: 0x0007E169 File Offset: 0x0007C369
			// (set) Token: 0x0600117F RID: 4479 RVA: 0x0007E171 File Offset: 0x0007C371
			public bool SendProfEmail { get; set; }

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06001180 RID: 4480 RVA: 0x0007E17A File Offset: 0x0007C37A
			// (set) Token: 0x06001181 RID: 4481 RVA: 0x0007E182 File Offset: 0x0007C382
			public IList<SelfRegEmailLogicRule> MatchingEmailLogicRules { get; set; }
		}

		// Token: 0x020001D2 RID: 466
		internal enum eSelfRegJobAction
		{
			// Token: 0x04000545 RID: 1349
			Unknown,
			// Token: 0x04000546 RID: 1350
			Approve,
			// Token: 0x04000547 RID: 1351
			MarkMissingProf,
			// Token: 0x04000548 RID: 1352
			MarkBackToStaff,
			// Token: 0x04000549 RID: 1353
			Abort
		}

		// Token: 0x020001D3 RID: 467
		internal class EmailLogicRuleEmail
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06001183 RID: 4483 RVA: 0x0007E18B File Offset: 0x0007C38B
			// (set) Token: 0x06001184 RID: 4484 RVA: 0x0007E193 File Offset: 0x0007C393
			public SelfRegEmailLogicRule EmailLogicRule { get; set; }
		}

		// Token: 0x020001D4 RID: 468
		internal class ProcessSingleSelfRegRequestCourseResult
		{
			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06001186 RID: 4486 RVA: 0x0007E19C File Offset: 0x0007C39C
			// (set) Token: 0x06001187 RID: 4487 RVA: 0x0007E1A4 File Offset: 0x0007C3A4
			public bool AtLeastOneApprovedWithSpecialAccommodations { get; set; }

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06001188 RID: 4488 RVA: 0x0007E1AD File Offset: 0x0007C3AD
			// (set) Token: 0x06001189 RID: 4489 RVA: 0x0007E1B5 File Offset: 0x0007C3B5
			public SelfRegManager.RequestEmailInfo RequestEmailInfo { get; set; }

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x0600118A RID: 4490 RVA: 0x0007E1BE File Offset: 0x0007C3BE
			// (set) Token: 0x0600118B RID: 4491 RVA: 0x0007E1C6 File Offset: 0x0007C3C6
			public bool AtLeastOneNotApprovedOrHasNote { get; set; }
		}

		// Token: 0x020001D5 RID: 469
		internal class ProcessSingleSelfRegRequestCourseParameters
		{
			// Token: 0x0600118D RID: 4493 RVA: 0x0007E1CF File Offset: 0x0007C3CF
			public ProcessSingleSelfRegRequestCourseParameters()
			{
				this.LogicEmailsCache = new SelfRegManager.LogicEmailsTemporaryDataCache();
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x0600118E RID: 4494 RVA: 0x0007E1E5 File Offset: 0x0007C3E5
			// (set) Token: 0x0600118F RID: 4495 RVA: 0x0007E1ED File Offset: 0x0007C3ED
			public ISelfRegDAO Dao { get; set; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06001190 RID: 4496 RVA: 0x0007E1F6 File Offset: 0x0007C3F6
			// (set) Token: 0x06001191 RID: 4497 RVA: 0x0007E1FE File Offset: 0x0007C3FE
			public IStudentAccommodationRequestManager StudentAccommodationRequestMan { get; set; }

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06001192 RID: 4498 RVA: 0x0007E207 File Offset: 0x0007C407
			// (set) Token: 0x06001193 RID: 4499 RVA: 0x0007E20F File Offset: 0x0007C40F
			public IWebSettingManager WebSettingMan { get; set; }

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06001194 RID: 4500 RVA: 0x0007E218 File Offset: 0x0007C418
			// (set) Token: 0x06001195 RID: 4501 RVA: 0x0007E220 File Offset: 0x0007C420
			public OperationContext OpContext { get; set; }

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06001196 RID: 4502 RVA: 0x0007E229 File Offset: 0x0007C429
			// (set) Token: 0x06001197 RID: 4503 RVA: 0x0007E231 File Offset: 0x0007C431
			public PersonBase Student { get; set; }

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06001198 RID: 4504 RVA: 0x0007E23A File Offset: 0x0007C43A
			// (set) Token: 0x06001199 RID: 4505 RVA: 0x0007E242 File Offset: 0x0007C442
			public int LuCourseId { get; set; }

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x0600119A RID: 4506 RVA: 0x0007E24B File Offset: 0x0007C44B
			// (set) Token: 0x0600119B RID: 4507 RVA: 0x0007E253 File Offset: 0x0007C453
			public string CourseDescription { get; set; }

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x0600119C RID: 4508 RVA: 0x0007E25C File Offset: 0x0007C45C
			// (set) Token: 0x0600119D RID: 4509 RVA: 0x0007E264 File Offset: 0x0007C464
			public eSelfRegCoursesAccommodationsStatus AccChange { get; set; }

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x0600119E RID: 4510 RVA: 0x0007E26D File Offset: 0x0007C46D
			// (set) Token: 0x0600119F RID: 4511 RVA: 0x0007E275 File Offset: 0x0007C475
			public string NoteFromStudent { get; set; }

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0007E27E File Offset: 0x0007C47E
			// (set) Token: 0x060011A1 RID: 4513 RVA: 0x0007E286 File Offset: 0x0007C486
			public bool NeverApprove { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0007E28F File Offset: 0x0007C48F
			// (set) Token: 0x060011A3 RID: 4515 RVA: 0x0007E297 File Offset: 0x0007C497
			public IDictionary<int, LookupInstructor> Profs { get; set; }

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0007E2A0 File Offset: 0x0007C4A0
			// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0007E2A8 File Offset: 0x0007C4A8
			public bool HasAtLeastOneSpecialAccommodation { get; set; }

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0007E2B1 File Offset: 0x0007C4B1
			// (set) Token: 0x060011A7 RID: 4519 RVA: 0x0007E2B9 File Offset: 0x0007C4B9
			public List<StudentCourseAccommodationModificationRequestItem> AccommodationModificationRequests { get; set; }

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0007E2C2 File Offset: 0x0007C4C2
			// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0007E2CA File Offset: 0x0007C4CA
			public List<int> CidsToSkip { get; set; }

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x060011AA RID: 4522 RVA: 0x0007E2D3 File Offset: 0x0007C4D3
			// (set) Token: 0x060011AB RID: 4523 RVA: 0x0007E2DB File Offset: 0x0007C4DB
			public string BaseUrl { get; set; }

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x060011AC RID: 4524 RVA: 0x0007E2E4 File Offset: 0x0007C4E4
			// (set) Token: 0x060011AD RID: 4525 RVA: 0x0007E2EC File Offset: 0x0007C4EC
			public string PidEncodedForUrl { get; set; }

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x060011AE RID: 4526 RVA: 0x0007E2F5 File Offset: 0x0007C4F5
			// (set) Token: 0x060011AF RID: 4527 RVA: 0x0007E2FD File Offset: 0x0007C4FD
			public string LucidEncodedForUrl { get; set; }

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0007E306 File Offset: 0x0007C506
			// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0007E30E File Offset: 0x0007C50E
			public string IpAddressForLogging { get; set; }

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0007E317 File Offset: 0x0007C517
			// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0007E31F File Offset: 0x0007C51F
			public SelfRegEmailLogicRule[] EmailLogicRules { get; set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0007E328 File Offset: 0x0007C528
			// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0007E330 File Offset: 0x0007C530
			public SelfRegManager.LogicEmailsTemporaryDataCache LogicEmailsCache { get; set; }
		}

		// Token: 0x020001D6 RID: 470
		internal class LogicEmailsTemporaryDataCache
		{
			// Token: 0x1700025A RID: 602
			// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0007E339 File Offset: 0x0007C539
			// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0007E341 File Offset: 0x0007C541
			public List<DynamicData> PerStudentData { get; set; }

			// Token: 0x060011B8 RID: 4536 RVA: 0x0007E34C File Offset: 0x0007C54C
			public IDynamicDataManager GetDynamicDataManager(OperationContext opContext)
			{
				bool flag = this._ddm != null;
				IDynamicDataManager ddm;
				if (flag)
				{
					ddm = this._ddm;
				}
				else
				{
					this._ddm = new DynamicDataManager(opContext);
					ddm = this._ddm;
				}
				return ddm;
			}

			// Token: 0x060011B9 RID: 4537 RVA: 0x0007E388 File Offset: 0x0007C588
			public IAccommodationsManager GetAccommodationsManager(OperationContext opContext)
			{
				bool flag = this._am != null;
				IAccommodationsManager am;
				if (flag)
				{
					am = this._am;
				}
				else
				{
					this._am = new AccommodationsManager(opContext);
					am = this._am;
				}
				return am;
			}

			// Token: 0x04000563 RID: 1379
			private IDynamicDataManager _ddm;

			// Token: 0x04000564 RID: 1380
			private IAccommodationsManager _am;
		}
	}
}
