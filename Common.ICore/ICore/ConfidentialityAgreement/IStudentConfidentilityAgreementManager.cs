using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ConfidentialityAgreement;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.ICore.ConfidentialityAgreement
{
	// Token: 0x020000D4 RID: 212
	public interface IStudentConfidentilityAgreementManager : IBaseOperationContext<ConfidentialityAgreementOperationContext>
	{
		// Token: 0x0600069C RID: 1692
		void RecordSignedConfidentialityAgreement(int pid);

		// Token: 0x0600069D RID: 1693
		StudentConfidentialityAgreement LastSignedStudentConfidentialityAgreement(int pid);

		// Token: 0x0600069E RID: 1694
		bool IsConfidentialityAgreementSigningRequired(int pid);

		// Token: 0x0600069F RID: 1695
		string GetStudentConfidentialityAgreementText(int pid);
	}
}
