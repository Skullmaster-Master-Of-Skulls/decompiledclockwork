using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2
{
	// Token: 0x020008D7 RID: 2263
	[DataContract(Namespace = "http://tpro.ca")]
	public class Availability2ItemDTO : ICloneable<Availability2ItemDTO>, ICloneable
	{
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000036BD File Offset: 0x000018BD
		public Availability2ItemDTO()
		{
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000159E8 File Offset: 0x00013BE8
		public Availability2ItemDTO(Availability2ItemDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Availability2ItemId = item.Availability2ItemId;
				this.StartDateTime = item.StartDateTime;
				this.EndDateTime = item.EndDateTime;
				this.IsActive = item.IsActive;
				this.IsAvailable = item.IsAvailable;
				this.AvailabilityNote = item.AvailabilityNote;
				this.PersonId = item.PersonId;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x00015A62 File Offset: 0x00013C62
		// (set) Token: 0x06002DC4 RID: 11716 RVA: 0x00015A6A File Offset: 0x00013C6A
		[DataMember]
		public int Availability2ItemId { get; set; }

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x00015A73 File Offset: 0x00013C73
		// (set) Token: 0x06002DC6 RID: 11718 RVA: 0x00015A7B File Offset: 0x00013C7B
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x00015A84 File Offset: 0x00013C84
		// (set) Token: 0x06002DC8 RID: 11720 RVA: 0x00015A8C File Offset: 0x00013C8C
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x00015A95 File Offset: 0x00013C95
		// (set) Token: 0x06002DCA RID: 11722 RVA: 0x00015A9D File Offset: 0x00013C9D
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x00015AA6 File Offset: 0x00013CA6
		// (set) Token: 0x06002DCC RID: 11724 RVA: 0x00015AAE File Offset: 0x00013CAE
		[DataMember]
		public bool IsAvailable { get; set; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x00015AB7 File Offset: 0x00013CB7
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x00015ABF File Offset: 0x00013CBF
		[DataMember]
		public Availability2NoteDTO AvailabilityNote { get; set; }

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x00015AC8 File Offset: 0x00013CC8
		// (set) Token: 0x06002DD0 RID: 11728 RVA: 0x00015AD0 File Offset: 0x00013CD0
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00015ADC File Offset: 0x00013CDC
		public Availability2ItemDTO Clone()
		{
			return new Availability2ItemDTO(this);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00015AF4 File Offset: 0x00013CF4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
