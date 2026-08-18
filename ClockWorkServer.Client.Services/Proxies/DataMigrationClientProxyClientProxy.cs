using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000076 RID: 118
	internal class DataMigrationClientProxyClientProxy : ClientBase<IDataMigration>, IDataMigration, IService
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public DataMigrationClientProxyClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000E1B3 File Offset: 0x0000C3B3
		public DataMigrationClientProxyClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public CreateStudentsResp CreateStudents(CreateStudentsReq Request)
		{
			return base.Channel.CreateStudents(Request);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public MigrateStudentDataResp MigrateStudentData(MigrateStudentDataReq Request)
		{
			return base.Channel.MigrateStudentData(Request);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000E200 File Offset: 0x0000C400
		public MigrateStudentPerDateDataResp MigrateStudentPerDateData(MigrateStudentPerDateDataReq Request)
		{
			return base.Channel.MigrateStudentPerDateData(Request);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000E220 File Offset: 0x0000C420
		public MigrateAppointmentsResp MigrateAppointments(MigrateAppointmentsReq Request)
		{
			return base.Channel.MigrateAppointments(Request);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000E240 File Offset: 0x0000C440
		public MigrateAccommodationsResp MigrateAccommodations(MigrateAccommodationsReq Request)
		{
			return base.Channel.MigrateAccommodations(Request);
		}
	}
}
