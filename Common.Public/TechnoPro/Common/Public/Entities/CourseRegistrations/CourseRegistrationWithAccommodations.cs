using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Accommodations;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x02000437 RID: 1079
	public class CourseRegistrationWithAccommodations
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x00024CCF File Offset: 0x00022ECF
		public CourseRegistrationWithAccommodations()
		{
			this.CourseOrTemplateAccommodations = new List<AccommodationData>();
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00024CE5 File Offset: 0x00022EE5
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x00024CED File Offset: 0x00022EED
		public CourseRegistration CourseReg { get; set; }

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00024CF6 File Offset: 0x00022EF6
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x00024CFE File Offset: 0x00022EFE
		public IList<AccommodationData> CourseOrTemplateAccommodations { get; set; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00024D07 File Offset: 0x00022F07
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x00024D0F File Offset: 0x00022F0F
		public bool? IsUsingTemplateAccommodations { get; set; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00024D18 File Offset: 0x00022F18
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x00024D20 File Offset: 0x00022F20
		public int CoursesId { get; set; }
	}
}
