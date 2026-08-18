using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000925 RID: 2341
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppCancelInfoDTO : ICloneable<AppCancelInfoDTO>, ICloneable
	{
		// Token: 0x06002F66 RID: 12134 RVA: 0x000036BD File Offset: 0x000018BD
		public AppCancelInfoDTO()
		{
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000168D0 File Offset: 0x00014AD0
		public AppCancelInfoDTO(AppCancelInfoDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CancelReasonText = item.CancelReasonText;
				this.CancelReason = ((item.CancelReason == null) ? null : item.CancelReason.Clone());
				this.CancelledBy = ((item.CancelledBy == null) ? null : item.CancelledBy.Clone());
				this.CancelledDate = item.CancelledDate;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x00016943 File Offset: 0x00014B43
		// (set) Token: 0x06002F69 RID: 12137 RVA: 0x0001694B File Offset: 0x00014B4B
		[DataMember]
		public string CancelReasonText { get; set; }

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x00016954 File Offset: 0x00014B54
		// (set) Token: 0x06002F6B RID: 12139 RVA: 0x0001695C File Offset: 0x00014B5C
		[DataMember]
		public AppCancelReasonDTO CancelReason { get; set; }

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06002F6C RID: 12140 RVA: 0x00016965 File Offset: 0x00014B65
		// (set) Token: 0x06002F6D RID: 12141 RVA: 0x0001696D File Offset: 0x00014B6D
		[DataMember]
		public PersonBaseDTO CancelledBy { get; set; }

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x00016976 File Offset: 0x00014B76
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x0001697E File Offset: 0x00014B7E
		[DataMember]
		public DateTime CancelledDate { get; set; }

		// Token: 0x06002F70 RID: 12144 RVA: 0x00016988 File Offset: 0x00014B88
		public AppCancelInfoDTO Clone()
		{
			return new AppCancelInfoDTO(this);
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000169A0 File Offset: 0x00014BA0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
