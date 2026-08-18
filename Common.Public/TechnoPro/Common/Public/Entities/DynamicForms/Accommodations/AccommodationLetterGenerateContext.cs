using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Accommodations
{
	// Token: 0x020003B8 RID: 952
	public class AccommodationLetterGenerateContext
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00020FF9 File Offset: 0x0001F1F9
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x00021001 File Offset: 0x0001F201
		public int StaffPersonId { get; set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0002100A File Offset: 0x0001F20A
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x00021012 File Offset: 0x0001F212
		public int StudentPersonId { get; set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0002101B File Offset: 0x0001F21B
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x00021023 File Offset: 0x0001F223
		public int InstructorId { get; set; }

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0002102C File Offset: 0x0001F22C
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x00021034 File Offset: 0x0001F234
		public int AlternateContactId { get; set; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0002103D File Offset: 0x0001F23D
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00021045 File Offset: 0x0001F245
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0002104E File Offset: 0x0001F24E
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x00021056 File Offset: 0x0001F256
		public int PreferredTemplateId { get; set; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0002105F File Offset: 0x0001F25F
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00021067 File Offset: 0x0001F267
		public eAccommodationLetterGenerationType LetterType { get; set; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00021070 File Offset: 0x0001F270
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00021078 File Offset: 0x0001F278
		public eAccommodationLetterGenerationForWhom WhoGeneratingFor { get; set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00021081 File Offset: 0x0001F281
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00021089 File Offset: 0x0001F289
		public eAccommodationLetterGenerationOutputType OutputType { get; set; }
	}
}
