using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000075 RID: 117
	public class DataMigrationClientProxyReusableClientProxy : WCFTokenBasedReusableClientProxy<IDataMigration>, IDataMigration, IService
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x0000E077 File Offset: 0x0000C277
		public DataMigrationClientProxyReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000E082 File Offset: 0x0000C282
		public DataMigrationClientProxyReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000E090 File Offset: 0x0000C290
		public CreateStudentsResp CreateStudents(CreateStudentsReq Request)
		{
			return this.WrapServiceMethod<CreateStudentsResp>(() => this.Proxy.CreateStudents(Request));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public MigrateStudentDataResp MigrateStudentData(MigrateStudentDataReq Request)
		{
			return this.WrapServiceMethod<MigrateStudentDataResp>(() => this.Proxy.MigrateStudentData(Request));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000E100 File Offset: 0x0000C300
		public MigrateStudentPerDateDataResp MigrateStudentPerDateData(MigrateStudentPerDateDataReq Request)
		{
			return this.WrapServiceMethod<MigrateStudentPerDateDataResp>(() => this.Proxy.MigrateStudentPerDateData(Request));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000E138 File Offset: 0x0000C338
		public MigrateAppointmentsResp MigrateAppointments(MigrateAppointmentsReq Request)
		{
			return this.WrapServiceMethod<MigrateAppointmentsResp>(() => this.Proxy.MigrateAppointments(Request));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000E170 File Offset: 0x0000C370
		public MigrateAccommodationsResp MigrateAccommodations(MigrateAccommodationsReq Request)
		{
			return this.WrapServiceMethod<MigrateAccommodationsResp>(() => this.Proxy.MigrateAccommodations(Request));
		}
	}
}
