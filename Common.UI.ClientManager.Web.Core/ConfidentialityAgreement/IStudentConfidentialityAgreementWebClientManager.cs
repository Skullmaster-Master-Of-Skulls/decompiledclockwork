using System;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.ConfidentialityAgreement
{
	// Token: 0x02000014 RID: 20
	public interface IStudentConfidentialityAgreementWebClientManager
	{
		// Token: 0x06000045 RID: 69
		void RecordSignedConfidentialityAgreement(int personId);

		// Token: 0x06000046 RID: 70
		StudentConfidentialityAgreementDTO LastSignedStudentConfidentialityAgreement(int personId);

		// Token: 0x06000047 RID: 71
		bool IsConfidentialityAgreementSigningRequired(int pid);

		// Token: 0x06000048 RID: 72
		string GetStudentConfidentialityAgreementText(int pid);
	}
}
