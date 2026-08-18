using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x020001F8 RID: 504
	public class ServiceProvider : ServiceProviderBase
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00016804 File Offset: 0x00014A04
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0001681C File Offset: 0x00014A1C
		public new virtual int Id
		{
			get
			{
				return base.ServiceProviderId;
			}
			set
			{
				base.ServiceProviderId = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00016827 File Offset: 0x00014A27
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0001682F File Offset: 0x00014A2F
		public string AdditionalServices { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00016838 File Offset: 0x00014A38
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00016840 File Offset: 0x00014A40
		public string Specialization { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00016849 File Offset: 0x00014A49
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00016851 File Offset: 0x00014A51
		public string Notes1 { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0001685A File Offset: 0x00014A5A
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x00016862 File Offset: 0x00014A62
		public string Notes2 { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0001686B File Offset: 0x00014A6B
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00016873 File Offset: 0x00014A73
		public string Phone1 { get; set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0001687C File Offset: 0x00014A7C
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00016884 File Offset: 0x00014A84
		public string Phone2 { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0001688D File Offset: 0x00014A8D
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x00016895 File Offset: 0x00014A95
		public string PhoneNote { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0001689E File Offset: 0x00014A9E
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x000168A6 File Offset: 0x00014AA6
		public string Address { get; set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000168AF File Offset: 0x00014AAF
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x000168B7 File Offset: 0x00014AB7
		public DateTime DateEntered { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000168C0 File Offset: 0x00014AC0
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x000168C8 File Offset: 0x00014AC8
		public int WhoEnteredPersonId { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000168D1 File Offset: 0x00014AD1
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000168D9 File Offset: 0x00014AD9
		public bool IsActive { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000168E2 File Offset: 0x00014AE2
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x000168EA File Offset: 0x00014AEA
		public string IsActiveNote { get; set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000168F3 File Offset: 0x00014AF3
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x000168FB File Offset: 0x00014AFB
		public string Address2 { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00016904 File Offset: 0x00014B04
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0001690C File Offset: 0x00014B0C
		public string Email2 { get; set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00016915 File Offset: 0x00014B15
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x0001691D File Offset: 0x00014B1D
		public bool AddressActive { get; set; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00016926 File Offset: 0x00014B26
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0001692E File Offset: 0x00014B2E
		public bool Address2Active { get; set; }
	}
}
