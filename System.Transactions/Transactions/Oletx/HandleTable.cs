using System;
using System.Collections.Generic;

namespace System.Transactions.Oletx
{
	// Token: 0x02000088 RID: 136
	internal static class HandleTable
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00037544 File Offset: 0x00036944
		public static IntPtr AllocHandle(object target)
		{
			IntPtr result;
			lock (HandleTable.syncRoot)
			{
				int num = HandleTable.FindAvailableHandle();
				HandleTable.handleTable.Add(num, target);
				result = new IntPtr(num);
			}
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000375A4 File Offset: 0x000369A4
		public static bool FreeHandle(IntPtr handle)
		{
			bool result;
			lock (HandleTable.syncRoot)
			{
				result = HandleTable.handleTable.Remove(handle.ToInt32());
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00037604 File Offset: 0x00036A04
		public static object FindHandle(IntPtr handle)
		{
			object result;
			lock (HandleTable.syncRoot)
			{
				object obj2;
				if (!HandleTable.handleTable.TryGetValue(handle.ToInt32(), out obj2))
				{
					result = null;
				}
				else
				{
					result = obj2;
				}
			}
			return result;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00037664 File Offset: 0x00036A64
		private static int FindAvailableHandle()
		{
			int num;
			do
			{
				num = ((++HandleTable.currentHandle != 0) ? HandleTable.currentHandle : (++HandleTable.currentHandle));
			}
			while (HandleTable.handleTable.ContainsKey(num));
			return num;
		}

		// Token: 0x040001CC RID: 460
		private static Dictionary<int, object> handleTable = new Dictionary<int, object>(256);

		// Token: 0x040001CD RID: 461
		private static object syncRoot = new object();

		// Token: 0x040001CE RID: 462
		private static int currentHandle;
	}
}
