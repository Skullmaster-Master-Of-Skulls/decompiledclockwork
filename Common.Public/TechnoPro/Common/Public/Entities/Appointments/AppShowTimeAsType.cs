using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	public class AppShowTimeAsType : BusinessBase<int>
	{
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00027CEC File Offset: 0x00025EEC
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentShowTimeAsId
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

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x00027D04 File Offset: 0x00025F04
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00027D0C File Offset: 0x00025F0C
		public int AppCode { get; set; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00027D15 File Offset: 0x00025F15
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x00027D1D File Offset: 0x00025F1D
		public string Title { get; set; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00027D26 File Offset: 0x00025F26
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x00027D2E File Offset: 0x00025F2E
		public int? ColourArgB { get; set; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00027D38 File Offset: 0x00025F38
		public bool IsTentative
		{
			get
			{
				return this.AppCode == -1;
			}
		}
	}
}
