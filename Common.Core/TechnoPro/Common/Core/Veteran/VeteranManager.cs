using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Academic;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Veteran;
using TechnoPro.Common.DAO.Veteran;
using TechnoPro.Common.ICore.Academic;
using TechnoPro.Common.ICore.Veteran;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.Core.Veteran
{
	// Token: 0x02000029 RID: 41
	public class VeteranManager : IVeteranManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000074B7 File Offset: 0x000056B7
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000074BF File Offset: 0x000056BF
		public OperationContext OpContext { get; set; }

		// Token: 0x06000163 RID: 355 RVA: 0x000074C8 File Offset: 0x000056C8
		public VeteranManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000074DC File Offset: 0x000056DC
		public IList<ChangeInBenefitRequest> LoadChangeInBenefitRequests(int PersonId, DateTime StartDate, DateTime EndDate)
		{
			SettingManager settingManager = new SettingManager(this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitScreenNum);
			int settingValue2 = settingManager.GetSettingValue<int>(Setting.VETERANS_ChangeInBenefitStatusCid);
			IVeteranDAO veteranDAO = new VeteranDAO(this.OpContext);
			return veteranDAO.LoadBenefitRequests(PersonId, StartDate, EndDate, settingValue, settingValue2);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000752C File Offset: 0x0000572C
		public BenefitApplication LoadBenefitApplicationByStudentAndSemester(int PersonId, int SemesterId)
		{
			ISemesterManager semesterManager = new SemesterManager(this.OpContext);
			Semester semester = semesterManager.LoadSemesterById(SemesterId);
			VeteranChapter veteranChapter = this.LoadChapterByStudent(PersonId);
			throw new NotImplementedException();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000072EA File Offset: 0x000054EA
		public VeteranChapter LoadChapterByStudent(int PersonId)
		{
			throw new NotImplementedException();
		}
	}
}
