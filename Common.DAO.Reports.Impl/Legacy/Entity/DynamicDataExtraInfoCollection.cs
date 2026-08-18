using System;
using System.Collections;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200001A RID: 26
	public class DynamicDataExtraInfoCollection : CollectionBase
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x00027C54 File Offset: 0x00025E54
		public int Add(DynamicDataExtraInfo ei)
		{
			return base.List.Add(ei);
		}

		// Token: 0x17000067 RID: 103
		public DynamicDataExtraInfo this[int index]
		{
			get
			{
				return (DynamicDataExtraInfo)base.List[index];
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00027C98 File Offset: 0x00025E98
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
