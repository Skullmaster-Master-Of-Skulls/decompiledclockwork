using System;
using System.Collections.Generic;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200013B RID: 315
	public static class MappedDiagnosticsContext
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000199CA File Offset: 0x00017BCA
		private static IDictionary<string, object> ThreadDictionary
		{
			get
			{
				return ThreadLocalStorageHelper.GetDataForSlot<Dictionary<string, object>>(MappedDiagnosticsContext.dataSlot);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000199D6 File Offset: 0x00017BD6
		public static void Set(string item, string value)
		{
			MappedDiagnosticsContext.ThreadDictionary[item] = value;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000199E4 File Offset: 0x00017BE4
		public static void Set(string item, object value)
		{
			MappedDiagnosticsContext.ThreadDictionary[item] = value;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000199F2 File Offset: 0x00017BF2
		public static string Get(string item)
		{
			return MappedDiagnosticsContext.Get(item, null);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000199FB File Offset: 0x00017BFB
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(MappedDiagnosticsContext.GetObject(item), formatProvider);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00019A0C File Offset: 0x00017C0C
		public static object GetObject(string item)
		{
			object result;
			if (!MappedDiagnosticsContext.ThreadDictionary.TryGetValue(item, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00019A2B File Offset: 0x00017C2B
		public static ICollection<string> GetNames()
		{
			return MappedDiagnosticsContext.ThreadDictionary.Keys;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00019A37 File Offset: 0x00017C37
		public static bool Contains(string item)
		{
			return MappedDiagnosticsContext.ThreadDictionary.ContainsKey(item);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00019A44 File Offset: 0x00017C44
		public static void Remove(string item)
		{
			MappedDiagnosticsContext.ThreadDictionary.Remove(item);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00019A52 File Offset: 0x00017C52
		public static void Clear()
		{
			MappedDiagnosticsContext.ThreadDictionary.Clear();
		}

		// Token: 0x040002B7 RID: 695
		private static readonly object dataSlot = ThreadLocalStorageHelper.AllocateDataSlot();
	}
}
