using System;
using System.Collections;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class AccommodationCollection : CollectionBase
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00010080 File Offset: 0x0000E280
		public int Add(Accommodation accommodation)
		{
			return base.List.Add(accommodation);
		}

		// Token: 0x170000A7 RID: 167
		public Accommodation this[int index]
		{
			get
			{
				return (Accommodation)base.List[index];
			}
		}
	}
}
