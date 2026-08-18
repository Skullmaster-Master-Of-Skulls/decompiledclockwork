using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002EC RID: 748
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForestSeatDTO : ForestSeatBaseDTO
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x00007FA2 File Offset: 0x000061A2
		public ForestSeatDTO()
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00007FAC File Offset: 0x000061AC
		public ForestSeatDTO(SeatDTO seat)
		{
			this.Seat = seat;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00007FBE File Offset: 0x000061BE
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x00007FC6 File Offset: 0x000061C6
		[DataMember]
		public SeatDTO Seat { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00007FD0 File Offset: 0x000061D0
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00008008 File Offset: 0x00006208
		public override string Title
		{
			get
			{
				return (this.Seat == null) ? "" : (this.Seat.RoomTitle ?? "");
			}
			set
			{
				bool flag = this.Seat == null;
				if (flag)
				{
					this.Seat = new SeatDTO();
				}
				this.Seat.RoomTitle = value;
			}
		}
	}
}
