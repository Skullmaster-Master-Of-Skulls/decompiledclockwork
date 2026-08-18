using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.Notetaking;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Notetaking
{
	// Token: 0x020000AF RID: 175
	public class NotetakingManager : INotetakingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00025E08 File Offset: 0x00024008
		private OldUserSettingManager oldUserSettingManager
		{
			get
			{
				OldUserSettingManager result;
				if ((result = this.osm) == null)
				{
					result = (this.osm = new OldUserSettingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00025E33 File Offset: 0x00024033
		public NotetakingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.notetakingDao = new NotetakingDAO(opContext);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00025E51 File Offset: 0x00024051
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00025E59 File Offset: 0x00024059
		public OperationContext OpContext { get; set; }

		// Token: 0x06000681 RID: 1665 RVA: 0x00025E64 File Offset: 0x00024064
		public NotetakerBase LoadNotetakerBaseByUsername(string username)
		{
			return this.notetakingDao.LoadNotetakerBaseByUsername(username);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00025E84 File Offset: 0x00024084
		public NotetakerBase LoadNotetakerBaseById(int ServiceProviderId)
		{
			return this.notetakingDao.LoadNotetakerBaseById(ServiceProviderId);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00025EA4 File Offset: 0x000240A4
		public NotetakerBase LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId)
		{
			return this.notetakingDao.LoadNotetakerBaseByNotetakeeAndCourse(NotetakeePersonId, NotetakeeLuCourseId);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00025EC4 File Offset: 0x000240C4
		public List<LectureNoteDescription> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId)
		{
			return this.notetakingDao.LoadLectureNoteDescriptionsByNotetakerAndCourse(ServiceProviderId, NotetakerLuCourseId);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00025EE4 File Offset: 0x000240E4
		public LectureNote LoadLectureNoteById(int NotetakerDocumentId)
		{
			return this.notetakingDao.LoadLectureNoteById(NotetakerDocumentId);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00025F04 File Offset: 0x00024104
		public List<LookupCourseBase> LoadEquivalentCourses(int LuCourseId)
		{
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_ServiceProviders_EquivalentCourseMatchingNum);
			return this.notetakingDao.LoadEquivalentCourses(LuCourseId, settingValue_Int);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00025F44 File Offset: 0x00024144
		public List<NotetakerBaseWithLookupCourseBase> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId)
		{
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_ServiceProviders_EquivalentCourseMatchingNum);
			return this.notetakingDao.LoadMatchingNotetakersWithLectureNoteUploadsByCourse(LuCourseId, settingValue_Int);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00025F84 File Offset: 0x00024184
		public NotetakerBase LoadNotetakerBaseByStudentNumber(string StudentNumber)
		{
			return this.notetakingDao.LoadNotetakerBaseByStudentNumber(StudentNumber);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00025FA2 File Offset: 0x000241A2
		public void ChangeCourseRegistrationStatus(int ServiceProviderApplicationCourseId, eRegistrationStatus NewStatus)
		{
			this.notetakingDao.ChangeCourseRegistrationStatus(ServiceProviderApplicationCourseId, NewStatus);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00025FB4 File Offset: 0x000241B4
		public NotetakerCourseRegistration RegisterNotetakerInCourse(int ServiceProviderId, int Lucid, bool? ExemptCourseFromDataSyncForStudent = null)
		{
			return this.notetakingDao.RegisterNotetakerInCourse(ServiceProviderId, Lucid, ExemptCourseFromDataSyncForStudent);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00025FD4 File Offset: 0x000241D4
		public NotetakerCourseRegistration LoadCourseRegistration(int ServiceProviderId, int Lucid)
		{
			return this.notetakingDao.LoadCourseRegistration(ServiceProviderId, Lucid);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00025FF4 File Offset: 0x000241F4
		public void AddPotentialCoursesForNotetaker(int ServiceProviderId, IList<DataSyncExternalCourse> ExternalCourses)
		{
			DataSyncCourseDAO dataSyncCourseDAO = new DataSyncCourseDAO(this.OpContext);
			ILookupCourseDAO lookupCourseDAO = new LookupCourseDAO(this.OpContext);
			List<DataSyncExternalCourseSyncResult> list = new List<DataSyncExternalCourseSyncResult>();
			List<DataSyncExternalCourse> list2 = ExternalCourses.ToList<DataSyncExternalCourse>();
			IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
			List<DataSyncExternalCourse> list3 = dataSyncCourseManager.FindMatchingLookupCourses(ref list, ref list2);
			bool flag = list3 == null || list3.Count < 1;
			if (flag)
			{
				CWLogger.Logger.Error("NotetakingManager:AddPotentialCoursesForNotetaker:spid={0}:extCourses is null or empty", ServiceProviderId.ToString());
			}
			else
			{
				int num = this.notetakingDao.CreateOrRetrieveSpAppIdForCourses(ServiceProviderId);
				bool flag2 = num < 1;
				if (flag2)
				{
					CWLogger.Logger.Error("NotetakingDAO:AddPotentialCoursesForNotetaker:spid={0}:Can't find or create spaid", ServiceProviderId.ToString());
				}
				else
				{
					foreach (DataSyncExternalCourse dataSyncExternalCourse in list3)
					{
						this.notetakingDao.AddServiceProviderApplicationCourse(num, dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId);
					}
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00026100 File Offset: 0x00024300
		public int CreateNotetakerAccount(SPProvider Provider)
		{
			string text = null;
			bool flag = Provider == null;
			if (flag)
			{
				text = "Null provider";
			}
			else
			{
				bool flag2 = Provider.Person == null;
				if (flag2)
				{
					text = "Null Provider.Person";
				}
				else
				{
					bool flag3 = (Provider.Person.FirstName ?? "").Trim().Length < 1;
					if (flag3)
					{
						text = "Missing first name";
					}
					else
					{
						bool flag4 = (Provider.Person.LastName ?? "").Trim().Length < 1;
						if (flag4)
						{
							text = "Missing last name";
						}
						else
						{
							Provider.UserName = (Provider.UserName ?? "").Trim().ToUpper();
							Provider.Person.Student_no = (Provider.Person.Student_no ?? "").Trim().ToUpper();
						}
					}
				}
			}
			bool flag5 = Provider.UserName.Length < 1 && Provider.Person.Student_no.Length < 1;
			if (flag5)
			{
				text = "Can'tCreateAccountWithEmptyUsernameAndStudent_no";
			}
			bool flag6 = text != null;
			int result;
			if (flag6)
			{
				CWLogger.Logger.Error("NotetakingManager.CreateNotetakerAccount:{0}", text);
				result = 0;
			}
			else
			{
				result = this.notetakingDao.CreateNotetakerAccount(Provider);
			}
			return result;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00026246 File Offset: 0x00024446
		public void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId)
		{
			this.notetakingDao.RecordStudentDownloadedLectureNote(PersonId, NotetakerDocumentId);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00026258 File Offset: 0x00024458
		public IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId)
		{
			return this.notetakingDao.LoadStudentDownloadedLectureNoteHistory(PersonId, LuCourseId);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00026278 File Offset: 0x00024478
		public IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId)
		{
			return this.notetakingDao.LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(PersonId, LuCourseId);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00026298 File Offset: 0x00024498
		public NotetakerBase LoadNotetakerBaseByEmail(string Email)
		{
			return this.notetakingDao.LoadNotetakerBaseByEmail(Email);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000262B8 File Offset: 0x000244B8
		public int CreateLectureNote(LectureNote lectureNote)
		{
			string text = this.CheckLectureNoteValidity(lectureNote);
			bool flag = text != null;
			if (flag)
			{
				CWLogger.Logger.Warn("Common.Core.Notetaking.NotetakingManager.CreateLectureNote:msg={0}", text);
			}
			return this.notetakingDao.CreateLectureNote(lectureNote);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000262FC File Offset: 0x000244FC
		public void UpdateLectureNote(LectureNote lectureNote)
		{
			string text = this.CheckLectureNoteValidity(lectureNote);
			bool flag = text != null;
			if (flag)
			{
				CWLogger.Logger.Warn("Common.Core.Notetaking.NotetakingManager.UpdateLectureNote:msg={0}", text);
			}
			this.notetakingDao.UpdateLectureNote(lectureNote);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002633C File Offset: 0x0002453C
		private string CheckLectureNoteValidity(LectureNote lectureNote)
		{
			bool flag = lectureNote == null;
			string result;
			if (flag)
			{
				result = "Lecture note is null";
			}
			else
			{
				bool flag2 = lectureNote.LectureNoteDescription == null;
				if (flag2)
				{
					result = "LectureNote.LectureNoteDescription is null";
				}
				else
				{
					bool flag3 = lectureNote.LectureNoteDescription.NotetakerBaseInfo == null;
					if (flag3)
					{
						result = "LectureNote.LectureNoteDescription.NotetakerBaseInfo is null";
					}
					else
					{
						bool flag4 = lectureNote.LectureNoteDescription.CourseBaseInfo == null;
						if (flag4)
						{
							result = "LectureNote.LectureNoteDescription.CourseBaseInfo is null";
						}
						else
						{
							bool flag5 = lectureNote.LectureNoteDescription.NotetakerBaseInfo.ServiceProviderId < 1;
							if (flag5)
							{
								result = "Invalid spid";
							}
							else
							{
								bool flag6 = lectureNote.LectureNoteDescription.CourseBaseInfo.LuCourseId < 1;
								if (flag6)
								{
									result = "Invalid lucid";
								}
								else
								{
									result = null;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000263EF File Offset: 0x000245EF
		public void DeleteLectureNote(int NotetakerDocumentId)
		{
			this.notetakingDao.DeleteLectureNote(NotetakerDocumentId);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00026400 File Offset: 0x00024600
		public IList<DateTime> LoadUniqueAvailableCourseStartDatesByNotetaker(int NotetakerId)
		{
			return this.notetakingDao.LoadUniqueCourseStartDatesForNotetakerAvailableCourses(NotetakerId);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00026420 File Offset: 0x00024620
		public IList<LookupCourseBase> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate)
		{
			return this.notetakingDao.LoadNotetakerAvailableCourses(NotetakerId, StartDate, EndDate);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00026440 File Offset: 0x00024640
		public IList<ServiceRequestBase> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId)
		{
			return this.notetakingDao.LoadUniqueStudentsReceivingNotes(NotetakerId, LuCourseId, 128);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00026464 File Offset: 0x00024664
		public List<LectureNoteDescription> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId)
		{
			return this.notetakingDao.LoadLectureNoteDescriptionsByStudentAndCourse(StudentPersonId, StudentLuCourseId);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00026484 File Offset: 0x00024684
		public bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid)
		{
			return this.notetakingDao.AssignNotetaker(studentPid, studentLucid, serviceProviderId, serviceProviderLucid);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000264A8 File Offset: 0x000246A8
		public NotetakerBaseWithLookupCourseBase CancelNotetakerAssignment(int studentPid, int studentLucid, string why)
		{
			return this.notetakingDao.CancelNotetakerAssignment(studentPid, studentLucid, why);
		}

		// Token: 0x0400013C RID: 316
		private INotetakingDAO notetakingDao;

		// Token: 0x0400013D RID: 317
		private OldUserSettingManager osm;
	}
}
