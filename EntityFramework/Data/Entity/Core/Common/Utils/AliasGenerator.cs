using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020002FE RID: 766
	internal sealed class AliasGenerator
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x000864F6 File Offset: 0x000846F6
		internal AliasGenerator(string prefix) : this(prefix, 250)
		{
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00086504 File Offset: 0x00084704
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.Collections.Generic.Dictionary`2<System.String,System.String[]>.#ctor(System.Int32,System.Collections.Generic.IEqualityComparer`1<System.String>)")]
		internal AliasGenerator(string prefix, int cacheSize)
		{
			this._prefix = (prefix ?? string.Empty);
			if (0 < cacheSize)
			{
				string[] array = null;
				Dictionary<string, string[]> prefixCounter;
				while ((prefixCounter = AliasGenerator._prefixCounter) == null || !prefixCounter.TryGetValue(prefix, out this._cache))
				{
					if (array == null)
					{
						array = new string[cacheSize];
					}
					int num = 1 + ((prefixCounter != null) ? prefixCounter.Count : 0);
					Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>(num, StringComparer.InvariantCultureIgnoreCase);
					if (prefixCounter != null && num < 500)
					{
						foreach (KeyValuePair<string, string[]> keyValuePair in prefixCounter)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					dictionary.Add(prefix, array);
					Interlocked.CompareExchange<Dictionary<string, string[]>>(ref AliasGenerator._prefixCounter, dictionary, prefixCounter);
				}
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000865E8 File Offset: 0x000847E8
		internal string Next()
		{
			this._counter = Math.Max(1 + this._counter, 0);
			return this.GetName(this._counter);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0008660C File Offset: 0x0008480C
		internal string GetName(int index)
		{
			string text;
			if (this._cache == null || this._cache.Length <= index)
			{
				text = this._prefix + index.ToString(CultureInfo.InvariantCulture);
			}
			else if ((text = this._cache[index]) == null)
			{
				if (AliasGenerator._counterNames.Length <= index)
				{
					text = index.ToString(CultureInfo.InvariantCulture);
				}
				else if ((text = AliasGenerator._counterNames[index]) == null)
				{
					text = (AliasGenerator._counterNames[index] = index.ToString(CultureInfo.InvariantCulture));
				}
				text = (this._cache[index] = this._prefix + text);
			}
			return text;
		}

		// Token: 0x0400096F RID: 2415
		private const int MaxPrefixCount = 500;

		// Token: 0x04000970 RID: 2416
		private const int CacheSize = 250;

		// Token: 0x04000971 RID: 2417
		private static readonly string[] _counterNames = new string[250];

		// Token: 0x04000972 RID: 2418
		private static Dictionary<string, string[]> _prefixCounter;

		// Token: 0x04000973 RID: 2419
		private int _counter;

		// Token: 0x04000974 RID: 2420
		private readonly string _prefix;

		// Token: 0x04000975 RID: 2421
		private readonly string[] _cache;
	}
}
