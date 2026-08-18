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
	// Token: 0x02000089 RID: 137
	internal class DynamicDataForReportsClientBaseProxy : ClientBase<IDynamicDataForReports>, IDynamicDataForReports, IService
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0000FE77 File Offset: 0x0000E077
		public DynamicDataForReportsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000FE82 File Offset: 0x0000E082
		public DynamicDataForReportsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000FE90 File Offset: 0x0000E090
		public CrossReferenceDataIntoSingleTableResp CrossReferenceDataIntoSingleTable(CrossReferenceDataIntoSingleTableReq Request)
		{
			return base.Channel.CrossReferenceDataIntoSingleTable(Request);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		public CrossReferencePerStudentDataResp CrossReferencePerStudentData(CrossReferencePerStudentDataReq Request)
		{
			return base.Channel.CrossReferencePerStudentData(Request);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public CrossReferencePerAppointmentDataResp CrossReferencePerAppointmentData(CrossReferencePerAppointmentDataReq Request)
		{
			return base.Channel.CrossReferencePerAppointmentData(Request);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public CrossReferenceAccommodationDataTemplateOnlyResp CrossReferenceAccommodationDataTemplateOnly(CrossReferenceAccommodationDataTemplateOnlyReq Request)
		{
			return base.Channel.CrossReferenceAccommodationDataTemplateOnly(Request);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000FF10 File Offset: 0x0000E110
		public CrossReferenceAccommodationDataTemplateOrCourseSpecificResp CrossReferenceAccommodationDataTemplateOrCourseSpecific(CrossReferenceAccommodationDataTemplateOrCourseSpecificReq Request)
		{
			return base.Channel.CrossReferenceAccommodationDataTemplateOrCourseSpecific(Request);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000FF30 File Offset: 0x0000E130
		public LoadStudentReportInfoResp LoadStudentReportInfo(LoadStudentReportInfoReq Request)
		{
			return base.Channel.LoadStudentReportInfo(Request);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000FF50 File Offset: 0x0000E150
		[DebuggerStepThrough]
		public Task<LoadStudentReportInfoResp> LoadStudentReportInfoAsync(LoadStudentReportInfoReq Request)
		{
			DynamicDataForReportsClientBaseProxy.<LoadStudentReportInfoAsync>d__8 <LoadStudentReportInfoAsync>d__ = new DynamicDataForReportsClientBaseProxy.<LoadStudentReportInfoAsync>d__8();
			<LoadStudentReportInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadStudentReportInfoResp>.Create();
			<LoadStudentReportInfoAsync>d__.<>4__this = this;
			<LoadStudentReportInfoAsync>d__.Request = Request;
			<LoadStudentReportInfoAsync>d__.<>1__state = -1;
			<LoadStudentReportInfoAsync>d__.<>t__builder.Start<DynamicDataForReportsClientBaseProxy.<LoadStudentReportInfoAsync>d__8>(ref <LoadStudentReportInfoAsync>d__);
			return <LoadStudentReportInfoAsync>d__.<>t__builder.Task;
		}
	}
}
