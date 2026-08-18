using System;
using System.Collections.Generic;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000069 RID: 105
	public static class GlobalDiagnosticsContext
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00008E14 File Offset: 0x00007014
		public static void Set(string item, string value)
		{
			lock (GlobalDiagnosticsContext.dict)
			{
				GlobalDiagnosticsContext.dict[item] = value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008E5C File Offset: 0x0000705C
		public static void Set(string item, object value)
		{
			lock (GlobalDiagnosticsContext.dict)
			{
				GlobalDiagnosticsContext.dict[item] = value;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008EA4 File Offset: 0x000070A4
		public static string Get(string item)
		{
			return GlobalDiagnosticsContext.Get(item, null);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008EAD File Offset: 0x000070AD
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(GlobalDiagnosticsContext.GetObject(item), formatProvider);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008EBC File Offset: 0x000070BC
		public static object GetObject(string item)
		{
			object result;
			lock (GlobalDiagnosticsContext.dict)
			{
				object obj2;
				if (!GlobalDiagnosticsContext.dict.TryGetValue(item, out obj2))
				{
					obj2 = null;
				}
				result = obj2;
			}
			return result;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008F0C File Offset: 0x0000710C
		public static ICollection<string> GetNames()
		{
			ICollection<string> keys;
			lock (GlobalDiagnosticsContext.dict)
			{
				keys = GlobalDiagnosticsContext.dict.Keys;
			}
			return keys;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008F54 File Offset: 0x00007154
		public static bool Contains(string item)
		{
			bool result;
			lock (GlobalDiagnosticsContext.dict)
			{
				result = GlobalDiagnosticsContext.dict.ContainsKey(item);
			}
			return result;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008F9C File Offset: 0x0000719C
		public static void Remove(string item)
		{
			lock (GlobalDiagnosticsContext.dict)
			{
				GlobalDiagnosticsContext.dict.Remove(item);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008FE4 File Offset: 0x000071E4
		public static void Clear()
		{
			lock (GlobalDiagnosticsContext.dict)
			{
				GlobalDiagnosticsContext.dict.Clear();
			}
		}

		// Token: 0x040000D0 RID: 208
		private static Dictionary<string, object> dict = new Dictionary<string, object>();
	}
}
