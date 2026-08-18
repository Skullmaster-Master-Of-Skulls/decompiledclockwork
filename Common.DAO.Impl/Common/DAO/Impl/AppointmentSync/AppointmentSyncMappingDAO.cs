using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Impl.AppointmentSync
{
	// Token: 0x0200013D RID: 317
	public class AppointmentSyncMappingDAO : IAppointmentSyncMappingDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0005EF30 File Offset: 0x0005D130
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0005EF38 File Offset: 0x0005D138
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0005EF41 File Offset: 0x0005D141
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0005EF49 File Offset: 0x0005D149
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000930 RID: 2352 RVA: 0x0005EF52 File Offset: 0x0005D152
		public AppointmentSyncMappingDAO(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0005EF84 File Offset: 0x0005D184
		public ClockWorkExternalAppMapping LoadMappingByExternalUniqueAppointmentId(string externalUniqueAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@uniqueappointmentid", DbType.String, externalUniqueAppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @uniqueappointmentid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0005F000 File Offset: 0x0005D200
		public ClockWorkExternalAppMapping LoadMappingByExternalUniqueAppointmentId2(string uniqueid2)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@uniqueid2", DbType.String, uniqueid2)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.UniqueId2 COLLATE Latin1_General_CS_AS = @uniqueid2", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0005F07C File Offset: 0x0005D27C
		public IList<ClockWorkExternalAppMapping> LoadMappingByExternalMasterRecurrenceAppointmentId(string masterRecurrenceAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@masterrecurrenceappointmentid", DbType.String, masterRecurrenceAppointmentId)
			};
			List<ClockWorkExternalAppMapping> list = new List<ClockWorkExternalAppMapping>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.MasterRecurrenceAppointmentId COLLATE Latin1_General_CS_AS = @masterrecurrenceappointmentid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkExternalAppMapping mappingFromReader = AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
						bool flag2 = mappingFromReader != null;
						if (flag2)
						{
							list.Add(mappingFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0005F11C File Offset: 0x0005D31C
		public void CreateMapping(ClockWorkExternalAppMapping mapping)
		{
			bool flag = mapping.ClockWorkLastUpdatedDate != null;
			DbParameter parameter;
			if (flag)
			{
				DateTime value = mapping.ClockWorkLastUpdatedDate.Value;
				mapping.ClockWorkLastUpdatedDate = new DateTime?(new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second));
				parameter = this.DatabaseManager.GetParameter("@clockworklastmodifieddate", DbType.DateTime, value);
			}
			else
			{
				parameter = this.DatabaseManager.GetParameter("@clockworklastmodifieddate", DbType.DateTime, DBNull.Value);
			}
			bool flag2 = mapping.ExternalApplicationLastUpdatedDate != null;
			DbParameter parameter2;
			if (flag2)
			{
				DateTime value2 = mapping.ExternalApplicationLastUpdatedDate.Value;
				mapping.ExternalApplicationLastUpdatedDate = new DateTime?(new DateTime(value2.Year, value2.Month, value2.Day, value2.Hour, value2.Minute, value2.Second));
				parameter2 = this.DatabaseManager.GetParameter("@outlooklastmodifieddate", DbType.DateTime, value2);
			}
			else
			{
				parameter2 = this.DatabaseManager.GetParameter("@outlooklastmodifieddate", DbType.DateTime, DBNull.Value);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, mapping.ClockWorkAppointmentId),
				parameter,
				this.DatabaseManager.GetParameter("@outlookglobalappointmentid", DbType.String, mapping.ExternalApplicationUniqueAppointmentId ?? string.Empty),
				parameter2,
				this.DatabaseManager.GetParameter("@uniqueid2", DbType.String, mapping.ExternalApplicationUniqueAppointmentId2 ?? string.Empty),
				this.DatabaseManager.GetParameter("@masterrecurrenceappointmentid", DbType.String, mapping.ExternalApplicationMasterRecurrenceAppointmentId ?? string.Empty)
			};
			this.DatabaseManager.ExecuteNonQuery("IF NOT EXISTS(SELECT clockworkappointmentid FROM appointmentmappingsclockworkoutlook WHERE clockworkappointmentid=@clockworkappointmentid AND outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid)\r\nINSERT INTO AppointmentMappingsClockWorkOutlook (clockworkappointmentid,clockworklastmodifieddate,outlookglobalappointmentid,outlooklastmodifieddate,uniqueid2,MasterRecurrenceAppointmentId)\r\nVALUES (@clockworkappointmentid,@clockworklastmodifieddate,@outlookglobalappointmentid,@outlooklastmodifieddate,@uniqueid2,@masterrecurrenceappointmentid)", parameters);
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@uniqueid", DbType.String, mapping.ExternalApplicationUniqueAppointmentId),
				this.DatabaseManager.GetParameter("@globalappointmentid", DbType.String, mapping.ExternalApplicationGlobalAppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO AppointmentMappingsClockWorkOutlookGlobalAppointmentId (uniqueid,OutlookGlobalAppointmentId) VALUES (@uniqueid,@globalappointmentid)", parameters);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0005F354 File Offset: 0x0005D554
		public void UpdateMappingClockWorkChange(int clockworkAppId, DateTime newLastDateModified)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, clockworkAppId),
				databaseLayer.GetParameter("@clockworklastmodifieddate", DbType.DateTime, new DateTime(newLastDateModified.Year, newLastDateModified.Month, newLastDateModified.Day, newLastDateModified.Hour, newLastDateModified.Minute, newLastDateModified.Second))
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmentmappingsclockworkoutlook SET clockworklastmodifieddate=@clockworklastmodifieddate\r\nWHERE clockworkappointmentid=@clockworkappointmentid", parameters);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0005F3F4 File Offset: 0x0005D5F4
		public void UpdateMappingExternalChange(ExternalAppointmentId exAppId, DateTime newLastDateModified)
		{
			CWLogger.Logger.Debug(string.Format("AppointmentSyncMappingDAO::UpdateMappingExternalChange: newLastDateModified.year={0}, month={1}, day={2}, hours={3}, minutes={4}, seconds={5}, kind={6}, exAppId.cwappid={7}, globalappid={8}, uniqueid={9}, uniqueid2={10}", new object[]
			{
				newLastDateModified.Year,
				newLastDateModified.Month,
				newLastDateModified.Day,
				newLastDateModified.Hour,
				newLastDateModified.Minute,
				newLastDateModified.Second,
				newLastDateModified.Kind,
				exAppId.ClockWorkAppId,
				exAppId.GlobalAppId ?? string.Empty,
				exAppId.UniqueId ?? string.Empty,
				exAppId.UniqueId2 ?? string.Empty
			}));
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, exAppId.ClockWorkAppId),
				this.DatabaseManager.GetParameter("@outlookglobalappointmentid", DbType.String, exAppId.UniqueId ?? string.Empty),
				this.DatabaseManager.GetParameter("@uniqueid2", DbType.String, exAppId.UniqueId2 ?? string.Empty),
				databaseLayer.GetParameter("@outlooklastmodifieddate", DbType.DateTime, new DateTime(newLastDateModified.Year, newLastDateModified.Month, newLastDateModified.Day, newLastDateModified.Hour, newLastDateModified.Minute, newLastDateModified.Second))
			};
			this.DatabaseManager.ExecuteNonQuery("if not exists (select uniqueid2 from appointmentmappingsclockworkoutlook where ClockWorkAppointmentId=@clockworkappointmentid and (UniqueId2 is not null or UniqueId2 <> ''))\r\nbegin\r\n UPDATE appointmentmappingsclockworkoutlook set UniqueId2=@uniqueid2 where ClockWorkAppointmentId=@clockworkappointmentid\r\nend\r\n\r\nUPDATE appointmentmappingsclockworkoutlook SET outlooklastmodifieddate=@outlooklastmodifieddate \r\nWHERE (( not uniqueid2 is null and @uniqueid2 <> '' and uniqueid2 COLLATE Latin1_General_CS_AS = @uniqueid2 ) or outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid)", parameters);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0005F5AC File Offset: 0x0005D7AC
		public void DeleteMapping(ClockWorkExternalAppMapping mapping)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, mapping.ClockWorkAppointmentId),
				this.DatabaseManager.GetParameter("@outlookglobalappointmentid", DbType.String, mapping.ExternalApplicationUniqueAppointmentId ?? string.Empty),
				this.DatabaseManager.GetParameter("uniqueid2", DbType.String, mapping.ExternalApplicationUniqueAppointmentId2 ?? string.Empty)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmentmappingsclockworkoutlook WHERE clockworkappointmentid=@clockworkappointmentid AND (( not uniqueid2 is null and @uniqueid2 <> '' and uniqueid2 COLLATE Latin1_General_CS_AS = @uniqueid2 ) or (not outlookglobalappointmentid is NULL and @outlookglobalappointmentid <> '' and  outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid))", parameters);
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@uniqueid", DbType.String, mapping.ExternalApplicationUniqueAppointmentId ?? string.Empty),
				this.DatabaseManager.GetParameter("@globalappointmentid", DbType.String, mapping.ExternalApplicationGlobalAppointmentId ?? string.Empty)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM AppointmentMappingsClockWorkOutlookGlobalAppointmentId WHERE not uniqueid is NULL and @uniqueid <> '' and uniqueid COLLATE Latin1_General_CS_AS = @uniqueid AND not OutlookGlobalAppointmentId is NULL and @globalappointmentid <> '' and OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @globalappointmentid", parameters);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0005F69C File Offset: 0x0005D89C
		public ClockWorkExternalAppMapping LoadMappingByClockWorkAppointmentId(int clockWorkAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, clockWorkAppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.ClockWorkAppointmentId = @clockworkappointmentid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					if (dataReader.Read())
					{
						ClockWorkExternalAppMapping mappingFromReader = AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
						bool flag2 = mappingFromReader != null;
						if (flag2)
						{
							return mappingFromReader;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0005F738 File Offset: 0x0005D938
		public ClockWorkExternalAppMapping LoadMappingByExternalGlobalAppointmentId(string externalGlobalAppointmentId)
		{
			bool flag = string.IsNullOrEmpty(externalGlobalAppointmentId);
			ClockWorkExternalAppMapping result;
			if (flag)
			{
				CWLogger.Logger.Debug("AppointmentSyncMappingDAO::LoadMappingByExternalGlobalAppointmentId: externalGlobalAppointmentId is NULL");
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@outlookglobalappointmentid", DbType.String, externalGlobalAppointmentId)
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId  where appidmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0005F7D4 File Offset: 0x0005D9D4
		public string LoadUniqueIdByGlobalAppointmentId(string globalAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.String, globalAppointmentId)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("SELECT uniqueid FROM AppointmentMappingsClockWorkOutlookGlobalAppointmentId WHERE OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @id", parameters);
			bool flag = dataTable.Rows.Count > 0;
			string result;
			if (flag)
			{
				result = dataTable.Rows[0][0].ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0005F844 File Offset: 0x0005DA44
		public void UpdateMappingsLookupTable(string oldUniqueId, string newUniqueId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@olduniqueid", DbType.String, oldUniqueId),
				this.DatabaseManager.GetParameter("@newuniqueid", DbType.String, newUniqueId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE AppointmentMappingsClockWorkOutlookGlobalAppointmentId SET uniqueid=@newuniqueid WHERE uniqueid COLLATE Latin1_General_CS_AS = @olduniqueid", parameters);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0005F898 File Offset: 0x0005DA98
		public void UpdateMappingsTable(string oldUniqueId, string newUniqueId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@olduniqueid", DbType.String, oldUniqueId),
				this.DatabaseManager.GetParameter("@newuniqueid", DbType.String, newUniqueId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE AppointmentMappingsClockWorkOutlook SET OutlookGlobalAppointmentId =@newuniqueid WHERE OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @olduniqueid", parameters);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0005F8EC File Offset: 0x0005DAEC
		public void UpdateMappingsTable(int cwappid, string uniqueId, string newUniqueId2)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@clockworkappointmentid", DbType.Int32, cwappid),
				this.DatabaseManager.GetParameter("@uniqueid", DbType.String, uniqueId ?? string.Empty),
				this.DatabaseManager.GetParameter("@uniqueid2", DbType.String, newUniqueId2 ?? string.Empty)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE appointmentmappingsclockworkoutlook SET UniqueId2=@uniqueid2 \r\nWHERE clockworkappointmentid=@clockworkappointmentid AND outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @uniqueid", parameters);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0005F96C File Offset: 0x0005DB6C
		public IList<ClockWorkExternalAppMapping> LoadAllMappingsWithNoUniqueId2()
		{
			List<ClockWorkExternalAppMapping> list = new List<ClockWorkExternalAppMapping>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select am.ClockWorkAppointmentId as appointmentid, am.OutlookGlobalAppointmentId as UniqueId, am.UniqueId2,am.MasterRecurrenceAppointmentId,\r\n\t   am.OutlookLastModifiedDate as outlooklastupdateddate, am.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook am\r\ninner join Appointments a on a.AppointmentID = am.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non am.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere a.startDate >= DATEADD(DAY, -14, GETDATE()) and (am.UniqueId2 is NULL or am.UniqueId2 = '')"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkExternalAppMapping mappingFromReader = AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
						bool flag2 = mappingFromReader != null;
						if (flag2)
						{
							list.Add(mappingFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0005F9E8 File Offset: 0x0005DBE8
		public IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			List<ClockWorkExternalAppMapping> list = new List<ClockWorkExternalAppMapping>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select appm.UniqueId2, appm.ClockWorkAppointmentId AS appointmentid,appm.OutlookGlobalAppointmentId as UniqueId,appm.MasterRecurrenceAppointmentId,\r\nappm.OutlookLastModifiedDate as outlooklastupdateddate, appm.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\nappidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook appm\r\nleft join appointments app on app.AppointmentID=appm.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap on appm.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere app.startDate >= @startdate AND app.startDate<=@enddate and (appm.UniqueId2 is not null and appm.UniqueId2 <> '')\r\nAND NOT appm.uniqueid2 IS NULL AND appm.uniqueid2 COLLATE Latin1_General_CS_AS IN \r\n    (select appm.uniqueid2\r\n    from AppointmentMappingsClockWorkOutlook appm\r\n    left join appointments app on app.appointmentid = appm.clockworkappointmentid\r\n    where uniqueid2 is not null and UniqueId2 <> '' and app.startDate >= GETDATE()\r\n    group by UniqueId2, app.startdate\r\n    having count(appm.ClockWorkAppointmentId) > 1)\r\norder by appm.UniqueId2, appm.ClockWorkAppointmentId", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkExternalAppMapping mappingFromReader = AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
						bool flag2 = mappingFromReader != null;
						if (flag2)
						{
							list.Add(mappingFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0005FAB4 File Offset: 0x0005DCB4
		public IList<ClockWorkExternalAppMapping> FindDuplicateMappingsOneClockWorkMultipleExternal(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			SyncOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			List<ClockWorkExternalAppMapping> list = new List<ClockWorkExternalAppMapping>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select appm.UniqueId2, appm.ClockWorkAppointmentId AS appointmentid,appm.OutlookGlobalAppointmentId as UniqueId,appm.MasterRecurrenceAppointmentId,\r\nappm.OutlookLastModifiedDate as outlooklastupdateddate, appm.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\nappidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook appm\r\nleft join appointments app on app.AppointmentID=appm.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap on appm.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere app.startDate >= @startdate AND app.startDate<=@enddate and (appm.UniqueId2 is not null and appm.UniqueId2 <> '')\r\nAND appm.ClockWorkAppointmentId IN\r\n    (select appm.ClockWorkAppointmentId\r\n    from AppointmentMappingsClockWorkOutlook appm\r\n    left join appointments app on app.appointmentid = appm.clockworkappointmentid\r\n    where uniqueid2 is not null and app.startDate >= GETDATE()\r\n    group by ClockWorkAppointmentId, app.startdate\r\n    having count(uniqueid2) > 1)\r\norder by appm.ClockWorkAppointmentId,appm.UniqueId2", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkExternalAppMapping mappingFromReader = AppointmentSyncMappingDAO.GetMappingFromReader(dataReader);
						bool flag2 = mappingFromReader != null;
						if (flag2)
						{
							list.Add(mappingFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0005FB80 File Offset: 0x0005DD80
		internal static ClockWorkExternalAppMapping GetMappingFromReader(IDataReader reader)
		{
			ClockWorkExternalAppMapping clockWorkExternalAppMapping = new ClockWorkExternalAppMapping
			{
				ClockWorkAppointmentId = 0,
				ExternalApplicationUniqueAppointmentId = "",
				ExternalApplicationUniqueAppointmentId2 = "",
				ExternalApplicationGlobalAppointmentId = "",
				ExternalApplicationMasterRecurrenceAppointmentId = ""
			};
			object obj = reader["UniqueId"];
			bool flag = obj != DBNull.Value;
			if (flag)
			{
				object obj2 = reader["outlooklastupdateddate"];
				object obj3 = reader["clockworklastupdateddate"];
				object obj4 = reader["appointmentid"];
				object obj5 = reader["outlookglobalappointmentid"];
				object obj6 = reader["UniqueId2"];
				object obj7 = reader.ContainsColumn("MasterRecurrenceAppointmentId") ? reader["MasterRecurrenceAppointmentId"] : null;
				clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId = (string)obj;
				bool flag2 = obj4 != DBNull.Value;
				if (flag2)
				{
					clockWorkExternalAppMapping.ClockWorkAppointmentId = (int)obj4;
				}
				bool flag3 = obj2 != DBNull.Value;
				if (flag3)
				{
					DateTime dateTime = (DateTime)obj2;
					clockWorkExternalAppMapping.ExternalApplicationLastUpdatedDate = new DateTime?(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second));
				}
				else
				{
					clockWorkExternalAppMapping.ExternalApplicationLastUpdatedDate = null;
				}
				bool flag4 = obj3 != DBNull.Value;
				if (flag4)
				{
					DateTime dateTime2 = (DateTime)obj3;
					clockWorkExternalAppMapping.ClockWorkLastUpdatedDate = new DateTime?(new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, dateTime2.Hour, dateTime2.Minute, dateTime2.Second));
				}
				else
				{
					clockWorkExternalAppMapping.ClockWorkLastUpdatedDate = null;
				}
				bool flag5 = obj5 != DBNull.Value;
				if (flag5)
				{
					clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId = (string)obj5;
				}
				bool flag6 = obj6 != DBNull.Value;
				if (flag6)
				{
					clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 = (string)obj6;
				}
				bool flag7 = obj7 != DBNull.Value;
				if (flag7)
				{
					clockWorkExternalAppMapping.ExternalApplicationMasterRecurrenceAppointmentId = (string)obj7;
				}
			}
			return clockWorkExternalAppMapping;
		}
	}
}
