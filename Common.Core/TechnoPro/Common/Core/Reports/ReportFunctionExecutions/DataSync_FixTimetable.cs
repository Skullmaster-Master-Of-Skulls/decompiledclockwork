using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000070 RID: 112
	public class DataSync_FixTimetable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00019908 File Offset: 0x00017B08
		public DataSync_FixTimetable()
		{
			this.dao = new ReportDAO(this.OpContext);
			this.SetupDayOfWeekMappings();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001992A File Offset: 0x00017B2A
		public DataSync_FixTimetable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
			this.SetupDayOfWeekMappings();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00019950 File Offset: 0x00017B50
		private void SetupDayOfWeekMappings()
		{
			this._dayOfWeekMappings = new Dictionary<DayOfWeek, string>
			{
				{
					DayOfWeek.Monday,
					"mon"
				},
				{
					DayOfWeek.Tuesday,
					"tue"
				},
				{
					DayOfWeek.Wednesday,
					"wed"
				},
				{
					DayOfWeek.Thursday,
					"thu"
				},
				{
					DayOfWeek.Friday,
					"fri"
				},
				{
					DayOfWeek.Saturday,
					"sat"
				},
				{
					DayOfWeek.Sunday,
					"sun"
				}
			};
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000199C4 File Offset: 0x00017BC4
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000199CC File Offset: 0x00017BCC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000474 RID: 1140 RVA: 0x000199D8 File Offset: 0x00017BD8
		private string TimeOfDayToString(TimeSpan ts)
		{
			return DateTime.Now.Date.Add(ts).ToString("HH:mm");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00019A10 File Offset: 0x00017C10
		private IList<DataSync_FixTimetable.DayOfWeekWithTimes> ParseDaysOfWeeksWithTimes(DataRow dr, DataSyncFixTimetableParameters dataSyncFixTimetableParameters)
		{
			bool timeIncludesDate = dataSyncFixTimetableParameters.TimeType == eDataSyncFixTimetableTimeType.StartDateTimeEndDateTime;
			TimeSpan? st = this.ParseTime(dr[dataSyncFixTimetableParameters.StartTimeColName].ToString().Trim(), timeIncludesDate);
			TimeSpan? et = this.ParseTime(dr[dataSyncFixTimetableParameters.EndTimeColName].ToString().Trim(), timeIncludesDate);
			bool flag = st == null || et == null;
			IList<DataSync_FixTimetable.DayOfWeekWithTimes> result;
			if (flag)
			{
				result = new List<DataSync_FixTimetable.DayOfWeekWithTimes>();
			}
			else
			{
				bool isDayOfWeekInSeparateColumns = dataSyncFixTimetableParameters.IsDayOfWeekInSeparateColumns;
				if (isDayOfWeekInSeparateColumns)
				{
					result = this.ParseDaysOfWeeksWithTimesMultipleColumns(dr, dataSyncFixTimetableParameters, st.Value, et.Value);
				}
				else
				{
					string text = dr[dataSyncFixTimetableParameters.DayOfWeekColName].ToString().Replace(" ", "").Replace(",", "").Replace(".", "").Trim().ToLower();
					text = this.FixDowString(text, dataSyncFixTimetableParameters.DayOfWeekType);
					result = (from h in text.ToCharArray().Select(new Func<char, DayOfWeek?>(this.ParseDayOfWeek))
					where h != null
					select h into m
					select new DataSync_FixTimetable.DayOfWeekWithTimes(m.Value, st.Value, et.Value)).ToList<DataSync_FixTimetable.DayOfWeekWithTimes>();
				}
			}
			return result;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00019B84 File Offset: 0x00017D84
		private DayOfWeek? ParseDayOfWeek(char c)
		{
			DayOfWeek? result;
			if (c != 'f')
			{
				switch (c)
				{
				case 'm':
					return new DayOfWeek?(DayOfWeek.Monday);
				case 'r':
					return new DayOfWeek?(DayOfWeek.Thursday);
				case 's':
					return new DayOfWeek?(DayOfWeek.Saturday);
				case 't':
					return new DayOfWeek?(DayOfWeek.Tuesday);
				case 'u':
					return new DayOfWeek?(DayOfWeek.Sunday);
				case 'w':
					return new DayOfWeek?(DayOfWeek.Wednesday);
				}
				result = null;
			}
			else
			{
				result = new DayOfWeek?(DayOfWeek.Friday);
			}
			return result;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00019C20 File Offset: 0x00017E20
		private string FixDowString(string s, eDataSyncFixTimetableDayOfWeekType dayOfWeekType)
		{
			string text = s;
			switch (dayOfWeekType)
			{
			case eDataSyncFixTimetableDayOfWeekType.MTWRFSU:
				break;
			case eDataSyncFixTimetableDayOfWeekType.MTWTHFSSU:
				text = text.Replace("th", "r");
				text = text.Replace("su", "u");
				break;
			case eDataSyncFixTimetableDayOfWeekType.MondayTuesdayWednesdayThursdayFridaySaturdaySunday:
				text = text.Replace("monday", "m");
				text = text.Replace("tuesday", "t");
				text = text.Replace("wednesday", "w");
				text = text.Replace("thursday", "r");
				text = text.Replace("friday", "f");
				text = text.Replace("saturday", "s");
				text = text.Replace("sunday", "u");
				break;
			case eDataSyncFixTimetableDayOfWeekType.MonTueWedThuFriSatSun:
				text = text.Replace("mon", "m");
				text = text.Replace("tue", "t");
				text = text.Replace("wed", "w");
				text = text.Replace("thu", "r");
				text = text.Replace("fri", "f");
				text = text.Replace("sat", "s");
				text = text.Replace("sun", "u");
				break;
			case eDataSyncFixTimetableDayOfWeekType.MoTuWeThFrSaSu:
				text = text.Replace("mo", "m");
				text = text.Replace("tu", "t");
				text = text.Replace("we", "w");
				text = text.Replace("th", "r");
				text = text.Replace("fr", "f");
				text = text.Replace("sa", "s");
				text = text.Replace("su", "u");
				break;
			default:
				throw new InvalidParameterException(string.Format("", ""));
			}
			return text;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00019E0C File Offset: 0x0001800C
		private TimeSpan? ParseTime(string s, bool timeIncludesDate)
		{
			bool flag = string.IsNullOrEmpty(s);
			TimeSpan? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s.Length == 4;
				if (flag2)
				{
					s = s.Substring(0, 2) + ":" + s.Substring(2);
				}
				string s2 = timeIncludesDate ? s : (DateTime.Now.ToString("yyyy-MM-dd") + " " + s);
				DateTime dateTime;
				bool flag3 = !DateTime.TryParse(s2, out dateTime);
				if (flag3)
				{
					result = null;
				}
				else
				{
					result = new TimeSpan?(dateTime.TimeOfDay);
				}
			}
			return result;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00019EB0 File Offset: 0x000180B0
		private IList<DataSync_FixTimetable.DayOfWeekWithTimes> ParseDaysOfWeeksWithTimesMultipleColumns(DataRow dr, DataSyncFixTimetableParameters dataSyncFixTimetableParameters, TimeSpan st, TimeSpan et)
		{
			Dictionary<string, string> source = dataSyncFixTimetableParameters.DayOfWeekColName.Split(new char[]
			{
				','
			}).Select(delegate(string g)
			{
				string text = g.Trim();
				int num = text.IndexOf('=');
				return new string[]
				{
					text.Substring(0, num).ToLower().Trim(),
					text.Substring(num + 1).Trim()
				};
			}).ToDictionary((string[] g) => g[0], (string[] g) => g[1]);
			return (from g in source
			select (dr[g.Value].ToString().Trim().Length > 0) ? g.Key.ToLower().Trim() : "" into h
			where h.Length > 0
			select h into h2
			select this.ParseDayOfWeek(h2[0]) into h3
			where h3 != null
			select h3 into m
			select new DataSync_FixTimetable.DayOfWeekWithTimes(m.Value, st, et)).ToList<DataSync_FixTimetable.DayOfWeekWithTimes>();
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00019FE4 File Offset: 0x000181E4
		private void AddColumnToTable(ref DataTable t, string colName)
		{
			bool flag = t.Columns.Contains(colName);
			if (!flag)
			{
				t.Columns.Add(colName);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001A014 File Offset: 0x00018214
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				bool flag2 = string.IsNullOrEmpty(primaryDataTable.TableName);
				if (flag2)
				{
					primaryDataTable.TableName = "t";
				}
				DataTable dataTable = primaryDataTable;
				try
				{
					string defaultFunctionParameter = function.GetDefaultFunctionParameter();
					DataSyncFixTimetableParameters dataSyncFixTimetableParameters = defaultFunctionParameter.ConvertXmlToDataSyncFixTimetableParameters();
					this.AddColumnToTable(ref dataTable, "dayofweek");
					this.AddColumnToTable(ref dataTable, "starttime");
					this.AddColumnToTable(ref dataTable, "endtime");
					DataTable dataTable2 = dataTable.Clone();
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						IList<DataSync_FixTimetable.DayOfWeekWithTimes> list = this.ParseDaysOfWeeksWithTimes(dataRow, dataSyncFixTimetableParameters);
						bool flag3 = list.Count < 1;
						if (flag3)
						{
							dataTable2.ImportRow(dataRow);
						}
						else
						{
							foreach (DataSync_FixTimetable.DayOfWeekWithTimes dayOfWeekWithTimes in list)
							{
								dataRow["dayofweek"] = this._dayOfWeekMappings[dayOfWeekWithTimes.Dow];
								dataRow["starttime"] = this.TimeOfDayToString(dayOfWeekWithTimes.StartTime);
								dataRow["endtime"] = this.TimeOfDayToString(dayOfWeekWithTimes.EndTime);
								dataTable2.ImportRow(dataRow);
							}
						}
					}
					result.Data.Table = dataTable2;
				}
				catch (Exception ex)
				{
					string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_FixTimetable:err={0}", ex.ToString());
					result.Result = new RunFunctionResult
					{
						Status = new RunStatus
						{
							ErrorMessage = text,
							LastStatusStep = eRunStatusStep.Failed
						},
						Function = function
					};
					CWLogger.Logger.Error(text);
				}
			}
		}

		// Token: 0x040000D1 RID: 209
		private ReportDAO dao;

		// Token: 0x040000D2 RID: 210
		private Dictionary<DayOfWeek, string> _dayOfWeekMappings;

		// Token: 0x0200021E RID: 542
		internal class DayOfWeekWithTimes
		{
			// Token: 0x060012E5 RID: 4837 RVA: 0x0000672B File Offset: 0x0000492B
			public DayOfWeekWithTimes()
			{
			}

			// Token: 0x060012E6 RID: 4838 RVA: 0x0007FECD File Offset: 0x0007E0CD
			public DayOfWeekWithTimes(DayOfWeek dow, TimeSpan st, TimeSpan et)
			{
				this.Dow = dow;
				this.StartTime = st;
				this.EndTime = et;
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0007FEEF File Offset: 0x0007E0EF
			// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0007FEF7 File Offset: 0x0007E0F7
			public DayOfWeek Dow { get; set; }

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0007FF00 File Offset: 0x0007E100
			// (set) Token: 0x060012EA RID: 4842 RVA: 0x0007FF08 File Offset: 0x0007E108
			public TimeSpan StartTime { get; set; }

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x060012EB RID: 4843 RVA: 0x0007FF11 File Offset: 0x0007E111
			// (set) Token: 0x060012EC RID: 4844 RVA: 0x0007FF19 File Offset: 0x0007E119
			public TimeSpan EndTime { get; set; }
		}
	}
}
