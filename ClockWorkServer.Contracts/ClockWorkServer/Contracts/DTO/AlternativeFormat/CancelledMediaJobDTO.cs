using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA2 RID: 2978
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelledMediaJobDTO : MediaJobDTO, ICloneable<CancelledMediaJobDTO>, ICloneable
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		public CancelledMediaJobDTO()
		{
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		public CancelledMediaJobDTO(CancelledMediaJobDTO item) : base(item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CancelledOn = item.CancelledOn;
				this.CancelledBy = item.CancelledBy;
				this.CancellationReason = item.CancellationReason;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x0001F01B File Offset: 0x0001D21B
		// (set) Token: 0x06003F09 RID: 16137 RVA: 0x0001F023 File Offset: 0x0001D223
		[DataMember]
		public DateTime CancelledOn { get; set; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x0001F02C File Offset: 0x0001D22C
		// (set) Token: 0x06003F0B RID: 16139 RVA: 0x0001F03F File Offset: 0x0001D23F
		[DataMember]
		public override bool IsCancelled
		{
			get
			{
				return true;
			}
			set
			{
				base.IsCancelled = value;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x0001F04C File Offset: 0x0001D24C
		// (set) Token: 0x06003F0D RID: 16141 RVA: 0x0001F05F File Offset: 0x0001D25F
		[DataMember]
		public override bool IsCompleted
		{
			get
			{
				return false;
			}
			set
			{
				base.IsCompleted = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x0001F06A File Offset: 0x0001D26A
		// (set) Token: 0x06003F0F RID: 16143 RVA: 0x0001F072 File Offset: 0x0001D272
		[DataMember]
		public PersonBaseDTO CancelledBy { get; set; }

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x0001F07B File Offset: 0x0001D27B
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x0001F083 File Offset: 0x0001D283
		[DataMember]
		public string CancellationReason { get; set; }

		// Token: 0x06003F12 RID: 16146 RVA: 0x0001F08C File Offset: 0x0001D28C
		public new CancelledMediaJobDTO Clone()
		{
			return new CancelledMediaJobDTO(this);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
