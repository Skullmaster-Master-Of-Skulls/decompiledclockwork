using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000059 RID: 89
	public class DynamicDataExtraInfoCollection : CollectionBase
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0002104C File Offset: 0x0001F24C
		public int Add(DynamicDataExtraInfo ei)
		{
			return base.List.Add(ei);
		}

		// Token: 0x17000183 RID: 387
		public DynamicDataExtraInfo this[int index]
		{
			get
			{
				return (DynamicDataExtraInfo)base.List[index];
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00021090 File Offset: 0x0001F290
		public DynamicDataExtraInfo GetDateFormatExtraInfo()
		{
			foreach (object obj in base.List)
			{
				DynamicDataExtraInfo dynamicDataExtraInfo = (DynamicDataExtraInfo)obj;
				bool flag = dynamicDataExtraInfo.Code == 'f';
				if (flag)
				{
					return dynamicDataExtraInfo;
				}
			}
			return null;
		}
	}
}
