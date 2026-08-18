using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200013C RID: 316
	public static class MappedDiagnosticsLogicalContext
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00019A6C File Offset: 0x00017C6C
		private static IDictionary<string, object> LogicalThreadDictionary
		{
			get
			{
				ConcurrentDictionary<string, object> concurrentDictionary = CallContext.LogicalGetData("NLog.AsyncableMappedDiagnosticsContext") as ConcurrentDictionary<string, object>;
				if (concurrentDictionary == null)
				{
					concurrentDictionary = new ConcurrentDictionary<string, object>();
					CallContext.LogicalSetData("NLog.AsyncableMappedDiagnosticsContext", concurrentDictionary);
				}
				return concurrentDictionary;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00019A9E File Offset: 0x00017C9E
		public static string Get(string item)
		{
			return MappedDiagnosticsLogicalContext.Get(item, null);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00019AA7 File Offset: 0x00017CA7
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(MappedDiagnosticsLogicalContext.GetObject(item), formatProvider);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00019AB8 File Offset: 0x00017CB8
		public static object GetObject(string item)
		{
			object result;
			if (!MappedDiagnosticsLogicalContext.LogicalThreadDictionary.TryGetValue(item, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00019AD7 File Offset: 0x00017CD7
		public static void Set(string item, string value)
		{
			MappedDiagnosticsLogicalContext.LogicalThreadDictionary[item] = value;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00019AE5 File Offset: 0x00017CE5
		public static void Set(string item, object value)
		{
			MappedDiagnosticsLogicalContext.LogicalThreadDictionary[item] = value;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00019AF3 File Offset: 0x00017CF3
		public static ICollection<string> GetNames()
		{
			return MappedDiagnosticsLogicalContext.LogicalThreadDictionary.Keys;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00019AFF File Offset: 0x00017CFF
		public static bool Contains(string item)
		{
			return MappedDiagnosticsLogicalContext.LogicalThreadDictionary.ContainsKey(item);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00019B0C File Offset: 0x00017D0C
		public static void Remove(string item)
		{
			MappedDiagnosticsLogicalContext.LogicalThreadDictionary.Remove(item);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00019B1A File Offset: 0x00017D1A
		public static void Clear()
		{
			MappedDiagnosticsLogicalContext.Clear(false);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00019B22 File Offset: 0x00017D22
		public static void Clear(bool free)
		{
			if (free)
			{
				CallContext.FreeNamedDataSlot("NLog.AsyncableMappedDiagnosticsContext");
				return;
			}
			MappedDiagnosticsLogicalContext.LogicalThreadDictionary.Clear();
		}

		// Token: 0x040002B8 RID: 696
		private const string LogicalThreadDictionaryKey = "NLog.AsyncableMappedDiagnosticsContext";
	}
}
