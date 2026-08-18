using System;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.Legacy.ServiceProviders
{
	// Token: 0x020002F8 RID: 760
	public class LegacyServiceProviderRequestDetail : BusinessBase<int>
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0001C26C File Offset: 0x0001A46C
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderRequestDetailId
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

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0001C284 File Offset: 0x0001A484
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0001C28C File Offset: 0x0001A48C
		public int CounsellorPid { get; set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0001C295 File Offset: 0x0001A495
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0001C29D File Offset: 0x0001A49D
		public string Rationale { get; set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0001C2A6 File Offset: 0x0001A4A6
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0001C2AE File Offset: 0x0001A4AE
		public string SpecialRequest { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0001C2B7 File Offset: 0x0001A4B7
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0001C2BF File Offset: 0x0001A4BF
		public string Plan { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0001C2D0 File Offset: 0x0001A4D0
		public bool? FsBswd { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0001C2D9 File Offset: 0x0001A4D9
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0001C2E1 File Offset: 0x0001A4E1
		public int? FsOsapStatus { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0001C2EA File Offset: 0x0001A4EA
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0001C2F2 File Offset: 0x0001A4F2
		public bool? FsWsib { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0001C2FB File Offset: 0x0001A4FB
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0001C303 File Offset: 0x0001A503
		public BinaryFile FsWsibLetterOfApprovalFile { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0001C30C File Offset: 0x0001A50C
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0001C314 File Offset: 0x0001A514
		public string FsWsibCaseWorkerPhone { get; set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0001C31D File Offset: 0x0001A51D
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0001C325 File Offset: 0x0001A525
		public bool? FsFirstNations { get; set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0001C32E File Offset: 0x0001A52E
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0001C336 File Offset: 0x0001A536
		public BinaryFile FsFirstNationsLetterOfApprovalFile { get; set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0001C33F File Offset: 0x0001A53F
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0001C347 File Offset: 0x0001A547
		public string FsFirstNationsCaseWorkerPhone { get; set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0001C350 File Offset: 0x0001A550
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0001C358 File Offset: 0x0001A558
		public bool? FsInterpreterFund { get; set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0001C361 File Offset: 0x0001A561
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x0001C369 File Offset: 0x0001A569
		public int? FsInterpreterFundCode { get; set; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0001C372 File Offset: 0x0001A572
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0001C37A File Offset: 0x0001A57A
		public bool? FsOther { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0001C383 File Offset: 0x0001A583
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0001C38B File Offset: 0x0001A58B
		public string FsOtherDetail { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0001C394 File Offset: 0x0001A594
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0001C39C File Offset: 0x0001A59C
		public int? FsBswdStatus { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0001C3A5 File Offset: 0x0001A5A5
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0001C3AD File Offset: 0x0001A5AD
		public int? FsWsibStatus { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0001C3B6 File Offset: 0x0001A5B6
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0001C3BE File Offset: 0x0001A5BE
		public int? FsFirstNationsStatus { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0001C3C7 File Offset: 0x0001A5C7
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0001C3CF File Offset: 0x0001A5CF
		public int? FsInterpreterFundStatus { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public int? FsOtherStatus { get; set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0001C3E9 File Offset: 0x0001A5E9
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0001C3F1 File Offset: 0x0001A5F1
		public BinaryFile FsOtherFile { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0001C3FA File Offset: 0x0001A5FA
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x0001C402 File Offset: 0x0001A602
		public DateTime DateEntered2 { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0001C40B File Offset: 0x0001A60B
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x0001C413 File Offset: 0x0001A613
		public bool FsSsd { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0001C41C File Offset: 0x0001A61C
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0001C424 File Offset: 0x0001A624
		public int? FsSsdStatus { get; set; }
	}
}
