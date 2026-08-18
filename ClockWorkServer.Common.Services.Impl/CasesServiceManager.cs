using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Core.Cases;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.Cases;
using TechnoPro.Common.ICore.Cases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002A RID: 42
	public class CasesServiceManager : ICases, IService
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00008FD0 File Offset: 0x000071D0
		public LoadCasesForDisplayForStudentResp LoadCasesForDisplayForStudent(LoadCasesForDisplayForStudentReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			IList<CaseForDisplay> list = caseManager.LoadCasesForDisplayForStudent(Request.PersonId, Request.ScreenNum, (Request.ControlIdsForDynamicFormSummaryItems == null) ? null : Request.ControlIdsForDynamicFormSummaryItems.ToArray<int>());
			LoadCasesForDisplayForStudentResp loadCasesForDisplayForStudentResp = new LoadCasesForDisplayForStudentResp();
			IList<CaseForDisplayDTO> casesForDisplay;
			if (list != null)
			{
				casesForDisplay = (from g in list
				select g.ToDTO()).ToList<CaseForDisplayDTO>();
			}
			else
			{
				casesForDisplay = null;
			}
			loadCasesForDisplayForStudentResp.CasesForDisplay = casesForDisplay;
			return loadCasesForDisplayForStudentResp;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009054 File Offset: 0x00007254
		public LoadCaseByIdResp LoadCaseById(LoadCaseByIdReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			Case @case = caseManager.LoadCaseById(Request.InfoPcId, Request.ScreenNum);
			return new LoadCaseByIdResp
			{
				Case = ((@case == null) ? null : @case.ToDTO())
			};
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000090A0 File Offset: 0x000072A0
		public CreateCaseResp CreateCase(CreateCaseReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			return new CreateCaseResp
			{
				NewCaseId = caseManager.CreateCase(Request.Case.ToDomainObject())
			};
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000090DC File Offset: 0x000072DC
		public void DeleteCase(DeleteCaseReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			caseManager.DeleteCase(Request.InfoPcId);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009104 File Offset: 0x00007304
		public void UpdateCase(UpdateCaseReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			caseManager.UpdateCase(Request.Case.ToDomainObject());
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009130 File Offset: 0x00007330
		public LoadBasicAppointmentsByCaseResp LoadBasicAppointmentsByCase(LoadBasicAppointmentsByCaseReq Request)
		{
			ICaseManager caseManager = new CaseManager(Request.GetOperationContext());
			IList<BaseBasicAppointment> list = caseManager.LoadBasicAppointmentsByCase(Request.CaseId);
			LoadBasicAppointmentsByCaseResp loadBasicAppointmentsByCaseResp = new LoadBasicAppointmentsByCaseResp();
			IList<BaseBasicAppointmentDTO> appointments;
			if (list == null)
			{
				appointments = null;
			}
			else
			{
				appointments = (from g in list
				select g.ToDTO()).ToList<BaseBasicAppointmentDTO>();
			}
			loadBasicAppointmentsByCaseResp.Appointments = appointments;
			return loadBasicAppointmentsByCaseResp;
		}
	}
}
