using System;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x02000439 RID: 1081
	public class CourseStudentSpecific
	{
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00024D43 File Offset: 0x00022F43
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x00024D4B File Offset: 0x00022F4B
		public string GradeLetter { get; set; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x00024D54 File Offset: 0x00022F54
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x00024D5C File Offset: 0x00022F5C
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00024D65 File Offset: 0x00022F65
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x00024D6D File Offset: 0x00022F6D
		public decimal Grade { get; set; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00024D76 File Offset: 0x00022F76
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x00024D7E File Offset: 0x00022F7E
		public decimal InProgressGrade { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x00024D87 File Offset: 0x00022F87
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x00024D8F File Offset: 0x00022F8F
		public double TuitionCost { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x00024D98 File Offset: 0x00022F98
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x00024DA0 File Offset: 0x00022FA0
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x00024DA9 File Offset: 0x00022FA9
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x00024DB1 File Offset: 0x00022FB1
		public string RegistrationNote { get; set; }
	}
}
