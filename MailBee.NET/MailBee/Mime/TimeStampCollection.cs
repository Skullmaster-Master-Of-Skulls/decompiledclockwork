using System;
using System.Collections;

namespace MailBee.Mime
{
	// Token: 0x0200056C RID: 1388
	public class TimeStampCollection : CollectionBase
	{
		// Token: 0x06002E21 RID: 11809 RVA: 0x000DE4B3 File Offset: 0x000DD4B3
		internal TimeStampCollection()
		{
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000DE4BB File Offset: 0x000DD4BB
		internal void a(TimeStamp A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x170005A8 RID: 1448
		public TimeStamp this[int index]
		{
			get
			{
				return (TimeStamp)base.List[index];
			}
		}
	}
}
