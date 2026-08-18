using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	public class AppType : AppTypeBase
	{
		// Token: 0x060024EE RID: 9454 RVA: 0x00027DF9 File Offset: 0x00025FF9
		public AppType()
		{
			this.AppTypeId = 0;
			base.Description = "";
			this.Group = new AppTypeGroup();
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00027E23 File Offset: 0x00026023
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00027E2B File Offset: 0x0002602B
		public AppTypeGroup Group { get; set; }

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00027E34 File Offset: 0x00026034
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00027E3C File Offset: 0x0002603C
		public int DefaultColourArgb { get; set; }

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00027E45 File Offset: 0x00026045
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x00027E4D File Offset: 0x0002604D
		public bool IsTestOrExam { get; set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00027E56 File Offset: 0x00026056
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x00027E5E File Offset: 0x0002605E
		public bool IsWorkshop { get; set; }

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00027E67 File Offset: 0x00026067
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x00027E6F File Offset: 0x0002606F
		public bool? IsActive { get; set; }
	}
}
