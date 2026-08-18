using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule2
{
	// Token: 0x02000487 RID: 1159
	public class Availability2Item : BusinessBase<int>
	{
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x00026A6C File Offset: 0x00024C6C
		// (set) Token: 0x060022E7 RID: 8935 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int Availability2ItemId
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

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00026A84 File Offset: 0x00024C84
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x00026A8C File Offset: 0x00024C8C
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00026A95 File Offset: 0x00024C95
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x00026A9D File Offset: 0x00024C9D
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00026AA6 File Offset: 0x00024CA6
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x00026AAE File Offset: 0x00024CAE
		public bool IsActive { get; set; }

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00026AB7 File Offset: 0x00024CB7
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x00026ABF File Offset: 0x00024CBF
		public bool IsAvailable { get; set; }

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00026AC8 File Offset: 0x00024CC8
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x00026AD0 File Offset: 0x00024CD0
		public int PersonId { get; set; }

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x00026AD9 File Offset: 0x00024CD9
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x00026AE1 File Offset: 0x00024CE1
		public Availability2Note AvailabilityNote { get; set; }
	}
}
