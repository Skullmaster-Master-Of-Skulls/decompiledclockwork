using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000088 RID: 136
	public class DynamicDataForReportsReusableClientProxy : WCFTokenBasedReusableClientProxy<IDynamicDataForReports>, IDynamicDataForReports, IService
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000FCC2 File Offset: 0x0000DEC2
		public DynamicDataForReportsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000FCCD File Offset: 0x0000DECD
		public DynamicDataForReportsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		public CrossReferenceDataIntoSingleTableResp CrossReferenceDataIntoSingleTable(CrossReferenceDataIntoSingleTableReq Request)
		{
			return this.WrapServiceMethod<CrossReferenceDataIntoSingleTableResp>(() => this.Proxy.CrossReferenceDataIntoSingleTable(Request));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public CrossReferencePerStudentDataResp CrossReferencePerStudentData(CrossReferencePerStudentDataReq Request)
		{
			return this.WrapServiceMethod<CrossReferencePerStudentDataResp>(() => this.Proxy.CrossReferencePerStudentData(Request));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		public CrossReferencePerAppointmentDataResp CrossReferencePerAppointmentData(CrossReferencePerAppointmentDataReq Request)
		{
			return this.WrapServiceMethod<CrossReferencePerAppointmentDataResp>(() => this.Proxy.CrossReferencePerAppointmentData(Request));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public CrossReferenceAccommodationDataTemplateOnlyResp CrossReferenceAccommodationDataTemplateOnly(CrossReferenceAccommodationDataTemplateOnlyReq Request)
		{
			return this.WrapServiceMethod<CrossReferenceAccommodationDataTemplateOnlyResp>(() => this.Proxy.CrossReferenceAccommodationDataTemplateOnly(Request));
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public CrossReferenceAccommodationDataTemplateOrCourseSpecificResp CrossReferenceAccommodationDataTemplateOrCourseSpecific(CrossReferenceAccommodationDataTemplateOrCourseSpecificReq Request)
		{
			return this.WrapServiceMethod<CrossReferenceAccommodationDataTemplateOrCourseSpecificResp>(() => this.Proxy.CrossReferenceAccommodationDataTemplateOrCourseSpecific(Request));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public LoadStudentReportInfoResp LoadStudentReportInfo(LoadStudentReportInfoReq Request)
		{
			return this.WrapServiceMethod<LoadStudentReportInfoResp>(() => this.Proxy.LoadStudentReportInfo(Request));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000FE2C File Offset: 0x0000E02C
		[DebuggerStepThrough]
		public Task<LoadStudentReportInfoResp> LoadStudentReportInfoAsync(LoadStudentReportInfoReq Request)
		{
			DynamicDataForReportsReusableClientProxy.<LoadStudentReportInfoAsync>d__8 <LoadStudentReportInfoAsync>d__ = new DynamicDataForReportsReusableClientProxy.<LoadStudentReportInfoAsync>d__8();
			<LoadStudentReportInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadStudentReportInfoResp>.Create();
			<LoadStudentReportInfoAsync>d__.<>4__this = this;
			<LoadStudentReportInfoAsync>d__.Request = Request;
			<LoadStudentReportInfoAsync>d__.<>1__state = -1;
			<LoadStudentReportInfoAsync>d__.<>t__builder.Start<DynamicDataForReportsReusableClientProxy.<LoadStudentReportInfoAsync>d__8>(ref <LoadStudentReportInfoAsync>d__);
			return <LoadStudentReportInfoAsync>d__.<>t__builder.Task;
		}
	}
}
