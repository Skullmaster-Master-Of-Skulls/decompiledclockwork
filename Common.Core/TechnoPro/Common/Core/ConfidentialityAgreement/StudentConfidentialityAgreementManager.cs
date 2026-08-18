using System;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.ConfidentialityAgreement;
using TechnoPro.Common.DAO.Impl.ConfidentialityAgreement;
using TechnoPro.Common.ICore.ConfidentialityAgreement;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ConfidentialityAgreement;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.ConfidentialityAgreement
{
	// Token: 0x0200011A RID: 282
	public class StudentConfidentialityAgreementManager : IStudentConfidentilityAgreementManager, IBaseOperationContext<ConfidentialityAgreementOperationContext>
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00054078 File Offset: 0x00052278
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00054080 File Offset: 0x00052280
		public ConfidentialityAgreementOperationContext OpContext { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00054089 File Offset: 0x00052289
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00054091 File Offset: 0x00052291
		private IStudentConfidentialityAgreementDAO ConfidentialityAgreementDAO { get; set; }

		// Token: 0x06000BFC RID: 3068 RVA: 0x0005409A File Offset: 0x0005229A
		public StudentConfidentialityAgreementManager(ConfidentialityAgreementOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ConfidentialityAgreementDAO = new StudentConfidentialityAgreementDAO(this.OpContext);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000540BE File Offset: 0x000522BE
		public void RecordSignedConfidentialityAgreement(int personId)
		{
			this.ConfidentialityAgreementDAO.RecordSignedConfidentialityAgreement(personId);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000540D0 File Offset: 0x000522D0
		public StudentConfidentialityAgreement LastSignedStudentConfidentialityAgreement(int personId)
		{
			return this.ConfidentialityAgreementDAO.LastSignedStudentConfidentialityAgreement(personId);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000540F0 File Offset: 0x000522F0
		public bool IsConfidentialityAgreementSigningRequired(int pid)
		{
			return this.ConfidentialityAgreementDAO.IsConfidentialityAgreementSigningRequired(pid);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00054110 File Offset: 0x00052310
		public string GetStudentConfidentialityAgreementText(int pid)
		{
			ISettingManager settingManager = new SettingManager(this.OpContext);
			return settingManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentConfidentialityAgreementText);
		}
	}
}
