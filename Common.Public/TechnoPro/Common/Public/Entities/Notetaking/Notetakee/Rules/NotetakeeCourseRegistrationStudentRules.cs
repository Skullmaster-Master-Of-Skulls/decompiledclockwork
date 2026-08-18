using System;

namespace TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Rules
{
	// Token: 0x02000288 RID: 648
	public class NotetakeeCourseRegistrationStudentRules
	{
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000198F2 File Offset: 0x00017AF2
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x000198FA File Offset: 0x00017AFA
		public int NotetakerApprovedForAllCoursesCid { get; set; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00019903 File Offset: 0x00017B03
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x0001990B File Offset: 0x00017B0B
		public int EquivalentCoursesNum { get; set; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00019914 File Offset: 0x00017B14
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x0001991C File Offset: 0x00017B1C
		public bool AllowedToViewNotesEvenIfNoNotetakerIsAssigned { get; set; }

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00019925 File Offset: 0x00017B25
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x0001992D File Offset: 0x00017B2D
		public bool AllowedStudentToCancelAssignedNotetaker { get; set; }

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00019936 File Offset: 0x00017B36
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x0001993E File Offset: 0x00017B3E
		public bool AllowStudentsToChooseTheirOwnNotetakers { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00019947 File Offset: 0x00017B47
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0001994F File Offset: 0x00017B4F
		public bool RestrictAccessBaseOnLoa { get; set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00019958 File Offset: 0x00017B58
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x00019960 File Offset: 0x00017B60
		public bool RestrictAccessBasedOnSelfReg { get; set; }

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00019969 File Offset: 0x00017B69
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x00019971 File Offset: 0x00017B71
		public int AccommodationsExpiryDateCid { get; set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0001997A File Offset: 0x00017B7A
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00019982 File Offset: 0x00017B82
		public bool TreatEmptyExpiryDateAsExpired { get; set; }
	}
}
