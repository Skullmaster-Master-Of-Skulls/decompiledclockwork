using System;
using System.Collections;

namespace ClockWorkAPI
{
	// Token: 0x0200007A RID: 122
	public class DynamicDataCollection : CollectionBase
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00022BD3 File Offset: 0x00021BD3
		public virtual void Add(DynamicData dynamicData)
		{
			base.List.Add(dynamicData);
		}

		// Token: 0x17000250 RID: 592
		public DynamicData this[int index]
		{
			get
			{
				return (DynamicData)base.List[index];
			}
		}

		// Token: 0x17000251 RID: 593
		public DynamicData this[int pid, int appid, int screenNum]
		{
			get
			{
				foreach (object obj in base.List)
				{
					DynamicData dynamicData = (DynamicData)obj;
					if (dynamicData.PersonId == pid && appid == dynamicData.AppointmentId && dynamicData.ScreenNum == screenNum)
					{
						return dynamicData;
					}
				}
				return null;
			}
		}
	}
}
