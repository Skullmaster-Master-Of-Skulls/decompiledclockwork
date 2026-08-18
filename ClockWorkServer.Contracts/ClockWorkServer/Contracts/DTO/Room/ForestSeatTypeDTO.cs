using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002ED RID: 749
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForestSeatTypeDTO : ForestSeatBaseDTO
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x00007FA2 File Offset: 0x000061A2
		public ForestSeatTypeDTO()
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0000803C File Offset: 0x0000623C
		public ForestSeatTypeDTO(eTestExamSeatType seatType)
		{
			this.SeatType = seatType;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0000804E File Offset: 0x0000624E
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00008056 File Offset: 0x00006256
		[DataMember]
		public eTestExamSeatType SeatType { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00008060 File Offset: 0x00006260
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00007F9F File Offset: 0x0000619F
		public override string Title
		{
			get
			{
				return this.SeatType.ToString();
			}
			set
			{
			}
		}
	}
}
