using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsWorkshops
{
	// Token: 0x020004AB RID: 1195
	public class WorkshopDefinition : BusinessBase<int>
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00027418 File Offset: 0x00025618
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int WorkshopId
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

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00027430 File Offset: 0x00025630
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x00027438 File Offset: 0x00025638
		public AppType AppTypeParent { get; set; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00027441 File Offset: 0x00025641
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00027449 File Offset: 0x00025649
		public string WorkshopTitle { get; set; }

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00027452 File Offset: 0x00025652
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x0002745A File Offset: 0x0002565A
		public string WorkshopDescription { get; set; }

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x00027463 File Offset: 0x00025663
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x0002746B File Offset: 0x0002566B
		public int MaxAttendeeDefaultCount { get; set; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x00027474 File Offset: 0x00025674
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x0002747C File Offset: 0x0002567C
		public double Fee { get; set; }

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00027485 File Offset: 0x00025685
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x0002748D File Offset: 0x0002568D
		public List<PersonBase> Facilitators { get; set; }

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x00027496 File Offset: 0x00025696
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x0002749E File Offset: 0x0002569E
		public string WorkshopLocation { get; set; }

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000274A7 File Offset: 0x000256A7
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x000274AF File Offset: 0x000256AF
		public string WorkshopNotes { get; set; }
	}
}
