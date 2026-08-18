using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002EE RID: 750
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForestSeatCampusDTO : ForestSeatBaseDTO
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00007FA2 File Offset: 0x000061A2
		public ForestSeatCampusDTO()
		{
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00008086 File Offset: 0x00006286
		public ForestSeatCampusDTO(string campus)
		{
			this.Campus = campus;
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00008098 File Offset: 0x00006298
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x000080A0 File Offset: 0x000062A0
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x000080AC File Offset: 0x000062AC
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x000080CD File Offset: 0x000062CD
		public override string Title
		{
			get
			{
				return this.Campus ?? "";
			}
			set
			{
				this.Campus = value;
			}
		}
	}
}
