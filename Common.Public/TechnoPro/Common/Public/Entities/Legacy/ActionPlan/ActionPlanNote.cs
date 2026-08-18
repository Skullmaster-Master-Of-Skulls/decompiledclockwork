using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Legacy.ActionPlan
{
	// Token: 0x020002FE RID: 766
	public class ActionPlanNote : BusinessBase<int>
	{
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0001C560 File Offset: 0x0001A760
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0000E258 File Offset: 0x0000C458
		public int NoteId
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

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0001C578 File Offset: 0x0001A778
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0001C580 File Offset: 0x0001A780
		public int PersonId { get; set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0001C589 File Offset: 0x0001A789
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0001C591 File Offset: 0x0001A791
		public int WhoAddedPersonId { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0001C59A File Offset: 0x0001A79A
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x0001C5A2 File Offset: 0x0001A7A2
		public int WhoLastModifiedPersonId { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0001C5AB File Offset: 0x0001A7AB
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0001C5B3 File Offset: 0x0001A7B3
		public string NoteGroup { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public string NoteDescription { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0001C5CD File Offset: 0x0001A7CD
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x0001C5D5 File Offset: 0x0001A7D5
		public string StaffNotes { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0001C5DE File Offset: 0x0001A7DE
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0001C5E6 File Offset: 0x0001A7E6
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0001C5EF File Offset: 0x0001A7EF
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0001C5F7 File Offset: 0x0001A7F7
		public DateTime DateAdded { get; set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0001C600 File Offset: 0x0001A800
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x0001C608 File Offset: 0x0001A808
		public PersonBase WhoLastModified { get; set; }
	}
}
