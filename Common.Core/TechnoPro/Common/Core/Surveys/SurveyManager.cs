using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Surveys;
using TechnoPro.Common.DAO.Surveys;
using TechnoPro.Common.ICore.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Surveys
{
	// Token: 0x02000038 RID: 56
	public class SurveyManager : ISurveyManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000C26C File Offset: 0x0000A46C
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000C274 File Offset: 0x0000A474
		public ISurveyDAO dao { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x0000C27D File Offset: 0x0000A47D
		public SurveyManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new SurveyDAO(opContext);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C29C File Offset: 0x0000A49C
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000242 RID: 578 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public List<Survey> GetAllSurveys()
		{
			return this.dao.GetAllSurveys();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public List<Survey> GetActiveSurveys()
		{
			return this.dao.GetActiveSurveys();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public Survey GetSurvey(int SurveyId)
		{
			return this.dao.GetSurvey(SurveyId);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C30E File Offset: 0x0000A50E
		public void DeleteSurvey(int SurveyId)
		{
			this.dao.DeleteSurvey(SurveyId);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C31E File Offset: 0x0000A51E
		public void UpdateSurvey(Survey Survey)
		{
			this.dao.UpdateSurvey(Survey);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C330 File Offset: 0x0000A530
		public int CreateSurvey(Survey Survey)
		{
			return this.dao.CreateNewSurvey(Survey);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C34E File Offset: 0x0000A54E
		public void DisableSurvey(int SurveyId)
		{
			this.dao.DisableSurvey(SurveyId);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C35E File Offset: 0x0000A55E
		public void EnableSurvey(int SurveyId)
		{
			this.dao.EnableSurvey(SurveyId);
		}
	}
}
