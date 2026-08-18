using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ConfidentialityAgreement;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.DAO.ConfidentialityAgreement
{
	// Token: 0x02000097 RID: 151
	public interface IStudentConfidentialityAgreementDAO : IBaseOperationContext<ConfidentialityAgreementOperationContext>
	{
		// Token: 0x060003E8 RID: 1000
		void RecordSignedConfidentialityAgreement(int pid);

		// Token: 0x060003E9 RID: 1001
		StudentConfidentialityAgreement LastSignedStudentConfidentialityAgreement(int pid);

		// Token: 0x060003EA RID: 1002
		bool IsConfidentialityAgreementSigningRequired(int pid);
	}
}
