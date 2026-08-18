using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000040 RID: 64
	internal class SymbolUsageManager
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x000145B8 File Offset: 0x000127B8
		internal bool ContainsKey(Symbol key)
		{
			return this.optionalColumnUsage.ContainsKey(key);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000145C8 File Offset: 0x000127C8
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

		// Token: 0x0600043C RID: 1084 RVA: 0x000145F4 File Offset: 0x000127F4
		internal void Add(Symbol sourceSymbol, Symbol symbolToAdd)
		{
			BoolWrapper value;
			if (sourceSymbol == null || !this.optionalColumnUsage.TryGetValue(sourceSymbol, out value))
			{
				value = new BoolWrapper();
			}
			this.optionalColumnUsage.Add(symbolToAdd, value);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014627 File Offset: 0x00012827
		internal void MarkAsUsed(Symbol key)
		{
			if (this.optionalColumnUsage.ContainsKey(key))
			{
				this.optionalColumnUsage[key].Value = true;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014649 File Offset: 0x00012849
		internal bool IsUsed(Symbol key)
		{
			return this.optionalColumnUsage[key].Value;
		}

		// Token: 0x040000F6 RID: 246
		private readonly Dictionary<Symbol, BoolWrapper> optionalColumnUsage = new Dictionary<Symbol, BoolWrapper>();
	}
}
