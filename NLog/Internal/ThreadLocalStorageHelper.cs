using System;
using System.Threading;

namespace NLog.Internal
{
	// Token: 0x020000B5 RID: 181
	internal static class ThreadLocalStorageHelper
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0000C472 File Offset: 0x0000A672
		public static object AllocateDataSlot()
		{
			return Thread.AllocateDataSlot();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000C47C File Offset: 0x0000A67C
		public static T GetDataForSlot<T>(object slot) where T : class, new()
		{
			LocalDataStoreSlot slot2 = (LocalDataStoreSlot)slot;
			object obj = Thread.GetData(slot2);
			if (obj == null)
			{
				obj = Activator.CreateInstance<T>();
				Thread.SetData(slot2, obj);
			}
			return (T)((object)obj);
		}
	}
}
