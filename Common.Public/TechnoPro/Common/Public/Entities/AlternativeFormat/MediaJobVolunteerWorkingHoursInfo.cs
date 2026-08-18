using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000586 RID: 1414
	public class MediaJobVolunteerWorkingHoursInfo : BusinessBase<int>
	{
		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x00032518 File Offset: 0x00030718
		// (set) Token: 0x06002DA4 RID: 11684 RVA: 0x0000E258 File Offset: 0x0000C458
		public int JobVolunteerWorkingHoursId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x00032530 File Offset: 0x00030730
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x00032538 File Offset: 0x00030738
		public AlternateFormatVolunteer Volunteer { get; set; }

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x00032541 File Offset: 0x00030741
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x00032549 File Offset: 0x00030749
		public int MediaJobId { get; set; }

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00032552 File Offset: 0x00030752
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x0003255A File Offset: 0x0003075A
		public DateTime StartWorkingTime { get; set; }

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00032563 File Offset: 0x00030763
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x0003256B File Offset: 0x0003076B
		public DateTime EndWorkingTime { get; set; }

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x00032574 File Offset: 0x00030774
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0003257C File Offset: 0x0003077C
		public PersonBase WhoAddWorkingHours { get; set; }

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x00032585 File Offset: 0x00030785
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x0003258D File Offset: 0x0003078D
		public string VolunteerWorkingHoursNotes { get; set; }
	}
}
