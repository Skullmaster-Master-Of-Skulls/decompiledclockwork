using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002EF RID: 751
	[DataContract(Namespace = "http://tpro.ca")]
	public class ForestSeatGroupDTO : ForestSeatBaseDTO
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000080D8 File Offset: 0x000062D8
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x000080E0 File Offset: 0x000062E0
		[DataMember]
		public SeatGroupDTO SeatGroup { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x000080EC File Offset: 0x000062EC
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00008124 File Offset: 0x00006324
		public override string Title
		{
			get
			{
				return (this.SeatGroup == null) ? "" : (this.SeatGroup.Title ?? "");
			}
			set
			{
				bool flag = this.SeatGroup == null;
				if (flag)
				{
					this.SeatGroup = new SeatGroupDTO();
				}
				this.SeatGroup.Title = value;
			}
		}
	}
}
