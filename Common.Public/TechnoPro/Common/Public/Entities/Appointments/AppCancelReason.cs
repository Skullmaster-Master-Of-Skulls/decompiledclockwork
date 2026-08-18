using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BD RID: 1213
	public class AppCancelReason : BusinessBase<int>
	{
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x00027BB0 File Offset: 0x00025DB0
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CancelReasonId
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

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00027BC8 File Offset: 0x00025DC8
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x00027BD0 File Offset: 0x00025DD0
		public AppCancelReasonGroup CancelReasonGroup { get; set; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x00027BD9 File Offset: 0x00025DD9
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x00027BE1 File Offset: 0x00025DE1
		public string CancelReasonTitle { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00027BEA File Offset: 0x00025DEA
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x00027BF2 File Offset: 0x00025DF2
		public int? Colour { get; set; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00027BFB File Offset: 0x00025DFB
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x00027C03 File Offset: 0x00025E03
		public int OrderNum { get; set; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00027C0C File Offset: 0x00025E0C
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x00027C14 File Offset: 0x00025E14
		public bool IsActive { get; set; }
	}
}
