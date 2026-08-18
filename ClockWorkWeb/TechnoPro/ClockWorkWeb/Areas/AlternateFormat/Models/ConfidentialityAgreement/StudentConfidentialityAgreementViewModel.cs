using System;
using System.ComponentModel.DataAnnotations;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Adapters;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.ConfidentialityAgreement
{
	// Token: 0x02000180 RID: 384
	public class StudentConfidentialityAgreementViewModel : AlternateFormatBaseViewModel
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x000496BF File Offset: 0x000478BF
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x000496C7 File Offset: 0x000478C7
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000496D0 File Offset: 0x000478D0
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x000496D8 File Offset: 0x000478D8
		public string ReturnUrl { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x000496E1 File Offset: 0x000478E1
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x000496E9 File Offset: 0x000478E9
		public string ConfidentialityAgreementText { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000496F2 File Offset: 0x000478F2
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x000496FA File Offset: 0x000478FA
		[BooleanRequired(ErrorMessage = "You must accept the terms and conditions.")]
		[Display(Name = "I accept the terms and conditions")]
		public bool ConfidentialityAgreementAccepted { get; set; }
	}
}
