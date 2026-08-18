using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x02000076 RID: 118
	public class DynamicDataExtraInfoCollection : CollectionBase
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00047F28 File Offset: 0x00046F28
		public int Add(DynamicDataExtraInfo ei)
		{
			return base.List.Add(ei);
		}

		// Token: 0x170001B0 RID: 432
		public DynamicDataExtraInfo this[int index]
		{
			get
			{
				return (DynamicDataExtraInfo)base.List[index];
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00047F6C File Offset: 0x00046F6C
		public DynamicDataExtraInfo GetDateFormatExtraInfo()
		{
			foreach (object obj in base.List)
			{
				DynamicDataExtraInfo dynamicDataExtraInfo = (DynamicDataExtraInfo)obj;
				if (dynamicDataExtraInfo.Code == 'f')
				{
					return dynamicDataExtraInfo;
				}
			}
			return null;
		}
	}
}
