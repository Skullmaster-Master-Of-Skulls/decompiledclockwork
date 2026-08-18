using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x02000391 RID: 913
	internal sealed class AliasGenerator
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x000C569C File Offset: 0x000C389C
		internal AliasGenerator(string prefix) : this(prefix, 250)
		{
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000C56AC File Offset: 0x000C38AC
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

		// Token: 0x0600328D RID: 12941 RVA: 0x000C5790 File Offset: 0x000C3990
		internal string Next()
		{
			this._counter = Math.Max(1 + this._counter, 0);
			return this.GetName(this._counter);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000C57B4 File Offset: 0x000C39B4
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

		// Token: 0x0400165B RID: 5723
		private const int MaxPrefixCount = 500;

		// Token: 0x0400165C RID: 5724
		private const int CacheSize = 250;

		// Token: 0x0400165D RID: 5725
		private static readonly string[] _counterNames = new string[250];

		// Token: 0x0400165E RID: 5726
		private static Dictionary<string, string[]> _prefixCounter;

		// Token: 0x0400165F RID: 5727
		private int _counter;

		// Token: 0x04001660 RID: 5728
		private readonly string _prefix;

		// Token: 0x04001661 RID: 5729
		private string[] _cache;
	}
}
