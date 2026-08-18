using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200058A RID: 1418
	public class ContactInfo : BusinessBase<int>
	{
		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000325FC File Offset: 0x000307FC
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ContactInfoId
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

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x00032614 File Offset: 0x00030814
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x0003261C File Offset: 0x0003081C
		public string Name { get; set; }

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x00032625 File Offset: 0x00030825
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x0003262D File Offset: 0x0003082D
		public string Phone { get; set; }

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x00032636 File Offset: 0x00030836
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x0003263E File Offset: 0x0003083E
		public string CellPhone { get; set; }

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x00032647 File Offset: 0x00030847
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x0003264F File Offset: 0x0003084F
		public string Address { get; set; }

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x00032658 File Offset: 0x00030858
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x00032660 File Offset: 0x00030860
		public string Fax { get; set; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x00032669 File Offset: 0x00030869
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x00032671 File Offset: 0x00030871
		public string Email { get; set; }

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x0003267A File Offset: 0x0003087A
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x00032682 File Offset: 0x00030882
		public string Website { get; set; }

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x0003268B File Offset: 0x0003088B
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x00032693 File Offset: 0x00030893
		public string Position { get; set; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x0003269C File Offset: 0x0003089C
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000326A4 File Offset: 0x000308A4
		public string Notes { get; set; }
	}
}
