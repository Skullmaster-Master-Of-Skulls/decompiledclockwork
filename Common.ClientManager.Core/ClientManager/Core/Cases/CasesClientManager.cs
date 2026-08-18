using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Cases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Cases
{
	// Token: 0x02000079 RID: 121
	public class CasesClientManager : ICasesClientManager, IWebService
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x000146AC File Offset: 0x000128AC
		public IList<CaseForDisplayDTO> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn)
		{
			LoadCasesForDisplayForStudentReq loadCasesForDisplayForStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCasesForDisplayForStudentReq>();
			loadCasesForDisplayForStudentReq.PersonId = PersonId;
			loadCasesForDisplayForStudentReq.ScreenNum = ScreenNum;
			loadCasesForDisplayForStudentReq.ControlIdsForDynamicFormSummaryItems = controlIdsToAddToColumn;
			return ClientServiceFactory.GetClientInstance<ICases>().LoadCasesForDisplayForStudent(loadCasesForDisplayForStudentReq).CasesForDisplay;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000146F4 File Offset: 0x000128F4
		public CaseDTO LoadCaseById(int InfoPcId, int ScreenNum)
		{
			LoadCaseByIdReq loadCaseByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCaseByIdReq>();
			loadCaseByIdReq.InfoPcId = InfoPcId;
			loadCaseByIdReq.ScreenNum = ScreenNum;
			CaseDTO @case = ClientServiceFactory.GetClientInstance<ICases>().LoadCaseById(loadCaseByIdReq).Case;
			bool flag = @case == null;
			CaseDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				@case.Clients = (@case.Clients ?? new List<CaseClientDTO>()).ToList<CaseClientDTO>();
				result = @case;
			}
			return result;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001475C File Offset: 0x0001295C
		public int CreateCase(CaseDTO Case)
		{
			CreateCaseReq createCaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCaseReq>();
			createCaseReq.Case = Case;
			return ClientServiceFactory.GetClientInstance<ICases>().CreateCase(createCaseReq).NewCaseId;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00014794 File Offset: 0x00012994
		public void DeleteCase(int InfoPcId)
		{
			DeleteCaseReq deleteCaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteCaseReq>();
			deleteCaseReq.InfoPcId = InfoPcId;
			ClientServiceFactory.GetClientInstance<ICases>().DeleteCase(deleteCaseReq);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000147C4 File Offset: 0x000129C4
		public void UpdateCase(CaseDTO Case)
		{
			UpdateCaseReq updateCaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCaseReq>();
			updateCaseReq.Case = Case;
			ClientServiceFactory.GetClientInstance<ICases>().UpdateCase(updateCaseReq);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000147F4 File Offset: 0x000129F4
		public IList<BaseBasicAppointmentDTO> LoadBasicAppointmentsByCase(int caseId)
		{
			LoadBasicAppointmentsByCaseReq loadBasicAppointmentsByCaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadBasicAppointmentsByCaseReq>();
			loadBasicAppointmentsByCaseReq.CaseId = caseId;
			return ClientServiceFactory.GetClientInstance<ICases>().LoadBasicAppointmentsByCase(loadBasicAppointmentsByCaseReq).Appointments;
		}
	}
}
