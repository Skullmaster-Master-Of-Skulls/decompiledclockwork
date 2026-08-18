using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Public.Entities.Room
{
	// Token: 0x0200020A RID: 522
	public class Seat : AppointmentRoom, ICloneable
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x000171D4 File Offset: 0x000153D4
		public Seat()
		{
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000171E0 File Offset: 0x000153E0
		public Seat(Seat seat)
		{
			this.Campus = seat.Campus;
			this.ParentSeatGroupId = seat.ParentSeatGroupId;
			this.AssetIds = seat.AssetIds.ToList<string>();
			this.SeatType = seat.SeatType;
			this.OrderNum = seat.OrderNum;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0001723B File Offset: 0x0001543B
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00017243 File Offset: 0x00015443
		public string Campus { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0001724C File Offset: 0x0001544C
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00017254 File Offset: 0x00015454
		public int ParentSeatGroupId { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0001725D File Offset: 0x0001545D
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x00017265 File Offset: 0x00015465
		public IList<string> AssetIds { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0001726E File Offset: 0x0001546E
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00017276 File Offset: 0x00015476
		public eTestExamSeatType SeatType { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0001727F File Offset: 0x0001547F
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x00017287 File Offset: 0x00015487
		public int OrderNum { get; set; }

		// Token: 0x06000FEC RID: 4076 RVA: 0x00017290 File Offset: 0x00015490
		public new Seat Clone()
		{
			return new Seat(this);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000172A8 File Offset: 0x000154A8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
