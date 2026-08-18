using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.AvailabilitySchedule;
using TechnoPro.Common.DAO.Entity.AvailabilitySchedule;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.DAO.Impl.AvailabilitySchedule
{
	// Token: 0x0200011F RID: 287
	public class AvailabilityScheduleDAO : IAvailabilityScheduleDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00053482 File Offset: 0x00051682
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0005348A File Offset: 0x0005168A
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00053494 File Offset: 0x00051694
		private PeopleDAO peopleDAO
		{
			get
			{
				bool flag = this.pd == null;
				if (flag)
				{
					this.pd = new PeopleDAO(this.OpContext);
				}
				return this.pd;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000534CA File Offset: 0x000516CA
		public AvailabilityScheduleDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000534FB File Offset: 0x000516FB
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00053503 File Offset: 0x00051703
		public OperationContext OpContext { get; set; }

		// Token: 0x06000824 RID: 2084 RVA: 0x0005350C File Offset: 0x0005170C
		private AvailabilityGroup GetAvailabilityScheduleGroupFromRecord(IDataReader record)
		{
			bool flag = record == null;
			AvailabilityGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["availabilitygroupid"] == DBNull.Value) ? 0 : ((int)record["availabilitygroupid"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new AvailabilityGroup
					{
						AvailabilityGroupId = num,
						Title = (PeopleDAO.ReaderContainsColumn(record, "availabilitygrouptitle") ? record["availabilitygrouptitle"].ToString() : record["availabilitytitle"].ToString()),
						Description = (PeopleDAO.ReaderContainsColumn(record, "availabilitygroupdescription") ? record["availabilitygroupdescription"].ToString() : record["availabilitydescription"].ToString()),
						ColourArgB = (int)record["colour"],
						Pattern = (int)record["pattern"]
					};
				}
			}
			return result;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0005360C File Offset: 0x0005180C
		private List<AvailabilityScheduleItemInfo> GetAvailabilityScheduleItemsFromRecord(IDataReader record)
		{
			bool flag = record == null || record["availability"] is DBNull;
			List<AvailabilityScheduleItemInfo> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AvailabilityTimeStorage availability = new AvailabilityTimeStorage
				{
					AvailabilityBytes = (byte[])record["availability"],
					AvailabilityBoundariesBytes = ((record["AvailabilityBoundaries"] is DBNull) ? null : ((byte[])record["AvailabilityBoundaries"]))
				};
				IList<Range<TimeSpan>> source = availability.ConvertCompressedTimesToTimespanRanges();
				DateTime date = ((DateTime)record["availabilitydate"]).Date;
				result = (from g in source
				select new AvailabilityScheduleItemInfo
				{
					DayAndTime = new AvailabilityScheduleDateAndTime
					{
						Date = date,
						Time = new AvailabilityScheduleTime
						{
							StartTime = g.Start,
							EndTime = g.End
						}
					}
				}).ToList<AvailabilityScheduleItemInfo>();
			}
			return result;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000536D8 File Offset: 0x000518D8
		private AvailabilityScheduleItemsForContext GetAvailabilityScheduleItemsForContextFromReader(AvailabilityScheduleContext context, IDataReader reader)
		{
			bool flag = reader == null;
			AvailabilityScheduleItemsForContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<AvailabilityScheduleItemInfo> list = new List<AvailabilityScheduleItemInfo>();
				while (reader.Read())
				{
					List<AvailabilityScheduleItemInfo> availabilityScheduleItemsFromRecord = this.GetAvailabilityScheduleItemsFromRecord(reader);
					bool flag2 = availabilityScheduleItemsFromRecord == null || availabilityScheduleItemsFromRecord.Count < 1;
					if (!flag2)
					{
						list.AddRange(availabilityScheduleItemsFromRecord);
					}
				}
				result = new AvailabilityScheduleItemsForContext
				{
					Context = context,
					AvailabilityScheduleItems = list
				};
			}
			return result;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00053748 File Offset: 0x00051948
		[DebuggerStepThrough]
		private Task<AvailabilityScheduleItemsForContext> GetAvailabilityScheduleItemsForContextFromReaderAsync(AvailabilityScheduleContext context, DbDataReader reader)
		{
			AvailabilityScheduleDAO.<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__15 <GetAvailabilityScheduleItemsForContextFromReaderAsync>d__ = new AvailabilityScheduleDAO.<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__15();
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AvailabilityScheduleItemsForContext>.Create();
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.<>4__this = this;
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.context = context;
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.reader = reader;
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.<>1__state = -1;
			<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.<>t__builder.Start<AvailabilityScheduleDAO.<GetAvailabilityScheduleItemsForContextFromReaderAsync>d__15>(ref <GetAvailabilityScheduleItemsForContextFromReaderAsync>d__);
			return <GetAvailabilityScheduleItemsForContextFromReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0005379C File Offset: 0x0005199C
		private AvailabilityScheduleContext GetAvailabilityContextFromRecord(IDataRecord record)
		{
			bool flag = record == null;
			AvailabilityScheduleContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AvailabilityScheduleContext
				{
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					AvailabilityGroupId = ((record["availabilitygroupid"] is DBNull) ? 0 : ((int)record["availabilitygroupid"]))
				};
			}
			return result;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0005381C File Offset: 0x00051A1C
		public IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", (from g in personIds
			select g.ToString()).ToArray<string>()));
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@gids";
			DbType pType = DbType.String;
			object value;
			if (availabilityGroupIds != null)
			{
				value = string.Join(",", (from g in availabilityGroupIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			array[2] = databaseLayer.GetParameter("@sd", DbType.DateTime, startDate.Date);
			array[3] = databaseLayer.GetParameter("@ed", DbType.DateTime, startDate.Date.AddDays((double)(numDays - 1)));
			DbParameter[] parameters = array;
			IList<AvailabilityScheduleItemsForContext> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS availabilitygroupid INTO #t2 FROM splitorderids(COALESCE(@gids,''),',');\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\nWHERE       av.personid IN (SELECT personid FROM #t1) \r\n            AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\n            AND (@gids IS NULL OR ag.availabilitygroupid IN (SELECT availabilitygroupid FROM #t2))\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate;\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AvailabilityScheduleItemsForContext> list = new List<AvailabilityScheduleItemsForContext>();
					AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = null;
					List<AvailabilityScheduleItemInfo> list2 = null;
					while (dataReader.Read())
					{
						AvailabilityScheduleContext availabilityContextFromRecord = this.GetAvailabilityContextFromRecord(dataReader);
						bool flag2 = availabilityScheduleItemsForContext == null || availabilityScheduleItemsForContext.Context.PersonId != availabilityContextFromRecord.PersonId || availabilityScheduleItemsForContext.Context.AvailabilityGroupId != availabilityContextFromRecord.AvailabilityGroupId;
						if (flag2)
						{
							list2 = new List<AvailabilityScheduleItemInfo>();
							availabilityScheduleItemsForContext = new AvailabilityScheduleItemsForContext
							{
								Context = availabilityContextFromRecord,
								AvailabilityScheduleItems = list2
							};
							list.Add(availabilityScheduleItemsForContext);
						}
						List<AvailabilityScheduleItemInfo> availabilityScheduleItemsFromRecord = this.GetAvailabilityScheduleItemsFromRecord(dataReader);
						bool flag3 = availabilityScheduleItemsFromRecord == null || availabilityScheduleItemsFromRecord.Count < 1;
						if (!flag3)
						{
							list2.AddRange(availabilityScheduleItemsFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00053A20 File Offset: 0x00051C20
		[DebuggerStepThrough]
		public Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays)
		{
			AvailabilityScheduleDAO.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__18 <LoadAvailabilityForMultipleContextsAndDatesAsync>d__ = new AvailabilityScheduleDAO.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__18();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<AvailabilityScheduleItemsForContext>>.Create();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>4__this = this;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.personIds = personIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.availabilityGroupIds = availabilityGroupIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.startDate = startDate;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.numDays = numDays;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>1__state = -1;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Start<AvailabilityScheduleDAO.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__18>(ref <LoadAvailabilityForMultipleContextsAndDatesAsync>d__);
			return <LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00053A84 File Offset: 0x00051C84
		public IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, PersonId);
			array[1] = databaseLayer.GetParameter("@gids", DbType.String, string.Join(",", (from g in AvailabilityGroupIds
			select g.ToString()).ToArray<string>()));
			array[2] = databaseLayer.GetParameter("@sd", DbType.DateTime, StartDate.Date);
			array[3] = databaseLayer.GetParameter("@ed", DbType.DateTime, EndDate.Date);
			DbParameter[] parameters = array;
			IList<DateTime> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT orderid AS availabilitygroupid INTO #t2 FROM splitorderids(COALESCE(@gids,''),',');\r\n\r\nSELECT  DISTINCT av.availabilitydate \r\nFROM    availabilityschedule av \r\nWHERE   av.personid=@pid \r\n        AND av.availabilitygroupid IN (SELECT availabilitygroupid FROM #t2)\r\n        AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\nORDER BY av.availabilitydate;\r\n\r\nDROP TABLE #t2", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DateTime> list = new List<DateTime>();
					while (dataReader.Read())
					{
						bool flag2 = dataReader["availabilitydate"] is DBNull;
						if (!flag2)
						{
							list.Add(((DateTime)dataReader["availabilitydate"]).Date);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00053BD4 File Offset: 0x00051DD4
		public IList<AvailabilityGroup> LoadAllAvailabilityGroups()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<AvailabilityGroup> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT availabilitygroupid,availabilitytitle,availabilitydescription,colour,pattern FROM availabilitygroup ORDER BY availabilitytitle"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<AvailabilityGroup> list = new List<AvailabilityGroup>();
					while (dataReader.Read())
					{
						AvailabilityGroup availabilityScheduleGroupFromRecord = this.GetAvailabilityScheduleGroupFromRecord(dataReader);
						bool flag2 = availabilityScheduleGroupFromRecord != null;
						if (flag2)
						{
							list.Add(availabilityScheduleGroupFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00053C6C File Offset: 0x00051E6C
		[DebuggerStepThrough]
		public Task<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByContextAndDateRangeAsync(AvailabilityScheduleContext context, DateTime startDate, int numDays)
		{
			AvailabilityScheduleDAO.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__21 <LoadAvailabilityItemsByContextAndDateRangeAsync>d__ = new AvailabilityScheduleDAO.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__21();
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AvailabilityScheduleItemsForContext>.Create();
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>4__this = this;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.context = context;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.startDate = startDate;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.numDays = numDays;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>1__state = -1;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder.Start<AvailabilityScheduleDAO.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__21>(ref <LoadAvailabilityItemsByContextAndDateRangeAsync>d__);
			return <LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00053CC8 File Offset: 0x00051EC8
		public AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContext context, DateTime startDate, int numDays)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, context.PersonId),
				databaseLayer.GetParameter("@groupid", DbType.Int32, context.AvailabilityGroupId),
				databaseLayer.GetParameter("@sd", DbType.DateTime, startDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, startDate.Date.AddDays((double)(numDays - 1)))
			};
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContextFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\nWHERE       av.personid=@pid \r\n            AND av.availabilitydate>=@startdate AND av.availabilitydate<@enddate\r\n            AND ag.availabilitygroupid=@groupid\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate", parameters))
			{
				availabilityScheduleItemsForContextFromReader = this.GetAvailabilityScheduleItemsForContextFromReader(context, dataReader);
			}
			return availabilityScheduleItemsForContextFromReader;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00053DA8 File Offset: 0x00051FA8
		public AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContext context, IList<DateTime> days)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, context.PersonId);
			array[1] = databaseLayer.GetParameter("@groupid", DbType.Int32, context.AvailabilityGroupId);
			array[2] = databaseLayer.GetParameter("@dates", DbType.String, string.Join(",", (from g in days
			select g.ToString("yyyy-MM-dd")).ToArray<string>()));
			DbParameter[] parameters = array;
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContextFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT date AS dt INTO #t1 FROM splitdates(@dates)\r\n\r\nSELECT    av.availabilityscheduleid,av.availabilitygroupid,ag.availabilitytitle AS availabilitygrouptitle,\r\n            ag.availabilitydescription AS availabilitygroupdescription,ag.colour,ag.pattern,\r\n            av.personid,p.firstname,p.lastname,p.student_no,p.middlename,\r\n            av.availabilitydate,av.availabilitysubcode,av.availability,\r\n            NULL AS roompersonid,NULL AS roomfirstname,NULL AS roomlastname,NULL AS roomstudent_no,\r\n            av.AvailabilityBoundaries,#t1.dt\r\nFROM        availabilityschedule av LEFT JOIN availabilitygroup ag ON ag.availabilitygroupid=av.availabilitygroupid\r\n            LEFT JOIN people p ON p.personid=av.personid\r\n\t\t\tLEFT JOIN #t1 ON #t1.dt=av.availabilitydate\r\nWHERE       av.personid=@pid \r\n\t\t\tAND ag.availabilitygroupid=@groupid\r\n\t\t\tAND NOT #t1.dt IS NULL\r\nORDER BY    av.personid,av.availabilitygroupid,av.availabilitydate\r\n\r\nDROP TABLE #t1", parameters))
			{
				availabilityScheduleItemsForContextFromReader = this.GetAvailabilityScheduleItemsForContextFromReader(context, dataReader);
			}
			return availabilityScheduleItemsForContextFromReader;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00053E88 File Offset: 0x00052088
		public void ResetAvailabilityByContextAndDate(AvailabilityScheduleContext context, DateTime date, IList<Range<TimeSpan>> newTimes)
		{
			bool flag = newTimes == null || newTimes.Count < 1;
			if (flag)
			{
				this.ClearAvailabilityForTheDay(context, new List<DateTime>
				{
					date
				});
			}
			else
			{
				AvailabilityTimeStorage availabilityTimeStorage = newTimes.ConvertTimespanRangesToCompressedTimes();
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@pid", DbType.Int32, context.PersonId),
					databaseLayer.GetParameter("@gid", DbType.Int32, context.AvailabilityGroupId),
					databaseLayer.GetParameter("@date", DbType.DateTime, date),
					databaseLayer.GetParameter("@availability", DbType.Binary, availabilityTimeStorage.AvailabilityBytes),
					databaseLayer.GetParameter("@availabilityBoundaries", DbType.Binary, availabilityTimeStorage.AvailabilityBoundariesBytes)
				};
				databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT availabilityscheduleid FROM availabilityschedule WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date)\r\n    UPDATE availabilityschedule SET availability=@availability,AvailabilityBoundaries=@availabilityBoundaries WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date\r\nELSE \r\n    INSERT INTO availabilityschedule (personid,availabilitygroupid,availabilitydate,availability,AvailabilityBoundaries) \r\n        VALUES (@pid,@gid,@date,@availability,@availabilityBoundaries)", parameters);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00053F70 File Offset: 0x00052170
		public void ClearAvailabilityForTheDay(AvailabilityScheduleContext context, IList<DateTime> days)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			foreach (DateTime dateTime in days)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@pid", DbType.Int32, context.PersonId),
					databaseLayer.GetParameter("@gid", DbType.Int32, context.AvailabilityGroupId),
					databaseLayer.GetParameter("@date", DbType.DateTime, dateTime)
				};
				databaseLayer.ExecuteNonQuery("DELETE FROM AvailabilitySchedule WHERE personid=@pid AND availabilitygroupid=@gid AND availabilitydate=@date", parameters);
			}
		}

		// Token: 0x040004D5 RID: 1237
		private PeopleDAO pd;
	}
}
