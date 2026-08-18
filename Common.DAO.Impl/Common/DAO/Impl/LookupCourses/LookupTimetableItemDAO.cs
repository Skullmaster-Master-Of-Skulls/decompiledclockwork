using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x0200009E RID: 158
	public class LookupTimetableItemDAO : ILookupTimetableItemDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000277B8 File Offset: 0x000259B8
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x000277C0 File Offset: 0x000259C0
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000455 RID: 1109 RVA: 0x000277C9 File Offset: 0x000259C9
		public LookupTimetableItemDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000277FA File Offset: 0x000259FA
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00027802 File Offset: 0x00025A02
		public OperationContext OpContext { get; set; }

		// Token: 0x06000458 RID: 1112 RVA: 0x0002780C File Offset: 0x00025A0C
		private int GetMinutesFromTime(TimeSpan ts)
		{
			return Convert.ToInt32(ts.TotalMinutes);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0002782C File Offset: 0x00025A2C
		public List<List<LookupTimetableItem>> OrganizeTimetableItemsForSaving(List<LookupTimetableItem> timetableItems)
		{
			List<List<LookupTimetableItem>> list = new List<List<LookupTimetableItem>>();
			List<LookupTimetableItem> list2 = new List<LookupTimetableItem>(timetableItems.ToArray());
			list2.Sort((LookupTimetableItem t1, LookupTimetableItem t2) => t1.DayOfWeek.CompareTo(t2.DayOfWeek));
			int i = 0;
			int num = 0;
			while (i < list2.Count)
			{
				LookupTimetableItem lookupTimetableItem = list2[i];
				DayOfWeek dayOfWeek = lookupTimetableItem.DayOfWeek;
				int j = i;
				while (j < list2.Count)
				{
					LookupTimetableItem lookupTimetableItem2 = list2[j];
					DayOfWeek dow = lookupTimetableItem2.DayOfWeek;
					bool flag = dow != dayOfWeek;
					if (flag)
					{
						break;
					}
					j++;
					bool flag2 = list.Count > 0;
					if (flag2)
					{
						LookupTimetableItem lookupTimetableItem3 = list[num].Find((LookupTimetableItem g) => g.DayOfWeek == dow);
						bool flag3 = lookupTimetableItem3 != null;
						if (flag3)
						{
							bool flag4 = false;
							Predicate<LookupTimetableItem> <>9__2;
							for (int k = 0; k < list.Count; k++)
							{
								List<LookupTimetableItem> list3 = list[k];
								Predicate<LookupTimetableItem> match;
								if ((match = <>9__2) == null)
								{
									match = (<>9__2 = ((LookupTimetableItem g) => g.DayOfWeek == dow));
								}
								lookupTimetableItem3 = list3.Find(match);
								bool flag5 = lookupTimetableItem3 == null;
								if (flag5)
								{
									num = k;
									flag4 = true;
									break;
								}
							}
							bool flag6 = !flag4;
							if (flag6)
							{
								num++;
							}
						}
					}
					bool flag7 = num >= list.Count;
					if (flag7)
					{
						list.Add(new List<LookupTimetableItem>());
					}
					List<LookupTimetableItem> list4 = list[num];
					list4.Add(lookupTimetableItem2);
				}
				i = j;
			}
			return list;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000279F0 File Offset: 0x00025BF0
		private bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00027A30 File Offset: 0x00025C30
		internal static List<LookupTimetableItem> GetTimetableItemsFromCourseRecord(string colPrefix, IDataReader record)
		{
			List<LookupTimetableItem> list = new List<LookupTimetableItem>();
			bool flag = PeopleDAO.ReaderContainsColumn(record, colPrefix + "sunstartminutes");
			if (flag)
			{
				for (int i = 0; i < LookupTimetableItemDAO.dowCaptions.Length; i++)
				{
					string str = LookupTimetableItemDAO.dowCaptions[i];
					string name = str + "startminutes";
					string name2 = str + "endminutes";
					bool flag2 = record[name] != DBNull.Value && record[name2] != DBNull.Value;
					if (flag2)
					{
						LookupTimetableItem lookupTimetableItem = new LookupTimetableItem();
						lookupTimetableItem.TimetableId = (int)record["timetableid"];
						int num = (int)record[name];
						int num2 = (int)record[name2];
						bool flag3 = num > 0 && num2 > 0;
						if (flag3)
						{
							DayOfWeek dayOfWeek = (DayOfWeek)i;
							TimeSpan startTime = new TimeSpan(0, num, 0);
							TimeSpan endTime = new TimeSpan(0, num2, 0);
							bool flag4 = list.Find((LookupTimetableItem f) => f.DayOfWeek == dayOfWeek && f.StartTime == startTime && f.EndTime == endTime) == null;
							if (flag4)
							{
								lookupTimetableItem.DayOfWeek = dayOfWeek;
								lookupTimetableItem.StartTime = startTime;
								lookupTimetableItem.EndTime = endTime;
								lookupTimetableItem.Room = record[str + "room"].ToString();
								list.Add(lookupTimetableItem);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00027BCC File Offset: 0x00025DCC
		private DateTime GetTimeFromMinutes(int minutes)
		{
			return DateTime.Now.Date.AddMinutes((double)minutes);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00027BF8 File Offset: 0x00025DF8
		public LookupTimetableItem LoadLookupTimetableItem(int TimetableId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@timetableid", DbType.Int32, TimetableId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    tt.timetableid\r\n            ,tt.sunstartminutes,tt.sunendminutes\r\n            ,tt.monstartminutes,tt.monendminutes\r\n            ,tt.tuestartminutes,tt.tueendminutes,tt.wedstartminutes,tt.wedendminutes\r\n            ,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n            ,tt.satstartminutes,tt.satendminutes\r\n            ,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom\r\nFROM        timetable tt \r\nWHERE       tt.timetableid=@timetableid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					List<LookupTimetableItem> timetableItemsFromCourseRecord = LookupTimetableItemDAO.GetTimetableItemsFromCourseRecord("", dataReader);
					return (timetableItemsFromCourseRecord.Count > 0) ? timetableItemsFromCourseRecord[0] : null;
				}
			}
			return null;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00027C94 File Offset: 0x00025E94
		public void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItem> items)
		{
			List<List<LookupTimetableItem>> list = this.OrganizeTimetableItemsForSaving(items);
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM timetable WHERE lucourseid=@lucid", array);
			foreach (List<LookupTimetableItem> list2 in list)
			{
				array = new DbParameter[22];
				int i = 0;
				int num = 0;
				while (i < 21)
				{
					array[i++] = this.DatabaseManager.GetParameter(LookupTimetableItemDAO.dowCaptions[num] + "startminutes", DbType.Int32, 0);
					array[i++] = this.DatabaseManager.GetParameter(LookupTimetableItemDAO.dowCaptions[num] + "endminutes", DbType.Int32, 0);
					array[i++] = this.DatabaseManager.GetParameter(LookupTimetableItemDAO.dowCaptions[num] + "room", DbType.String, "");
					num++;
				}
				array[21] = this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId);
				foreach (LookupTimetableItem lookupTimetableItem in list2)
				{
					int dayOfWeek = (int)lookupTimetableItem.DayOfWeek;
					int num2 = dayOfWeek * 3;
					array[num2].Value = this.GetMinutesFromTime(lookupTimetableItem.StartTime);
					array[num2 + 1].Value = this.GetMinutesFromTime(lookupTimetableItem.EndTime);
					array[num2 + 2].Value = lookupTimetableItem.Room;
				}
				int num3 = 0;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("IF EXISTS(SELECT timetableid FROM timetable WHERE lucourseid=@lucid \r\n        AND sunstartminutes=@sunstartminutes AND sunendminutes=@sunendminutes \r\n        AND monstartminutes=@monstartminutes AND monendminutes=@monendminutes \r\n        AND tuestartminutes=@tuestartminutes AND tueendminutes=@tueendminutes \r\n        AND wedstartminutes=@wedstartminutes AND wedendminutes=@wedendminutes \r\n        AND thustartminutes=@thustartminutes AND thuendminutes=@thuendminutes \r\n        AND fristartminutes=@fristartminutes AND friendminutes=@friendminutes \r\n        AND satstartminutes=@satstartminutes AND satendminutes=@satendminutes \r\n        AND sunroom=@sunroom AND monroom=@monroom AND tueroom=@tueroom\r\n        AND wedroom=@wedroom AND thuroom=@thuroom AND friroom=@friroom AND satroom=@satroom)\r\n    SELECT CAST(x.timetableid as int) FROM (SELECT timetableid FROM timetable WHERE lucourseid=@lucid \r\n        AND sunstartminutes=@sunstartminutes AND sunendminutes=@sunendminutes \r\n        AND monstartminutes=@monstartminutes AND monendminutes=@monendminutes \r\n        AND tuestartminutes=@tuestartminutes AND tueendminutes=@tueendminutes \r\n        AND wedstartminutes=@wedstartminutes AND wedendminutes=@wedendminutes \r\n        AND thustartminutes=@thustartminutes AND thuendminutes=@thuendminutes \r\n        AND fristartminutes=@fristartminutes AND friendminutes=@friendminutes \r\n        AND satstartminutes=@satstartminutes AND satendminutes=@satendminutes \r\n        AND sunroom=@sunroom AND monroom=@monroom AND tueroom=@tueroom\r\n        AND wedroom=@wedroom AND thuroom=@thuroom AND friroom=@friroom AND satroom=@satroom) x\r\nELSE\r\nBEGIN\r\n    INSERT INTO timetable (lucourseid,timetabletype,sunstartminutes,sunendminutes,monstartminutes,monendminutes\r\n        ,tuestartminutes,tueendminutes,wedstartminutes,wedendminutes,thustartminutes,thuendminutes\r\n        ,fristartminutes,friendminutes,satstartminutes,satendminutes,sunroom,monroom,tueroom\r\n        ,wedroom,thuroom,friroom,satroom) \r\n    VALUES (@lucid,'C',@sunstartminutes,@sunendminutes,@monstartminutes,@monendminutes\r\n        ,@tuestartminutes,@tueendminutes,@wedstartminutes,@wedendminutes,@thustartminutes,@thuendminutes\r\n        ,@fristartminutes,@friendminutes,@satstartminutes,@satendminutes,@sunroom,@monroom,@tueroom\r\n        ,@wedroom,@thuroom,@friroom,@satroom);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS timetableid\r\nEND", array))
				{
					bool flag = dataReader != null && dataReader.Read();
					if (flag)
					{
						num3 = (int)dataReader[0];
					}
				}
				bool flag2 = num3 < 1;
				if (flag2)
				{
					throw new Exception("Unable to insert timetable");
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00027F1C File Offset: 0x0002611C
		public IList<LookupCourse> LoadLookupTimetableItemsByStudent(int StudentPid, DateTime StartDateTime, DateTime EndDateTime)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPid),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDateTime.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDateTime.Date)
			};
			IList<LookupCourse> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    c.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.section,luc.timeofday,luc.campus,\r\n            tt.timetableid,tt.sunstartminutes,tt.sunendminutes\r\n            ,tt.monstartminutes,tt.monendminutes\r\n            ,tt.tuestartminutes,tt.tueendminutes,tt.wedstartminutes,tt.wedendminutes\r\n            ,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n            ,tt.satstartminutes,tt.satendminutes\r\n            ,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom\r\nFROM        courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN timetable tt ON tt.lucourseid=c.lucourseid\r\nWHERE       c.personid=@pid\r\n            AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n            AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\nORDER BY    c.lucourseid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupCourse> list = new List<LookupCourse>();
					LookupCourse lookupCourse = null;
					while (dataReader.Read())
					{
						int num = (int)dataReader["lucourseid"];
						bool flag2 = lookupCourse == null || lookupCourse.LuCourseId != num;
						if (flag2)
						{
							lookupCourse = new LookupCourse
							{
								LuCourseId = num,
								StartDate = (DateTime)dataReader["startdate"],
								EndDate = (DateTime)dataReader["enddate"],
								Term = dataReader["term"].ToString(),
								Duration = dataReader["duration"].ToString(),
								Subject = new LookupSubject
								{
									SubjectDescription = dataReader["subject"].ToString()
								},
								Course = dataReader["course"].ToString(),
								Section = dataReader["section"].ToString(),
								TimeOfDay = dataReader["timeofday"].ToString(),
								Campus = dataReader["campus"].ToString(),
								TimetableItems = new List<LookupTimetableItem>()
							};
							list.Add(lookupCourse);
						}
						List<LookupTimetableItem> timetableItemsFromCourseRecord = LookupTimetableItemDAO.GetTimetableItemsFromCourseRecord("", dataReader);
						bool flag3 = timetableItemsFromCourseRecord != null && timetableItemsFromCourseRecord.Count > 0;
						if (flag3)
						{
							foreach (LookupTimetableItem item in timetableItemsFromCourseRecord)
							{
								lookupCourse.TimetableItems.Add(item);
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000281A8 File Offset: 0x000263A8
		[DebuggerStepThrough]
		public Task<IList<LookupCourse>> LoadLookupTimetableItemsByStudentAsync(int StudentPid, DateTime StartDateTime, DateTime EndDateTime)
		{
			LookupTimetableItemDAO.<LoadLookupTimetableItemsByStudentAsync>d__18 <LoadLookupTimetableItemsByStudentAsync>d__ = new LookupTimetableItemDAO.<LoadLookupTimetableItemsByStudentAsync>d__18();
			<LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<LookupCourse>>.Create();
			<LoadLookupTimetableItemsByStudentAsync>d__.<>4__this = this;
			<LoadLookupTimetableItemsByStudentAsync>d__.StudentPid = StudentPid;
			<LoadLookupTimetableItemsByStudentAsync>d__.StartDateTime = StartDateTime;
			<LoadLookupTimetableItemsByStudentAsync>d__.EndDateTime = EndDateTime;
			<LoadLookupTimetableItemsByStudentAsync>d__.<>1__state = -1;
			<LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder.Start<LookupTimetableItemDAO.<LoadLookupTimetableItemsByStudentAsync>d__18>(ref <LoadLookupTimetableItemsByStudentAsync>d__);
			return <LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001EA RID: 490
		private static string[] dowCaptions = new string[]
		{
			"sun",
			"mon",
			"tue",
			"wed",
			"thu",
			"fri",
			"sat"
		};
	}
}
