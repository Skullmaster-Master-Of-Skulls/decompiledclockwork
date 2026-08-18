using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000078 RID: 120
	internal class DataSyncCourseClientProxy : ClientBase<IDataSyncCourse>, IDataSyncCourse, IService
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000E358 File Offset: 0x0000C558
		public DataSyncCourseClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000E363 File Offset: 0x0000C563
		public DataSyncCourseClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000E370 File Offset: 0x0000C570
		public DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq Request)
		{
			return base.Channel.DataSyncCourses(Request);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000E390 File Offset: 0x0000C590
		public ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq Request)
		{
			return base.Channel.ParseExternalCourseRowParts(Request);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request)
		{
			return base.Channel.DataSyncLookupCoursesByTable(request);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		public DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request)
		{
			return base.Channel.DataSyncLookupCourses(request);
		}
	}
}
