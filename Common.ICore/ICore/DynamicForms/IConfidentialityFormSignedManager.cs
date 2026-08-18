using System;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000096 RID: 150
	public interface IConfidentialityFormSignedManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000434 RID: 1076
		bool IsConfidentialityAgreementSigningRequired(int pid, Setting reSignPolicySetting, string controlName, string controlCaption);

		// Token: 0x06000435 RID: 1077
		void RecordConfidentialityAgreementSignedByTutor(int PersonId, string controlName, string controlCaption);

		// Token: 0x06000436 RID: 1078
		DynamicField GetLastSignedConfidentialityAgreementField(string controlName, string controlCaption);

		// Token: 0x06000437 RID: 1079
		Range<DateTime> GetConfidentialityResignDateRange(Setting reSignPolicySetting);
	}
}
