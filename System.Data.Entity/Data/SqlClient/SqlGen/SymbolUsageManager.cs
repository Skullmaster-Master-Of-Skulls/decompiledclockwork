using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200003D RID: 61
	internal class SymbolUsageManager
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x000177DA File Offset: 0x000159DA
		internal bool ContainsKey(Symbol key)
		{
			return this.optionalColumnUsage.ContainsKey(key);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000177E8 File Offset: 0x000159E8
		internal bool TryGetValue(Symbol key, out bool value)
		{
			BoolWrapper boolWrapper;
			if (this.optionalColumnUsage.TryGetValue(key, out boolWrapper))
			{
				value = boolWrapper.Value;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017814 File Offset: 0x00015A14
		internal void Add(Symbol sourceSymbol, Symbol symbolToAdd)
		{
			BoolWrapper value;
			if (sourceSymbol == null || !this.optionalColumnUsage.TryGetValue(sourceSymbol, out value))
			{
				value = new BoolWrapper();
			}
			this.optionalColumnUsage.Add(symbolToAdd, value);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00017847 File Offset: 0x00015A47
		internal void MarkAsUsed(Symbol key)
		{
			if (this.optionalColumnUsage.ContainsKey(key))
			{
				this.optionalColumnUsage[key].Value = true;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017869 File Offset: 0x00015A69
		internal bool IsUsed(Symbol key)
		{
			return this.optionalColumnUsage[key].Value;
		}

		// Token: 0x04000745 RID: 1861
		private readonly Dictionary<Symbol, BoolWrapper> optionalColumnUsage = new Dictionary<Symbol, BoolWrapper>();
	}
}
