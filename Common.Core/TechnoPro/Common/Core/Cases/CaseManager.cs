using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Cases;
using TechnoPro.Common.DAO.Impl.Cases;
using TechnoPro.Common.ICore.Cases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Cases
{
	// Token: 0x02000121 RID: 289
	public class CaseManager : ICaseManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00055850 File Offset: 0x00053A50
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00055858 File Offset: 0x00053A58
		public ICaseDAO dao { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00055861 File Offset: 0x00053A61
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00055869 File Offset: 0x00053A69
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C34 RID: 3124 RVA: 0x00055872 File Offset: 0x00053A72
		public CaseManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new CaseDAO(opContext);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00055891 File Offset: 0x00053A91
		public void MergeCasesForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeCasesForTwoStudents(PersonIdNew, PersonIdOld);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000558A4 File Offset: 0x00053AA4
		public int CreateCase(Case Case)
		{
			return this.dao.CreateCase(Case);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000558C2 File Offset: 0x00053AC2
		public void DeleteCase(int InfoPcId)
		{
			this.dao.DeleteCase(InfoPcId);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000558D2 File Offset: 0x00053AD2
		public void UpdateCaseClientsAndRespondents(int InfoPcId, IList<CaseClient> FullClientListForCase)
		{
			this.dao.UpdateCaseClientsAndRespondents(InfoPcId, FullClientListForCase);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000558E4 File Offset: 0x00053AE4
		public void UpdateCase(Case Case)
		{
			bool flag = Case == null || Case.InfoPcId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("CaseManager:UpdateCase:Caseid=" + ((Case == null) ? "NULL" : Case.InfoPcId.ToString()));
			}
			this.dao.UpdateBasicCaseInfo(Case.InfoPcId, Case.Title);
			this.UpdateCaseClientsAndRespondents(Case.InfoPcId, Case.Clients);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00055958 File Offset: 0x00053B58
		public IList<CaseForDisplay> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn)
		{
			return this.dao.LoadCasesForDisplayForStudent(PersonId, ScreenNum, controlIdsToAddToColumn);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00055978 File Offset: 0x00053B78
		public Case LoadCaseById(int InfoPcId, int ScreenNum)
		{
			return this.dao.LoadCaseById(InfoPcId, ScreenNum);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00055998 File Offset: 0x00053B98
		public IList<BaseBasicAppointment> LoadBasicAppointmentsByCase(int infoPcId)
		{
			return this.dao.LoadBasicAppointmentsByCase(infoPcId);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000559B8 File Offset: 0x00053BB8
		[DebuggerStepThrough]
		public Task<IList<BaseBasicAppointment>> LoadBasicAppointmentsByCaseAsync(int infoPcId)
		{
			CaseManager.<LoadBasicAppointmentsByCaseAsync>d__17 <LoadBasicAppointmentsByCaseAsync>d__ = new CaseManager.<LoadBasicAppointmentsByCaseAsync>d__17();
			<LoadBasicAppointmentsByCaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<BaseBasicAppointment>>.Create();
			<LoadBasicAppointmentsByCaseAsync>d__.<>4__this = this;
			<LoadBasicAppointmentsByCaseAsync>d__.infoPcId = infoPcId;
			<LoadBasicAppointmentsByCaseAsync>d__.<>1__state = -1;
			<LoadBasicAppointmentsByCaseAsync>d__.<>t__builder.Start<CaseManager.<LoadBasicAppointmentsByCaseAsync>d__17>(ref <LoadBasicAppointmentsByCaseAsync>d__);
			return <LoadBasicAppointmentsByCaseAsync>d__.<>t__builder.Task;
		}
	}
}
