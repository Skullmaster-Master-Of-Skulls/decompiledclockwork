using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000261 RID: 609
	public class StaffCommonInfo : BusinessBase<int>
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000189C8 File Offset: 0x00016BC8
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000189E0 File Offset: 0x00016BE0
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x000189E8 File Offset: 0x00016BE8
		public string Email { get; set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000189F1 File Offset: 0x00016BF1
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x000189F9 File Offset: 0x00016BF9
		public string Phone { get; set; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00018A02 File Offset: 0x00016C02
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00018A0A File Offset: 0x00016C0A
		public string Title { get; set; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00018A13 File Offset: 0x00016C13
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00018A1B File Offset: 0x00016C1B
		public int SignatureDataId { get; set; }
	}
}
