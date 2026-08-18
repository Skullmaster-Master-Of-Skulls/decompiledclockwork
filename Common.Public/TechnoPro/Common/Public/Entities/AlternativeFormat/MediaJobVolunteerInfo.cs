using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000585 RID: 1413
	public class MediaJobVolunteerInfo : BusinessBase<int>
	{
		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x00032454 File Offset: 0x00030654
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x0000E258 File Offset: 0x0000C458
		public int JobVolunteerId
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

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0003246C File Offset: 0x0003066C
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x00032474 File Offset: 0x00030674
		public AlternateFormatVolunteer Volunteer { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x0003247D File Offset: 0x0003067D
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x00032485 File Offset: 0x00030685
		public int MediaJobId { get; set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0003248E File Offset: 0x0003068E
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x00032496 File Offset: 0x00030696
		public DateTime MediaJobStartTime { get; set; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x0003249F File Offset: 0x0003069F
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000324A7 File Offset: 0x000306A7
		public DateTime MediaJobDueDate { get; set; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000324B0 File Offset: 0x000306B0
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000324B8 File Offset: 0x000306B8
		public string MediaContentTitle { get; set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000324C1 File Offset: 0x000306C1
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x000324C9 File Offset: 0x000306C9
		public MediaContentFormat MediaContentFormatName { get; set; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000324D2 File Offset: 0x000306D2
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x000324DA File Offset: 0x000306DA
		public DateTime WhenWasAssigned { get; set; }

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000324E3 File Offset: 0x000306E3
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x000324EB File Offset: 0x000306EB
		public PersonBase WhoAssigned { get; set; }

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000324F4 File Offset: 0x000306F4
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x000324FC File Offset: 0x000306FC
		public string JobVolunteerNotes { get; set; }

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x00032505 File Offset: 0x00030705
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x0003250D File Offset: 0x0003070D
		public bool IsActive { get; set; }
	}
}
