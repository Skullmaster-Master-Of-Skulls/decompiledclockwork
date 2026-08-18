using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.CourseRegistrations;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.CourseRegistrations;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x02000109 RID: 265
	public class DataSyncCourseManager : IDataSyncCourseManager, IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00045374 File Offset: 0x00043574
		private int DropCourseEndDateBuffer
		{
			get
			{
				bool flag = this._dropCourseEndDateBuffer != null;
				int result;
				if (flag)
				{
					result = this._dropCourseEndDateBuffer.Value;
				}
				else
				{
					OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
					int num = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DataSync_DropCourseEndDateBuffer);
					bool flag2 = num < 0;
					if (flag2)
					{
						num = 7;
					}
					this._dropCourseEndDateBuffer = new int?(num);
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000453DF File Offset: 0x000435DF
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000453E7 File Offset: 0x000435E7
		internal IDataSyncCourseDAO Dao { get; set; }

		// Token: 0x06000ACB RID: 2763 RVA: 0x000453F0 File Offset: 0x000435F0
		public DataSyncCourseManager(DataSyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.Dao = new DataSyncCourseDAO(opContext);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0004540F File Offset: 0x0004360F
		public DataSyncCourseManager(OperationContext opContext)
		{
			this.OpContext = opContext.ConvertTo<DataSyncOperationContext>();
			this.Dao = new DataSyncCourseDAO(this.OpContext);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00045438 File Offset: 0x00043638
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00045440 File Offset: 0x00043640
		public DataSyncOperationContext OpContext { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0004544C File Offset: 0x0004364C
		private ILookupSubjectDAO LookupSubjectDao
		{
			get
			{
				ILookupSubjectDAO result;
				if ((result = this._lsd) == null)
				{
					result = (this._lsd = new LookupSubjectDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0004547C File Offset: 0x0004367C
		private ClassTestDefinitionDAO ClassTestDao
		{
			get
			{
				ClassTestDefinitionDAO result;
				if ((result = this._classTestDao) == null)
				{
					result = (this._classTestDao = new ClassTestDefinitionDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000454AC File Offset: 0x000436AC
		private LookupInstructorDAO LookupInstructorDao
		{
			get
			{
				LookupInstructorDAO result;
				if ((result = this._lid) == null)
				{
					result = (this._lid = new LookupInstructorDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x000454DC File Offset: 0x000436DC
		private ILookupTimetableItemDAO LookupTimetableItemDao
		{
			get
			{
				ILookupTimetableItemDAO result;
				if ((result = this._ltd) == null)
				{
					result = (this._ltd = new LookupTimetableItemDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0004550C File Offset: 0x0004370C
		private LookupCourseDAO LookupCourseDao
		{
			get
			{
				LookupCourseDAO result;
				if ((result = this._lcd) == null)
				{
					result = (this._lcd = new LookupCourseDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0004553C File Offset: 0x0004373C
		private PeopleDAO PeopleDao
		{
			get
			{
				PeopleDAO result;
				if ((result = this._pd) == null)
				{
					result = (this._pd = new PeopleDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0004556C File Offset: 0x0004376C
		private ICourseRegistrationDAO CourseRegistrationDao
		{
			get
			{
				bool flag = this._crd == null;
				if (flag)
				{
					this._crd = new CourseRegistrationDAO(this.OpContext);
				}
				bool flag2 = this._crd.OpContext == null;
				if (flag2)
				{
					this._crd.OpContext = new OperationContext();
				}
				return this._crd;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000455C8 File Offset: 0x000437C8
		private static int CompareRowParts(DataSyncExternalCourseRowPart p1, DataSyncExternalCourseRowPart p2)
		{
			int num = p1.ExternalCourseId.CompareStringsIgnoreCase(p2.ExternalCourseId);
			bool flag = num != 0;
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				num = p1.StartDate.Date.CompareTo(p2.StartDate.Date);
				bool flag2 = num != 0;
				if (flag2)
				{
					result = num;
				}
				else
				{
					num = p1.Term.CompareStringsIgnoreCase(p2.Term);
					bool flag3 = num != 0;
					if (flag3)
					{
						result = num;
					}
					else
					{
						num = p1.Duration.CompareStringsIgnoreCase(p2.Duration);
						bool flag4 = num != 0;
						if (flag4)
						{
							result = num;
						}
						else
						{
							num = p1.Subject.CompareStringsIgnoreCase(p2.Subject);
							bool flag5 = num != 0;
							if (flag5)
							{
								result = num;
							}
							else
							{
								num = p1.Course.CompareStringsIgnoreCase(p2.Course);
								bool flag6 = num != 0;
								if (flag6)
								{
									result = num;
								}
								else
								{
									num = p1.Section.CompareStringsIgnoreCase(p2.Section);
									bool flag7 = num != 0;
									if (flag7)
									{
										result = num;
									}
									else
									{
										num = p1.TimeOfDay.CompareStringsIgnoreCase(p2.TimeOfDay);
										bool flag8 = num != 0;
										if (flag8)
										{
											result = num;
										}
										else
										{
											result = p1.Campus.CompareStringsIgnoreCase(p2.Campus);
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0004570C File Offset: 0x0004390C
		private bool CreateLookupCoursesFromCustomCoursesTable(int rowsPerPage, int pageNumber)
		{
			DataTable dataTable = this.Dao.LoadCustomCoursesTable(rowsPerPage, pageNumber);
			bool flag = dataTable.Rows.Count < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = this.GetRowPartsFromDataTable(dataTable);
				List<DataSyncExternalCourse> list = this.ParseExternalCourseRowParts(rowPartsFromDataTable);
				foreach (DataSyncExternalCourse dataSyncExternalCourse in list)
				{
					bool flag2;
					LookupSubject lookupSubject = this.FindSubjectCreateIfNecessary(dataSyncExternalCourse.Subject, dataSyncExternalCourse.SubjectLong, out flag2);
					bool flag3 = lookupSubject == null;
					if (!flag3)
					{
						LookupCourse lookupCourse = this.Dao.FindLookupCourse(dataSyncExternalCourse, lookupSubject.SubjectId);
						bool flag4 = lookupCourse == null;
						if (flag4)
						{
							this.Dao.CreateLookupCourse(dataSyncExternalCourse, lookupSubject.SubjectId, null);
						}
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000457F8 File Offset: 0x000439F8
		private static DataSyncExternalCourseFinalExamInfo GetFinalExamInfo(DataRow dr, string sds, string eds, string externalExamId, string location)
		{
			DateTime value;
			DateTime value2;
			bool flag = !DateTime.TryParse(sds, out value) || !DateTime.TryParse(eds, out value2);
			DataSyncExternalCourseFinalExamInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DataSyncExternalCourseFinalExamInfo
				{
					StartDateTime = new DateTime?(value),
					EndDateTime = new DateTime?(value2),
					ExternalId = externalExamId,
					Location = location
				};
			}
			return result;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0004585C File Offset: 0x00043A5C
		private static DataSyncExternalCourseTimetableItem GetTimetableItemRowPartFromDataRow(DataColumnCollection columns, DataRow dr)
		{
			string text = (ESpecialColumnType.TimetableDayOfWeek.GetSpecialColumnValue(columns, dr) ?? "").Trim().ToLower();
			bool flag = string.IsNullOrEmpty(text);
			DataSyncExternalCourseTimetableItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TimeSpan? specialColumnValueTime = ESpecialColumnType.TimetableStartTime.GetSpecialColumnValueTime(columns, dr);
				bool flag2 = specialColumnValueTime == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					TimeSpan? specialColumnValueTime2 = ESpecialColumnType.TimetableEndTime.GetSpecialColumnValueTime(columns, dr);
					bool flag3 = specialColumnValueTime2 == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						bool flag4 = specialColumnValueTime2.Value < specialColumnValueTime.Value;
						if (flag4)
						{
							result = null;
						}
						else
						{
							DayOfWeek dayOfWeek;
							bool flag5 = !text.TryParseDayOfWeek(out dayOfWeek);
							if (flag5)
							{
								result = null;
							}
							else
							{
								result = new DataSyncExternalCourseTimetableItem
								{
									Room = ESpecialColumnType.TimetableRoom.GetSpecialColumnValue(columns, dr),
									StartTime = specialColumnValueTime.Value,
									EndTime = specialColumnValueTime2.Value,
									DayOfWeek = dayOfWeek
								};
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00045950 File Offset: 0x00043B50
		private static DataSyncExternalCourseInstructor GetInstructorRowPartFromDataRow(DataColumnCollection columns, DataRow dr)
		{
			string specialColumnValue = ESpecialColumnType.InstructorName.GetSpecialColumnValue(columns, dr);
			bool flag = string.IsNullOrEmpty(specialColumnValue);
			DataSyncExternalCourseInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DataSyncExternalCourseInstructor
				{
					Name = specialColumnValue,
					Email = ESpecialColumnType.InstructorEmail.GetSpecialColumnValue(columns, dr),
					Phone = ESpecialColumnType.InstructorPhone.GetSpecialColumnValue(columns, dr),
					Username = ESpecialColumnType.InstructorUsername.GetSpecialColumnValue(columns, dr),
					EmployeeId = ESpecialColumnType.InstructorEmployeeId.GetSpecialColumnValue(columns, dr),
					IsPrimary = ESpecialColumnType.InstructorIsPrimary.GetSpecialColumnValueBool(columns, dr),
					Percentage = ESpecialColumnType.InstructorPercentage.GetSpecialColumnValueInt(columns, dr, 0),
					ExternalInstructorId = ESpecialColumnType.InstructorExternalId.GetSpecialColumnValue(columns, dr)
				};
			}
			return result;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000459FC File Offset: 0x00043BFC
		private static IList<DataSyncExternalCourseFinalExamInfo> GetFinalExamInfosFromDataRow(DataColumnCollection columns, DataRow dr)
		{
			string specialColumnValue = ESpecialColumnType.FinalExamInfoStartDate.GetSpecialColumnValue(columns, dr);
			string specialColumnValue2 = ESpecialColumnType.FinalExamInfoEndDate.GetSpecialColumnValue(columns, dr);
			bool flag = string.IsNullOrEmpty(specialColumnValue) || string.IsNullOrEmpty(specialColumnValue2);
			IList<DataSyncExternalCourseFinalExamInfo> result;
			if (flag)
			{
				result = new List<DataSyncExternalCourseFinalExamInfo>();
			}
			else
			{
				string specialColumnValue3 = ESpecialColumnType.FinalExamInfoId.GetSpecialColumnValue(columns, dr);
				string specialColumnValue4 = ESpecialColumnType.FinalExamInfoLocation.GetSpecialColumnValue(columns, dr);
				List<DataSyncExternalCourseFinalExamInfo> list = new List<DataSyncExternalCourseFinalExamInfo>();
				try
				{
					bool flag2 = specialColumnValue.Contains(",");
					if (flag2)
					{
						string[] array = specialColumnValue.Split(new char[]
						{
							','
						});
						string[] array2 = specialColumnValue2.Split(new char[]
						{
							','
						});
						string[] array3 = specialColumnValue3.Split(new char[]
						{
							','
						});
						string[] array4 = specialColumnValue4.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							string externalExamId = (i < array3.Length) ? array3[i] : "";
							string location = (i < array4.Length) ? array4[i] : "";
							DataSyncExternalCourseFinalExamInfo finalExamInfo = DataSyncCourseManager.GetFinalExamInfo(dr, array[i], array2[i], externalExamId, location);
							bool flag3 = finalExamInfo != null;
							if (flag3)
							{
								list.Add(finalExamInfo);
							}
						}
					}
					else
					{
						DataSyncExternalCourseFinalExamInfo finalExamInfo2 = DataSyncCourseManager.GetFinalExamInfo(dr, specialColumnValue, specialColumnValue2, specialColumnValue3, specialColumnValue4);
						bool flag4 = finalExamInfo2 != null;
						if (flag4)
						{
							list.Add(finalExamInfo2);
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("DataSyncCourses:CollectFinalExamInfo:Error={0}", ex.ToString());
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00045B90 File Offset: 0x00043D90
		private void SyncCourseRegistrations(ICourseRegistrationDAO crd, ref List<DataSyncExternalCourseSyncResult> results, List<CourseRegistrationWithStudentSpecificInfo> clockWorkCourses, List<DataSyncExternalCourse> extCourses, PersonBase p)
		{
			var list = (from g in clockWorkCourses
			select new
			{
				cwcourse = g,
				extcourse = extCourses.FirstOrDefault((DataSyncExternalCourse ec) => ec.MatchingClockWorkLookupCourse != null && ec.MatchingClockWorkLookupCourse.LuCourseId == g.Course.LuCourseId)
			}).ToList();
			foreach (var <>f__AnonymousType in list)
			{
				CourseRegistrationWithStudentSpecificInfo cwcourse = <>f__AnonymousType.cwcourse;
				bool flag = cwcourse.RegistrationStatus == eRegistrationStatus.NormalAndExemptFromDataSync;
				if (!flag)
				{
					DataSyncExternalCourse extcourse = <>f__AnonymousType.extcourse;
					CWLogger logger = CWLogger.Logger;
					string message = "SyncCourseRegistrations:cwCourse:{0}:extCourse:{1}";
					object obj;
					if (cwcourse == null)
					{
						obj = null;
					}
					else
					{
						LookupCourse course = cwcourse.Course;
						obj = ((course != null) ? course.LuCourseId.ToString() : null);
					}
					logger.Debug(message, obj ?? "NULL", (extcourse == null) ? "NULL" : ((extcourse.Subject ?? "") + " " + (extcourse.Course ?? "")));
					bool flag2 = extcourse != null;
					if (flag2)
					{
						bool flag3 = cwcourse.RegistrationStatus == eRegistrationStatus.Dropped;
						if (flag3)
						{
							crd.ChangeCourseRegistrationStatus(cwcourse.CoursesId, eRegistrationStatus.Normal);
							cwcourse.RegistrationStatus = eRegistrationStatus.Normal;
							results.Add(new DataSyncExternalCourseSyncResult
							{
								ExternalCourse = extcourse,
								Lucid = extcourse.MatchingClockWorkLookupCourse.LuCourseId,
								CourseRegistrationAction = eDataSyncCourseRegistrationAction.eUnDropped
							});
						}
					}
					else
					{
						bool flag4 = cwcourse.RegistrationStatus == eRegistrationStatus.Dropped;
						if (!flag4)
						{
							double num = 0.0;
							bool flag5 = cwcourse.Course != null && cwcourse.Course.EndDate >= DateTime.Now.Date;
							if (flag5)
							{
								num = (cwcourse.Course.EndDate.Date - DateTime.Now.Date).TotalDays;
							}
							bool flag6 = num <= (double)this.DropCourseEndDateBuffer;
							if (flag6)
							{
								results.Add(new DataSyncExternalCourseSyncResult
								{
									ExternalCourse = null,
									Lucid = 0,
									CourseRegistrationAction = eDataSyncCourseRegistrationAction.eNoChange,
									Msg = "Avoided dropping course because the course is ending in less than " + this.DropCourseEndDateBuffer.ToString()
								});
							}
							else
							{
								CWLogger.Logger.Trace("Common.Dao.Impl.DataSync.DataSyncCourseDAO:SyncCourseRegistrations:daysToCourseEnd>DropCourseEndDateBuffer:daysToCourseEnd={0}:clockWorkCourse.Course:lucid={1}:enddate={2}", num.ToString(), cwcourse.Course.LuCourseId.ToString(), cwcourse.Course.EndDate.ToString("yyyy-MM-dd"));
								crd.ChangeCourseRegistrationStatus(cwcourse.CoursesId, eRegistrationStatus.Dropped);
								cwcourse.RegistrationStatus = eRegistrationStatus.Dropped;
								results.Add(new DataSyncExternalCourseSyncResult
								{
									ExternalCourse = null,
									Lucid = 0,
									CourseRegistrationAction = eDataSyncCourseRegistrationAction.eDropped
								});
							}
						}
					}
				}
			}
			var list2 = (from g in extCourses
			select new
			{
				extcourse = g,
				cwcoursereg = clockWorkCourses.FirstOrDefault((CourseRegistrationWithStudentSpecificInfo h) => h.Course.LuCourseId == g.MatchingClockWorkLookupCourse.LuCourseId)
			}).ToList();
			foreach (var <>f__AnonymousType2 in list2)
			{
				CourseRegistrationWithStudentSpecificInfo courseRegistrationWithStudentSpecificInfo = <>f__AnonymousType2.cwcoursereg;
				DataSyncExternalCourse extcourse2 = <>f__AnonymousType2.extcourse;
				bool flag7 = courseRegistrationWithStudentSpecificInfo == null;
				if (flag7)
				{
					courseRegistrationWithStudentSpecificInfo = crd.RegisterStudentInCourse0<CourseRegistrationWithStudentSpecificInfo>(p.PersonId, extcourse2.MatchingClockWorkLookupCourse.LuCourseId, null);
					results.Add(new DataSyncExternalCourseSyncResult
					{
						ExternalCourse = extcourse2,
						Lucid = extcourse2.MatchingClockWorkLookupCourse.LuCourseId,
						CourseRegistrationAction = eDataSyncCourseRegistrationAction.eAdded
					});
				}
				this.SyncCourseRegChanges(crd, extcourse2, courseRegistrationWithStudentSpecificInfo, ref results);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00045F78 File Offset: 0x00044178
		private bool SyncCourseRegChanges(ICourseRegistrationDAO crd, DataSyncExternalCourse extCourse, CourseRegistrationWithStudentSpecificInfo cwCourseReg, ref List<DataSyncExternalCourseSyncResult> results)
		{
			bool flag = extCourse == null || cwCourseReg == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DataSyncExternalCourseStudentSpecific studentSpecificInfo = extCourse.StudentSpecificInfo;
				bool flag2 = studentSpecificInfo == null || studentSpecificInfo.IsEmpty;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = cwCourseReg.StudentSpecificInfo == null;
					if (flag3)
					{
						cwCourseReg.StudentSpecificInfo = new CourseStudentSpecific();
					}
					CourseStudentSpecific studentSpecificInfo2 = cwCourseReg.StudentSpecificInfo;
					bool flag4 = studentSpecificInfo.IsEqualTo(studentSpecificInfo2);
					if (flag4)
					{
						result = false;
					}
					else
					{
						crd.UpdateCourseRegistrationSpecificInfoNonEmptyFieldsOnly(cwCourseReg.CoursesId, studentSpecificInfo);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00045FFC File Offset: 0x000441FC
		private void FindFinalExams(int lucid, ref IList<ExternalExamInfo> externalExamInfos)
		{
			IList<ClassTest> list = this.ClassTestDao.LoadClassTestDefinitionsByCourse(lucid, eClassTestType.FinalExam);
			bool flag = list.Count < 1;
			if (!flag)
			{
				bool flag2 = list.Count == 1 && externalExamInfos.Count == 1;
				if (flag2)
				{
					externalExamInfos[0].ClockWorkClassTestDefinition = list[0];
				}
				else
				{
					using (IEnumerator<ExternalExamInfo> enumerator = externalExamInfos.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ExternalExamInfo eei = enumerator.Current;
							ClassTest classTest = list.FirstOrDefault((ClassTest g) => g.StartDateTime.Date == eei.StartDateTime.Date);
							bool flag3 = classTest != null;
							if (flag3)
							{
								eei.ClockWorkClassTestDefinition = classTest;
							}
						}
					}
					List<ExternalExamInfo> list2 = (from g in externalExamInfos
					where g.ClockWorkClassTestDefinition == null
					select g).ToList<ExternalExamInfo>();
					List<ExternalExamInfo> matchedExternalExamInfos = (from g in externalExamInfos
					where g.ClockWorkClassTestDefinition == null
					select g).ToList<ExternalExamInfo>();
					List<ClassTest> list3 = (from g in list
					where !matchedExternalExamInfos.Any((ExternalExamInfo h) => h.ClockWorkClassTestDefinition != null && h.ClockWorkClassTestDefinition.ExamId == g.ExamId)
					select g).ToList<ClassTest>();
					int num = 0;
					while (num < list3.Count && num < list2.Count)
					{
						list2[num].ClockWorkClassTestDefinition = list3[num];
						num++;
					}
				}
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0004619C File Offset: 0x0004439C
		private void SyncClassTestDefinitionsForFinalExams(IEnumerable<DataSyncExternalCourse> extCourses, ref List<DataSyncExternalCourseSyncResult> results)
		{
			foreach (DataSyncExternalCourse dataSyncExternalCourse in extCourses)
			{
				bool flag = dataSyncExternalCourse.MatchingClockWorkLookupCourse == null || dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId <= 0 || dataSyncExternalCourse.FinalExamInfos == null || dataSyncExternalCourse.FinalExamInfos.Count <= 0;
				if (!flag)
				{
					IEnumerable<DataSyncExternalCourseFinalExamInfo> source = from h in dataSyncExternalCourse.FinalExamInfos
					where h.StartDateTime != null && h.EndDateTime != null && h.StartDateTime.Value < h.EndDateTime.Value
					select h;
					IList<ExternalExamInfo> list = (from g in source
					select new ExternalExamInfo
					{
						StartDateTime = g.StartDateTime.Value,
						EndDateTime = g.EndDateTime.Value,
						ExternalExamId = g.ExternalId,
						Location = g.Location
					}).ToList<ExternalExamInfo>();
					this.FindFinalExams(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, ref list);
					foreach (ExternalExamInfo externalExamInfo in list)
					{
						bool flag2 = externalExamInfo.ClockWorkClassTestDefinition == null;
						if (flag2)
						{
							int instructorId = this.ClassTestDao.CreateClassTestDefinitionBase(new ClassTestBase
							{
								StartDateTime = externalExamInfo.StartDateTime,
								EndDateTime = externalExamInfo.EndDateTime,
								Course = new LookupCourseBase
								{
									LuCourseId = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId
								},
								ExamType = eClassTestType.FinalExam,
								Location = (externalExamInfo.Location ?? "")
							});
							results.Add(new DataSyncExternalCourseSyncResult
							{
								ExternalCourse = dataSyncExternalCourse,
								LookupCourseAction = eDataSyncCourseLookupCourseAction.eCreateFinalExam,
								Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId,
								InstructorId = instructorId
							});
						}
						else
						{
							int num = Convert.ToInt32((externalExamInfo.StartDateTime - externalExamInfo.ClockWorkClassTestDefinition.StartDateTime).TotalMinutes);
							int num2 = Convert.ToInt32((externalExamInfo.EndDateTime - externalExamInfo.ClockWorkClassTestDefinition.EndDateTime).TotalMinutes);
							bool flag3 = num == 0 && num2 == 0 && (externalExamInfo.Location ?? "").CompareTo(externalExamInfo.ClockWorkClassTestDefinition.Location ?? "") == 0;
							if (!flag3)
							{
								this.ClassTestDao.UpdateClassTestDefinitionBase(new ClassTestBase
								{
									ExamId = externalExamInfo.ClockWorkClassTestDefinition.ExamId,
									StartDateTime = externalExamInfo.StartDateTime,
									EndDateTime = externalExamInfo.EndDateTime,
									Course = new LookupCourseBase
									{
										LuCourseId = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId
									},
									ExamType = eClassTestType.FinalExam,
									Location = (externalExamInfo.Location ?? "")
								});
								results.Add(new DataSyncExternalCourseSyncResult
								{
									ExternalCourse = dataSyncExternalCourse,
									LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdateFinalExam,
									Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId,
									InstructorId = externalExamInfo.ClockWorkClassTestDefinition.ExamId
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000464FC File Offset: 0x000446FC
		private void SyncCourseChanges(List<DataSyncExternalCourse> extCourses0, ref List<DataSyncExternalCourseSyncResult> results, bool isSyncFinalExamsDisabled)
		{
			List<DataSyncExternalCourse> list = (this.OpContext == null || this.OpContext.BatchDataSyncLogId < 1) ? extCourses0 : (from g in extCourses0
			where g.MatchingClockWorkLookupCourse == null || g.MatchingClockWorkLookupCourse.BatchDataSyncLogId < 1 || g.MatchingClockWorkLookupCourse.BatchDataSyncLogId != this.OpContext.BatchDataSyncLogId
			select g).ToList<DataSyncExternalCourse>();
			try
			{
				foreach (DataSyncExternalCourse dataSyncExternalCourse in list)
				{
					bool flag = dataSyncExternalCourse.MatchingClockWorkLookupCourse == null;
					if (flag)
					{
						results.Add(new DataSyncExternalCourseSyncResult
						{
							ExternalCourse = dataSyncExternalCourse,
							ErrorAction = eDataSyncCourseError.eMissingMatchingClockWorkCourse
						});
					}
					else
					{
						bool isExemptFromDataSync = dataSyncExternalCourse.MatchingClockWorkLookupCourse.IsExemptFromDataSync;
						if (!isExemptFromDataSync)
						{
							List<DataSyncExternalCourseInstructor> list2 = dataSyncExternalCourse.Instructors.FindAll((DataSyncExternalCourseInstructor pi) => pi.ClockWorkInstructor != null);
							List<LookupInstructor> profs1 = new List<LookupInstructor>();
							foreach (DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor in list2)
							{
								LookupInstructor clockWorkInstructor = dataSyncExternalCourseInstructor.ClockWorkInstructor;
								clockWorkInstructor.IsPrimary = dataSyncExternalCourseInstructor.IsPrimary;
								profs1.Add(clockWorkInstructor);
							}
							List<LookupInstructor> list3 = dataSyncExternalCourse.MatchingClockWorkLookupCourse.Instructors;
							list3 = (from g in list3
							where !g.IsExemptAssignmentFromDataSync || profs1.FirstOrDefault((LookupInstructor h) => h.InstructorId == g.InstructorId) != null
							select g).ToList<LookupInstructor>();
							List<LookupInstructor> list4;
							List<LookupInstructor> list5;
							bool flag3;
							string text;
							bool flag2 = DataSyncCourseManager.CompareInstructorLists(profs1, list3, out list4, out list5, out flag3, out text);
							bool flag4 = !flag2;
							if (flag4)
							{
								using (List<LookupInstructor>.Enumerator enumerator3 = list4.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										LookupInstructor removedProf = enumerator3.Current;
										bool flag5 = dataSyncExternalCourse.MatchingClockWorkLookupCourse.Instructors.Find((LookupInstructor pr) => pr.InstructorId.Equals(removedProf.InstructorId) && pr.IsExemptAssignmentFromDataSync) != null;
										if (!flag5)
										{
											this.LookupCourseDao.RemoveSecondaryInstructorFromCourse(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, removedProf.InstructorId);
											results.Add(new DataSyncExternalCourseSyncResult
											{
												ExternalCourse = dataSyncExternalCourse,
												InstructorId = removedProf.InstructorId,
												LookupCourseAction = eDataSyncCourseLookupCourseAction.eRemovedSecondaryInstructor,
												Msg = (text ?? "")
											});
											bool flag6 = !removedProf.IsPrimary;
											if (!flag6)
											{
												DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor2 = dataSyncExternalCourse.Instructors.Find((DataSyncExternalCourseInstructor ii) => ii.IsPrimary && ii.ClockWorkInstructor != null);
												bool flag7 = dataSyncExternalCourseInstructor2 == null;
												if (flag7)
												{
													dataSyncExternalCourseInstructor2 = dataSyncExternalCourse.Instructors.Find((DataSyncExternalCourseInstructor ii) => ii.ClockWorkInstructor != null);
													bool flag8 = dataSyncExternalCourseInstructor2 != null;
													if (flag8)
													{
														dataSyncExternalCourseInstructor2.IsPrimary = true;
													}
												}
												flag3 = true;
											}
										}
									}
								}
								foreach (LookupInstructor lookupInstructor in list5)
								{
									this.LookupCourseDao.AddSecondaryInstructorToCourse(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, lookupInstructor.InstructorId);
									results.Add(new DataSyncExternalCourseSyncResult
									{
										ExternalCourse = dataSyncExternalCourse,
										InstructorId = lookupInstructor.InstructorId,
										LookupCourseAction = eDataSyncCourseLookupCourseAction.eAddedSecondaryInstructor
									});
								}
								bool flag9 = flag3;
								if (flag9)
								{
									DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor3 = dataSyncExternalCourse.Instructors.Find((DataSyncExternalCourseInstructor ii) => ii.IsPrimary && ii.ClockWorkInstructor != null);
									bool flag10 = dataSyncExternalCourseInstructor3 == null;
									if (flag10)
									{
										dataSyncExternalCourseInstructor3 = dataSyncExternalCourse.Instructors.Find((DataSyncExternalCourseInstructor ii) => ii.ClockWorkInstructor != null);
										bool flag11 = dataSyncExternalCourseInstructor3 != null;
										if (flag11)
										{
											dataSyncExternalCourseInstructor3.IsPrimary = true;
										}
									}
									LookupInstructor primaryProf = (dataSyncExternalCourseInstructor3 != null) ? dataSyncExternalCourseInstructor3.ClockWorkInstructor : null;
									bool flag12 = primaryProf != null;
									if (flag12)
									{
										bool flag13 = dataSyncExternalCourse.MatchingClockWorkLookupCourse.Instructors.Find((LookupInstructor pr) => pr.InstructorId.Equals(primaryProf.InstructorId) && pr.IsExemptAssignmentFromDataSync) == null;
										if (flag13)
										{
											this.LookupCourseDao.SetPrimaryInstructor(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, primaryProf.InstructorId);
											results.Add(new DataSyncExternalCourseSyncResult
											{
												ExternalCourse = dataSyncExternalCourse,
												InstructorId = primaryProf.InstructorId,
												LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdatedPrimaryInstructor
											});
										}
									}
									else
									{
										this.LookupCourseDao.ClearPrimaryInstructor(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId);
									}
								}
							}
							List<LookupTimetableItem> list6 = dataSyncExternalCourse.TimetableItems.ConvertAll<LookupTimetableItem>((DataSyncExternalCourseTimetableItem f) => new LookupTimetableItem
							{
								DayOfWeek = f.DayOfWeek,
								StartTime = f.StartTime,
								EndTime = f.EndTime,
								Room = f.Room,
								TimetableType = 'C'
							});
							List<LookupTimetableItem> list7;
							List<LookupTimetableItem> list8;
							bool flag14 = DataSyncCourseManager.CompareTimetableLists(list6, dataSyncExternalCourse.MatchingClockWorkLookupCourse.TimetableItems, out list7, out list8);
							bool flag15 = !flag14;
							if (flag15)
							{
								this.LookupTimetableItemDao.SaveLookupTimetableItems(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, list6);
								results.Add(new DataSyncExternalCourseSyncResult
								{
									ExternalCourse = dataSyncExternalCourse,
									Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId,
									LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdatedTimetable
								});
							}
							bool flag16 = !dataSyncExternalCourse.MatchingClockWorkLookupCourse.Campus.AreStringsEqual(dataSyncExternalCourse.Campus);
							bool flag17 = !dataSyncExternalCourse.MatchingClockWorkLookupCourse.Department.AreStringsEqual(dataSyncExternalCourse.Department);
							bool flag18 = flag16 || flag17;
							if (flag18)
							{
								this.Dao.UpdateClockWorkCourse(dataSyncExternalCourse, flag16, flag17);
								bool flag19 = flag16;
								if (flag19)
								{
									results.Add(new DataSyncExternalCourseSyncResult
									{
										LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdatedCampus,
										Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId
									});
								}
								bool flag20 = flag17;
								if (flag20)
								{
									results.Add(new DataSyncExternalCourseSyncResult
									{
										LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdatedDepartment,
										Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId
									});
								}
								bool flag21 = !dataSyncExternalCourse.MatchingClockWorkLookupCourse.CourseNote.AreStringsEqual(dataSyncExternalCourse.CourseNote);
								bool flag22 = flag21;
								if (flag22)
								{
									this.LookupCourseDao.UpdateCourseNote(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, dataSyncExternalCourse.CourseNote);
									results.Add(new DataSyncExternalCourseSyncResult
									{
										LookupCourseAction = eDataSyncCourseLookupCourseAction.eUpdatedCourseNote,
										Lucid = dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId
									});
								}
							}
							bool flag23 = dataSyncExternalCourse.MatchingClockWorkLookupCourse.Credits != dataSyncExternalCourse.Credits;
							if (flag23)
							{
								this.LookupCourseDao.UpdateClockWorkCourseCredits(dataSyncExternalCourse.MatchingClockWorkLookupCourse.LuCourseId, dataSyncExternalCourse.Credits);
							}
						}
					}
				}
				LookupInstructorDAO lookupInstructorDao = this.LookupInstructorDao;
				foreach (DataSyncExternalCourse dataSyncExternalCourse2 in list)
				{
					bool flag24 = dataSyncExternalCourse2.MatchingClockWorkLookupCourse != null && dataSyncExternalCourse2.MatchingClockWorkLookupCourse.IsExemptFromDataSync;
					if (!flag24)
					{
						foreach (DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor4 in dataSyncExternalCourse2.Instructors)
						{
							bool flag25 = dataSyncExternalCourseInstructor4 == null || dataSyncExternalCourseInstructor4.ClockWorkInstructor == null || dataSyncExternalCourseInstructor4.ClockWorkInstructor.IsExemptFromDataSync;
							if (!flag25)
							{
								bool flag26 = dataSyncExternalCourseInstructor4.Email == null || dataSyncExternalCourseInstructor4.Email.Trim().Length <= 0 || dataSyncExternalCourseInstructor4.Email.AreStringsEqual(dataSyncExternalCourseInstructor4.ClockWorkInstructor.Email);
								bool flag27 = flag26;
								if (flag27)
								{
									bool flag28 = dataSyncExternalCourseInstructor4.Phone != null && dataSyncExternalCourseInstructor4.Phone.Trim().Length > 0 && !dataSyncExternalCourseInstructor4.ClockWorkInstructor.Phone.AreStringsEqual(dataSyncExternalCourseInstructor4.Phone);
									if (flag28)
									{
										flag26 = false;
									}
								}
								bool flag29 = flag26;
								if (flag29)
								{
									bool flag30 = dataSyncExternalCourseInstructor4.Name != null && dataSyncExternalCourseInstructor4.Name.Trim().Length > 0 && !dataSyncExternalCourseInstructor4.ClockWorkInstructor.Name.AreStringsEqual(dataSyncExternalCourseInstructor4.Name);
									if (flag30)
									{
										flag26 = false;
									}
								}
								bool flag31 = flag26;
								if (!flag31)
								{
									dataSyncExternalCourseInstructor4.ClockWorkInstructor.Email = dataSyncExternalCourseInstructor4.Email;
									dataSyncExternalCourseInstructor4.ClockWorkInstructor.Name = dataSyncExternalCourseInstructor4.Name;
									dataSyncExternalCourseInstructor4.ClockWorkInstructor.Phone = dataSyncExternalCourseInstructor4.Phone;
									lookupInstructorDao.SaveInstructor(dataSyncExternalCourseInstructor4.ClockWorkInstructor);
									results.Add(new DataSyncExternalCourseSyncResult
									{
										ExternalCourse = dataSyncExternalCourse2,
										InstructorId = dataSyncExternalCourseInstructor4.ClockWorkInstructor.InstructorId,
										InstructorAction = eDataSyncCourseInstructorAction.eUpdatedInstructorNameEmailPhone
									});
								}
							}
						}
					}
				}
				bool flag32 = !isSyncFinalExamsDisabled;
				if (flag32)
				{
					this.SyncClassTestDefinitionsForFinalExams(list, ref results);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			IDataSyncCourseDAO dao = this.Dao;
			List<int> lucids;
			if (list == null)
			{
				lucids = null;
			}
			else
			{
				lucids = list.FindAll((DataSyncExternalCourse g) => g.MatchingClockWorkLookupCourse != null && g.MatchingClockWorkLookupCourse.LuCourseId > 0).ConvertAll<int>((DataSyncExternalCourse h) => h.MatchingClockWorkLookupCourse.LuCourseId);
			}
			dao.FixNoPrimaryWhenSecondariesExistProblemWithProfs(lucids);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00046F18 File Offset: 0x00045118
		private static bool CompareTimetableLists(List<LookupTimetableItem> list1, List<LookupTimetableItem> list2, out List<LookupTimetableItem> removed, out List<LookupTimetableItem> added)
		{
			added = (from tti in list1
			where list2.Find((LookupTimetableItem p) => p.DayOfWeek == tti.DayOfWeek && p.StartTime == tti.StartTime && p.EndTime == tti.EndTime && p.Room.Equals(tti.Room, StringComparison.OrdinalIgnoreCase)) == null
			select tti).ToList<LookupTimetableItem>();
			removed = (from tti in list2
			where list1.Find((LookupTimetableItem p) => p.DayOfWeek == tti.DayOfWeek && p.StartTime == tti.StartTime && p.EndTime == tti.EndTime && p.Room.Equals(tti.Room, StringComparison.OrdinalIgnoreCase)) == null
			select tti).ToList<LookupTimetableItem>();
			return removed.Count < 1 && added.Count < 1;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00046F94 File Offset: 0x00045194
		private static bool CompareInstructorLists(List<LookupInstructor> list1, List<LookupInstructor> list2, out List<LookupInstructor> removed, out List<LookupInstructor> added, out bool primaryInstructorChanged, out string msg)
		{
			CWLogger.Logger.Debug("compareinstructorlists: list1count=" + list1.Count.ToString() + "; list2count=" + list2.Count.ToString());
			added = (from prof in list1
			where list2.Find((LookupInstructor p) => p.InstructorId == prof.InstructorId) == null
			select prof).ToList<LookupInstructor>();
			removed = (from prof in list2
			where list1.Find((LookupInstructor p) => p.InstructorId == prof.InstructorId) == null
			select prof).ToList<LookupInstructor>();
			LookupInstructor lookupInstructor = list1.Find((LookupInstructor p) => p.IsPrimary);
			LookupInstructor lookupInstructor2 = list2.Find((LookupInstructor p) => p.IsPrimary);
			int num = (lookupInstructor == null) ? 0 : lookupInstructor.InstructorId;
			int num2 = (lookupInstructor2 == null) ? 0 : lookupInstructor2.InstructorId;
			primaryInstructorChanged = (num != num2);
			bool flag = removed.Count < 1 && added.Count < 1 && !primaryInstructorChanged;
			bool result;
			if (flag)
			{
				msg = "";
				result = true;
			}
			else
			{
				msg = string.Concat(new object[]
				{
					"removedcount=",
					removed.Count,
					"; addedcount=",
					added.Count,
					"; primary1=",
					(lookupInstructor == null) ? "NULL" : lookupInstructor.InstructorId.ToString(),
					"; primary2=",
					(lookupInstructor2 == null) ? "NULL" : lookupInstructor2.InstructorId.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00047170 File Offset: 0x00045370
		public List<DataSyncExternalCourse> ParseExternalCourseRowParts(List<DataSyncExternalCourseRowPart> rowParts)
		{
			List<DataSyncExternalCourse> list = new List<DataSyncExternalCourse>();
			rowParts.Sort(new Comparison<DataSyncExternalCourseRowPart>(DataSyncCourseManager.CompareRowParts));
			int j;
			for (int i = 0; i < rowParts.Count; i = j)
			{
				for (j = i + 1; j < rowParts.Count; j++)
				{
					int num = DataSyncCourseManager.CompareRowParts(rowParts[i], rowParts[j]);
					bool flag = num != 0;
					if (flag)
					{
						break;
					}
				}
				DataSyncExternalCourse externalCourseFromRowParts = rowParts.GetExternalCourseFromRowParts(i, j - 1);
				bool flag2 = externalCourseFromRowParts != null;
				if (flag2)
				{
					list.Add(externalCourseFromRowParts);
					bool flag3;
					if (externalCourseFromRowParts.Instructors.Count > 0)
					{
						flag3 = (externalCourseFromRowParts.Instructors.Find((DataSyncExternalCourseInstructor f) => f.IsPrimary) == null);
					}
					else
					{
						flag3 = false;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						externalCourseFromRowParts.Instructors[0].IsPrimary = true;
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00047270 File Offset: 0x00045470
		public LookupCourse FindLookupCourse(DataSyncExternalCourse externalCourse)
		{
			bool flag;
			LookupSubject lookupSubject = this.FindSubjectCreateIfNecessary(externalCourse.Subject, externalCourse.SubjectLong, out flag);
			return (lookupSubject != null) ? this.Dao.FindLookupCourse(externalCourse, lookupSubject.SubjectId) : null;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000472B0 File Offset: 0x000454B0
		public LookupInstructor FindLookupInstructor(DataSyncExternalCourseInstructor externalInstructor)
		{
			return this.Dao.FindLookupInstructor(externalInstructor);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000472D0 File Offset: 0x000454D0
		public LookupSubject FindLookupSubject(string subjectName)
		{
			return this.Dao.FindLookupSubject(subjectName);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000472F0 File Offset: 0x000454F0
		public IList<DataSyncExternalCourseSyncResult> DataSyncCourses(string studentNumber, int batchDataSyncLogId = 0)
		{
			IDataSyncInfoManager dataSyncInfoManager = new DataSyncInfoManager(this.OpContext);
			DataSyncInfo dataSyncInfo = dataSyncInfoManager.LoadDataSyncInfo();
			int importStudentCoursesReportId = dataSyncInfo.ImportStudentCoursesReportId;
			bool flag = importStudentCoursesReportId < 1;
			IList<DataSyncExternalCourseSyncResult> result;
			if (flag)
			{
				CWLogger.Logger.Warn("DataSyncCourseManager:DataSyncCourses:StudentNumber={0};Can't data sync courses because data sync courses report is not defined in settings.", studentNumber ?? "NULL");
				result = null;
			}
			else
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				ReportParameter[] parameters = new ReportParameter[]
				{
					new ReportParameter
					{
						Name = "studentno",
						Value = studentNumber
					},
					new ReportParameter
					{
						Name = "student_no",
						Value = studentNumber
					}
				}.ToArray<ReportParameter>();
				RunReportResult runReportResult = reportManager.ExecuteReport2(importStudentCoursesReportId, parameters);
				DataTable dataTable;
				if (runReportResult == null)
				{
					dataTable = null;
				}
				else
				{
					RunFunctionData primaryData = runReportResult.PrimaryData;
					dataTable = ((primaryData != null) ? primaryData.Table : null);
				}
				DataTable dataTable2 = dataTable;
				bool flag2 = dataTable2 == null;
				if (flag2)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "DataSyncCourseManager:DataSyncCourses:StudentNumber={0}:rid={1};Data sync courses report failed:err={2}";
					object arg = studentNumber ?? "NULL";
					object arg2 = importStudentCoursesReportId;
					object obj;
					if (runReportResult == null)
					{
						obj = null;
					}
					else
					{
						RunStatus reportStatus = runReportResult.ReportStatus;
						obj = ((reportStatus != null) ? reportStatus.ErrorMessage : null);
					}
					logger.Warn(message, arg, arg2, obj ?? "NULL");
					result = null;
				}
				else
				{
					DataSyncOperationContext dataSyncOperationContext = this.OpContext.ConvertTo<DataSyncOperationContext>();
					dataSyncOperationContext.BatchDataSyncLogId = batchDataSyncLogId;
					IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(dataSyncOperationContext);
					List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = dataSyncCourseManager.GetRowPartsFromDataTable(dataTable2);
					List<DataSyncExternalCourse> allExternalCourses = dataSyncCourseManager.ParseExternalCourseRowParts(rowPartsFromDataTable);
					result = dataSyncCourseManager.DataSyncCourses(studentNumber, allExternalCourses);
				}
			}
			return result;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00047458 File Offset: 0x00045658
		public List<DataSyncExternalCourseSyncResult> DataSyncCourses(string studentNumber, List<DataSyncExternalCourse> allExternalCourses)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_DataSync_DisableFinalExamSync);
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"finalexamsyncdisabled",
					settingValue_Bool
				}
			} ?? new Dictionary<string, object>();
			List<DataSyncExternalCourseSyncResult> result;
			try
			{
				DateTime now = DateTime.Now;
				List<DataSyncExternalCourse> list = allExternalCourses.FindAll((DataSyncExternalCourse ec) => ec.EndDate >= now);
				List<DataSyncExternalCourseSyncResult> list2 = new List<DataSyncExternalCourseSyncResult>();
				ICourseRegistrationDAO courseRegistrationDao = this.CourseRegistrationDao;
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				PersonBase personBase = peopleManager.LoadPersonByStudentNumber(studentNumber);
				CWLogger.Logger.Trace("START log ALL extcourses");
				foreach (DataSyncExternalCourse dataSyncExternalCourse in allExternalCourses)
				{
					bool flag = dataSyncExternalCourse == null;
					if (flag)
					{
						CWLogger.Logger.Trace("NULL extcourse!");
					}
					else
					{
						CWLogger.Logger.Trace("extcourse={0} {1} {2} ({3} to {4})", new object[]
						{
							dataSyncExternalCourse.Subject ?? "empty subject",
							dataSyncExternalCourse.Course ?? "empty course",
							dataSyncExternalCourse.Section ?? "empty section",
							dataSyncExternalCourse.StartDate.ToString("yyyy-MM-dd"),
							dataSyncExternalCourse.EndDate.ToString("yyyy-MM-dd")
						});
					}
				}
				CWLogger.Logger.Trace("END log ALL extcourses");
				bool flag2 = allExternalCourses.Count < 1;
				if (flag2)
				{
					ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
					DynamicField dynamicField = (DynamicField)cacheStorageManager["DataSyncCoursesNoCoursesFlagField"];
					bool flag3 = dynamicField == null;
					if (flag3)
					{
						Guid uniqueId = new Guid("9C6CF540-A914-418D-BFFB-A3070B43B6CE");
						IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
						dynamicField = dynamicFieldManager.LoadFieldByUniqueId(uniqueId);
						bool flag4 = dynamicField == null;
						if (flag4)
						{
							dynamicFieldManager.CreateField(new DynamicField
							{
								UniqueId = uniqueId.ToString(),
								ControlCaption = "No courses in Data Sync",
								ControlCode = eControlCode.Date
							});
							dynamicField = dynamicFieldManager.LoadFieldByUniqueId(uniqueId);
						}
						bool flag5 = dynamicField != null;
						if (flag5)
						{
							cacheStorageManager.Insert("DataSyncCoursesNoCoursesFlagField", dynamicField);
						}
					}
					bool flag6 = dynamicField != null;
					if (flag6)
					{
						List<DynamicData> data = new List<DynamicData>
						{
							new DynamicData
							{
								Field = dynamicField,
								Value = DateTime.Now
							}
						};
						DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
						dynamicDataDAO.SaveData(new DynamicDataContext
						{
							PrimaryId = personBase.PersonId
						}, data, eDynamicFormType.PerStudent);
					}
					CWLogger.Logger.Trace("DataSyncCourseDAO:DataSyncCourses:StudentHasNoCoursesSoFlagWasMarkedCid={0}:snum={1}:pid={2}", ((dynamicField != null) ? dynamicField.ControlId.ToString() : null) ?? "NULL", studentNumber ?? "NULL", ((personBase != null) ? personBase.PersonId.ToString() : null) ?? "NULL");
					result = new List<DataSyncExternalCourseSyncResult>
					{
						new DataSyncExternalCourseSyncResult
						{
							MiscAction = eDataSyncCourseMiscAction.eSkippedSyncBecauseOfEmptyCourseListButMarkedCid8
						}
					};
				}
				else
				{
					List<DataSyncExternalCourse> list3 = this.FindMatchingLookupCourses(ref list2, ref list);
					CWLogger.Logger.Trace("START log extcourses");
					foreach (DataSyncExternalCourse dataSyncExternalCourse2 in list3)
					{
						bool flag7 = dataSyncExternalCourse2 == null;
						if (flag7)
						{
							CWLogger.Logger.Trace("NULL extcourse!");
						}
						else
						{
							CWLogger.Logger.Trace("extcourse={0} {1} {2} ({3} to {4})", new object[]
							{
								dataSyncExternalCourse2.Subject ?? "empty subject",
								dataSyncExternalCourse2.Course ?? "empty course",
								dataSyncExternalCourse2.Section ?? "empty section",
								dataSyncExternalCourse2.StartDate.ToString("yyyy-MM-dd"),
								dataSyncExternalCourse2.EndDate.ToString("yyyy-MM-dd")
							});
						}
					}
					CWLogger.Logger.Trace("END log extcourses");
					bool flag8 = personBase == null || personBase.PersonId < 1;
					if (flag8)
					{
						list2.Add(new DataSyncExternalCourseSyncResult
						{
							ErrorAction = eDataSyncCourseError.eUnableToLocateStudentPersonId
						});
						result = list2;
					}
					else
					{
						List<CourseRegistrationWithStudentSpecificInfo> list4 = courseRegistrationDao.LoadStudentsCoursesWithStudentSpecificInfo(DateTime.Now, DateTime.Now.AddYears(50), personBase.PersonId, true);
						CWLogger.Logger.Trace("START log cw courses");
						foreach (CourseRegistrationWithStudentSpecificInfo courseRegistrationWithStudentSpecificInfo in list4)
						{
							bool flag9 = courseRegistrationWithStudentSpecificInfo == null;
							if (flag9)
							{
								CWLogger.Logger.Trace("NULL cwcourse");
							}
							else
							{
								CWLogger logger = CWLogger.Logger;
								string message = "cwcourse={0}: {1}";
								LookupCourse course = courseRegistrationWithStudentSpecificInfo.Course;
								object arg = ((course != null) ? course.LuCourseId : 0).ToString();
								LookupCourse course2 = courseRegistrationWithStudentSpecificInfo.Course;
								logger.Trace(message, arg, ((course2 != null) ? course2.GetCourseDescription() : null) ?? "NULL");
							}
						}
						CWLogger.Logger.Trace("END log cwcourses");
						this.SyncCourseRegistrations(courseRegistrationDao, ref list2, list4, list3, personBase);
						bool isSyncFinalExamsDisabled = dictionary.ContainsKey("finalexamsyncdisabled") && (bool)dictionary["finalexamsyncdisabled"];
						this.SyncCourseChanges(list3, ref list2, isSyncFinalExamsDisabled);
						result = list2;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncCourseManager:DataSyncCourses:ex={0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00047A68 File Offset: 0x00045C68
		public LookupInstructor FindInstructorCreateIfNecessary(DataSyncExternalCourseInstructor externalProf, out bool createdProf)
		{
			LookupInstructor lookupInstructor = this.FindLookupInstructor(externalProf);
			bool flag = lookupInstructor == null;
			LookupInstructor result;
			if (flag)
			{
				ILookupInstructorDAO lookupInstructorDao = this.LookupInstructorDao;
				lookupInstructor = new LookupInstructor
				{
					Email = externalProf.Email,
					Name = externalProf.Name,
					Phone = externalProf.Phone,
					Username = externalProf.Username,
					ExternalId = externalProf.ExternalInstructorId,
					EmployeeId = externalProf.EmployeeId
				};
				string text = (lookupInstructor.Name ?? "").Trim();
				bool flag2 = text.Length < 1;
				if (flag2)
				{
					CWLogger.Logger.Error("DataSyncCourseDAO:FindInstructorCreateIfNecessary:Professor with missing name encountered; professor was ignored");
					createdProf = false;
					result = null;
				}
				else
				{
					bool flag3 = (lookupInstructor.ExternalId ?? "").Trim().Length < 1 && (lookupInstructor.Username ?? "").Trim().Length < 1 && (lookupInstructor.EmployeeId ?? "").Trim().Length < 1 && (lookupInstructor.Email ?? "").Trim().Length < 1;
					if (flag3)
					{
						CWLogger.Logger.Error("DataSyncCourseDAO:FindInstructorCreateIfNecessary:Professor does not have an email, externalid, username, or employeeid; professor was ignored:Name={0}:username={1}:email={2}:externalid={3}:employeeid={4}", new object[]
						{
							text ?? "",
							lookupInstructor.Username ?? "",
							lookupInstructor.Email ?? "",
							lookupInstructor.ExternalId ?? "",
							lookupInstructor.EmployeeId ?? ""
						});
						createdProf = false;
						result = null;
					}
					else
					{
						try
						{
							lookupInstructorDao.SaveInstructor(lookupInstructor);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("DataSyncCourses:CantCreateProf:prof={0}:err={1}", lookupInstructor.Name, ex.ToString());
						}
						bool flag4 = lookupInstructor.InstructorId < 1;
						if (flag4)
						{
							CWLogger.Logger.Error("DataSyncCourses:CantCreateProf:prof={0}", lookupInstructor.Name);
							createdProf = false;
							result = null;
						}
						else
						{
							createdProf = true;
							result = lookupInstructor;
						}
					}
				}
			}
			else
			{
				createdProf = false;
				result = lookupInstructor;
			}
			return result;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<DataSyncExternalCourseSyncResult> DataSyncCoursesForNotetakers(string studentNumber, List<DataSyncExternalCourse> externalCourses)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00047C94 File Offset: 0x00045E94
		public void CreateLookupCoursesFromCustomCoursesTable()
		{
			IDataSyncDAO dataSyncDAO = new DataSyncDAO(this.OpContext);
			IList<string> databaseCustomColumnNames = dataSyncDAO.GetDatabaseCustomColumnNames("courses");
			int num = 1;
			bool flag;
			do
			{
				flag = !this.CreateLookupCoursesFromCustomCoursesTable(200, num++);
			}
			while (!flag);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00047CE0 File Offset: 0x00045EE0
		public List<DataSyncExternalCourseRowPart> GetRowPartsFromDataTable(DataTable table)
		{
			table.TrimSpacesAndEnsureNotReadOnlyForEveryCell();
			return (from DataRow dr in table.Rows
			let sd = ESpecialColumnType.StartDate.GetSpecialColumnValueDateTime(table.Columns, dr)
			let ed = ESpecialColumnType.EndDate.GetSpecialColumnValueDateTime(table.Columns, dr)
			where sd != null && ed != null && sd.Value <= ed.Value
			select new DataSyncExternalCourseRowPart
			{
				StartDate = sd.Value,
				EndDate = new DateTime(ed.Value.Year, ed.Value.Month, ed.Value.Day, 23, 59, 59),
				Duration = ESpecialColumnType.Duration.GetSpecialColumnValue(table.Columns, dr),
				Term = ESpecialColumnType.Term.GetSpecialColumnValue(table.Columns, dr),
				Subject = ESpecialColumnType.Subject.GetSpecialColumnValue(table.Columns, dr),
				SubjectLong = ESpecialColumnType.SubjectLong.GetSpecialColumnValue(table.Columns, dr),
				Course = ESpecialColumnType.CourseCode.GetSpecialColumnValue(table.Columns, dr),
				Section = ESpecialColumnType.Section.GetSpecialColumnValue(table.Columns, dr),
				TimeOfDay = ESpecialColumnType.TimeOfDay.GetSpecialColumnValue(table.Columns, dr),
				Campus = ESpecialColumnType.Campus.GetSpecialColumnValue(table.Columns, dr),
				Department = ESpecialColumnType.Department.GetSpecialColumnValue(table.Columns, dr),
				Location = ESpecialColumnType.LocationRoom.GetSpecialColumnValue(table.Columns, dr),
				ExternalCourseId = ESpecialColumnType.ExternalCourseId.GetSpecialColumnValue(table.Columns, dr),
				Instructor = DataSyncCourseManager.GetInstructorRowPartFromDataRow(table.Columns, dr),
				TimetableItems = DataSyncCourseManager.GetTimetableItemRowPartFromDataRow(table.Columns, dr).MakeItemAList<DataSyncExternalCourseTimetableItem>().ToList<DataSyncExternalCourseTimetableItem>(),
				FinalExamInfos = (table.Columns.DoAllSpecialColumnsExist(new ESpecialColumnType[]
				{
					ESpecialColumnType.FinalExamInfoStartDate,
					ESpecialColumnType.FinalExamInfoEndDate
				}) ? DataSyncCourseManager.GetFinalExamInfosFromDataRow(table.Columns, dr) : null),
				CourseNote = ESpecialColumnType.CourseNote.GetSpecialColumnValue(table.Columns, dr),
				Credits = ESpecialColumnType.Credits.GetSpecialColumnValueDecimal(table.Columns, dr),
				StudentSpecificInfo = DataSyncCourseManager.GetStudentSpecificInfoRowPartFromDataRow(table.Columns, dr)
			}).ToList<DataSyncExternalCourseRowPart>();
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00047D78 File Offset: 0x00045F78
		public IList<DataSyncExternalCourseSyncResult> ImportOldCourses(string StudentNumber, IList<DataSyncExternalCourse> ExternalCourses)
		{
			DateTime now = DateTime.Now.Date;
			List<DataSyncExternalCourse> list = (from g in ExternalCourses
			where g.EndDate < now
			select g).ToList<DataSyncExternalCourse>();
			List<DataSyncExternalCourseSyncResult> list2 = new List<DataSyncExternalCourseSyncResult>();
			List<DataSyncExternalCourse> source = this.FindMatchingLookupCourses(ref list2, ref list);
			List<int> list3 = (from h in source.Select(delegate(DataSyncExternalCourse g)
			{
				LookupCourse matchingClockWorkLookupCourse = g.MatchingClockWorkLookupCourse;
				return (matchingClockWorkLookupCourse != null) ? matchingClockWorkLookupCourse.LuCourseId : 0;
			})
			where h > 0
			select h).Distinct<int>().ToList<int>();
			bool flag = list3.Count < 1;
			IList<DataSyncExternalCourseSyncResult> result;
			if (flag)
			{
				result = new List<DataSyncExternalCourseSyncResult>();
			}
			else
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				PersonBase personBase = peopleManager.LoadPersonByStudentNumber(StudentNumber);
				bool flag2 = personBase == null || personBase.PersonId < 1;
				if (flag2)
				{
					result = new List<DataSyncExternalCourseSyncResult>
					{
						new DataSyncExternalCourseSyncResult
						{
							ErrorAction = eDataSyncCourseError.eUnableToLocateStudentPersonId
						}
					};
				}
				else
				{
					ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
					int[] lucidsStudentIsRegisteredIn = courseRegistrationManager.LoadStudentCourseRegistrationLuCourseIds(personBase.PersonId, true);
					List<int> list4 = (from g in list3
					where !lucidsStudentIsRegisteredIn.Contains(g)
					select g).ToList<int>();
					using (List<int>.Enumerator enumerator = list4.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int missingLucid = enumerator.Current;
							courseRegistrationManager.RegisterStudentInCourse(personBase.PersonId, missingLucid);
							list2.Add(new DataSyncExternalCourseSyncResult
							{
								ExternalCourse = source.FirstOrDefault((DataSyncExternalCourse g) => g.MatchingClockWorkLookupCourse != null && g.MatchingClockWorkLookupCourse.LuCourseId == missingLucid),
								Lucid = missingLucid,
								CourseRegistrationAction = eDataSyncCourseRegistrationAction.eAdded
							});
						}
					}
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00047F6C File Offset: 0x0004616C
		private static DataSyncExternalCourseStudentSpecificRowPart GetStudentSpecificInfoRowPartFromDataRow(DataColumnCollection columns, DataRow dr)
		{
			return new DataSyncExternalCourseStudentSpecificRowPart
			{
				Grade = ESpecialColumnType.CourseGrade.GetSpecialColumnValueDecimal(columns, dr),
				GradeLetter = ESpecialColumnType.CourseGradeLetter.GetSpecialColumnValue(columns, dr),
				InProgressGrade = ESpecialColumnType.CourseInProgressGrade.GetSpecialColumnValueDecimal(columns, dr),
				InProgressGradeLetter = ESpecialColumnType.CourseInProgressGradeLetter.GetSpecialColumnValue(columns, dr),
				RegistrationDate = ESpecialColumnType.RegistrationDate.GetSpecialColumnValueDateTime(columns, dr),
				RegistrationNote = ESpecialColumnType.RegistrationNote.GetSpecialColumnValue(columns, dr),
				TuitionCost = ESpecialColumnType.Tuition.GetSpecialColumnDouble(columns, dr)
			};
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00047FF4 File Offset: 0x000461F4
		public LookupSubject FindSubjectCreateIfNecessary(string subjectDescription, string subjectLong, out bool created)
		{
			LookupSubject lookupSubject = this.FindLookupSubject(subjectDescription);
			bool flag = lookupSubject != null;
			LookupSubject result;
			if (flag)
			{
				string subjectCode = lookupSubject.SubjectCode;
				string text = ((subjectCode != null) ? subjectCode.Trim() : null) ?? "";
				string text2 = ((subjectLong != null) ? subjectLong.Trim() : null) ?? "";
				bool flag2 = text2.Length > 0 && !text.Equals(text2, StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					lookupSubject.SubjectCode = subjectLong;
					ILookupSubjectDAO lookupSubjectDao = this.LookupSubjectDao;
					lookupSubjectDao.SaveSubject(lookupSubject);
					CWLogger.Logger.Trace("DataSyncCourseManager:FindSubjectCreateIfNecessary:UpdatedSubjectLong:lucoursedataid={0}:oldSubjectLong={1}:newSubjectLong={2}", lookupSubject.SubjectId.ToString(), text, text2);
				}
				created = false;
				result = lookupSubject;
			}
			else
			{
				ILookupSubjectDAO lookupSubjectDao2 = this.LookupSubjectDao;
				lookupSubject = new LookupSubject
				{
					SubjectDescription = subjectDescription,
					SubjectCode = (((subjectLong != null) ? subjectLong.Trim() : null) ?? "")
				};
				lookupSubjectDao2.SaveSubject(lookupSubject);
				created = (lookupSubject.SubjectId > 0);
				bool flag3 = !created;
				if (flag3)
				{
					CWLogger.Logger.Error("Data Sync Courses:Unable to create subject: {0}", subjectDescription);
				}
				result = lookupSubject;
			}
			return result;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00048118 File Offset: 0x00046318
		public List<DataSyncExternalCourse> FindMatchingLookupCourses(ref List<DataSyncExternalCourseSyncResult> results, ref List<DataSyncExternalCourse> ExternalCourses)
		{
			int num = 0;
			foreach (DataSyncExternalCourse dataSyncExternalCourse in ExternalCourses)
			{
				try
				{
					bool flag;
					LookupSubject lookupSubject = this.FindSubjectCreateIfNecessary(dataSyncExternalCourse.Subject, dataSyncExternalCourse.SubjectLong, out flag);
					bool flag2 = flag;
					if (flag2)
					{
						results.Add(new DataSyncExternalCourseSyncResult
						{
							ExternalCourse = dataSyncExternalCourse,
							MiscAction = eDataSyncCourseMiscAction.eCreatedSubject
						});
					}
					bool flag3 = lookupSubject == null;
					if (flag3)
					{
						CWLogger.Logger.Trace("FindMatchingLookupCourses:CantFindOrCreateSubject:subject={0}", (dataSyncExternalCourse == null) ? "NULL extCourse" : (dataSyncExternalCourse.Subject ?? "empty subject"));
						break;
					}
					List<DataSyncExternalCourseInstructor> list = new List<DataSyncExternalCourseInstructor>();
					foreach (DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor in dataSyncExternalCourse.Instructors)
					{
						bool flag4;
						LookupInstructor lookupInstructor = this.FindInstructorCreateIfNecessary(dataSyncExternalCourseInstructor, out flag4);
						bool flag5 = lookupInstructor == null || lookupInstructor.InstructorId < 1;
						if (flag5)
						{
							list.Add(dataSyncExternalCourseInstructor);
						}
						else
						{
							dataSyncExternalCourseInstructor.ClockWorkInstructor = lookupInstructor;
							bool flag6 = flag4;
							if (flag6)
							{
								results.Add(new DataSyncExternalCourseSyncResult
								{
									InstructorId = lookupInstructor.InstructorId,
									ExternalCourse = dataSyncExternalCourse,
									InstructorAction = eDataSyncCourseInstructorAction.eCreatedInstructor
								});
							}
						}
						bool flag7 = lookupInstructor == null;
						if (flag7)
						{
							CWLogger.Logger.Trace("FindMatchingLookupCourses:CantFindOrCreateInstructor:prof={0}", (dataSyncExternalCourseInstructor == null) ? "NULL extProf" : (dataSyncExternalCourseInstructor.Name ?? "empty prof name"));
						}
					}
					foreach (DataSyncExternalCourseInstructor item in list)
					{
						dataSyncExternalCourse.Instructors.Remove(item);
					}
					LookupCourse lookupCourse = this.Dao.FindLookupCourse(dataSyncExternalCourse, lookupSubject.SubjectId);
					bool flag8 = lookupCourse == null;
					if (flag8)
					{
						lookupCourse = this.Dao.CreateLookupCourse(dataSyncExternalCourse, lookupSubject.SubjectId, results);
					}
					else
					{
						CWLogger.Logger.Trace("existinglookupcoursefound:{0} {1} {2}:matching={3}", new object[]
						{
							dataSyncExternalCourse.Subject,
							dataSyncExternalCourse.Course,
							dataSyncExternalCourse.Section,
							lookupCourse.LuCourseId.ToString()
						});
					}
					dataSyncExternalCourse.MatchingClockWorkLookupCourse = lookupCourse;
					bool flag9 = lookupCourse == null;
					if (flag9)
					{
						CWLogger.Logger.Trace("FindMatchingLookupCourses:lookupcourseisnull:extcourseSubject={0}", (dataSyncExternalCourse == null) ? "NULL" : (dataSyncExternalCourse.Subject ?? "empty subject"));
					}
					else
					{
						bool flag10 = lookupCourse.LuCourseId < 1;
						if (flag10)
						{
							CWLogger.Logger.Trace("FindMatchingLookupCourses:lookupcourseinvalidlucourseid:extcourseSubject={0}", (dataSyncExternalCourse == null) ? "NULL" : (dataSyncExternalCourse.Subject ?? "empty subject"));
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("FindMatchingLookupCourses:ERROR:ex={0}", ex.ToString());
					throw;
				}
				num++;
			}
			CWLogger.Logger.Trace("FindMatchingLookupCoursesComplete:ctr={0}", num.ToString());
			return ExternalCourses.FindAll((DataSyncExternalCourse ec) => ec.MatchingClockWorkLookupCourse != null);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000484C0 File Offset: 0x000466C0
		public IList<DataSyncExternalCourseSyncResult> DataSyncLookupCourses(DataTable table)
		{
			List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = this.GetRowPartsFromDataTable(table);
			List<DataSyncExternalCourse> allExternalCourses = this.ParseExternalCourseRowParts(rowPartsFromDataTable);
			return this.DataSyncLookupCourses(allExternalCourses);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000484EC File Offset: 0x000466EC
		public IList<DataSyncExternalCourseSyncResult> DataSyncLookupCourses(IList<DataSyncExternalCourse> allExternalCourses)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_DataSync_DisableFinalExamSync);
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"finalexamsyncdisabled",
					settingValue_Bool
				}
			} ?? new Dictionary<string, object>();
			IList<DataSyncExternalCourseSyncResult> result;
			try
			{
				DateTime now = DateTime.Now;
				List<DataSyncExternalCourse> list = (from ec in allExternalCourses
				where ec.EndDate >= now
				select ec).ToList<DataSyncExternalCourse>();
				List<DataSyncExternalCourseSyncResult> list2 = new List<DataSyncExternalCourseSyncResult>();
				CWLogger.Logger.Trace("DataSyncLookupCourses:START log ALL extcourses");
				foreach (DataSyncExternalCourse dataSyncExternalCourse in allExternalCourses)
				{
					bool flag = dataSyncExternalCourse == null;
					if (flag)
					{
						CWLogger.Logger.Trace("NULL extcourse!");
					}
					else
					{
						CWLogger.Logger.Trace("extcourse={0} {1} {2} ({3} to {4})", new object[]
						{
							dataSyncExternalCourse.Subject ?? "empty subject",
							dataSyncExternalCourse.Course ?? "empty course",
							dataSyncExternalCourse.Section ?? "empty section",
							dataSyncExternalCourse.StartDate.ToString("yyyy-MM-dd"),
							dataSyncExternalCourse.EndDate.ToString("yyyy-MM-dd")
						});
					}
				}
				CWLogger.Logger.Trace("END log ALL extcourses");
				List<DataSyncExternalCourse> extCourses = this.FindMatchingLookupCourses(ref list2, ref list);
				bool isSyncFinalExamsDisabled = dictionary.ContainsKey("finalexamsyncdisabled") && (bool)dictionary["finalexamsyncdisabled"];
				this.SyncCourseChanges(extCourses, ref list2, isSyncFinalExamsDisabled);
				result = list2;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DataSyncLookupCourses:ex={0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x040001D4 RID: 468
		private const int DefaultRowsPerPage = 200;

		// Token: 0x040001D5 RID: 469
		private int? _dropCourseEndDateBuffer;

		// Token: 0x040001D7 RID: 471
		private ICourseRegistrationDAO _crd;

		// Token: 0x040001D8 RID: 472
		private PeopleDAO _pd;

		// Token: 0x040001D9 RID: 473
		private LookupCourseDAO _lcd;

		// Token: 0x040001DA RID: 474
		private ILookupTimetableItemDAO _ltd;

		// Token: 0x040001DB RID: 475
		private LookupInstructorDAO _lid;

		// Token: 0x040001DC RID: 476
		private ClassTestDefinitionDAO _classTestDao;

		// Token: 0x040001DD RID: 477
		private ILookupSubjectDAO _lsd;
	}
}
