using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using SettingsPermissions;
using UnivOleDb;

namespace ClockWorkAPI.ServiceProviders.Matching
{
	// Token: 0x0200006C RID: 108
	public class MatchingRule
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001D76C File Offset: 0x0001C76C
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001D784 File Offset: 0x0001C784
		public int ServiceProviderType
		{
			get
			{
				return this.serviceProviderType;
			}
			set
			{
				this.serviceProviderType = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001D790 File Offset: 0x0001C790
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001D7A8 File Offset: 0x0001C7A8
		public int LuCourseId
		{
			get
			{
				return this.lucourseId;
			}
			set
			{
				this.lucourseId = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001D7B4 File Offset: 0x0001C7B4
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001D7CC File Offset: 0x0001C7CC
		public int PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				this.personId = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001D7D8 File Offset: 0x0001C7D8
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001D7F0 File Offset: 0x0001C7F0
		public MatchType MatchType
		{
			get
			{
				return this.matchType;
			}
			set
			{
				this.matchType = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001D7FC File Offset: 0x0001C7FC
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001D814 File Offset: 0x0001C814
		public bool PartialOverlapOnSameDayAllowed
		{
			get
			{
				return this.partialOverlapOnSameDayAllowed;
			}
			set
			{
				this.partialOverlapOnSameDayAllowed = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001D820 File Offset: 0x0001C820
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001D838 File Offset: 0x0001C838
		public bool PartialFullDaysOverlapAllowed
		{
			get
			{
				return this.partialFullDaysOverlapAllowed;
			}
			set
			{
				this.partialFullDaysOverlapAllowed = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001D844 File Offset: 0x0001C844
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0001D85C File Offset: 0x0001C85C
		public SortType SortType
		{
			get
			{
				return this.sortType;
			}
			set
			{
				this.sortType = value;
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001D868 File Offset: 0x0001C868
		public MatchingRule(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.da = da;
			this.tripleDES = tripleDES;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001D8C8 File Offset: 0x0001C8C8
		public static string GetDayOfWeek3CharacterShortLowerCase(DayOfWeek dayOfWeek)
		{
			string result;
			switch (dayOfWeek)
			{
			case DayOfWeek.Sunday:
				result = "sun";
				break;
			case DayOfWeek.Monday:
				result = "mon";
				break;
			case DayOfWeek.Tuesday:
				result = "tue";
				break;
			case DayOfWeek.Wednesday:
				result = "wed";
				break;
			case DayOfWeek.Thursday:
				result = "thu";
				break;
			case DayOfWeek.Friday:
				result = "fri";
				break;
			case DayOfWeek.Saturday:
				result = "sat";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001D998 File Offset: 0x0001C998
		public List<MatchingRuleFoundMatch> FindMatchesWithStudentCourse(DateTime sdate, DateTime edate)
		{
			List<MatchingRuleFoundMatch> list = new List<MatchingRuleFoundMatch>();
			DataTable table = this.LoadPotentialMatches(sdate, edate);
			DataView dataView = new DataView(table);
			dataView.Sort = "serviceproviderid";
			List<int> allEquivalentCourses = MatchingRule.GetAllEquivalentCourses(this.lucourseId, this.da);
			List<TimeTableItem> list2 = TimeTableItem.LoadCourseTimetable(this.lucourseId, this.da);
			list2.Sort((TimeTableItem tti1, TimeTableItem tti2) => tti1.DayOfWeek.CompareTo(tti2.DayOfWeek));
			if (list2.Count > 0)
			{
				int j;
				for (int i = 0; i < dataView.Count; i = j)
				{
					DataRow row = dataView[i].Row;
					int num = (int)row["serviceproviderid"];
					MatchingRuleFoundMatch matchingRuleFoundMatch = new MatchingRuleFoundMatch(list2);
					matchingRuleFoundMatch.ServiceProvider = new ServiceProviderUser(row);
					for (j = i; j < dataView.Count; j++)
					{
						DataRow row2 = dataView[j].Row;
						int num2 = (int)row2["serviceproviderid"];
						if (num2 != num)
						{
							break;
						}
						int num3 = (row2["personid"] == DBNull.Value) ? 0 : ((int)row2["personid"]);
						if (num3 > 0)
						{
							matchingRuleFoundMatch.AddStudentPidAssignedTo(num3);
						}
						foreach (TimeTableItem timeTableItem in list2)
						{
							string dayOfWeek3CharacterShortLowerCase = MatchingRule.GetDayOfWeek3CharacterShortLowerCase(timeTableItem.DayOfWeek);
							string columnName = dayOfWeek3CharacterShortLowerCase + "startminutes";
							string columnName2 = dayOfWeek3CharacterShortLowerCase + "endminutes";
							if (row2[columnName] != DBNull.Value && row2[columnName2] != DBNull.Value)
							{
								int num4 = (int)row2[columnName];
								int num5 = (int)row2[columnName2];
								if (num4 <= timeTableItem.StartMinutes && num5 >= timeTableItem.EndMinutes)
								{
									if (matchingRuleFoundMatch.MatchedCount(timeTableItem) < 1)
									{
										matchingRuleFoundMatch.AddMatched(timeTableItem);
									}
								}
							}
						}
					}
					if (matchingRuleFoundMatch.MatchesTimeTableItemCount > 0)
					{
						list.Add(matchingRuleFoundMatch);
					}
				}
			}
			list.Sort((MatchingRuleFoundMatch c1, MatchingRuleFoundMatch c2) => c2.MatchedPercentage.CompareTo(c1.MatchedPercentage));
			return list;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001DC70 File Offset: 0x0001CC70
		private static List<int> GetAllEquivalentCourses(int lucid, UnivDataAdapter da)
		{
			SettingWithValueCollection settingWithValueCollection = Settings.LoadEveryoneSettings(da, new int[]
			{
				99633
			});
			SettingWithValue settingWithValue = settingWithValueCollection[99633];
			string text;
			if (settingWithValue != null)
			{
				text = settingWithValue.ValStr;
			}
			else
			{
				text = "1";
			}
			if (text.Equals("1"))
			{
				text = "";
			}
			string commandText = "SELECT lucourseid FROM equivalentcourses" + text + " (@lucid)";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			List<int> list = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int item = (int)dataRow["lucourseid"];
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001DDCC File Offset: 0x0001CDCC
		private DataTable LoadPotentialMatches(DateTime sdate, DateTime edate)
		{
			this.da.SelectCommand.CommandText = this.queryLoadMatchInfo_likeProfessionalNotetaker;
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@lucid", this.lucourseId);
			this.da.SelectCommand.Parameters.Add("@sptype", this.serviceProviderType);
			this.da.SelectCommand.Parameters.Add("@sdate", sdate);
			this.da.SelectCommand.Parameters.Add("@edate", edate);
			DataTable dataTable = new DataTable();
			this.da.Fill(dataTable);
			return this.tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"spfirstname",
				"splastname",
				"spstudent_no",
				"spemail",
				"phone1",
				"phone2",
				"notes1",
				"notes2",
				"specialization",
				"additionalservices"
			});
		}

		// Token: 0x04000263 RID: 611
		private MatchType matchType = MatchType.None;

		// Token: 0x04000264 RID: 612
		private bool partialOverlapOnSameDayAllowed = false;

		// Token: 0x04000265 RID: 613
		private bool partialFullDaysOverlapAllowed = true;

		// Token: 0x04000266 RID: 614
		private SortType sortType = SortType.None;

		// Token: 0x04000267 RID: 615
		private int lucourseId = 0;

		// Token: 0x04000268 RID: 616
		private int personId = 0;

		// Token: 0x04000269 RID: 617
		private int serviceProviderType = 0;

		// Token: 0x0400026A RID: 618
		private UnivDataAdapter da;

		// Token: 0x0400026B RID: 619
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400026C RID: 620
		private string queryLoadMatchInfo_likeProfessionalNotetaker = "SELECT\tx.sunstartminutes,x.sunendminutes\r\n\t\t,x.monstartminutes,x.monendminutes\r\n\t\t,x.tuestartminutes,x.tueendminutes\r\n\t\t,x.wedstartminutes,x.wedendminutes\r\n\t\t,x.thustartminutes,x.thuendminutes\r\n\t\t,x.fristartminutes,x.friendminutes\r\n\t\t,x.satstartminutes,x.satendminutes\r\n\t\t,x.serviceproviderid\r\n\t\t,sp.firstname AS spfirstname,sp.lastname AS splastname\r\n\t\t,sp.student_no AS spstudent_no,sp.email AS spemail,sp.phone1,sp.phone2\r\n\t\t,sp.notes1,sp.notes2,sp.specialization,sp.additionalservices,sp.phonenote\r\n\t\t,sp.[address]\r\n\t\t,spr.personid,spr.lucourseid \r\nFROM\r\n(\r\n\tSELECT\tDISTINCT spa.sunstartminutes,spa.sunendminutes\r\n\t\t\t,spa.monstartminutes,spa.monendminutes\r\n\t\t\t,spa.tuestartminutes,spa.tueendminutes\r\n\t\t\t,spa.wedstartminutes,spa.wedendminutes\r\n\t\t\t,spa.thustartminutes,spa.thuendminutes\r\n\t\t\t,spa.fristartminutes,spa.friendminutes\r\n\t\t\t,spa.satstartminutes,spa.satendminutes\r\n\t\t\t,spapp.serviceproviderid\r\n\tFROM\tServiceProviderAvailability spa \r\n\t\t\tLEFT JOIN ServiceProviderApplications spapp ON spapp.ServiceProviderApplicationId=spa.ServiceProviderApplicationId \r\n\tWHERE\tspa.isactive=1 \r\n\t\t\tAND NOT ( ( spa.enddate<@sdate ) OR (spa.startdate > @edate ) )\r\n\t\t\tAND spapp.serviceprovidertype=@sptype\r\n) x LEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=x.ServiceProviderId \r\nLEFT JOIN serviceproviderrequests spr ON spr.ServiceProviderId=x.ServiceProviderId AND spr.serviceprovidertype=@sptype\r\n                                AND NOT ( ( spr.enddate<@sdate ) OR (spr.startdate > @edate ) )";
	}
}
