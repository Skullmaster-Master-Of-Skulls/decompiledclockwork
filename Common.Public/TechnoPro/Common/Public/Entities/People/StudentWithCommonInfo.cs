using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000266 RID: 614
	public class StudentWithCommonInfo
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00018B36 File Offset: 0x00016D36
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00018B3E File Offset: 0x00016D3E
		public PersonBase Student { get; set; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00018B47 File Offset: 0x00016D47
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00018B4F File Offset: 0x00016D4F
		public StudentCommonInfo CommonInfo { get; set; }
	}
}
