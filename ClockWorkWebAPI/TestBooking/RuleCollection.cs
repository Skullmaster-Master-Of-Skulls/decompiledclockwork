using System;
using System.Collections;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class RuleCollection : CollectionBase
	{
		// Token: 0x1700010E RID: 270
		public Rule this[int index]
		{
			get
			{
				return (Rule)base.List[index];
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00018FC8 File Offset: 0x000171C8
		public int Add(Rule rule)
		{
			return base.List.Add(rule);
		}
	}
}
