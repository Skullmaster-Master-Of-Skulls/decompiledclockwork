using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031F RID: 799
	public class InventoryReservationGroup : BusinessBase<int>
	{
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0001DA28 File Offset: 0x0001BC28
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ReservationGroupId
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

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0001DA40 File Offset: 0x0001BC40
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0001DA48 File Offset: 0x0001BC48
		public DateTime StartDate { get; set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0001DA51 File Offset: 0x0001BC51
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0001DA59 File Offset: 0x0001BC59
		public DateTime EndDate { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0001DA62 File Offset: 0x0001BC62
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0001DA6A File Offset: 0x0001BC6A
		public string ReservationNotes { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0001DA73 File Offset: 0x0001BC73
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x0001DA7B File Offset: 0x0001BC7B
		public PersonBase WhoMadeReservation { get; set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0001DA84 File Offset: 0x0001BC84
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public PersonBase WhoReservedStaffPerson { get; set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0001DA95 File Offset: 0x0001BC95
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0001DA9D File Offset: 0x0001BC9D
		public DateTime CreationDate { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0001DAA6 File Offset: 0x0001BCA6
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0001DAAE File Offset: 0x0001BCAE
		public IList<string> NotificationEmails { get; set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0001DABF File Offset: 0x0001BCBF
		public bool BeNotified { get; set; }
	}
}
