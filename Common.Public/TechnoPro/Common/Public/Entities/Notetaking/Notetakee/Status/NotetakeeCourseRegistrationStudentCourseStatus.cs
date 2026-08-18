using System;

namespace TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Status
{
	// Token: 0x02000286 RID: 646
	public class NotetakeeCourseRegistrationStudentCourseStatus
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0001986A File Offset: 0x00017A6A
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00019872 File Offset: 0x00017A72
		public bool RequiresApprovedSelfRegistrationRequest { get; set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0001987B File Offset: 0x00017A7B
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00019883 File Offset: 0x00017A83
		public bool RequiresLoaGeneration { get; set; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0001988C File Offset: 0x00017A8C
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x00019894 File Offset: 0x00017A94
		public bool AllowedToViewExistingNotes { get; set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0001989D File Offset: 0x00017A9D
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x000198A5 File Offset: 0x00017AA5
		public bool AllowedToAutoCreateServiceProviderRequest { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000198AE File Offset: 0x00017AAE
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000198B6 File Offset: 0x00017AB6
		public bool AllowedToSelectNotetaker { get; set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000198BF File Offset: 0x00017ABF
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x000198C7 File Offset: 0x00017AC7
		public bool AllowedToCancelNotetaker { get; set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000198D0 File Offset: 0x00017AD0
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x000198D8 File Offset: 0x00017AD8
		public bool HasAtLeastOnePotentialNotetakerAvailable { get; set; }
	}
}
