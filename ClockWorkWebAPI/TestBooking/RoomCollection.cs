using System;
using System.Collections;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class RoomCollection : CollectionBase
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00018834 File Offset: 0x00016A34
		public int Add(Room room)
		{
			int num = this.nextPriorityNumber;
			this.nextPriorityNumber = num + 1;
			room.PriorityNumber = num;
			return base.List.Add(room);
		}

		// Token: 0x17000101 RID: 257
		public Room this[int index]
		{
			get
			{
				return (Room)base.List[index];
			}
		}

		// Token: 0x040001A3 RID: 419
		private int nextPriorityNumber = 1;
	}
}
