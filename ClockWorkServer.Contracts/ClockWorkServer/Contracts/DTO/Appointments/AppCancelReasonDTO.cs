using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000926 RID: 2342
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppCancelReasonDTO : ICloneable<AppCancelReasonDTO>, ICloneable
	{
		// Token: 0x06002F72 RID: 12146 RVA: 0x000036BD File Offset: 0x000018BD
		public AppCancelReasonDTO()
		{
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000169B8 File Offset: 0x00014BB8
		public AppCancelReasonDTO(AppCancelReasonDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CancelReasonId = item.CancelReasonId;
				this.CancelReasonGroup = ((item.CancelReasonGroup == null) ? null : item.CancelReasonGroup.Clone());
				this.CancelReasonTitle = item.CancelReasonTitle;
				this.Colour = item.Colour;
				this.OrderNum = item.OrderNum;
				this.IsActive = item.IsActive;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x00016A35 File Offset: 0x00014C35
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x00016A3D File Offset: 0x00014C3D
		[DataMember]
		public int CancelReasonId { get; set; }

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x00016A46 File Offset: 0x00014C46
		// (set) Token: 0x06002F77 RID: 12151 RVA: 0x00016A4E File Offset: 0x00014C4E
		[DataMember]
		public AppCancelReasonGroupDTO CancelReasonGroup { get; set; }

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x00016A57 File Offset: 0x00014C57
		// (set) Token: 0x06002F79 RID: 12153 RVA: 0x00016A5F File Offset: 0x00014C5F
		[DataMember]
		public string CancelReasonTitle { get; set; }

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x00016A68 File Offset: 0x00014C68
		// (set) Token: 0x06002F7B RID: 12155 RVA: 0x00016A70 File Offset: 0x00014C70
		[DataMember]
		public int? Colour { get; set; }

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x00016A79 File Offset: 0x00014C79
		// (set) Token: 0x06002F7D RID: 12157 RVA: 0x00016A81 File Offset: 0x00014C81
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x00016A8A File Offset: 0x00014C8A
		// (set) Token: 0x06002F7F RID: 12159 RVA: 0x00016A92 File Offset: 0x00014C92
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x06002F80 RID: 12160 RVA: 0x00016A9C File Offset: 0x00014C9C
		public AppCancelReasonDTO Clone()
		{
			return new AppCancelReasonDTO(this);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x00016AB4 File Offset: 0x00014CB4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
