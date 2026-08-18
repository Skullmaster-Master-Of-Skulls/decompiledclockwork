using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000010 RID: 16
	public class ServiceProviderOriginalApplicationCourseDAO : IServiceProviderOriginalApplicationCourseDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D74 File Offset: 0x00000F74
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D7C File Offset: 0x00000F7C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002D85 File Offset: 0x00000F85
		public ServiceProviderOriginalApplicationCourseDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DB6 File Offset: 0x00000FB6
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002DBE File Offset: 0x00000FBE
		public ServiceProvidersOperationContext OpContext { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public IList<LookupCourseBase> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid", DbType.Int32, ServiceProviderId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date),
				databaseLayer.GetParameter("@sptype", DbType.Int32, ServiceProviderType)
			};
			List<LookupCourseBase> list = new List<LookupCourseBase>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    spac.serviceproviderapplicationcourseid,spac.serviceprovidertype,spac.lucourseid,spac.registrationstatus,\r\n            luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n            luc.course,luc.[section],luc.timeofday,luc.campus,luc.coursenote,luc.location,luc.campus,luc.department\r\nFROM        serviceproviderapplications spa LEFT JOIN serviceproviderapplicationcourses spac ON spac.serviceproviderapplicationid=spa.serviceproviderapplicationid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=spac.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       spa.serviceproviderid=@spid AND spac.serviceprovidertype=@sptype\r\n            AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n            AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader("", dataReader);
					bool flag2 = courseBaseFromReader != null;
					if (flag2)
					{
						list.Add(courseBaseFromReader);
					}
				}
			}
			return list;
		}
	}
}
