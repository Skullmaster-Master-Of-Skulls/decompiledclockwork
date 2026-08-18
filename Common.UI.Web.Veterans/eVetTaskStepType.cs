using System;

namespace TechnoPro.Common.UI.Web.Veterans.Controls
{
	// Token: 0x02000004 RID: 4
	public enum eVetTaskStepType
	{
		// Token: 0x04000026 RID: 38
		[VetTaskStep("", "", "")]
		Unknown,
		// Token: 0x04000027 RID: 39
		[VetTaskStep("Select term", "", "Please select the term you would like to apply for benefits for:")]
		ChooseTermDates,
		// Token: 0x04000028 RID: 40
		[VetTaskStep("Register with us", "register.aspx", "Complete the registration application.")]
		Register,
		// Token: 0x04000029 RID: 41
		[VetTaskStep("Indicate your Chapter", "", "Please enter your chapter below:")]
		SelectChapter,
		// Token: 0x0400002A RID: 42
		[VetTaskStep("Complete Benefit Request Form", "ben.aspx", "Complete the application and upload relevant documents.")]
		CompleteBenefitRequestForm,
		// Token: 0x0400002B RID: 43
		[VetTaskStep("Consent to agreement form", "agreement.aspx", "Review your application and consent to the terms in order to begin the process.  Note that you will not be able to change any of your forms online after you submit your consent.")]
		ConsentToAgreementForm,
		// Token: 0x0400002C RID: 44
		[VetTaskStep("Benefit counselor review", "", "A benefit counselor will review your file.")]
		BenefitCounselorReview,
		// Token: 0x0400002D RID: 45
		[VetTaskStep("Administrator review", "", "An administrator will review your file.")]
		AdministratorReview
	}
}
