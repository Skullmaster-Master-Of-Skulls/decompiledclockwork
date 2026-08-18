using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000077 RID: 119
	public class DataSyncCourseReusableClientProxy : WCFTokenBasedReusableClientProxy<IDataSyncCourse>, IDataSyncCourse, IService
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0000E25E File Offset: 0x0000C45E
		public DataSyncCourseReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000E269 File Offset: 0x0000C469
		public DataSyncCourseReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000E278 File Offset: 0x0000C478
		public DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq Request)
		{
			return this.WrapServiceMethod<DataSyncCoursesResp>(() => this.Proxy.DataSyncCourses(Request));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq Request)
		{
			return this.WrapServiceMethod<ParseExternalCourseRowPartsResp>(() => this.Proxy.ParseExternalCourseRowParts(Request));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq Request)
		{
			return this.WrapServiceMethod<DataSyncLookupCoursesByTableResp>(() => this.Proxy.DataSyncLookupCoursesByTable(Request));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000E320 File Offset: 0x0000C520
		public DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq Request)
		{
			return this.WrapServiceMethod<DataSyncLookupCoursesResp>(() => this.Proxy.DataSyncLookupCourses(Request));
		}
	}
}
