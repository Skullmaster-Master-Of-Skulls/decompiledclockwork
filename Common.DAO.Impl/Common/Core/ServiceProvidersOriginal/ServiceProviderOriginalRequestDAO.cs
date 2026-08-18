using System;
using System.Data;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000014 RID: 20
	public class ServiceProviderOriginalRequestDAO : IServiceProviderOriginalRequestDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000039A0 File Offset: 0x00001BA0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000039A8 File Offset: 0x00001BA8
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000072 RID: 114 RVA: 0x000039B1 File Offset: 0x00001BB1
		public ServiceProviderOriginalRequestDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000039E4 File Offset: 0x00001BE4
		public ServiceRequest GetServiceRequestFromRecord(IDataReader record)
		{
			return this.GetServiceRequestFromRecord<ServiceRequest>(record, null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A00 File Offset: 0x00001C00
		public T GetServiceRequestFromRecord<T>(IDataReader record, IBatchDecryptor decryptor = null) where T : ServiceRequest
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			int num = (record["serviceproviderrequestid"] is DBNull) ? 0 : ((int)record["serviceproviderrequestid"]);
			bool flag = num < 1;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T t = Activator.CreateInstance<T>();
				t.ServiceProviderRequestId = num;
				t.CourseBase = LookupCourseDAO.GetCourseBaseFromReader("student_", record);
				t.AssignedServiceProviderCourse = LookupCourseDAO.GetCourseBaseFromReader("provider_", record);
				t.Student = PeopleDAO.GetPersonFromReader("student_", record, this.OpContext, decryptor);
				t.WhoEntered = PeopleDAO.GetPersonFromReader("whoentered_", record, this.OpContext, decryptor);
				t.StartDate = ((record["startdate"] is DBNull) ? null : new DateTime?((DateTime)record["startdate"]));
				t.EndDate = ((record["enddate"] is DBNull) ? null : new DateTime?((DateTime)record["enddate"]));
				t.DateEntered = ((record["dateentered"] is DBNull) ? null : new DateTime?((DateTime)record["dateentered"]));
				t.DateAssigned = ((record["dateassigned"] is DBNull) ? null : new DateTime?((DateTime)record["dateassigned"]));
				t.DateInserted = ((record["dateinserted"] is DBNull) ? null : new DateTime?((DateTime)record["dateinserted"]));
				t.StartDateTimeRequest = ((record["startdatetimerequest"] is DBNull) ? null : new DateTime?((DateTime)record["startdatetimerequest"]));
				t.EndDateTimeRequest = ((record["enddatetimerequest"] is DBNull) ? null : new DateTime?((DateTime)record["enddatetimerequest"]));
				t.AssignedServiceProviderId = ((record["ServiceProviderId"] is DBNull) ? 0 : ((int)record["ServiceProviderId"]));
				t.IsActive = (record["isactive"] != DBNull.Value && (bool)record["isactive"]);
				t.IsAssignedPrivate = (t.DateAssigned != null && t.AssignedServiceProviderId < 1);
				t.StudentRequested = (record["studentrequested"] != DBNull.Value && (bool)record["studentrequested"]);
				t.Notes = ((record["notes"] is DBNull) ? "" : ((decryptor == null) ? databaseLayer.Encryption.Decrypt((byte[])record["notes"]) : decryptor.Decrypt((byte[])record["notes"])));
				t.SpecialInstructions = ((record["SpecialInstructions"] is DBNull) ? "" : ((decryptor == null) ? databaseLayer.Encryption.Decrypt((byte[])record["SpecialInstructions"]) : decryptor.Decrypt((byte[])record["SpecialInstructions"])));
				t.StudentRequestedCancelNote = ((record["studentrequestedcancelnote"] is DBNull) ? "" : ((decryptor == null) ? databaseLayer.Encryption.Decrypt((byte[])record["studentrequestedcancelnote"]) : decryptor.Decrypt((byte[])record["studentrequestedcancelnote"])));
				t.DateTimeRequestTitle = ((record["datetimerequesttitle"] is DBNull) ? "" : record["datetimerequesttitle"].ToString().Trim());
				int serviceProviderTypeId = (record["serviceprovidertype"] is DBNull) ? 0 : ((int)record["serviceprovidertype"]);
				t.ProviderType = serviceProviderTypeId.GetServiceProviderType(this.OpContext);
				t.PartsDescription = ((record["partsdescription"] is DBNull) ? "" : record["partsdescription"].ToString());
				result = t;
			}
			return result;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003F29 File Offset: 0x00002129
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003F31 File Offset: 0x00002131
		public ServiceProvidersOperationContext OpContext { get; set; }
	}
}
