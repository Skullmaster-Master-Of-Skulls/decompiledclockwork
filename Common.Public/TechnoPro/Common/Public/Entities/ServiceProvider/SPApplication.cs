using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001E3 RID: 483
	public class SPApplication : BusinessBase<int>
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00015FBC File Offset: 0x000141BC
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPApplicationId
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

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00015FD4 File Offset: 0x000141D4
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00015FDC File Offset: 0x000141DC
		public SPProvider Provider { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00015FE5 File Offset: 0x000141E5
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00015FED File Offset: 0x000141ED
		public SPProviderType ProviderType { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00015FF6 File Offset: 0x000141F6
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00015FFE File Offset: 0x000141FE
		public SPApplicationAvailabilityType ApplicationAvailabilityType { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00016007 File Offset: 0x00014207
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0001600F File Offset: 0x0001420F
		public string Note1 { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00016018 File Offset: 0x00014218
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00016020 File Offset: 0x00014220
		public string Note2 { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00016029 File Offset: 0x00014229
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00016031 File Offset: 0x00014231
		public DateTime DateEntered { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0001603A File Offset: 0x0001423A
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00016042 File Offset: 0x00014242
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0001604B File Offset: 0x0001424B
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00016053 File Offset: 0x00014253
		public bool IsActive { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0001605C File Offset: 0x0001425C
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x00016064 File Offset: 0x00014264
		public float RateOfPay { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0001606D File Offset: 0x0001426D
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00016075 File Offset: 0x00014275
		public SPRateOfPayType RateOfPayType { get; set; }
	}
}
