using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule2
{
	// Token: 0x02000486 RID: 1158
	public class Availability2Marker : BusinessBase<int>
	{
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00026A20 File Offset: 0x00024C20
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int Availability2MarkerId
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

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00026A38 File Offset: 0x00024C38
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x00026A40 File Offset: 0x00024C40
		public string MarkerText { get; set; }

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x00026A49 File Offset: 0x00024C49
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x00026A51 File Offset: 0x00024C51
		public int? MarkerColourArgB { get; set; }

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00026A5A File Offset: 0x00024C5A
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x00026A62 File Offset: 0x00024C62
		public int OrderNum { get; set; }
	}
}
