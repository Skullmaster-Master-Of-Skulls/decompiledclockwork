using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004DB RID: 1243
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyServiceProviderRequestDetailDTO
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
		[DataMember]
		public int ServiceProviderRequestDetailId { get; set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
		[DataMember]
		public int CounsellorPid { get; set; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		[DataMember]
		public string Rationale { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x0000C105 File Offset: 0x0000A305
		[DataMember]
		public string SpecialRequest { get; set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0000C10E File Offset: 0x0000A30E
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x0000C116 File Offset: 0x0000A316
		[DataMember]
		public string Plan { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0000C11F File Offset: 0x0000A31F
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0000C127 File Offset: 0x0000A327
		[DataMember]
		public bool? FsBswd { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0000C130 File Offset: 0x0000A330
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x0000C138 File Offset: 0x0000A338
		[DataMember]
		public int? FsOsapStatus { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0000C141 File Offset: 0x0000A341
		// (set) Token: 0x06001A2B RID: 6699 RVA: 0x0000C149 File Offset: 0x0000A349
		[DataMember]
		public bool? FsWsib { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0000C152 File Offset: 0x0000A352
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x0000C15A File Offset: 0x0000A35A
		[DataMember]
		public BinaryFileDTO FsWsibLetterOfApprovalFile { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0000C163 File Offset: 0x0000A363
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x0000C16B File Offset: 0x0000A36B
		[DataMember]
		public string FsWsibCaseWorkerPhone { get; set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x0000C17C File Offset: 0x0000A37C
		[DataMember]
		public bool? FsFirstNations { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0000C185 File Offset: 0x0000A385
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x0000C18D File Offset: 0x0000A38D
		[DataMember]
		public BinaryFileDTO FsFirstNationsLetterOfApprovalFile { get; set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x0000C196 File Offset: 0x0000A396
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x0000C19E File Offset: 0x0000A39E
		[DataMember]
		public string FsFirstNationsCaseWorkerPhone { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		[DataMember]
		public bool? FsInterpreterFund { get; set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		[DataMember]
		public int? FsInterpreterFundCode { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		[DataMember]
		public bool? FsOther { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
		[DataMember]
		public string FsOtherDetail { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x0000C1F3 File Offset: 0x0000A3F3
		[DataMember]
		public int? FsBswdStatus { get; set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x0000C204 File Offset: 0x0000A404
		[DataMember]
		public int? FsWsibStatus { get; set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0000C20D File Offset: 0x0000A40D
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x0000C215 File Offset: 0x0000A415
		[DataMember]
		public int? FsFirstNationsStatus { get; set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0000C21E File Offset: 0x0000A41E
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0000C226 File Offset: 0x0000A426
		[DataMember]
		public int? FsInterpreterFundStatus { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0000C22F File Offset: 0x0000A42F
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0000C237 File Offset: 0x0000A437
		[DataMember]
		public int? FsOtherStatus { get; set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0000C240 File Offset: 0x0000A440
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x0000C248 File Offset: 0x0000A448
		[DataMember]
		public BinaryFileDTO FsOtherFile { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0000C251 File Offset: 0x0000A451
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x0000C259 File Offset: 0x0000A459
		[DataMember]
		public DateTime DateEntered2 { get; set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0000C262 File Offset: 0x0000A462
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0000C26A File Offset: 0x0000A46A
		[DataMember]
		public bool FsSsd { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0000C273 File Offset: 0x0000A473
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0000C27B File Offset: 0x0000A47B
		[DataMember]
		public int? FsSsdStatus { get; set; }
	}
}
