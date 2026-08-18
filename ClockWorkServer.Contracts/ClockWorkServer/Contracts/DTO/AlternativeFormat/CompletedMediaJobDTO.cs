using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA3 RID: 2979
	[DataContract(Namespace = "http://tpro.ca")]
	public class CompletedMediaJobDTO : MediaJobDTO, ICloneable<CompletedMediaJobDTO>, ICloneable
	{
		// Token: 0x06003F14 RID: 16148 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		public CompletedMediaJobDTO()
		{
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		public CompletedMediaJobDTO(CompletedMediaJobDTO item) : base(item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CompletedOn = item.CompletedOn;
				this.CompletedBy = item.CompletedBy;
				this.CompletedNotes = item.CompletedNotes;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x0001F103 File Offset: 0x0001D303
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x0001F10B File Offset: 0x0001D30B
		[DataMember]
		public DateTime CompletedOn { get; set; }

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x0001F114 File Offset: 0x0001D314
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x0001F03F File Offset: 0x0001D23F
		[DataMember]
		public override bool IsCancelled
		{
			get
			{
				return false;
			}
			set
			{
				base.IsCancelled = value;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x0001F128 File Offset: 0x0001D328
		// (set) Token: 0x06003F1B RID: 16155 RVA: 0x0001F05F File Offset: 0x0001D25F
		[DataMember]
		public override bool IsCompleted
		{
			get
			{
				return true;
			}
			set
			{
				base.IsCompleted = value;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x0001F13B File Offset: 0x0001D33B
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x0001F143 File Offset: 0x0001D343
		[DataMember]
		public PersonBaseDTO CompletedBy { get; set; }

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x0001F14C File Offset: 0x0001D34C
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x0001F154 File Offset: 0x0001D354
		[DataMember]
		public string CompletedNotes { get; set; }

		// Token: 0x06003F20 RID: 16160 RVA: 0x0001F160 File Offset: 0x0001D360
		public new CompletedMediaJobDTO Clone()
		{
			return new CompletedMediaJobDTO(this);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0001F178 File Offset: 0x0001D378
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
