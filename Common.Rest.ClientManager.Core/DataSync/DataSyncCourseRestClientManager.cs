using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DataSync
{
	// Token: 0x0200005B RID: 91
	public class DataSyncCourseRestClientManager : BearerTokenRestProxy<IDataSyncCourseClientManager>, IDataSyncCourseClientManager, IWebService
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000AB1D File Offset: 0x00008D1D
		public DataSyncCourseRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000AB27 File Offset: 0x00008D27
		public DataSyncCourseRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000AB32 File Offset: 0x00008D32
		public ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq request)
		{
			return new ParseExternalCourseRowPartsResp
			{
				ExternalCourses = base.Post<ParseExternalCourseRowPartsReq, IList<DataSyncExternalCourseDTO>>(request, "datasynccourse/parseexternalcourserowparts")
			};
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000AB4B File Offset: 0x00008D4B
		public DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq request)
		{
			return new DataSyncCoursesResp
			{
				Results = base.Post<DataSyncCoursesReq, IList<DataSyncExternalCourseSyncResultDTO>>(request, "datasynccourse")
			};
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AB64 File Offset: 0x00008D64
		public DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request)
		{
			return new DataSyncLookupCoursesByTableResp
			{
				Results = base.Post<DataSyncLookupCoursesByTableReq, IList<DataSyncExternalCourseSyncResultDTO>>(request, "datasynccourse/lookupbytable")
			};
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AB7D File Offset: 0x00008D7D
		public DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request)
		{
			return new DataSyncLookupCoursesResp
			{
				Results = base.Post<DataSyncLookupCoursesReq, IList<DataSyncExternalCourseSyncResultDTO>>(request, "datasynccourse/lookup")
			};
		}
	}
}
