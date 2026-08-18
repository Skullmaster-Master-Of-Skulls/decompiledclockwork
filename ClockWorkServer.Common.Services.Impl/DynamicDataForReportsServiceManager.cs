using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003C RID: 60
	public class DynamicDataForReportsServiceManager : IDynamicDataForReports, IService
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000B6BC File Offset: 0x000098BC
		public CrossReferenceDataIntoSingleTableResp CrossReferenceDataIntoSingleTable(CrossReferenceDataIntoSingleTableReq Request)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			DataTable table = dynamicDataForReportsManager.CrossReferenceDataIntoSingleTable(Request.TableWithData, Request.ControlIds);
			return new CrossReferenceDataIntoSingleTableResp
			{
				Table = table
			};
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B6FC File Offset: 0x000098FC
		public CrossReferencePerStudentDataResp CrossReferencePerStudentData(CrossReferencePerStudentDataReq Request)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			DataTable table = dynamicDataForReportsManager.CrossReferencePerStudentData(Request.TableWithData, Request.ControlIds);
			return new CrossReferencePerStudentDataResp
			{
				Table = table
			};
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B73C File Offset: 0x0000993C
		public CrossReferencePerAppointmentDataResp CrossReferencePerAppointmentData(CrossReferencePerAppointmentDataReq Request)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			DataTable table = dynamicDataForReportsManager.CrossReferencePerAppointmentData(Request.TableWithData, Request.ControlIds);
			return new CrossReferencePerAppointmentDataResp
			{
				Table = table
			};
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B77C File Offset: 0x0000997C
		public CrossReferenceAccommodationDataTemplateOnlyResp CrossReferenceAccommodationDataTemplateOnly(CrossReferenceAccommodationDataTemplateOnlyReq Request)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			DataTable table = dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOnly(Request.TableWithData, Request.ControlIds);
			return new CrossReferenceAccommodationDataTemplateOnlyResp
			{
				Table = table
			};
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B7BC File Offset: 0x000099BC
		public CrossReferenceAccommodationDataTemplateOrCourseSpecificResp CrossReferenceAccommodationDataTemplateOrCourseSpecific(CrossReferenceAccommodationDataTemplateOrCourseSpecificReq Request)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			DataTable table = dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOrCourseSpecific(Request.TableWithData, Request.ControlIds);
			return new CrossReferenceAccommodationDataTemplateOrCourseSpecificResp
			{
				Table = table
			};
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B7FC File Offset: 0x000099FC
		public LoadStudentReportInfoResp LoadStudentReportInfo(LoadStudentReportInfoReq Request)
		{
			eDynamicStudentReportInfoType[] typesToLoad = Request.TypesToLoad;
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(Request.GetOperationContext());
			IList<StudentInfoItemBase>[] array = dynamicDataForReportsManager.LoadStudentReportInfo(Request.StudentPersonIds, Request.TypesToLoad, Request.ControlIds);
			LoadStudentReportInfoResp loadStudentReportInfoResp = new LoadStudentReportInfoResp();
			List<StudentInfoItemBaseDTO>[] items;
			if (array == null)
			{
				items = null;
			}
			else
			{
				items = (from g in array
				select (from h in g
				select h.ToDTO()).ToList<StudentInfoItemBaseDTO>()).ToArray<List<StudentInfoItemBaseDTO>>();
			}
			loadStudentReportInfoResp.Items = items;
			return loadStudentReportInfoResp;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B878 File Offset: 0x00009A78
		[DebuggerStepThrough]
		public Task<LoadStudentReportInfoResp> LoadStudentReportInfoAsync(LoadStudentReportInfoReq Request)
		{
			DynamicDataForReportsServiceManager.<LoadStudentReportInfoAsync>d__6 <LoadStudentReportInfoAsync>d__ = new DynamicDataForReportsServiceManager.<LoadStudentReportInfoAsync>d__6();
			<LoadStudentReportInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadStudentReportInfoResp>.Create();
			<LoadStudentReportInfoAsync>d__.<>4__this = this;
			<LoadStudentReportInfoAsync>d__.Request = Request;
			<LoadStudentReportInfoAsync>d__.<>1__state = -1;
			<LoadStudentReportInfoAsync>d__.<>t__builder.Start<DynamicDataForReportsServiceManager.<LoadStudentReportInfoAsync>d__6>(ref <LoadStudentReportInfoAsync>d__);
			return <LoadStudentReportInfoAsync>d__.<>t__builder.Task;
		}
	}
}
